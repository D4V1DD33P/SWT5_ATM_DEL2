using System.Collections.Generic;

namespace ATMLibrary
{
    public interface IDetectVicinity
    {
        void checkVicinity(List<ITrackData> listOfTrackInfo);
         
    }
}
