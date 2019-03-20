using System;
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

        //[Test]
        //public void detectCollision_x1at11000x2at11500y1at10500y2at11500_collisionstatustrue()
        //{
        //    List<FlightDataDTO> TrackData = new List<FlightDataDTO>(); 
        //    FlightDataDTO flight1 = new FlightDataDTO("ATR423", 11000, 10500, 7000, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, "");
        //    FlightDataDTO flight2 = new FlightDataDTO("ATB675", 11500, 11500, 7000, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, "");
        //    TrackData.Add(flight1);
        //    TrackData.Add(flight2);

        //    UUT_.DetectCollision(TrackData);

        //    Assert.That(UUT_.collisionStatus_, Is.EqualTo(true));
        //}

    }
}
