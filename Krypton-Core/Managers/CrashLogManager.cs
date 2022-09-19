using Krypton_Core;

namespace Managers
{
    public class CrashLogManager
    {
        //public static CrashLogManager Instance;
        //public static Api? api { get; set; }
        private static readonly string PATH = $@"{Environment.CurrentDirectory}/Log.txt";

        public CrashLogManager()
        {
            //if (Instance != null) return;
            if (!File.Exists(PATH))
            {
                var file = File.Create(PATH);
                file.Close();
            }
            //api = apiGot;
            //Instance = this;
        }

        public enum Type
        {
            WARNING,
            FATAL,
            ERROR,
            MESSAGE
        }

        public static void Send(LogMessage logMsg, string message, Type type = Type.MESSAGE)
        {
            string typing = string.Empty;
            switch (type)
            {
                case Type.WARNING:
                    typing = "Warning";
                    break;
                case Type.FATAL:
                    typing = "Fatal";
                    break;
                case Type.ERROR:
                    typing = "Error";
                    break;
                case Type.MESSAGE:
                    typing = "Message";
                    break;
            }
            //TODO : Fix when writing in same time => used by another process, so crashing
            //using StreamWriter wr = File.AppendText(PATH);
            //{
            //    wr.WriteLineAsync($"[{DateTime.Now}] |{typing.ToUpper()}| {message}");
            //}
            //TODO : Api._user.LogMessages.Add();

            if (logMsg != null) logMsg?.AddMessage($"[{DateTime.Now}] |{typing.ToUpper()}| {message}");
            else
            {
                //Console.WriteLine($"Can't log : {message}");
            }

        }
    }
}
