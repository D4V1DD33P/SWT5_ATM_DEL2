using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ATMLibrary;

namespace ATMLibrary
{
    public class TrackData : ITrackData
    {

        public string Tag { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Altitude { get; set; }
        public int Speed { get; set; }
        public int Course { get; set; }

        public DateTime Timestamp { get; set; }

        public TrackData()
        {
            X = 0;
            Y = 0;
            Altitude = 0;
            Speed = 0;
            Course = 0;
            Timestamp = DateTime.MinValue;
        }


        //Det her er udskriften som kommer i console
        public override string ToString()
        {

            return $"Flight IN Airspace: {Tag}: ({X}, " + $"{Y}) " + "\n" + 
                   $"ALT: {Altitude} meters, " + "\n" + 
                   $"VEL: {Speed} m/s, " + "\n" + 
                   $"CRS: {Course} degrees.";
            
        }

        public void Print(List<ITrackData> listOfTrackDatas)
        {
            foreach (var track in listOfTrackDatas)
            {
                System.Console.WriteLine($"{track}");
                Console.WriteLine("====================================================================");
            }
        }


    }
}
