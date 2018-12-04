using System.Collections.Generic;
using ATMLibrary;
using ATMLibrary.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;


namespace AirTrafficMonitoring.Test.Unit
{

    [TestFixture]
    class ParsingTest
    {

        private ParseData _uut;  
        private ITransponderReceiver _receiver;
        private RawTransponderDataEventArgs _fakeTransponderDataEventArgs;
        private IFilterData _filtering;       


        [SetUp]
        public void Setup()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _filtering = Substitute.For<IFilterData>();
            _uut = new ParseData(_receiver, _filtering);
            _fakeTransponderDataEventArgs = new RawTransponderDataEventArgs(new List<string>()
                { "DEE001;67892;67890;12000;20160101100909111" });

        }

        private void FakeEventRaised()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeTransponderDataEventArgs);
        }

        [Test]
        public void TrackInList_withOne_CorrectCount()
        {
            FakeEventRaised();
            _filtering.Received().ConfirmTracks(Arg.Is<List<ITrackData>>(x => x.Count == 1));
        }

        //Test af tag
        [Test]
        public void TrackInList_withOne_CorrectTag()
        {
            FakeEventRaised();
            _filtering.Received().ConfirmTracks(Arg.Is<List<ITrackData>>(x => x[0].Tag == "DEE001"));

        }

        [Test]
        public void TrackInList_withOne_CorrectX()
        {
            FakeEventRaised();
            _filtering.Received().ConfirmTracks(Arg.Is<List<ITrackData>>(x => x[0].X == 67892));
        }


        [Test]
        public void TrackInList_withOne_CorrectY()
        {
            FakeEventRaised();
            _filtering.Received().ConfirmTracks(Arg.Is<List<ITrackData>>(x => x[0].Y == 67890));
        }

        [Test]
        public void TrackInList_withThree_CorrectCount()
        {
            _fakeTransponderDataEventArgs.TransponderData.Add("DEE001;67892;67890;12000;20160101100909111");
            _fakeTransponderDataEventArgs.TransponderData.Add("DEE001;67892;67890;12000;20160101100909111");
            FakeEventRaised();
            _filtering.Received().ConfirmTracks(Arg.Is<List<ITrackData>>(x => x.Count == 3));

        }

        [Test]
        public void TrackInList_withThree_CorrectTag()
        {
            _fakeTransponderDataEventArgs.TransponderData.Add("DEE001;67892;67890;12000;20160101100909111");
            _fakeTransponderDataEventArgs.TransponderData.Add("DEE003;67892;67890;12000;20160101100909111");
            FakeEventRaised();
            _filtering.Received().ConfirmTracks(Arg.Is<List<ITrackData>>(x => x[2].Tag == "DEE003"));
        }

    }
}
