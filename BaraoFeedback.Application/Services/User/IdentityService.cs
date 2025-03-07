﻿using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.User;
using BaraoFeedback.Application.Services.User;
using BaraoFeedback.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options; 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;

    }

    public async Task<UserLoginResponse> LoginAsync(UserLoginRequest userLogin)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, isPersistent: false, lockoutOnFailure: true);
  
        if (signInResult.Succeeded)
        {
            var credenciais = await GenerateCredentials(userLogin.UserName);
            return credenciais;
        }

        UserLoginResponse userLoginResponse = new UserLoginResponse(signInResult.Succeeded);
        if (!signInResult.Succeeded)
        {
            if (signInResult.IsLockedOut)
            {
                userLoginResponse.Errors.AddError("Esta conta está bloqueada.");
            }
            else if (signInResult.IsNotAllowed)
            {
                userLoginResponse.Errors.AddError("Esta conta não tem permissão para entrar.");
            }
            else if (signInResult.RequiresTwoFactor)
            {
                userLoginResponse.Errors.AddError("Confirme seu email.");
            }
            else
            {
                userLoginResponse.Errors.AddError("Nome de usuário ou senha estão incorretos.");
            }
        }

        return userLoginResponse;
    }
    public async Task<UserRegisterResponse> RegisterAdminAsync(string type, AdminRegisterRequest request)
    {
        string email = request.Username + "@baraodemaua.br";

        var user = new ApplicationUser()
        {
            Email = email,
            Type = type,
            Name = request.Name,
            UserName = request.Username,
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        return await ValidateRegisterAsync(result);

    }

    public async Task<UserRegisterResponse> RegisterStudentAsync(string type, StudentRegisterRequest userRegister)
    {
        string email = userRegister.StudentCode + "@baraodemaua.edu.br";
        var user = new ApplicationUser()
        {
            Email = email,
            Type = type,
            Name = userRegister.Name,
            UserName = userRegister.StudentCode,
        };         

        IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

        return await ValidateRegisterAsync(result);
    }
    private async Task<UserRegisterResponse> ValidateRegisterAsync(IdentityResult result)
    {

        UserRegisterResponse userRegisterResponse = new UserRegisterResponse(result.Succeeded);

        if (!result.Succeeded)
        {
            foreach (var erroAtual in result.Errors)
            {
                switch (erroAtual.Code)
                {
                    case "PasswordRequiresNonAlphanumeric":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um caracter especial - ex( * | ! ).");
                        break;

                    case "PasswordRequiresDigit":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um número (0 - 9).");
                        break;

                    case "PasswordRequiresUpper":
                        userRegisterResponse.Errors.AddError("A senha precisa conter pelo menos um caracter em maiúsculo.");
                        break;

                    case "DuplicateUserName":
                        userRegisterResponse.Errors.AddError("O email informado já foi cadastrado!");
                        break;

                    default:
                        userRegisterResponse.Errors.AddError("Erro ao criar usuário.");
                        break;
                }

            }
        }

        return userRegisterResponse;
    }
    public async Task<IEnumerable<ApplicationUser>> GetUser()
    {
        var result = _userManager.Users.AsEnumerable();

        return result;

    }
    public async Task<DefaultResponse> DeleteUser(string email)
    {
        var response = new DefaultResponse();
        var user = await _userManager.FindByEmailAsync(email);
         
        await _userManager.DeleteAsync(user);

        return response;
    }

    protected async Task<UserLoginResponse> GenerateCredentials(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        var accessTokenClaims = await GetClaims(user, adicionarClaimsUsuario: true);
        var refreshTokenClaims = await GetClaims(user, adicionarClaimsUsuario: false);

        var dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
        var dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(10800);

        var accessToken = GenerateToken(accessTokenClaims, dataExpiracaoAccessToken);
        var refreshToken = GenerateToken(refreshTokenClaims, dataExpiracaoRefreshToken);
        var expirationAcessToken = _jwtOptions.AccessTokenExpiration.ToString();
        var expirationTimeRefreshToken = _jwtOptions.RefreshTokenExpiration.ToString();

        return new UserLoginResponse
        (
            true,
            user.Type,
            accessToken, 
            refreshToken,
            expirationTimeRefreshToken,
            expirationAcessToken,
            user.Name,
            user.Id,
            user.Email
        );
    }

    protected string GenerateToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
    {
        JwtSecurityToken token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, DateTime.Now, expires: dataExpiracao, signingCredentials: _jwtOptions.SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private async Task<IList<Claim>> GetClaims(ApplicationUser user, bool adicionarClaimsUsuario)
    {
        var claims = await _userManager.GetClaimsAsync(user);

        var roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        var now = DateTimeOffset.UtcNow;
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, now.ToUnixTimeSeconds().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }


}