using System;
using System.IO;
using System.Reflection;
using ConsoleWithNancy.Modules.Modules;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Responses;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using System.Linq;

namespace ConsoleWithNancy
{
    public class MyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            Assembly assembly = Assembly.GetAssembly(typeof(AdministrationModule));
            var resourceNames = assembly.GetManifestResourceNames();
            var assemblyName = assembly.GetName().Name;

            nancyConventions.StaticContentsConventions.Add((context, path) =>
            {
                if (Path.GetDirectoryName(context.Request.Path) != null)
                {
                    var _path = assemblyName +
                                Path.GetDirectoryName(context.Request.Path)
                                    .Replace(Path.DirectorySeparatorChar, '.')
                                    .Replace("-", "_");
                    var _file = Path.GetFileName(context.Request.Path);
                    var _name = String.Concat(_path, ".", _file);
                    if (resourceNames.Contains(_name))
                    {
                        return new EmbeddedFileResponse(assembly, _path, _file);
                    }
                }
                return null;

            });
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            //This should be the assembly your views are embedded in
            var assembly = GetType().Assembly;
            var externalModules = Assembly.GetAssembly(typeof (AdministrationModule));
            ResourceViewLocationProvider
                .RootNamespaces
                //TODO: replace NancyEmbeddedViews.MyViews with your resource prefix
                .Add(externalModules, "ConsoleWithNancy.Modules");

        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder);
            }
        }

        void OnConfigurationBuilder(NancyInternalConfiguration x)
        {
            x.ViewLocationProvider = typeof(ResourceViewLocationProvider);
        }
    }
}