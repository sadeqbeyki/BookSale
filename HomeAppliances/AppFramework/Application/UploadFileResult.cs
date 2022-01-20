namespace AppFramework.Application
{
    public class UploadFileResult
    {
        public bool FileNotFound { get; set; }
        public string Message { get; set; }

        public UploadFileResult()
        {
            FileNotFound = false;
        }
        public UploadFileResult NotSelected(string message = "فایلی  را برگزینید")
        {
            FileNotFound = false;
            Message = message;
            return this;
        }
        public UploadFileResult FileSelected(string message)
        {
            FileNotFound = true;
            Message = message;
            return this;
        }
    }
}
