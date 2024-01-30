namespace Server
{
    public class History
    {
        private DateTime Time { get; set; }
        private string Message { get; set; }

        public string Log { get { return $"[{Time}]{Message}"; } }
        public History(string message) { 
            Time = DateTime.Now;
            Message = message;
        }
    }
}
