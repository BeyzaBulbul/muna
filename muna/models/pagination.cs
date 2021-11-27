using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace muna.models
{
    public class pagination
    {
        /*"total": 2727,
            "pages": 137,
            "page": 1,
            "limit": 20,
            "links":*/
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public links links { get; set; }
    }
}
