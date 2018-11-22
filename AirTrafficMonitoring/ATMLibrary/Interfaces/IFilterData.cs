using System.Collections.Generic;

namespace ATMLibrary
{
    public interface IFilterData
    {
        int _minX { get; set; }
        int _maxX { get; set; }
        int _minY { get; set; }
        int _maxY { get; set; }
        int _minAltitude { get; set; }
        int _maxAltitude { get; set; }

        void ConfirmTracks(List<ITrackData> trackData);
        void LeftAirspace(List<ITrackData> trackData); // Hurtig tilføjelse 
    }
}
