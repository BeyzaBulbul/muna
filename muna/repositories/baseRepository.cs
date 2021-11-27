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
        public response getUsers()
        {


            response result = new response();
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
        public user postUser(user user)
        {
            user savedUser = new user();
            try
            {
                var client = new RestClient("https://gorest.co.in/public/v1/users"); // ıstek atacagım url ı tanımlıyoruz
                client.Timeout = -1;//ıstegın gerı bekleme suresı
                var request = new RestRequest(Method.POST); // ıstek turu
                request.AddHeader("Content-Type", "application/json");// ıcıerık tıpı json olacak
                request.AddHeader("Authorization", "Bearer 539ab1b71b2e0ec43ba9e2853bc8c53ee3e923abbd829e003a78787ff509af82"); // logın token
                string jsonString = JsonSerializer.Serialize<user>(user);
                var body = jsonString;
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                responsePostUser responsePostUser = JsonSerializer.Deserialize<responsePostUser>(response.Content);
                savedUser = responsePostUser.data;
            }
            catch (Exception ex)
            {

            }

            return savedUser;
        }
    }
}
