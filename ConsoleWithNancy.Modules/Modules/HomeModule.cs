using Nancy;
using Nancy.Security;

namespace ConsoleWithNancy.Modules.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
            : base("/")
        {
            this.RequiresAuthentication();


            // would capture routes to /products/list sent as a GET request
            Get["/"] = parameters => View["Home2"];
            //{
            //    return "The list of products";
            //};
        }
    }
}