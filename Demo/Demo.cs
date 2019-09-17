using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

public class DllMapDemo
{
    public static void Main()
    {
        try
        {
            Assembly mainAssembly = Assembly.GetExecutingAssembly();

            var assemblyNames = ExtractOwnAssemblyNames(mainAssembly);

            Console.WriteLine("Local Libraries");

            var relatedAssemblies = new List<Assembly>();
            //relatedAssemblies.Add(mainAssembly);
            foreach (var name in assemblyNames)
            {
                // AUTO LOAD ALL ASSEMBLIES
                    // SHOULD BE ASKED BY NAME
                relatedAssemblies.Add(Assembly.Load(name));                
            }

            var preload = new PreloadedDllMap(relatedAssemblies.ToArray());

            for (var i = 0; i < 3; i += 1)
            {
                // TESTING IF DLL MAP IS ONLY CALLED ONCE
                int thirty = NativeSum(10, 20);
                Console.WriteLine($"OldLib.NativeSum(10,20) = {thirty}");
            }

            NetStandardLib.SimpleFib.Display(4);

            Console.WriteLine("GetSum [NewLib.NativeSum(10,20)] = {0}", NetStandardLib.SimpleFib.GetSum(10, 20));

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message} Line: {e.Source}");
        }
    }

    private static AssemblyName[] ExtractOwnAssemblyNames(Assembly mainAssembly)
    {
        var context = DependencyContextLoader.Default.Load(mainAssembly);
        var ignoreLibraries = new StringCollection()
        {
            "mscorelib",
            "WindowBase",
            "runtime.win-x64.Microsoft.NETCore.DotNetHostPolicy",
            "runtime.win-x64.Microsoft.NETCore.App",
            "NETStandard.Library"
        };

        var assemblyNames = new List<AssemblyName>();
        foreach (RuntimeLibrary lib in context.RuntimeLibraries)
        {
            if (FilterAssemblyByName(ignoreLibraries, lib.Name))
            {
                foreach (var defaultName in lib.GetDefaultAssemblyNames(context))
                {
                    if (defaultName.Name == lib.Name)
                    {
                        assemblyNames.Add(defaultName);
                        break;
                    }
                }
            }
        }
        return assemblyNames.ToArray();
    }

    private static bool FilterAssemblyByName(StringCollection ignoreLibraries, string assemblyName)
    {
        if (assemblyName.StartsWith("System"))
        {
            return false;
        }
        else if (assemblyName.StartsWith("Microsoft"))
        {
            return false;
        }
        else if (ignoreLibraries.Contains(assemblyName))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    [DllImport("OldLib")]
    static extern int NativeSum(int arg1, int arg2);
}
