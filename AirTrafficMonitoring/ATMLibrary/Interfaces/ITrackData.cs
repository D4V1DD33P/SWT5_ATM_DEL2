using System;
using System.Collections.Generic;

namespace ATMLibrary
{
    public interface ITrackData
    {
        string Tag { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int Altitude { get; set; }
        int Speed { get; set; }
        int Course { get; set; }

        DateTime Timestamp { get; set; }
        string ToString();
        void Print(List<ITrackData> listOfTrackDatas);


    }
}
