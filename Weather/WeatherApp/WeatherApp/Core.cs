using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            //Sign up for a free API key at http://openweathermap.org/appid  
            //string key = "YOUR API KEY HERE";
            string key = "3d8cf2e912c973578143971854dc8f52";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
                + zipCode + ",jp&appid=" + key + "&units=imperial";

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                var kashi = results["main"]["temp"];
                var seshi = (5.0 / 9) * ((double)kashi - 32);

                var meter = (double)results["wind"]["speed"] * 0.447;
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                //weather.Temperature = (string)results["main"]["temp"] + " F";
                weather.Temperature = seshi.ToString() + " C";
                //weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Wind = meter.ToString() + "m/s";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new System.DateTime(1970, 1, 1, 9, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() ;
                weather.Sunset = sunset.ToString() ;
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}
