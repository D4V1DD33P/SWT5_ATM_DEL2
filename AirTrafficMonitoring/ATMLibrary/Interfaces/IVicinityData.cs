using System;
using System.Collections.Generic;

namespace ATMLibrary.Interfaces
{
    public interface IVicinityData
    {
        string TagOne { get; set; }
        string TagTwo { get; set; }
        DateTime Timestamp { get; set; }
        void LogToFile(List<IVicinityData> vicinityDatas);
        void PrintEvent(List<IVicinityData> vicinityDatas);
    }
}
