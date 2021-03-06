﻿using System;
using System.Linq;
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
                Timestamp = new DateTime(2016, 05, 13, 10, 50, 35),
                Speed = 141
            };

            _creatingFakeTrackData2 = new TrackData
            {
                Tag = "DEEP401",
                X = 50000,
                Y = 50000,
                Altitude = 12000,
                Course = 0,
                Timestamp = new DateTime(2018, 10, 13, 10, 50, 35),
                Speed = 141
            };

        }

        [Test]
        public void ValidateTracks_ValidTracks_PrintsCalculatedVelocity()
        {
            _creatingFakeTrackDataList.Clear();
            //Adds fake data to list
            _creatingFakeTrackDataList.Add(_creatingFakeTrackData1);
            _filteringData.ConfirmTracks(_creatingFakeTrackDataList);

            _trackDataRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[0].Tag == "JAS002" && data[0].Speed == (int)141));

            _creatingFakeTrackDataList.Clear();

            _creatingFakeTrackDataList.Add(_creatingFakeTrackData2);
            _filteringData.ConfirmTracks(_creatingFakeTrackDataList);

            _trackDataRendition.Received().Print(Arg.Is<List<ITrackData>>(data => data[1].Tag == "DEEP401" && data[1].Speed == (int)141));
        }
    }
}
