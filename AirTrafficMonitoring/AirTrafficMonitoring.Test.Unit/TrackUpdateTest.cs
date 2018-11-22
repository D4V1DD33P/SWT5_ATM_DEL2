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
            private ITrackData _track1;
            private ITrackData _track2;

            private List<ITrackData> _trackData; //List holds trackdataobjects
            private ITrackData _trackRendition;     
            private IDetectVicinity _detectVicinity;      

            [SetUp]

            public void SetUp()
            {
                _trackRendition = Substitute.For<ITrackData>();
                _detectVicinity = Substitute.For<IDetectVicinity>();

                _trackData = new List<ITrackData>();                //initial

                _track1 = Substitute.For<ITrackData>();
                _track2 = Substitute.For<ITrackData>();

                _track1.Timestamp.Returns(new DateTime(2018, 05, 13, 10, 50, 35));
                _track2.Timestamp.Returns(new DateTime(2018, 05, 13, 10, 20, 31));


            }

            [Test]
            public void Update_TimeStampOldandNew_returnsEqual()
            {

                var uut = new UpdateTrack(_trackRendition, _detectVicinity);
                _trackData.Add(_track1);
                _trackData.Add(_track2);

                uut.Update(_trackData);                     //new list
                Assert.That(_trackData[0].Timestamp, Is.EqualTo(uut.oldList[0].Timestamp)); //New list is equal to oldList
            }
        }
}
