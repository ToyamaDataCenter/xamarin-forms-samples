using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    public class DataService
    {
        public static async Task<dynamic> getDataFromService(string queryString)
        {

            //Handlerに設定
            //ProxyはOSで設定したプロキシサーバーURLとポートが取得可能
            //認証情報は取得できなかったのでPickerによって取得
            //HttpClientHandler handler = new HttpClientHandler();
            //handler.Proxy = System.Net.WebRequest.DefaultWebProxy;
            //handler.Proxy.Credentials = new System.Net.NetworkCredential("internal\\itou", "1844");

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }
    }
}
