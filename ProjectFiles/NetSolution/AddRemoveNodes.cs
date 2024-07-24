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

public class AddRemoveNodes : BaseNetLogic
{
    [ExportMethod]
    public void AddNodes()
    {
        // Create Base folder
        var uiFolder = InformationModel.MakeObject<Folder>("UI");
        Project.Current.Add(uiFolder);
        var newPanel = InformationModel.MakeObject<Panel>("Panel");
        uiFolder.Add(newPanel);
        var newLabel = InformationModel.MakeObject<Label>("MyLabel");
        newPanel.Add(newLabel);

    }
    [ExportMethod]
    public void RemoveLabel()
    {
        var myPanel = Project.Current.FindObject("Panel");
        var objToRemove = Project.Current.FindObject("MyLabel");
        myPanel.Remove(objToRemove);
    }

    [ExportMethod]
    public void RemovePanel()
    {
        var uiFolder = Project.Current.FindObject("UI");
        var objToRemove = Project.Current.FindObject("Panel");
        uiFolder.Remove(objToRemove);
    }

    [ExportMethod]
    public void RemoveUIFolder()
    {
        Owner.Remove(Project.Current.FindObject("UI"));
    }
}
