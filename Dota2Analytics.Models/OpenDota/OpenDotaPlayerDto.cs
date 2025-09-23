using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Models.OpenDota
{
    public class OpenDotaPlayerDto
    {
        public string? Name { get; set; }
        public string? NickName { get; set; }
        public long OpenDotaId { get; set; }
    }
}
