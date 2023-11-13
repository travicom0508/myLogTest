#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.DataLogger;
using System.IO;
using System.Text.RegularExpressions;
#endregion

public class CalcoloSpazioOccupato : BaseNetLogic
{
    private PeriodicTask myPeriodicTask;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        myPeriodicTask = new PeriodicTask(FunzioneContinuamenteEseguita, 1000, LogicObject);
        myPeriodicTask.Start();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        myPeriodicTask.Dispose();
    }

    private void FunzioneContinuamenteEseguita()
    {
        // Il DB viene salvato in un file che si chiama come il NodeID senza "-" e con estensione sqlite 
        // per togliere "-" ho aggiunto using System.Text.RegularExpressions;
        var nodoDatabase = (NodeId)LogicObject.GetVariable("Database").Value;       
        string nomeFile = nodoDatabase.Id.ToString();
        nomeFile = Regex.Replace(nomeFile, "-", string.Empty);

        // compongo il percorso completo
        string pathFile = ResourceUri.FromApplicationRelativePath(nomeFile + ".sqlite").Uri;
        
        // per utilizzare FileInfo ho aggiunto using System.IO;
        FileInfo fileinfo = new FileInfo(pathFile);
        
        // scrivo la dimensione del file nella variabile dello script
        LogicObject.GetVariable("Dimensione").Value = fileinfo.Length;
        
    }
}
