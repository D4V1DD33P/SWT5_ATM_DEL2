using System;
using System.Collections.Generic;
using System.Threading;
using ATMLibrary.Interfaces;

namespace ATMLibrary
{
   public class FilterData : IFilterData
    {
        private readonly IUpdateTrack _trackUpdate;
        public int _minX { get; set; }
        public int _maxX { get; set; }
        public int _minY { get; set; }
        public int _maxY { get; set; }
        public int _minAltitude { get; set; }
        public int _maxAltitude { get; set; }

        //public FilterData()
        //{
            
        //}

        public FilterData(IUpdateTrack trackUpdate)
        {
            _trackUpdate = trackUpdate;
            _minX = 10000;
            _maxX = 90000;
            _minY = 10000;
            _maxY = 90000;
            _minAltitude = 500;
            _maxAltitude = 20000;
        }
        List<ITrackData> myTracks = new List<ITrackData>();

        public void ConfirmTracks(List<ITrackData> trackInfo)
        {
            
            foreach (var track in trackInfo)
            {
                if (track.X >= _minX && track.X <= _maxX && track.Y >= _minY && track.Y <= _maxY)
                {
                    if (track.Altitude >= _minAltitude && track.Altitude <= _maxAltitude)
                    { 
                        myTracks.Add(track);
                        Console.WriteLine("Flight entered airspace");
                    }
                }
            }     
            _trackUpdate.Update(myTracks);
        }

        // Dette er en hurtig tilføjelse 
        public void LeftAirspace(List<ITrackData> trackInfo)
        {
            //List<ITrackData> leftTracks = new List<ITrackData>();
            foreach (var track in new ITrackData[])Info)
            {
                if (track.X <= _minX || track.X >= _maxX || track.Y <= _minY || track.Y >= _maxY)
                {
                    if (track.Altitude <= _minAltitude || track.Altitude >= _maxAltitude)
                    { 
                        myTracks.Remove(track);
                        Console.WriteLine("Hello World!")

                    }
                }
            }
            
            _trackUpdate.Update(myTracks);
        }

        //public void LeftAirspace()
        //{

        //}
    }
}
