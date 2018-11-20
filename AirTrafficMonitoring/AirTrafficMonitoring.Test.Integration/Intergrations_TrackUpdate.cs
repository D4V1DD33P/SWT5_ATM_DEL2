using System;
using System.Collections.Generic;
using ATMLibrary;
using ATMLibrary.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    public class Intergrations_TrackUpdate
    {
        //private List<ITrackData> _trackData;
        private ITrackData _trackData; //trackrendition
        private IVicinityData _eventRendition; //eventrendition
        private IDetectVicinity _proximityDetection;
        private IVicinityData _proximityDetectionData;
        private IUpdateTrack _trackUpdate;
        //private ITrackData _track1;
        //private ITrackData _track2;
        private ITrackData _fakeTrackDataValid1;
        private ITrackData _fakeTrackDataValid2;
        private List<ITrackData> _fakeTrackDataList;

        [SetUp]
        public void SetUp()
        {
            // _trackData = new List<ITrackData>();  //elementerne i listen skal subtitutes
            //_filtering = Substitute.For<IFiltering>();

            _trackData = Substitute.For<ITrackData>();
            _eventRendition = Substitute.For<IVicinityData>();
            _proximityDetectionData = Substitute.For<IVicinityData>();
            _proximityDetection = new DetectVicinity(_eventRendition, _proximityDetectionData);
            //_proximityDetectionData = Substitute.For<IProximityDetectionData>();
            _trackUpdate = new UpdateTrack(_trackData, _proximityDetection);
            //_track1 = Substitute.For<ITrackData>();
            //_track2 = Substitute.For<ITrackData>();

            _fakeTrackDataList = new List<ITrackData>();

            _fakeTrackDataValid1 = new TrackData
            {
                Tag = "JAS002",
                X = 50000,
                Y = 50000,
                Altitude = 12000,
                Course = 0,
                Timestamp = new DateTime(2018, 05, 13, 10, 50, 35),
                Speed = 0
            };

            _fakeTrackDataValid2 = new TrackData
            {
                Tag = "J5S002",
                X = 50100,
                Y = 50100,
                Altitude = 12000,
                Course = 0,
                Timestamp = new DateTime(2018, 05, 13, 10, 50, 35),
                Speed = 0
            };



        }


    }
}
