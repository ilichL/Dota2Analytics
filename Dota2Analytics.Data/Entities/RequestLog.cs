using Dota2Analytics.Data.Abstractions;

namespace Dota2Analytics.Data.Entities
{
    public class RequestLog : BaseEntity
    {
        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public string? QueryString { get; set; }//example ?id=123&filter=active
        public string? ClientIP { get; set; }
        public string? UserAgent { get; set; }//ex: Mozilla/5.0
        public int StatusCode { get; set; }
        public long ResponseTimeMs { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime? ResponseTime { get; set; }
    }
}
