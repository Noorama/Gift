namespace HedyoCom_AspNet.Utilities
{
    public class EmailSender
    {
        public static void QueueEmail(Email email)
        {
            // todo
        }
    }

    public class Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}