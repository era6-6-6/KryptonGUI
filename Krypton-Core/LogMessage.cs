namespace Krypton_Core
{
    public class LogMessage
    {
        public string? Message { get; set; }

        public event EventHandler<LogMessage>? OnMessageReceive;
        public void AddMessage(string msg)
        {
            Message = msg;
            OnMessageReceive?.Invoke(this, this);
        }
    }
}