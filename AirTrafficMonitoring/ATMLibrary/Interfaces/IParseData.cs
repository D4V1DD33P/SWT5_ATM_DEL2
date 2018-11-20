using TransponderReceiver;

namespace ATMLibrary
{
    public interface IParseData
    {
        TrackData Parsing(string data);
        void Data(object o, RawTransponderDataEventArgs args);
    }
}
