using System;
using System.Collections.Generic;
using ATMLibrary;
using ATMLibrary.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficMonitoring.Test.Unit
{
   
        [TestFixture]
        public class TrackUpdateTests
        {
            //private List<ITrackData> _track;
            private ITrackData _track1;
            private ITrackData _track2;

            private List<ITrackData> _trackData;                      //List holds trackdataobjects
            //private ITrackUpdate _uut;
            private ITrackData _trackRendition;     //private ITrackRendition _trackRendition;
            private IDetectVicinity _proximityDetection;      //private IProximityDetection _proximityDetection;

            [SetUp]

            public void SetUp()
            {
                _trackRendition = Substitute.For<ITrackData>();
                _proximityDetection = Substitute.For<IDetectVicinity>();

                _trackData = new List<ITrackData>();                //initial
                //_uut=new TrackUpdate(_trackData);

                _track1 = Substitute.For<ITrackData>();
                _track2 = Substitute.For<ITrackData>();

                _track1.Timestamp.Returns(new DateTime(2018, 05, 13, 10, 50, 35));
                _track2.Timestamp.Returns(new DateTime(2018, 05, 13, 10, 20, 31));


            }

            [Test]
            public void Update_TimeStampOldandNew_returnsEqual()
            {

                var uut = new UpdateTrack(_trackRendition, _proximityDetection);
                _trackData.Add(_track1);
                _trackData.Add(_track2);

                uut.Update(_trackData);                     //new list
                Assert.That(_trackData[0].Timestamp, Is.EqualTo(uut.oldList[0].Timestamp)); //New list is equal to oldList
            }
        }
}
