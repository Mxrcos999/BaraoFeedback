namespace BaraoFeedback.Application.DTOs.Shared;


public sealed class DefaultResponse
{
    public bool Sucess => Errors.Message.Count == 0;
    public object Data { get; set; }
    public Errors Errors { get; set; } = new Errors();

}
