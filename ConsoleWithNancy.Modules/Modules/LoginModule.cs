using System;
using ConsoleWithNancy.Modules.ViewModel;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using Nancy.Security;

namespace ConsoleWithNancy.Modules.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule()
            : base("/")
        {
            Get["/login"] = x =>
            {


                //Model.login = new loginModel() { Error = this.Request.Query.error.HasValue, ReturnUrl = this.Request.Url };
                //return View["login", Model];
                LoginViewModel model = new LoginViewModel();
                model.ReturnUrl = this.Request.Url;

                return View["Login", model];
            };

            Post["/login"] = x =>
            {
                var userGuid = HomeViewModel.MyUserMapper.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    return Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                string returnUrl = Context.Request.Query.ReturnUrl;
                return this.LoginAndRedirect(userGuid.Value, expiry, returnUrl );

                
                //return this.LoginAndRedirect(userGuid.Value, expiry);
                //Context.Request.Path

                //return Response.AsRedirect(x.Request.Query.RedirectUrl);
                
               // return Context.GetRedirect(returnUrl);// .GetRedirect(x.Request.Query.RedirectUrl.ToString());
            };

            Post["/logout"] = x =>
            {
                return this.LogoutAndRedirect("~/");
            };
        }
    }
}