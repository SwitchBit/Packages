using System;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace SwitchBit.Extensibility
{
    public static class ExtensibilityExtensions
    {
        private const string _pluginSearchFilter = "*.dll";

        public static ContainerConfiguration WithPluginsInPath(this ContainerConfiguration configuration, string path, SearchOption searchOption = SearchOption.TopDirectoryOnly)
            => WithPluginsInPath(configuration, path, null, searchOption);

        public static ContainerConfiguration WithPluginsInPath(this ContainerConfiguration configuration, string path, AttributedModelProvider conventions, SearchOption searchOption = SearchOption.TopDirectoryOnly)
            => configuration.WithAssemblies(
                    Directory.GetFiles(path, _pluginSearchFilter, searchOption)
                            .Select(AssemblyLoadContext.GetAssemblyName)
                            .Select(AssemblyLoadContext.Default.LoadFromAssemblyName), conventions);
        //Lol that you can do this...
    }
}
