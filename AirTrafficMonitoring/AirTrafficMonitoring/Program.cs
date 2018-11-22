using System;
using TransponderReceiver;
using ATMLibrary.Interfaces;
using ATMLibrary;


namespace AirTrafficMonitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Vent... ";
            ITransponderReceiver transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();


            ITrackData renderTrack = new TrackData();
            IVicinityData vicinityData = new VicinityData();


            IDetectVicinity vicinity = new DetectVicinity(vicinityData, vicinityData);
            IUpdateTrack trackUpdate = new UpdateTrack(renderTrack, vicinity);
            IFilterData filtering = new FilterData(trackUpdate);

            var decoder = new ParseData(transponderDataReceiver, filtering);

            Console.WriteLine(str);
            Console.ReadLine();
        }
    }
}
