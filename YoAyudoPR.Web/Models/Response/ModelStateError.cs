namespace YoAyudoPR.Web.Models.Response
{
    public class ModelStateError
    {
        public string Key { get; set; } = null!;
        public IEnumerable<string> Errors { get; set; } = null!;
    }
}
