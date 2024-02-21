namespace Application.DTO.Response
{
    public class MessageResponse
    {
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
