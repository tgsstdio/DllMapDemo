using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;

public class PreloadedDllMapEntry
{
    private readonly ReadOnlyDictionary<string, string> mKeys;
    public PreloadedDllMapEntry(Assembly assembly)
    {
        mKeys = BuildRemappings(assembly);
        NativeLibrary.SetDllImportResolver(assembly, this.MapAndLoad);
    }

    private IntPtr MapAndLoad(string libraryName, Assembly assembly, DllImportSearchPath? dllImportSearchPath)
    {
        bool found = mKeys.TryGetValue(libraryName, out string mappedName);
        return NativeLibrary.Load(found ? mappedName : libraryName, assembly, dllImportSearchPath);
    }

    private static ReadOnlyDictionary<string, string> BuildRemappings(Assembly assembly)
    {
        var output = new Dictionary<string, string>();
        var assemblyLocation = assembly.Location;

        string xmlPath = Path.Combine(Path.GetDirectoryName(assemblyLocation),
            Path.GetFileNameWithoutExtension(assemblyLocation) + ".xml");

        if (!File.Exists(xmlPath))
            return WrapDictionary(output);

        foreach (var el in XElement.Load(xmlPath).Elements("dllmap"))
        {
            output.Add(el.Attribute("dll").Value, el.Attribute("target").Value);
        }

        return WrapDictionary(output);
    }

    private static ReadOnlyDictionary<string, string> WrapDictionary(Dictionary<string, string> output)
    {
        return new ReadOnlyDictionary<string, string>(output);
    }
}
