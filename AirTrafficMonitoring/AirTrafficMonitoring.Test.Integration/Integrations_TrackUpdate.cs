using System;
using System.Collections.Generic;
using ATMLibrary;
using ATMLibrary.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    public class Integrations_TrackUpdate
    {
        private ITrackData _trackData;
        private IVicinityData _eventRendition; 
        private IDetectVicinity _proximityDetection;
        private IVicinityData _vicinityData;
        private IUpdateTrack _updateTrack;
        private ITrackData _creatingfakeTrackData1;
        private ITrackData _creatingfakeTrackData2;
        private ITrackData _creatingfakeTrackData3;
        private ITrackData _creatingfakeTrackData4;
        private List<ITrackData> _creatingfakeTrackDataList;

        [SetUp]
        public void SetUp()
        {
  
            _trackData = Substitute.For<ITrackData>();
            _eventRendition = Substitute.For<IVicinityData>();
            _vicinityData = Substitute.For<IVicinityData>();
            _proximityDetection = new DetectVicinity(_eventRendition, _vicinityData);
            _updateTrack = new UpdateTrack(_trackData, _proximityDetection);

            _creatingfakeTrackDataList = new List<ITrackData>();

            _creatingfakeTrackData1 = new TrackData
            {
                Tag = "DEEP4072s",
                X = 50000,
                Y = 50000,
                Altitude = 12000,
                Course = 0,
                Timestamp = new DateTime(2018, 05, 13, 10, 50, 35),
                Speed = 0
            };

            _creatingfakeTrackData2 = new TrackData
            {
                Tag = "J5S004",
                X = 50100,
                Y = 50100,
                Altitude = 12000,
                Course = 0,
                Timestamp = new DateTime(2018, 11, 22, 10, 50, 35),
                Speed = 0
            };

        }

        [Test]
        public void UpdateSeperation_EventTrue_DataPrinted()
        {
            _creatingfakeTrackDataList.Add(_creatingfakeTrackData1);
            _creatingfakeTrackDataList.Add(_creatingfakeTrackData2);
            _updateTrack.Update(_creatingfakeTrackDataList);

            _eventRendition.Received().PrintEvent(Arg.Is<List<IVicinityData>>(data => data[0].TagOne == "J5S004"));
        }

        [Test]
        public void Update_SeperationEventTrue_SeperationDataLoggedToFile()
        {
            _creatingfakeTrackDataList.Add(_creatingfakeTrackData1);
            _creatingfakeTrackDataList.Add(_creatingfakeTrackData2);
            _updateTrack.Update(_creatingfakeTrackDataList);

            _eventRendition.Received().LogToFile(Arg.Is<List<IVicinityData>>(data => data[0].TagTwo == "DEEP4072s"));
        }

    }
}
