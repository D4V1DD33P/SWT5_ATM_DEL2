using System.Collections.Generic;
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

        public FilterData()
        {
        }

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

        public void ConfirmTracks(List<ITrackData> trackInfo)
        {
            List<ITrackData> myTracks = new List<ITrackData>();
            foreach (var track in trackInfo)
            {
                if (track.X >= _minX && track.X <= _maxX && track.Y >= _minY && track.Y <= _maxY)
                {
                    if (track.Altitude >= _minAltitude && track.Altitude <= _maxAltitude)
                        myTracks.Add(track);  
                }        
            }
            _trackUpdate.Update(myTracks);
        }
    }
}
