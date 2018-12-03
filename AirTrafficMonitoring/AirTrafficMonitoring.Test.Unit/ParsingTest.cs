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
                { "JAS001;12345;67890;12000;20160101100909111" });

        }

        private void RaiseFakeEvent()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_fakeTransponderDataEventArgs);
        }

        [Test]
        public void OneTrackInList_CountCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ConfirmTracks(Arg.Is<List<ITrackData>>(x => x.Count == 1));
        }

        //Test af tag
        [Test]
        public void OneTrackInList_TagCorrect()
        {
            RaiseFakeEvent();
            _filtering.Received().ConfirmTracks(Arg.Is<List<ITrackData>>(x => x[0].Tag == "JAS001"));

        }
    }
}
