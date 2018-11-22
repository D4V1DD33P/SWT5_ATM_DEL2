using System;
using System.Collections.Generic;
using ATMLibrary;
using ATMLibrary.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    public class Intergrations_FilterData
    {

        private ITrackData _trackDataRendition;
        private IUpdateTrack _updateTrack;
        private IVicinityData _eventRendition;
        private IVicinityData _vicinityData;
        private IDetectVicinity _vicinityDetection;
        private IFilterData _filteringData;

        private ITrackData _creatingFakeTrackData1;
        private ITrackData _creatingFakeTrackData2;
        private ITrackData _creatingFakeTrackData3;
        private ITrackData _creatingFakeTrackData4;
        private List<ITrackData> _creatingFakeTrackDataList;

        [SetUp]
        public void SetUp()
        {
            _trackDataRendition = Substitute.For<ITrackData>();
            _vicinityData = new VicinityData();
            _eventRendition = new VicinityData();
            _vicinityDetection = new DetectVicinity(_eventRendition, _vicinityData);
            _updateTrack = new UpdateTrack(_trackDataRendition, _vicinityDetection);
            _filteringData = new FilterData(_updateTrack);

            _creatingFakeTrackDataList = new List<ITrackData>();

            _creatingFakeTrackData1 = new TrackData
            {
                Tag = "JAS002",
                X = 50000,
                Y = 50000,
                Altitude = 12000,
                Course = 0,
                Timestamp = new DateTime(2018, 05, 13, 10, 50, 35),
                Speed = 0
            };

            _creatingFakeTrackData2 = new TrackData
            {
                Tag = "JAS002",
                X = 50100,
                Y = 50100,
                Altitude = 12000,
                Course = 0,
                Timestamp = new DateTime(2018, 05, 13, 10, 50, 36),
                Speed = 0
            };
        }


        [Test]
        public void ValidateTracks_ValidTracks_PrintsCalculatedVelocity()
        {
            //Vi tilføjer fake data til listen
            _creatingFakeTrackDataList.Add(_creatingFakeTrackData1);
            _filteringData.ConfirmTracks(_creatingFakeTrackDataList);

            _creatingFakeTrackDataList.Clear();
            _creatingFakeTrackDataList.Add(_creatingFakeTrackData2);

            _filteringData.ConfirmTracks(_creatingFakeTrackDataList);

            _trackDataRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Tag == "JAS002" && data[0].Speed == (int)141));
        }

    }
}
