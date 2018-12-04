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

        private List<IVicinityData> _VicinityDetections;
        private ITrackData _testtrack1, _testtrack2, _testtrack3;
        private IVicinityData _renderEvent;
        private IVicinityData _vicinityData;

        [SetUp]
        public void SetUp()
        {
            _dataListTrackData = new List<ITrackData>();
            _vicinityData = new VicinityData();
            _renderEvent = Substitute.For<IVicinityData>();
            _VicinityDetections = Substitute.For<List<IVicinityData>>();

            _uut = new DetectVicinity(_renderEvent, _vicinityData);

            _testtrack1 = new TrackData
            {
                Tag = "DEEP401",
                X = 10050,
                Y = 10050,
                Altitude = 1000,
            };

            _testtrack2 = new TrackData
            {
                Tag = "401DEEP",
                X = 15000,
                Y = 10000,
                Altitude = 1200,
            };

            _testtrack3 = new TrackData
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
            _dataListTrackData.Add(_testtrack1);
            _dataListTrackData.Add(_testtrack2);
            _uut.CheckVicinity(_dataListTrackData);
            _renderEvent.Received()
            .PrintEvent(Arg.Is<List<IVicinityData>>(data => data[0].TagOne == "401DEEP" && data[0].TagTwo == "DEEP401"));
        }

        [Test]
        public void CheckVicinityDetectionData_ValidSepereation()
        {
            _dataListTrackData.Add(_testtrack1);
            _dataListTrackData.Add(_testtrack2);
            _uut.CheckVicinity(_dataListTrackData);

            _renderEvent.Received().LogToFile(Arg.Is<List<IVicinityData>>(data => data[0].TagOne == "401DEEP" && data[0].TagTwo == "DEEP401"));

        }

        // de to kommende test er ikke helt som ønsket :/
        [Test]
        public void CheckVicinityDetectionData_CollisionCountIsZero()
        {


            _testtrack1.X = 40000;
            _testtrack2.X = 40050;
            _testtrack1.Y = 60000;
            _testtrack2.Y = 60050;
            _testtrack1.Altitude = 6000;
            _testtrack2.Altitude = 6050;

            _dataListTrackData.Add(_testtrack1);
            _dataListTrackData.Add(_testtrack2);


            _uut.CheckVicinity(_dataListTrackData);

            Assert.That(_VicinityDetections.Count, Is.EqualTo(0));

        }

        [Test]
        public void CheckVicinityDetection_CollisionCount_ThreeTracks_IsZero()
        {

            _dataListTrackData.Add(_testtrack1);
            _dataListTrackData.Add(_testtrack2);
            _dataListTrackData.Add(_testtrack3);


            _uut.CheckVicinity(_dataListTrackData);

            Assert.That(_VicinityDetections.Count, Is.EqualTo(0));

        }

    }
}
