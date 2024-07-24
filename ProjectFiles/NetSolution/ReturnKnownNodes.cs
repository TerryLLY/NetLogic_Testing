#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.CODESYS;
using FTOptix.NativeUI;
using FTOptix.RAEtherNetIP;
#endregion

public class ReturnKnownNodes : BaseNetLogic
{
    [ExportMethod]
    public void ReturnNodesTest()
    {
        // Get project
        var projectRoot = Project.Current;

        // Create Base folder
        var baseFolder = InformationModel.MakeObject<Folder>("BaseFolder");
        projectRoot.Add(baseFolder);

        // Create Sub folder
        var subFolder = InformationModel.MakeObject<Folder>("SubFolder");
        baseFolder.Add(subFolder);

        // Create Variable
        var variable = InformationModel.MakeVariable("TestVar", OpcUa.DataTypes.Boolean);
        baseFolder.Add(variable);

        // Create Model folder
        var modelFolder = InformationModel.MakeObject<Folder>("Model");
        baseFolder.Add(modelFolder);

        // Create motor type with varaibles
        var motorObjectType = InformationModel.MakeObjectType("MotorType");
        modelFolder.Add(motorObjectType);
        var variableSpeed = InformationModel.MakeVariable("Speed", OpcUa.DataTypes.Int32);
        motorObjectType.Add(variableSpeed);
        var variableAcceleration = InformationModel.MakeVariable("Acceleration", OpcUa.DataTypes.Int32);
        motorObjectType.Add(variableAcceleration);

        // Create Motor1 which is of type MotorType
        var motor1Object = InformationModel.MakeObject("Motor1", motorObjectType.NodeId);
        modelFolder.Add(motor1Object);
        motor1Object.GetVariable("Speed").RemoteWrite(10);
        motor1Object.GetVariable("Acceleration").RemoteWrite(20);

        // Create a panel that will use the motor
        var MotorWidget = InformationModel.MakeObjectType<PanelType>("MotorWidget");
        MotorWidget.Height = 450;
        MotorWidget.Width = 400;
        subFolder.Add(MotorWidget);

        // Add and set alias
        var alias = InformationModel.MakeAlias("Motor");
        MotorWidget.Add(alias);
        MotorWidget.SetAlias("Motor", motorObjectType);

        // IUANode.Get(browsePath) Test
        if (Owner.Get("BaseFolder/SubFolder") is IUANode)
        {
            Log.Info("IUANode.Get(browsePath) Passed");
        }
        else
        {
            Log.Info("IUANode.Get(browsePath) Failed");
        }

        // IUANode.GetObject(browsePath)
        if (Owner.GetObject("BaseFolder/SubFolder") is IUAObject)
        {
            Log.Info("IUANode.GetObject(browsePath) Passed");
        }
        else
        {
            Log.Info("IUANode.GetObject(browsePath) Failed");
        }

        // IUANode.GetVariable(browsePath) Test
        if (Owner.GetVariable("BaseFolder/TestVar") is IUAVariable)
        {
            Log.Info("IUANode.GetVariable(browsePath) Passed");
        }
        else
        {
            Log.Info("IUANode.GetVariable(browsePath) Failed");
        }

        // IUANode.Get<T>(browsePath) Test
        if (Owner.Get<IUANode>("BaseFolder/SubFolder") is IUANode)
        {
            Log.Info("IUANode.Get<T>(browsePath) Passed");
        }
        else
        {
            Log.Info("IUANode.Get<T>(browsePath) Failed");
        }

        // IUANode.GetByType<T>() Test
        if (Owner.GetByType<IUANode>().BrowseName != null)
        {
            Log.Info("IUANode.Get<T>() Passed");
        }
        else
        {
            Log.Info("IUANode.Get<T>() Failed");
        }

        // IUANode.GetNodesByType<T>() Test
        if (Owner.GetNodesByType<IUANode>() != null)
        {
            Log.Info("IUANode.GetNodesByType<T>() Passed");
        }
        else
        {
            Log.Info("IUANode.GetNodesByType<T>() Failed");
        }

        // IUANode.GetAlias(aliasName) Test
        if (MotorWidget.GetAlias("Motor") is IUANode)
        {
            Log.Info("IUANode.GetAlias(aliasName) Passed");
        }
        else
        {
            Log.Info("IUANode.GetAlias(aliasName) Failed");
        }

        CleanUp();
    }

    private void CleanUp()
    {
        // Get project
        Owner.Get("BaseFolder").Delete();
    }
}
