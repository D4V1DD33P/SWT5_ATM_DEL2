using System;
using System.Collections.Generic;
using ATMLibrary.Interfaces;
namespace ATMLibrary
{
   public class DetectVicinity : IDetectVicinity
    {
        private int _HS = 5000; //Horisontal seperation < 5000 m
        private int _VS = 300; //Vertikal seperation < 300 m

        private readonly IVicinityData _detectedVicinityData, _renderEvent;
        private readonly List<IVicinityData> _detectedVicinityDatas;


        public DetectVicinity(IVicinityData renderEvent, IVicinityData detectedVicinityData)
        {
            _renderEvent = renderEvent;
            _detectedVicinityData = detectedVicinityData;
            _detectedVicinityDatas = new List<IVicinityData>();
        }

        public DetectVicinity()
        {
        }

        public void checkVicinity(List<ITrackData> listOfTrackInfo)
        {
            // Tjekker for kollision distance
            foreach (var track1 in listOfTrackInfo)
            {
                foreach (var track2 in listOfTrackInfo)
                {
                    // Horisontal distance og Vertikal distance
                    double HD = Math.Sqrt(Math.Pow(track1.X - track2.X, 2) + Math.Pow(track1.Y - track2.Y, 2));
                    double VD = Math.Abs(track2.Altitude - track1.Altitude);


                    if (HD <= _HS && VD <= _VS)
                    {
                        if (track1.Tag != track2.Tag)
                        {
                            _detectedVicinityData.TagOne = track1.Tag;
                            _detectedVicinityData.TagTwo = track2.Tag;
                            _detectedVicinityData.Timestamp = DateTime.Now;

                            _detectedVicinityDatas.Add(_detectedVicinityData);

                            _renderEvent.PrintEvent(_detectedVicinityDatas);
                            _renderEvent.LogToFile(_detectedVicinityDatas);
                        }
                    }
                }
            }
        }
    }
}
