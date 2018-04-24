using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GetAPIAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        // static void Main(string[] args)
        // {
        //     Test();
        //     Console.WriteLine("Main Test End!");
        //     Console.ReadLine();
        // }

        // static void Test()
        // {
        //     var test1=Test1();

        //     Task.Run(() =>
        //     {
        //         test1.Wait();
        //         Console.WriteLine("Test1 End!");
        //     });
        // }

        // static Task Test1()
        // {
        //     Thread.Sleep(1000);

        //     Console.WriteLine("create task in test1");

        //     return Task.Run(() =>
        //     {
        //         Thread.Sleep(3000);
        //         Console.WriteLine("Test1");
        //     });
        // }

        static Stopwatch stopwatch = new Stopwatch();
        static async void GetAPIAsync()
        {
            var adress = "http://localhost:5000";
            var disco = await DiscoveryClient.GetAsync(adress);


            //var tokenResponse = await GetTokenBypassword(disco);
            var tokenResponse = await GetToken(disco);
            // stopwatch.Start();
            // for (int i = 0; i < 20; i++)
            // {
            CallAPIAsync(tokenResponse);
            // }            
            // Console.WriteLine($"{stopwatch.ElapsedMilliseconds/1000}second");

        }
        static async Task<TokenResponse> GetToken(DiscoveryResponse disco)
        {
            // 请求令牌
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine($"result:{tokenResponse.Json}");
            return tokenResponse;
        }

        static async Task<TokenResponse> GetTokenBypassword(DiscoveryResponse disco)
        {
            // 请求令牌
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "123456789");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "123456", "api1");//使用用户名密码

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
            return tokenResponse;

        }
        private static async void CallAPIAsync(TokenResponse tokenResponse)
        {
            // 调用api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5003/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
                // var ar = JArray.Parse(content);
                // Console.WriteLine(ar);
                // Console.WriteLine($"{stopwatch.ElapsedMilliseconds}second");
                // return ar;
            }
            // return new JArray();
        }
    }
}