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

public class LogMessages : BaseNetLogic
{
    [ExportMethod]
    public void GenerateLogMessages()
    {
        Log.Debug("This is a debug message");
        Log.Debug("CustomCategory", "This is a debug message");
        Log.Error("This is an error message");
        Log.Error("CustomCategory", "This is an error message");
        Log.Info("This is an info message");
        Log.Info("CustomCategory", "This is an info message");
        Log.Info("Error on node " + Log.Node(LogicObject));
        Log.Warning("This is a warning message");
        Log.Warning("CustomCategory", "This is a warning message");
    }
}
