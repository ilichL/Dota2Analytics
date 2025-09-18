using Dota2Analytics.Data;
using Dota2Analytics.Data.Entities;
using System;
using System.Diagnostics;

namespace Dota2Analytics.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate nextMiddleware;
        private readonly ILogger<LoggingMiddleware> logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> _logger)
        {
            nextMiddleware = next;
            logger = _logger;
        }

        public async Task InvokeAsync(HttpContext context, DotaContext dbContext)
        {//логирование в бд всех http запросов(вызывается перед каждым запросом)
            var requestLog = new RequestLog
            {
                Path = context.Request.Path,
                Method = new HttpMethod(context.Request.Method),
                QueryString = context.Request.QueryString.Value,
                ClientIP = context.Connection.RemoteIpAddress?.ToString(),
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                RequestTime = DateTime.UtcNow
            };

            var runTime = Stopwatch.StartNew();//запускаем таймер выполнения запроса

            try
            {
                await nextMiddleware(context);//отдали запрос next middleware
                requestLog.StatusCode = context.Response.StatusCode;
            }
            catch(Exception ex)
            {
                requestLog.StatusCode = 500;
                logger.LogError(ex," Error while sending request with path: ", context.Request.Path);
                throw;
            }
            finally
            {

                runTime.Stop();
                requestLog.ResponseTimeMs = runTime.ElapsedMilliseconds;//время выполнения 
                requestLog.ResponseTime = DateTime.UtcNow;//вермя завершения обработки

                try
                {
                    await dbContext.RequestLogs.AddAsync(requestLog);
                    await dbContext.SaveChangesAsync();//записали в бд
                }
                catch (Exception dbEx)
                {
                    logger.LogError(dbEx, "Failed to save request log to database with path:", context.Request.Path);
                }
            }
        }
    }
}
