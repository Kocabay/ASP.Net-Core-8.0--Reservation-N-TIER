using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
 
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            if (string.IsNullOrEmpty(mailRequest.ReceiverMail))
            {
                // ReceiverMail boş veya null ise hata durumunu ele al
                // Bu örnekte basit bir geri dönüş yapıyorum, sizin gereksinimlerinize göre değişebilir.
                return BadRequest("Alıcı e-posta adresi boş olamaz.");
            }

            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "asdasd@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAdressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAdressTo);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body=bodyBuilder.ToMessageBody();
            mimeMessage.Subject = mailRequest.Subject;
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("asdasd@gmail.com", "aaaa");
            client.Send(mimeMessage);
            client.Disconnect(true);
            return View();
        }
    }
}
