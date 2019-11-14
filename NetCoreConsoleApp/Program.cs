using NetStandardCommon;
using System;
using System.IO;

namespace NetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo asm = new FileInfo(@"..\..\..\..\AspNetCoreApp\bin\debug\netcoreapp3.0\AspNetCoreApp.dll");
            var moduleDirectory = asm.DirectoryName;

            ModuleAssemblyLoadContext context = new ModuleAssemblyLoadContext(asm.Name, moduleDirectory, typeof(IModule));
            context.Scan();

            //Lets ModuleAssemblyLoadContext be designated as the 'correct' ALC, H/T @davidfowl
            using var _ = context.EnterContextualReflection();
            
            foreach (var module in context.GetImplementations<IModule>())
            {
                module.Start();
            }
        }
    }
}
