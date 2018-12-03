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
        private ITrackData _track1, _track2;
        private IVicinityData _renderEvent;
        private IVicinityData _vicinityData;

        [SetUp]
        public void SetUp()
        {
            _dataListTrackData = new List<ITrackData>();
            _vicinityData = new VicinityData(); 
            _renderEvent = Substitute.For<IVicinityData>();
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

    }
}
