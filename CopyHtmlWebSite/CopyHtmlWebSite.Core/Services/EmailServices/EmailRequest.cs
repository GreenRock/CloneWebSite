using System.Net.Mail;

namespace ShopMeDang.Infrastructure.Services.EmailServices
{
    public class EmailRequest
    {
        public MailAddress Sender { get; set; }

        public MailAddressCollection To { get; set; }

        public MailAddressCollection Bcc { get; set; }

        public string Subject { get; set; }

        public string HtmlContent { get; set; }

        public string TextContent { get; set; }

        public AttachmentCollection Attachments { get; set; }
    }
}