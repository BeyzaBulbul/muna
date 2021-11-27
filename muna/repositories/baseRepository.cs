using muna.models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace muna.repositories
{
    public class baseRepository
    {
        public response getUsers() {
            //string jsonString = JsonSerializer.Serialize<WeatherForecast>(weatherForecast);

            response result =new response();
            //verileri doldurmak için gorest sitesine istek atıyoruz
            var client = new RestClient("https://gorest.co.in/public/v1/users");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            result =
               JsonSerializer.Deserialize<response>(response.Content);
            return result;
        }
    }
}
