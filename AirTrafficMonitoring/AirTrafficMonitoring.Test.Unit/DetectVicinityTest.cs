using System.Collections.Generic;
using ATMLibrary;
using ATMLibrary.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class DetectVicinityTest
    {
        private IDetectVicinity _uut;
        private List<ITrackData> _dataListTrackData;

        private List<IVicinityData> _proximityDetections;
        private ITrackData _track1, _track2, _track3;
        private IVicinityData _renderEvent;
        private IVicinityData _vicinityData;

        [SetUp]
        public void SetUp()
        {
            _dataListTrackData = new List<ITrackData>();
            _vicinityData = new VicinityData();
            _renderEvent = Substitute.For<IVicinityData>();
            _proximityDetections = Substitute.For<List<IVicinityData>>();

            _uut = new DetectVicinity(_renderEvent, _vicinityData);

            _track1 = new TrackData
            {
                Tag = "DEEP401",
                X = 10050,
                Y = 10050,
                Altitude = 1000,
            };

            _track2 = new TrackData
            {
                Tag = "401DEEP",
                X = 15000,
                Y = 10000,
                Altitude = 1200,
            };

            _track3 = new TrackData
            {
                Tag = "402DEEP",
                X = 15000,
                Y = 10000,
                Altitude = 1200,
            };
        }

        [Test]
        public void ConfirmTracksCorrectTagPrinted()
        {
            _dataListTrackData.Add(_track1);
            _dataListTrackData.Add(_track2);
            _uut.CheckVicinity(_dataListTrackData);
            _renderEvent.Received()
            .PrintEvent(Arg.Is<List<IVicinityData>>(data => data[0].TagOne == "401DEEP" && data[0].TagTwo == "DEEP401"));
        }

        [Test]
        public void CheckProcximityDetection_SeperationValid_IsCorrect()
        {
            _dataListTrackData.Add(_track1);
            _dataListTrackData.Add(_track2);
            _uut.CheckVicinity(_dataListTrackData);

            _renderEvent.Received().LogToFile(Arg.Is<List<IVicinityData>>(data => data[0].TagOne == "401DEEP" && data[0].TagTwo == "DEEP401"));

        }

        // de to kommende test er ikke helt som ønsket :/
        [Test]
        public void CheckProximityDetection_CollisionCountIsOne()
        {


            _track1.Tag = "ABC123";
            _track2.Tag = "123ABC";
            _track1.X = 30000;
            _track2.X = 30050;
            _track1.Y = 50000;
            _track2.Y = 50050;
            _track1.Altitude = 5000;
            _track2.Altitude = 5050;

            _dataListTrackData.Add(_track1);
            _dataListTrackData.Add(_track2);


            _uut.CheckVicinity(_dataListTrackData);

            Assert.That(_proximityDetections.Count, Is.EqualTo(0));

        }

        [Test]
        public void CheckProximityDetection_CollisionCountIsOne_withThreeTracks()
        {

            _track1.Tag = "ART123";
            _track2.Tag = "THF334";
            _track1.X = 30000;
            _track2.X = 30050;
            _track1.Y = 50000;
            _track2.Y = 50050;
            _track1.Altitude = 5000;
            _track2.Altitude = 5050;
            _track3.X = 50000;
            _track3.Y = 70000;
            _track3.Altitude = 3000;

            _dataListTrackData.Add(_track1);
            _dataListTrackData.Add(_track2);
            _dataListTrackData.Add(_track3);


            _uut.CheckVicinity(_dataListTrackData);

            Assert.That(_proximityDetections.Count, Is.EqualTo(0));

        }

    }
}
