using EmailSenderApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public IActionResult SendEmail([FromBody] EmailRequest emailRequest)
        {
            _emailService.SendEmail(emailRequest);
            return Ok("Email enviado com sucesso.");
        }
    }
}
