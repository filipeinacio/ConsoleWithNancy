using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ConsoleWithNancy
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(
                host =>
                {
                    host.Service<MyHost>(service =>
                    {
                        service.ConstructUsing(name => new MyHost());
                        service.WhenStarted(tc => tc.Start());
                        service.WhenStopped(tc => tc.Stop());
                    });
                }
                );
        }
    }
}
