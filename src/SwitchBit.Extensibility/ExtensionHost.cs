using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SwitchBit.Extensibility
{
    /// <summary>
    /// Handles loading of MEF plugins according to the type provided. 
    /// *Very prototype code
    /// </summary>
    /// <typeparam name="TExtention"></typeparam>
    public class ExtensionHost<TExtention>
    {
        string _originalRoot;

        public List<TExtention> Extensions { get; set; }

        protected ExtensionHost(string vizorRoot)
        {
            _originalRoot = vizorRoot;
        }

        public void Load(out List<TExtention> extensions)
        {
            Debug.WriteLine($@"[Extensibility.ExtensionHost.Load] Creating ConventionBuilder .ForTypesDerivedFrom<IExchangeVizor>");

            //var conventions = new ConventionBuilder();
            //conventions.ForTypesDerivedFrom<TExtention>().Export<IExchangeVizor>().Shared(); //configure the builder

            //var filter = "*.dll";
            //Debug.WriteLine($@"[Extensibility.ExtensionHost.Load] Probing [./] for [{filter}] recursively...");
            //Debug.WriteLine($@"[Extensibility.ExtensionHost.Load] Path: ./ Exists: {Directory.Exists("./")}");

            //some dependency issue here doing a .Select
            //TODO: Figure out what dependency is throwing a NotImplemented when using .Select off an array. 
            //TODO: linking possibly causing issues, manually loading things for now
            //NOTE: This should be fixable, but doesn't quite matter?

            //var assemblies = new List<Assembly>();
            //foreach(var file in Directory.EnumerateFiles("./", filter, SearchOption.AllDirectories))
            //{
            //    assemblies.Add(AssemblyLoadContext.Default.LoadFromAssemblyPath(file));
            //}

            //Debug.WriteLine($@"[Extensibility.ExtensionHost.Load] Found {assemblies.Count} assemblies with potential vizors");
            //var configuration = new ContainerConfiguration()
            //    .WithAssemblies(assemblies, conventions);

            //using (var c = configuration.CreateContainer())
            //{
            //    Debug.WriteLine($@"[Extensibility.ExtensionHost.Load] Retrieving discovered Vizor instances...");
            //    vizors = new List<IExchangeVizor>(c.GetExports<IExchangeVizor>());
            //}

            //extensions = extensions ?? throw new Exception("extensions was null in .Load"); //never hits for now

            extensions = new List<TExtention>
            {
                //new CustomExtension(),
                //new AnotherExtension(),
            };

            //Debug.WriteLine($@"[Extensibility.ExtensionHost.Load] Initializing Vizors");
            //foreach(var extension in extensions)
            //{
            //    Debug.WriteLine($@"[Extensibility.ExtensionHost.Load] {extension.DisplayName} Initializing...");

            //    await extension.Initialize();

            //    Debug.WriteLine($@"[Extensibility.ExtensionHost.Load] {extension.DisplayName} Complete");
            //}

            Extensions = extensions;
        }

        public static ExtensionHost<TExtention> Create<TExtension>(string rootLocation = ".") //TODO: Bad default, fails on different platforms
        {
            Debug.WriteLine($@"[Extensibility.ExtensionHost.Create] Creating ExtensionHost");
            var newHost = new ExtensionHost<TExtention>(rootLocation);
            return newHost;
        }
    }

}
