using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NorthwAPI2021syksy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaatietoController : ControllerBase
    {
        
        [HttpGet]
        [Route("{key}")]
        public string GetWeather(string key)
        {
            WebClient client = new WebClient();
            try
            {
                string data = client.DownloadString("https://ilmatieteenlaitos.fi/saa/" + key);
                int index = data.IndexOf("temperature");
                if (index > 0)
                {
                    string weather = data.Substring(index + 34, 3);
                    string stilisoitu = weather.Replace("<", "");
                    return stilisoitu;
                }
            }
            finally
            {
                client.Dispose();
            }
            return "(unknown)";
        }
    }
}
