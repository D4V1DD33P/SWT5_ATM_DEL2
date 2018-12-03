using System.Collections.Generic;
using ATMLibrary;
using ATMLibrary.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
   public class FilterDataTest
    {
        private IUpdateTrack _updateTrack;
        private FilterData _uut;
        private ITrackData _ValidfakeTrackData;
        private ITrackData _InvalidFakeTrackData;
        private List<ITrackData> _fakeTrackDataList;

        [SetUp]
        public void Setup()
        {
            _fakeTrackDataList = new List<ITrackData>();
            _updateTrack = Substitute.For<IUpdateTrack>();
            _uut = new FilterData(_updateTrack);
            _ValidfakeTrackData = new TrackData
            {
                X = 20000,
                Y = 30000,
                Altitude = 10000
            };
            _InvalidFakeTrackData = new TrackData
            {
                X = 9000,
                Y = 9000,
                Altitude = 200
            };
        }

        [Test]
        public void ConfirmTracks_TracksInArea_IsCorrect()
        {
            _fakeTrackDataList.Add(_ValidfakeTrackData);

            _uut.ConfirmTracks(_fakeTrackDataList);

            _updateTrack.Received().Update(Arg.Is<List<ITrackData>>(x => x.Count == 1));
        }

        [Test]
        public void ValidateTracks_TracksNotInArea_IsCorrect()
        {
            _fakeTrackDataList.Add(_InvalidFakeTrackData);

            _uut.ConfirmTracks(_fakeTrackDataList);

            _updateTrack.Received().Update(Arg.Is<List<ITrackData>>(x => x.Count == 0));
        }


    }
}
