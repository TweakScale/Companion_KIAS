﻿using System.Reflection;
using System.Runtime.InteropServices;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("TweakScaleCompanion_KIS")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(TweakScaleCompanion.KIS.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(TweakScaleCompanion.KIS.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(TweakScaleCompanion.KIS.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(TweakScaleCompanion.KIS.LegalMamboJambo.Trademark)]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access destination type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("5226e364-e21c-4d6a-81e7-faad64bbd330")]


// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(TweakScaleCompanion.KIS.Version.Number)]
[assembly: AssemblyFileVersion(TweakScaleCompanion.KIS.Version.Number)]
[assembly: KSPAssembly("TweakScaleCompanion_KIS", TweakScaleCompanion.KIS.Version.major, TweakScaleCompanion.KIS.Version.minor)]
[assembly: KSPAssemblyDependency("KSPe.Light.TweakScale", 2, 4)]
