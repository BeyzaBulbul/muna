using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace muna.models
{
    public class response
    {
        public response()
        {
            //this.data = new data();
        }
        public List<user>  data { get; set; }
        public meta meta { get; set; }

    }
}
