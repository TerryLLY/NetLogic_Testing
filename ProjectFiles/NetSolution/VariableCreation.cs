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
#endregion

public class VariableCreation : BaseNetLogic
{
    [ExportMethod]
    public void CreateVariables()
    {
        // Create Base folder
        var variablesFolder = InformationModel.MakeObject<Folder>("Variables");
        Project.Current.Add(variablesFolder);

        // InformationModel.MakeAnalogItem(browseName, dataTypeId) Test Step
        variablesFolder.Add(InformationModel.MakeAnalogItem("MyAnalogVar", OpcUa.DataTypes.Float));

        // InformationModel.MakeVariable(browseName, dataTypeId, arrayDimensions) Test Step
        var arrayDimensions = new uint[1];
        arrayDimensions[0] = 3;
        var myVar = InformationModel.MakeVariable("MyArray", OpcUa.DataTypes.Int32, arrayDimensions);
        variablesFolder.Add(myVar);

        // InformationModel.MakeVariable(browseName, dataTypeId, variableTypeId, arrayDimensions) Test Step
        var myVar2 = InformationModel.MakeVariable("Tag2", OpcUa.DataTypes.Int32, FTOptix.CODESYS.VariableTypes.Tag); ;
        variablesFolder.Add(myVar2);

        // InformationModel.MakeVariableType(browseName, dataTypeId, arrayDimensions) Test Step
        var myVar3 = InformationModel.MakeVariableType("MyVarType", OpcUa.DataTypes.Int32, arrayDimensions);
        variablesFolder.Add(myVar3);

        // InformationModel.MakeVariableType(browseName, variableTypeId, dataTypeId, arrayDimensions) Test Step
        var myVar4 = InformationModel.MakeVariableType("MyTagType", FTOptix.RAEtherNetIP.VariableTypes.Tag, OpcUa.DataTypes.Int32, arrayDimensions);
        variablesFolder.Add(myVar4);

        // InformationModel.MakeVariable<T>(browseName, dataTypeId, arrayDimensions) Test Step
        var myTag = InformationModel.MakeVariable<FTOptix.CODESYS.Tag>("CodesysTag", OpcUa.DataTypes.UInt16, arrayDimensions);
        variablesFolder.Add(myTag);

        // InformationModel.MakeVariableType<T>(browseName, dataTypeId, arrayDimensions) Test Step
        var myCustomTagType = InformationModel.MakeVariableType<FTOptix.CODESYS.TagType>("CustomCoDeSysTagType", OpcUa.DataTypes.UInt16, arrayDimensions);
        variablesFolder.Add(myCustomTagType);
    }

    [ExportMethod]
    public void CleanUp()
    {
        Owner.Get("Variables").Delete();
    }
}
