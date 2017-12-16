using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PetLibrary.Helper
{
    public class HttpHelper
    {
        public virtual string ConsumeGet(string baseAddress, string method)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress); 
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string requestUrl = method;  
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {                
                    return result;                    
                }
                else
                {
                    throw new Exception("Web API Error " + response.StatusCode + "; " + result);                    
                }
            }
        }        
    }
}
