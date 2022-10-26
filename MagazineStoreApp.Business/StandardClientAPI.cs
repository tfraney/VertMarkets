using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace MagazineStoreApp.Business
{
    /// <summary>
    /// Standard Internal Client to call a general API by parameters
    /// </summary>
    internal class StandardClientAPI
    {
        /// <summary>
        /// Read by URL, ation, and args to an API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL">URL of internet API</param>
        /// <param name="action">The controller action </param>
        /// <param name="args">string list of arguments (optional) </param>
        /// <returns></returns>
        internal async static Task<T?> ReadAPI<T>(string URL, string action, params string[] args )
        {
           
            StringBuilder urlParams = new(action);
            HttpClient client = new();
            List<string>? arguments = null;
           
       
            try   {
                client.BaseAddress = new Uri(URL);
                arguments = args?.ToList();
                arguments?.ForEach(a => urlParams.Append($"/{a}"));

                using var response = await client.GetAsync(urlParams.ToString());
                response.EnsureSuccessStatusCode();

                //read json stream and deseriale to type T
                var json = await response.Content.ReadAsStringAsync();
                var ret =  !string.IsNullOrWhiteSpace(json) ? JsonConvert.DeserializeObject<T>(json) : default;                
                return ret;
            }
            catch { throw; }
            finally { client.Dispose(); arguments?.Clear();  urlParams.Length = 0; }
        }

        /// <summary>
        /// Post by URL, ation, and entity that will be serialized
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL">URL of internet API</param>
        /// <param name="action">The controller action </param>
        /// <param name="postEntity">object to be posted into request </param>
        /// <returns></returns>
        internal async static Task<T?> PostAPI<T>(string URL, string action, object postEntity)
        {
          
            HttpClient client = new();
            List<string>? arguments = null;         
            try  {
                client.BaseAddress = new Uri(URL);

                //serialze object into string content as JSON type
                HttpRequestMessage request = new(HttpMethod.Post, action);
                request.Content = new StringContent(JsonConvert.SerializeObject(postEntity));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");              
                
                //Post data
                using var response = await client.SendAsync(request);

                //read json stream and deseriale to type T
                var json = await response.Content.ReadAsStringAsync();
                return !string.IsNullOrWhiteSpace(json) ? JsonConvert.DeserializeObject<T>(json) : default;
            }
            catch { throw; }
            finally { client.Dispose(); arguments?.Clear();  }
        }
    }
}
