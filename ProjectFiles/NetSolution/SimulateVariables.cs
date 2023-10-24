#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NativeUI;
using FTOptix.HMIProject;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.DataLogger;
using FTOptix.CommunicationDriver;
using FTOptix.EventLogger;
using FTOptix.Recipe;
#endregion

public class SimulateVariables : BaseNetLogic
{

    private PeriodicTask MyTask;
    private int iCounter;
    private int iCounter2;
    private int iCounter3;
    private int iCounter4;
    private double dCounter;
    private double dCounter2;
    private double dCounter3;
    private double dCounter4;
    private bool bRun;

    public override void Start()
    {
        MyTask = new PeriodicTask(Simulation, 250, LogicObject);
        iCounter = 0;
        iCounter2 = 0;
        iCounter3 = 0;
        iCounter4 = 0;
        dCounter = 0;
        dCounter2 = 0;
        dCounter3 = 0;
        dCounter4 = 0;
        MyTask.Start();
    }

    public void Simulation()
    {
        bRun = LogicObject.GetVariable("bRunSimulation").Value;
        if (bRun == true)
        {
            if (iCounter<=99)
            {
                iCounter =  iCounter + 1;
            }
            else
            {
                iCounter = 0;
            }
            if (iCounter2 <= 500)
            {
                iCounter2 = iCounter2 + 10;
            }
            else
            {
                iCounter2 = 0;
            }
            if (iCounter3 <= 999)
            {
                iCounter3 = iCounter3 + 5;
            }
            else
            {
                iCounter3 = 0;
            }
            if (iCounter4 <= 1500)
            {
                iCounter4 = iCounter4 + 20;
            }
            else
            {
                iCounter4 = 0;
            }
            dCounter = dCounter + 0.05;
            dCounter2 = dCounter2 + 0.1;
            dCounter3 = dCounter3 + 0.2;
            dCounter4 = dCounter4 + 0.5;
            LogicObject.GetVariable("iRamp").Value = iCounter;
            LogicObject.GetVariable("iRamp2").Value = iCounter2;
            LogicObject.GetVariable("iRamp3").Value = iCounter3;
            LogicObject.GetVariable("iRamp4").Value = iCounter4;
            LogicObject.GetVariable("iSin").Value = Math.Sin(dCounter) * 100;
            LogicObject.GetVariable("iCos").Value = Math.Cos(dCounter) * 50;
            LogicObject.GetVariable("iSin2").Value = Math.Sin(dCounter2) * 100;
            LogicObject.GetVariable("iCos2").Value = Math.Cos(dCounter2) * 50;
            LogicObject.GetVariable("iSin3").Value = Math.Sin(dCounter3) * 100;
            LogicObject.GetVariable("iCos3").Value = Math.Cos(dCounter3) * 50;
            LogicObject.GetVariable("iSin4").Value = Math.Sin(dCounter4) * 100;
            LogicObject.GetVariable("iCos4").Value = Math.Cos(dCounter4) * 50;
        }

    }

    public override void Stop()
    {
        if (MyTask != null)
        {
            MyTask.Dispose();
            MyTask = null;
        }
    }
}
