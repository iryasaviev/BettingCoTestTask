using System.Net;

namespace BettingCoTestTask.Services
{
    public class NetworkHelper
    {
        public string GetHtml(string uriAdress)
        {
            return new WebClient().DownloadString(uriAdress);
        }
    }
}