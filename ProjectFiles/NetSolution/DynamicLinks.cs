#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.UI;
using FTOptix.CODESYS;
using FTOptix.NativeUI;
using FTOptix.RAEtherNetIP;
#endregion

public class DynamicLinks : BaseNetLogic
{
    [ExportMethod]
    public void GenerateDynamicLinks()
    {
        // Setup
        // Add Label to MainWindow
        var projectRoot = Project.Current;
        var staticObjectFolder = projectRoot.Get<Folder>("StaticObjects");
        var mainWindow = staticObjectFolder.Get<WindowType>("MainWindow");

        // Create Label1
        var label1 = InformationModel.MakeObject<Label>("Label1");
        mainWindow.Add(label1);

        // Create Label2
        var label2 = InformationModel.MakeObject<Label>("Label2");
        mainWindow.Add(label2);

        // Create Models Folder
        var modelsFolder = InformationModel.MakeObject<Folder>("Models");
        Project.Current.Add(modelsFolder);

        // Add variables to Models folder
        var arrayDimensions = new uint[1];
        arrayDimensions[0] = 3;

        var speedList = InformationModel.MakeVariable("SpeedList", OpcUa.DataTypes.Int32, arrayDimensions);
        var speed1 = InformationModel.MakeVariable("Speed1", OpcUa.DataTypes.Int32);
        var speed2 = InformationModel.MakeVariable("Speed2", OpcUa.DataTypes.Int32);
        speedList.Add(speed1);
        speedList.Add(speed2);
        modelsFolder.Add(speedList);

        // IUAVariable.SetDynamicLink(source, mode) Test Step
        label1.TextVariable.SetDynamicLink(speed1, DynamicLinkMode.ReadWrite);

        // IUAVariable.SetDynamicLink(source, sourceArrayIndex, mode) Test Step
        label2.TextVariable.SetDynamicLink(speedList, 1, DynamicLinkMode.ReadWrite);
    }

    [ExportMethod]
    public void ResetDynamicLinks()
    {
        var label1 = Owner.Get<Label>("StaticObjects/MainWindow/Label1");
        var label2 = Owner.Get<Label>("StaticObjects/MainWindow/Label2");

        label1.TextVariable.ResetDynamicLink();
        label2.TextVariable.ResetDynamicLink();
    }

    [ExportMethod]
    public void CleanUp()
    {
        Owner.Get("Models").Delete();
        Owner.Get<Label>("StaticObjects/MainWindow/Label1").Delete();
        Owner.Get<Label>("StaticObjects/MainWindow/Label2").Delete();
    }
}
