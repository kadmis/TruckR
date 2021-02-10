using Email.API.Infrastructure.Models;
using Email.API.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Email.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel email)
        {
            try
            {
                await _emailService.SendEmail(email);
                return Ok(SendEmailResponse.Success());
            }
            catch (Exception ex)
            {
                return new JsonResult(SendEmailResponse.Fail(ex.Message));
            }
        }
    }
}
