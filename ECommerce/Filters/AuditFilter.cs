using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Filters
{
    public class AuditFilter :ActionFilterAttribute
    {
        public string FileName = 
            Directory.GetCurrentDirectory() + "/Logs/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

        //Before action executed
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("---------------");
            stringBuilder.AppendLine($"DateTime : {DateTime.Now.ToString()}");
            stringBuilder.AppendLine(Environment.NewLine);
            stringBuilder.AppendLine($"Path Requsted {context.HttpContext.Request.Path}");
            stringBuilder.AppendLine(Environment.NewLine);
            stringBuilder.AppendLine($"Cuurent User {context.HttpContext.User.FindFirstValue("FullName")}");
            File.AppendAllText(FileName, stringBuilder.ToString());
        }
        //After action executed
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
