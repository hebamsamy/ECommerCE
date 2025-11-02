using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {

        private readonly MailService _mailService;
        public ExceptionHandler(MailService mailService) {
            _mailService = mailService;
        }

        public override void OnException(ExceptionContext context)
        {
            _mailService.SendMessage("Admin@gmail.com",$"On {DateTime.Now.ToString()} Exception Occurred",
                $"Message: {context.Exception.Message} \n StackTrace: {context.Exception.StackTrace}");

            context.ExceptionHandled = true;

            context.Result = new ViewResult()
            {
                ViewName = "Error"
            };
            
        }
    }
}
