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

public class ReturnNodesBySearch : BaseNetLogic
{
    [ExportMethod]
    public void ReturnNodesSearchTest()
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
        var variableSpeed = InformationModel.MakeVariable("MotorSpeed", OpcUa.DataTypes.Int32);
        motorObjectType.Add(variableSpeed);
        var variableAcceleration = InformationModel.MakeVariable("Acceleration", OpcUa.DataTypes.Int32);
        motorObjectType.Add(variableAcceleration);

        // Create Motor1 which is of type MotorType
        var motor1Object = InformationModel.MakeObject("Motor1", motorObjectType.NodeId);
        modelFolder.Add(motor1Object);
        motor1Object.GetVariable("MotorSpeed").RemoteWrite(10);
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

        // IUANode.Find(browseName) Test
        if (motor1Object.Find("MotorSpeed") is IUANode)
        {
            Log.Info("IUANode.Find(browseName) Passed");
        }
        else
        {
            Log.Info("IUANode.Find(browseName) Failed");
        }

        // IUANode.FindObject(browseName) Test
        if (Project.Current.FindObject("Motor1") is IUAObject)
        {
            Log.Info("IUANode.FindObject(browseName) Passed");
        }
        else
        {
            Log.Info("IUANode.FindObject(browseName) Failed");
        }

        // IUANode.FindVariable(browseName) Test
        if (Project.Current.FindVariable("MotorSpeed") is IUAVariable)
        {
            Log.Info("IUANode.FindVariable(browseName) Passed");
        }
        else
        {
            Log.Info("IUANode.FindVariable(browseName) Failed");
        }

        // IUANode.Find<T>(browseName) Test
        if (Owner.Find<IUANode>("BaseFolder") is IUANode)
        {
            Log.Info("IUANode.Find<T>(browseName) Passed");
        }
        else
        {
            Log.Info("IUANode.Find<T>(browseName) Failed");
        }

        // IUANode.FindByType<T>() Test
        if (Project.Current.FindByType<IUANode>() is not null)
        {
            Log.Info("IUANode.FindByType<T>() Passed");
        }
        else
        {
            Log.Info("IUANode.FindByType<T>() Failed");
        }

        // IUANode.FindNodesByType<T>() Test
        if (Project.Current.FindNodesByType<IUANode>() is not null)
        {
            Log.Info("IUANode.FindNodesByType<T>() Passed");
        }
        else
        {
            Log.Info("IUANode.FindNodesByType<T>() Failed");
        }

        CleanUp();
    }
    private void CleanUp()
    {
        // Get project
        Owner.Get("BaseFolder").Delete();
    }
}
