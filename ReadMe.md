# DllMapDemo

Fork of .NET core 3.0 Preview 9 of DLLMapDemo (i.e. NativeLibrary API) loader sample

DllMapDemo is cross-platform .NET core dynamic library linker demo (DLL) which uses .XML file to retarget different assembly name (NewLib) where pre-existing bindings uses another library name (OldLib).

## Notes

1. Download .NET 3.0 Preview 9 SDK x86 and/or x64.
2. Compiling NewLib C++ project and Demo .NET Core must be same platform (i.e. Demo x86 and NewLib Win32x86 or Demo x64 and NewLib x64), therefore you have to change Platform target of the Demo program.
3. Changed NewLib output folder to automatically build into Demo binaries folder.

### Links
Source code (https://github.com/dotnet/samples/tree/6cdfb0b32381d8934757dca0e6268e9dc50dc980/core/extensions/DllMapDemo)

Walkthrough: Create and use your own Dynamic Link Library (C++) [Link](https://docs.microsoft.com/en-us/cpp/build/walkthrough-creating-and-using-a-dynamic-link-library-cpp?view=vs-2019)

Issue: __"An attempt was made to load a program with an incorrect format. (0x8007000B)"__  [Github](https://github.com/dotnet/core/issues/1678)

More info found at [README](https://github.com/dotnet/samples/blob/6cdfb0b32381d8934757dca0e6268e9dc50dc980/core/extensions/DllMapDemo/ReadMe.md)