using muna.models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Build.Framework;
using Dapper;

namespace muna.repositories
{
    public class baseRepository
    {
        public response getUsers()
        {
            response result = new response();
            //verileri doldurmak için gorest sitesine istek atıyoruz
            var client = new RestClient("https://gorest.co.in/public/v1/users");//client ile istek yapılacak url tanımlanır
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);//istek türü
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            result =JsonSerializer.Deserialize<response>(response.Content); //json formatındaki veri deserialize yapılır
            return result;
        }
        public user postUser(user user)
        {
            user savedUser = new user();
            try
            {   
                var client = new RestClient("https://gorest.co.in/public/v1/users"); 
                client.Timeout = -1;//ıstegın gerı bekleme suresı
                var request = new RestRequest(Method.POST); 
                request.AddHeader("Content-Type", "application/json");// ıcerık tıpı json olacak
                request.AddHeader("Authorization", "Bearer 539ab1b71b2e0ec43ba9e2853bc8c53ee3e923abbd829e003a78787ff509af82"); // logın token
                string jsonString = JsonSerializer.Serialize<user>(user); //verileri json formatında kaydederken serialize yapıyorum
                var body = jsonString;//serialize ettiğim data siteye gönderdiğim verinin bodysi oluyor
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);//oluşturduğumuz istekleri karşı tarafa gönderiyoruz
                Console.WriteLine(response.Content);//bize dönen cevap
                responsePostUser responsePostUser = JsonSerializer.Deserialize<responsePostUser>(response.Content); 
                savedUser = responsePostUser.data;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return savedUser;
        }
        
        public user putUser(user user) {

            user updateUser = new user();
            try
            {
                var client = new RestClient("https://gorest.co.in/public/v1/users/"+user.id+""); 
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT); 
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer 539ab1b71b2e0ec43ba9e2853bc8c53ee3e923abbd829e003a78787ff509af82"); 
                string jsonString = JsonSerializer.Serialize<user>(user); 
                var body = jsonString;
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                responsePostUser responsePutUser = JsonSerializer.Deserialize<responsePostUser>(response.Content);
                updateUser = responsePutUser.data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return updateUser;
        }
        public user deleteUser (user user)
        {
            user delUser = new user();
            try
            {
                var client = new RestClient("https://gorest.co.in/public/v1/users/" +user.id+"");
                client.Timeout = -1;
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer 539ab1b71b2e0ec43ba9e2853bc8c53ee3e923abbd829e003a78787ff509af82");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return delUser; 

        }

        public void DapperInsert(List<user> AktarilacakListe)
        {
            try
            {
                SqlConnectionStringBuilder myConn = new SqlConnectionStringBuilder
                {
                    InitialCatalog = "json_veri",
                    DataSource = ".",
                    UserID="sa",
                    Password=""
                };

                using (SqlConnection sqlConnect = new SqlConnection(myConn.ConnectionString))
                {
                    foreach (user satir in AktarilacakListe)
                    {
                        sqlConnect.Query<user>(@"INSERT INTO [dbo].[TBL_JSON_VERİ]([id],[name],[email],[gender],[status])
                                                                             VALUES(@id, @name, @email, @gender, @status)", satir);
                    }
                }
                MessageBox.Show("Aktarma yapıldı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

    }
}
