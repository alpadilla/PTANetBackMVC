namespace Alicunde.System.Exam.Contracts;

public class ResponseDto<T>
{
    public T? Payload { get; set; }
    public List<string> Errors { get; set; }
    
    public ResponseDto(T? payload)
    {
        Payload = payload;
        Errors = new List<string>();
    }
}
