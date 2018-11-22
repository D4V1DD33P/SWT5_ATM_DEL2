using System;
using System.Collections.Generic;
using System.Threading;
using TransponderReceiver;

namespace ATMLibrary
{
    public class ParseData : IParseData
    {
        private readonly IFilterData _filter;
        private List<TrackData> _listOfTracks;

        public ParseData(ITransponderReceiver rcv, IFilterData filter) 
        {
            rcv.TransponderDataReady += Data;
            //rcv.TransponderDataReady += DataLeft;
            _filter = filter;
            _listOfTracks = new List<TrackData>();

        }
        public TrackData Parsing(string data) 
        {
            TrackData myTrack = new TrackData();

            var words = data.Split(';');

            myTrack.Tag = words[0];
            myTrack.X = int.Parse(words[1]);
            myTrack.Y = int.Parse(words[2]);
            myTrack.Altitude = int.Parse(words[3]);
            myTrack.Timestamp = DateTime.ParseExact(words[4], "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture);
            myTrack.Course = 0;
            myTrack.Speed = 0;

            return myTrack;
        }

        List<ITrackData> listOfTracks = new List<ITrackData>();

        public void Data(object o, RawTransponderDataEventArgs args)
        {   
            listOfTracks.Clear();

            foreach (var data in args.TransponderData)
            {
                listOfTracks.Add(Parsing(data));
            }
            _filter.ConfirmTracks(listOfTracks);
            //Thread.Sleep(5000);  
            //_filter.LeftAirspace(listOfTracks);
        }

        //public void DataLeft(object o, RawTransponderDataEventArgs args)
        //{
        //    //List<ITrackData> listOfTracks = new List<ITrackData>();
        //    //listOfTracks.Clear();

        //    foreach (var data in args.TransponderData)
        //    {
        //        listOfTracks.Add(Parsing(data));
        //    } 
        //    _filter.LeftAirspace(listOfTracks);
        //}

    }
}
