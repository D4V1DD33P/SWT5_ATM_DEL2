using System.Collections.Generic;

namespace ATMLibrary
{
    public interface IDetectVicinity
    {
        void CheckVicinity(List<ITrackData> listOfTrackInfo);
         
    }
}
