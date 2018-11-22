using System;
using System.Collections.Generic;
using System.Text;
using ATMLibrary.Interfaces;
using System.IO;


namespace ATMLibrary
{
    public class VicinityData : IVicinityData
    {
        public string TagOne { get; set; }
        public string TagTwo { get; set; }
        public DateTime Timestamp { get; set; }
        public VicinityData()
        {
            Timestamp = DateTime.MinValue;
        }


        public void LogToFile(List<IVicinityData> proximityDetectionDatas)
        {

            using (StreamWriter outputFile = new StreamWriter(@"Logfile.txt", true))
            {
                foreach (var proximityDetectionData in proximityDetectionDatas)
                {
                    string text = "Planes in conflict: " + proximityDetectionData.TagOne + " and " +
                                  proximityDetectionData.TagTwo +
                                  "\nTime of occurance: " + proximityDetectionData.Timestamp;
                    outputFile.WriteLine(text);
                }
            }
        }

        public void PrintEvent(List<IVicinityData> vicinityDatas)
        {
            //Print the collision warning to the console
            foreach (var data in vicinityDatas)
            {
                System.Console.WriteLine(data);
            }
        }

        public override string ToString()
        {
            var str = $"{TagOne}: conflicts with {TagTwo} at {Timestamp}";      //Conflict print
            return str;

        }

    }
}
