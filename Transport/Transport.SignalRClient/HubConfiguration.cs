namespace Transport.SignalRClient
{
    public class HubConfiguration
    {
        private string _url;

        public string Transport => _url + "/transport";

        public HubConfiguration(string url)
        {
            _url = url;
        }
    }
}
