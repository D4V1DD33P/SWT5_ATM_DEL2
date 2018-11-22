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
        private ITrackData _fakeTrackData;
        private List<ITrackData> _fakeTrackDataList;

        [SetUp]
        public void Setup()
        {
            _fakeTrackDataList = new List<ITrackData>();
            _updateTrack = Substitute.For<IUpdateTrack>();
            _uut = new FilterData(_updateTrack);
            _fakeTrackData = new TrackData
            {
                X = 20000,
                Y = 30000,
                Altitude = 10000
            };
        }

        [Test]
        public void ConfirmTracks_TracksInArea_IsCorrect()
        {
            _fakeTrackDataList.Add(_fakeTrackData);

            _uut.ConfirmTracks(_fakeTrackDataList);

            _updateTrack.Received().Update(Arg.Is<List<ITrackData>>(x => x.Count == 1));
        }
    }
}
