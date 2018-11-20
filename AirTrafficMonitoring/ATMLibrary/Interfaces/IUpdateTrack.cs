using System.Collections.Generic;

namespace ATMLibrary.Interfaces
{
    public interface IUpdateTrack
    {
        void Update(List<ITrackData> newList);
        int CalSpeed(ITrackData track1, ITrackData track2);
        int CalCourse(ITrackData track1, ITrackData track2);
    }
}
