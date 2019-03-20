﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT_Gruppe10_AirTraficMonitoring;

namespace AirTrafficMonitoring_Test_unit
{
    [TestFixture]
    class CollisionStatusTest
    {
        private CollisionStatus UUT_;
        private IDataCalculator fakeDataCalculator;

        [SetUp]
        public void SetUp()
        {
            fakeDataCalculator = Substitute.For<IDataCalculator>(); 
            UUT_ = new CollisionStatus(fakeDataCalculator);
        }

        [TestCase(10000, 11000, 7000, 18000, 11000, 7000, false)]
        [TestCase(10000, 11000, 7000, 14500, 11000, 7000, true)]
        public void detectCollision_dependingonXcoordinates_collisionstatustrue(int x1, int y1, int altitude1, int x2, int y2, int altitude2, bool collisionStatus_)
        {
            List<FlightDataDTO> TrackData = new List<FlightDataDTO>(); 
            FlightDataDTO flight1 = new FlightDataDTO("ATR423", x1, y1, altitude1, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, "");
            FlightDataDTO flight2 = new FlightDataDTO("ATB675", x2, y2, altitude2, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, "");
            TrackData.Add(flight1);
            TrackData.Add(flight2);

            UUT_.DetectCollision(TrackData);

            Assert.That(UUT_.collisionStatus_, Is.EqualTo(true));
        }

    }
}
