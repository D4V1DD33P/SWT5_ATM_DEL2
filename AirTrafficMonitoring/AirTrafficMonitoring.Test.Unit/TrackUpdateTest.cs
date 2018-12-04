﻿using System;
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
            private ITrackData _track3;
            private ITrackData _track4;

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
                _track3 = Substitute.For<ITrackData>();
                _track4 = Substitute.For<ITrackData>();

                _track1.Timestamp.Returns(new DateTime(2018, 5, 13, 10, 50, 35));
                _track2.Timestamp.Returns(new DateTime(2018, 5, 13, 10, 20, 31));
                _track3.Timestamp.Returns(new DateTime(2018, 10, 26, 11, 56, 42));
                _track4.Timestamp.Returns(new DateTime(2018, 12, 15, 12, 30, 21));


            }

            [Test]
            public void Update_TimeStampOldandNew_returnsEqual()
            {
                var uut = new UpdateTrack(_trackRendition, _detectVicinity);
                _trackData.Add(_track1);
                _trackData.Add(_track2);

                uut.Update(_trackData);                     //new list
                Assert.That(_trackData[0].Timestamp, Is.EqualTo(uut.OldList[0].Timestamp)); //New list is equal to OldList
            }

            [Test]
            public void Update_TimeStamp()
            {
                var uut = new UpdateTrack(_trackRendition, _detectVicinity);
                _trackData.Add(_track3);
                _trackData.Add(_track4);

                uut.Update(_trackData);   //new list
                Assert.That(_trackData[1].Timestamp, Is.EqualTo(uut.OldList[1].Timestamp));  // Still works with data[1]
            }

        [Test]
        public void Update_VelocityOldandNew_returnsEqual()
        {

            var uut = new UpdateTrack(_trackRendition, _detectVicinity);
            _trackData.Add(_track1);
            _trackData.Add(_track2);

            uut.Update(_trackData);                     //new list
            Assert.That(_trackData[0].Altitude, Is.EqualTo(uut.OldList[0].Altitude));
        }

            [Test]
            public void Update_VelocityOldandNew_returnsNotEqual()
            {

                var uut = new UpdateTrack(_trackRendition, _detectVicinity);
                _track1.Timestamp.Returns(new DateTime(2018, 05, 13, 10, 50, 30));
                _track1.X.Returns(58000);
                _track1.Y.Returns(67000);
                _track1.Tag.Returns("SHN63");


                _trackData.Add(_track1);

                uut.Update(_trackData);         //new list SHN63 og old list SHN63

                // _trackData.Clear();

                _track2.Timestamp.Returns(new DateTime(2018, 05, 13, 10, 50, 36));
                _track2.X.Returns(65000);
                _track2.Y.Returns(71000);
                _track2.Tag.Returns("SHN63");



                _trackData.Add(_track2);
                var vel = new UpdateTrack(_trackRendition, _detectVicinity).CalSpeed(_track1, _track2);
                uut.Update(_trackData);

                Assert.AreNotEqual(vel, uut.OldList[0].Speed);
            }
    }
}
