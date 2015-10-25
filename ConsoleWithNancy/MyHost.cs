using System;
using Nancy.Hosting.Self;

namespace ConsoleWithNancy
{
    public class MyHost
    {
        private NancyHost _host;
        private IDisposable _webApiHost;

        public void Start()
        {
            _host = new NancyHost(new Uri("http://localhost:8888/nancy/"));
            _host.Start();

        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}