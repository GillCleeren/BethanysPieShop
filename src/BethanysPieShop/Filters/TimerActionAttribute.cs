using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BethanysPieShop.Filters
{
    public class TimerAction : ActionFilterAttribute
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();
        //private readonly ILogger _logger;

        //public TimerAction(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger("TimerActionFilterLogger");
        //}

        public TimerAction()
        {
            //_logger = loggerFactory.CreateLogger("TimerActionFilterLogger");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _stopWatch.Stop();
            //var elapsed = Encoding.ASCII.GetBytes(_stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            //_logger.LogInformation("Action executed - elapsed time: " + _stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
            _stopWatch.Stop();
            var elapsed = Encoding.ASCII.GetBytes(_stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            //context.HttpContext.Response.Body.Write(elapsed, 0, elapsed.Length);
        }
    }
}
