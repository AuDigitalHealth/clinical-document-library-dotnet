# Ported version of "CDA.Sample" to .NET Core 2.2

### What was involved

Step 1 - Upgrade dependencies 

First, the dependant projects, CDA.Generator, CDA.Generator.Common, CDA.GeneratedCode and Nehta.VendorLibrary.Common were ported to .NET 4.7.2, which made them .NET Standard 2.0 compatible.

Step 2 - Upgrade CDA.Sample project

The source code files for CDA.Sample were copied to a newly created .NET Core 2.2 console application. Then, the project references were added for CDA.Generator, CDA.Generator.Common, CDA.GeneratedCode and finally, Nehta.VendorLibrary.Common was cloned from github, added as a project reference, and portal to .NET 4.7.2 for .NET Standard 2.0 compatibility.

This was a proof of concept as I noticed you re-wrote the implementation of this library in Java, mainly for "cross platform compatibility reasons" however now as you can see, CDA.Sample runs as a .NET Core 2.2 application, making it 100% cross platform compatible.

### Next steps

Port remaining projects, CDA.NPDR, CDA.PCML, CDA.R5Sample etc ..