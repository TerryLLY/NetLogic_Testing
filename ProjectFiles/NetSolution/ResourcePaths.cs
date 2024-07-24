#region Using directives
using System;
using System.IO;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.CODESYS;
using FTOptix.RAEtherNetIP;
#endregion

public class ResourcePaths : BaseNetLogic
{
    [ExportMethod]
    public void ResourcePathsTest()
    {
        // ResourceURI.FromAbsolutePath(path) Test Step
        var applicationAbsoluteResourceUri = ResourceUri.FromAbsoluteFilePath("C:\\");
        if (applicationAbsoluteResourceUri is ResourceUri)
        {
            Log.Info("Result: " + applicationAbsoluteResourceUri.Uri +" ResourceURI.FromAbsolutePath(path) Passed");
        }
        else
        {
            Log.Info("ResourceURI.FromAbsolutePath(path) Failed");
        }

        // ResourceURI.FromProjectRelativePath(path) Test Step
        var projectRelativeResourceUri = ResourceUri.FromProjectRelativePath("ProjectRelativePath.txt");
        File.WriteAllText(projectRelativeResourceUri.Uri, "Hello world!");
        if (File.Exists(projectRelativeResourceUri.Uri))
        {
            Log.Info("ResourceURI.FromProjectRelativePath(path) Passed");
        }
        else
        {
            Log.Info("ResourceURI.FromProjectRelativePath(path) Failed");
        }

        // ResourceURI.FromURI(uri) Test Step
        var website  = ResourceUri.FromUri("https://www.rockwellautomation.com/");
        if (website is ResourceUri)
        {
            Log.Info("Result: " + website.Uri + " ResourceURI.FromURI(uri) Passed");
        }
        else
        {
            Log.Info("ResourceURI.FromURI(uri) Failed");
        }
    }

    [ExportMethod]
    public void CleanUp()
    {
        // Delete Files
        var projectRelativeResourceUri = ResourceUri.FromProjectRelativePath("ProjectRelativePath.txt");
        File.Delete(projectRelativeResourceUri.Uri);
    }
}
