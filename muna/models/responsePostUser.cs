using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace muna.models
{
    public class responsePostUser
    {
        public user data { get; set; } //responseları ayırmamızın sebebi kullanıcıları hem kaydediyor hem sonrasında listeliyor oluşumuz
    }
}
