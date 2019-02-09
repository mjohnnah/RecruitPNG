namespace RecruitPNG.Web
{
    public class AppOptions
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpFromEmail {get; set;}
        public string SmtpFromName {get; set;}
        public string NotificationEmails { get; set; }
    }
}