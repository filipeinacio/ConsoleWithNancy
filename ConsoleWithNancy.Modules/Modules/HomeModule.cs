using Nancy;

namespace ConsoleWithNancy.Modules.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
            : base("/")
        {
            // would capture routes to /products/list sent as a GET request
            Get["/"] = parameters => View["Home2"];
            //{
            //    return "The list of products";
            //};
        }
    }
}