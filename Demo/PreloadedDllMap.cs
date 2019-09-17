using System;
using System.Reflection;

public class PreloadedDllMap
{
    private PreloadedDllMapEntry[] mEntries;
    public PreloadedDllMap(Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            mEntries = Array.Empty<PreloadedDllMapEntry>();
        }
        else
        {
            int count = assemblies.Length;
            var entries = new PreloadedDllMapEntry[count];

            for (var i = 0; i < count; i += 1)
            {
                entries[i] = new PreloadedDllMapEntry(assemblies[i]);
            }

            mEntries = entries;
        }
    } 
}
