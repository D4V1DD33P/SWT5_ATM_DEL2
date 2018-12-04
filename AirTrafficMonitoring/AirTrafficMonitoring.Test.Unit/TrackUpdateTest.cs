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
            private ITrackData _testtrack1, _testtrack2, _testtrack3, _testtrack4;

            private List<ITrackData> _trackData; 
            private ITrackData _RenditiontrackData;     
            private IDetectVicinity _detectVicinity;      

            [SetUp]

            public void SetUp()
            {
                _RenditiontrackData = Substitute.For<ITrackData>();
                _detectVicinity = Substitute.For<IDetectVicinity>();

                _trackData = new List<ITrackData>();                //initial

            _testtrack1 = Substitute.For<ITrackData>();
            _testtrack2 = Substitute.For<ITrackData>();
            _testtrack3 = Substitute.For<ITrackData>();
            _testtrack4 = Substitute.For<ITrackData>();

            _testtrack1.Timestamp.Returns(new DateTime(2018, 12, 04, 10, 50, 35));
            _testtrack2.Timestamp.Returns(new DateTime(2018, 12, 04, 10, 20, 31));
            _testtrack3.Timestamp.Returns(new DateTime(2018, 12, 04, 11, 56, 42));
            _testtrack4.Timestamp.Returns(new DateTime(2018, 12, 04, 12, 30, 21));


            }

            [Test]
            public void Update_NewAndOldTimestamp_returningEqual()
            {
                var uut = new UpdateTrack(_RenditiontrackData, _detectVicinity);
                _trackData.Add(_testtrack1);
                _trackData.Add(_testtrack2);

                uut.Update(_trackData);
                Assert.That(_trackData[0].Timestamp, Is.EqualTo(uut.OldList[0].Timestamp)); //New list is equal to OldList
            }

            [Test]
            public void Updating_Timestamp()
            {
                var uut = new UpdateTrack(_RenditiontrackData, _detectVicinity);
                _trackData.Add(_testtrack3);
                _trackData.Add(_testtrack4);

                uut.Update(_trackData);   //new list
                Assert.That(_trackData[1].Timestamp, Is.EqualTo(uut.OldList[1].Timestamp));  // Still works with data[1]
            }

        [Test]
        public void Update_OldAndNewVelocity_returningEqual()
        {

            var uut = new UpdateTrack(_RenditiontrackData, _detectVicinity);
            _trackData.Add(_testtrack1);
            _trackData.Add(_testtrack2);

            uut.Update(_trackData);
            Assert.That(_trackData[0].Altitude, Is.EqualTo(uut.OldList[0].Altitude));
        }

            [Test]
            public void Update_OldandNewVelocity_returningNotEqual()
            {

                var uut = new UpdateTrack(_RenditiontrackData, _detectVicinity);
            _testtrack1.Timestamp.Returns(new DateTime(2018, 05, 13, 10, 50, 30));
            _testtrack1.X.Returns(58000);
            _testtrack1.Y.Returns(67000);
            _testtrack1.Tag.Returns("DEE73");


                _trackData.Add(_testtrack1);

                uut.Update(_trackData);

            _testtrack2.Timestamp.Returns(new DateTime(2018, 05, 13, 10, 50, 36));
            _testtrack2.X.Returns(65000);
            _testtrack2.Y.Returns(71000);
            _testtrack2.Tag.Returns("DEE73");



                _trackData.Add(_testtrack2);
                var vel = new UpdateTrack(_RenditiontrackData, _detectVicinity).CalSpeed(_testtrack1, _testtrack2);
                uut.Update(_trackData);

                Assert.AreNotEqual(vel, uut.OldList[0].Speed);
            }

        [Test]
            public void RenderTrack_IsCalled_True()
            {
                var uut = new UpdateTrack(_RenditiontrackData, _detectVicinity);
                uut.Update(_trackData);

                _RenditiontrackData.Received().Print(_trackData);
            }

            [Test]
            public void VicinityDetection_IsCalled_True()
            {
                var uut = new UpdateTrack(_RenditiontrackData, _detectVicinity);
                uut.Update(_trackData);

                _detectVicinity.Received().CheckVicinity(_trackData);
            }

    }
}
