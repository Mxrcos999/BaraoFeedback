namespace BaraoFeedback.Application.DTOs.Shared;


public sealed class DefaultResponse
{
    public bool Sucess { get; set; }
    public object Data { get; set; }
    public Errors Errors { get; set; } = new Errors();

}
