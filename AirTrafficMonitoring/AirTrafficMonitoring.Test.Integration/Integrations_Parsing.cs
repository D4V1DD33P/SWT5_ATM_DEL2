using System;
using System.Collections.Generic;
using ATMLibrary;
using ATMLibrary.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    public class Integrations_Parsing
    {
        private IUpdateTrack _updateTrack;
        private ITrackData _trackData;
        private IFilterData _filteringData;
        private IDetectVicinity _detectVicinity;
        private IParseData _parsingData;
        private IVicinityData _eventRenditionVicinityData;
        private ITransponderReceiver _transponderReceiver;
        private IVicinityData _vicinityData;
        private RawTransponderDataEventArgs _dataEvent;
        private ITrackData _fakeTrackData;
        private List<ITrackData> _fakeTrackList;

        [SetUp]     //SetUp for tests
        public void Setup()
        {

            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _trackData = Substitute.For<ITrackData>();
            _vicinityData = new VicinityData();
            _eventRenditionVicinityData = new VicinityData();
            _detectVicinity = new DetectVicinity(_eventRenditionVicinityData, _vicinityData);
            _updateTrack = new UpdateTrack(_trackData, _detectVicinity);
            _filteringData = new FilterData(_updateTrack);
            _parsingData = new ParseData(_transponderReceiver, _filteringData);
            _fakeTrackList = new List<ITrackData>();

            _dataEvent = new RawTransponderDataEventArgs(new List<string>()
                { "DDAS011;53241;85634;17000;20181122100909111" });

            _fakeTrackData = new TrackData
            {
                Tag = "DDAS011",
                X = 53241,
                Y = 85634,
                Altitude = 17000,
                Course = 0,
                Speed = 0,
                Timestamp = DateTime.ParseExact("20181122100909111", "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture)
            };

        }


        private void FakeEventRaised()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_dataEvent);
        }


        [Test]
        public void ValidTracks_EventRaised_CorrectTrackCount()
        {
            FakeEventRaised();
            _trackData.Received().Print(Arg.Is<List<ITrackData>>(data => data.Count == 1));

        }



        [Test]
        public void ValidTracks_EventRaised_CorrectTrackTagName()
        {

            FakeEventRaised();
            _trackData.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Tag == "DDAS011"));

        }


        [Test]
        public void ValidTracks_EventRaised_CorrectXValueReceived()
        {
            FakeEventRaised();
            _trackData.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].X == 53241));
        }


        [Test]
        public void ValidTracks_EventRaised_CorrectYValueReceived()
        {
            FakeEventRaised();
            _trackData.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Y == 85634));
        }

        [Test]
        public void ValidTracks_EventRaised_Correct_AltitudeReceived()
        {
            FakeEventRaised();
            _trackData.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Altitude == 17000));
        }

        [Test]
        public void ValidTracks_EventRaised_Correct_TimeStampYearReceived()
        {
            FakeEventRaised();
            _trackData.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Timestamp.Year == 2018));
        }


    }
}
