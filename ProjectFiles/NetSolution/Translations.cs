#region Using directives
using System;
using System.Collections.Generic;
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

public class Translations : BaseNetLogic
{
    [ExportMethod]
    public void TranslationsTest()
    {
        // Setup
        var translationsFolder = InformationModel.MakeObject<Folder>("TranslationsFolder");
        Project.Current.Add(translationsFolder);

        var label1 = InformationModel.MakeObject<Label>("Label1");
        translationsFolder.Add(label1);

        var label2 = InformationModel.MakeObject<Label>("Label2");
        translationsFolder.Add(label2);

        // InformationModel.LookupTranslation(localizedText) Test Step
        var myLocalizedText = new LocalizedText("Hello");
        var translation = InformationModel.LookupTranslation(myLocalizedText);
        label1.Text = "Translation: " + translation.Text;

        // InformationModel.LookupTranslation(localizedText, localeIds) Test Step
        var myLocalizedText2 = new LocalizedText("Hello");
        // intentionally remove en-US from list below so it falls back to it-IT
        var translation2 = InformationModel.LookupTranslation(myLocalizedText2, new List<string>() { "it-IT", "pl-PL" });
        label2.Text = "Translation: " + translation2.LocaleId;

        // // InformationModel.AddTranslation(localizedText, localizationDictionary) Test Step
        var myLocalizedText3 = new LocalizedText("NewKey", "New translation", "en-US");
        InformationModel.AddTranslation(myLocalizedText3);

        // InformationModel.SetTranslation(localizedText) Test Step
        var localizedText = new LocalizedText("Hello", "Hi", "en-US");
        InformationModel.SetTranslation(localizedText);
    }

    [ExportMethod]
    public void CleanUp()
    {
        Owner.Get("TranslationsFolder").Delete();
        var localizedText = new LocalizedText("Hello", "Hello", "en-US");
        InformationModel.SetTranslation(localizedText);
        // Manually need to remove the "NewKey" item
    }
}
