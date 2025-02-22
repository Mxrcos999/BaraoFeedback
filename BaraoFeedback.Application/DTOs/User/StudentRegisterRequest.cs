using System.ComponentModel.DataAnnotations;

namespace BaraoFeedback.Application.DTOs.User;

public class StudentRegisterRequest
{
    public string StudentCode { get; set; }
    [Required(ErrorMessage = "O nome deve ser informado")]
    [StringLength(50, ErrorMessage = "O nome deve conter até 50 caracteres.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "O nome não deve conter números.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O Email deve ser informado.")]
    [EmailAddress(ErrorMessage = "Informe um email válido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha deve ser informada.")]
    public string Password { get; set; }
    [Compare(nameof(Password), ErrorMessage = "As senhas devem ser iguais.")]
    public string ConfirmPassword { get; set; }
}

public class AdminRegisterRequest
{
    [Required(ErrorMessage = "O nome deve ser informado")]
    [StringLength(50, ErrorMessage = "O nome deve conter até 50 caracteres.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "O nome não deve conter números.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O Email deve ser informado.")]
    [EmailAddress(ErrorMessage = "Informe um email válido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha deve ser informada.")]
    public string Password { get; set; }
    [Compare(nameof(Password), ErrorMessage = "As senhas devem ser iguais.")]
    public string ConfirmPassword { get; set; }
}
