using Nancy;

namespace ConsoleWithNancy.Modules.Modules
{
    public class AdministrationModule : NancyModule
    {
        public AdministrationModule()
            : base("/admin2")
        {
            // would capture routes to /products/list sent as a GET request
            Get["/"] = parameters => View["Admin"];
            //{
            //    return "The list of products";
            //};
        }
    }
}
