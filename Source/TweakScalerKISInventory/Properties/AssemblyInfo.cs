using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("TweakScalerKISInventory")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(TweakScaleCompanion.KIAS.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(TweakScaleCompanion.KIAS.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(TweakScaleCompanion.KIAS.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(TweakScaleCompanion.KIAS.LegalMamboJambo.Trademark)]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access destination type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("a41726c2-bfeb-426b-a203-b2a84ea9b273")]

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
[assembly: AssemblyVersion(TweakScaleCompanion.KIAS.Version.Number)]
[assembly: AssemblyFileVersion(TweakScaleCompanion.KIAS.Version.Number)]
[assembly: KSPAssembly("TweakScalerKISInventory", TweakScaleCompanion.KIAS.Version.major, TweakScaleCompanion.KIAS.Version.minor)]
[assembly: KSPAssemblyDependency("TweakScaleCompanion_KIS", TweakScaleCompanion.KIAS.Version.major, TweakScaleCompanion.KIAS.Version.minor)]
[assembly: KSPAssemblyDependency("KSPe.Light.TweakScale", 2, 4)]
