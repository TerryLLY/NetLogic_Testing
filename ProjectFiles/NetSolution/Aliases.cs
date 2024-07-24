#region Using directives
using System;
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

public class Aliases : BaseNetLogic
{
    [ExportMethod]
    public void GenerateAliases()
    {
        // Setup
        var projectRoot = Project.Current;
        var aliasesFolder = InformationModel.MakeObject<Folder>("AliasesFolder");
        projectRoot.Add(aliasesFolder);

        var window = InformationModel.MakeObject<Window>("AliasWindow");
        aliasesFolder.Add(window);

        var m1 = LogicObject.GetAlias("Motor");
        var motorAlias = InformationModel.MakeAlias("MotorAlias");
        window.Add(motorAlias);
        window.SetAlias("MotorAlias", m1);
    }

    [ExportMethod]
    public void CleanUp()
    {
        Owner.Get("AliasesFolder").Delete();
    }
}
