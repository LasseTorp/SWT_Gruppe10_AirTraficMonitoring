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
    public class DataCalculatorTest
    {
        private DataCalculator UUT_;
        private ISortTrackData fakeSortTrackData;

        [SetUp]
        public void setUp()
        {
            fakeSortTrackData = Substitute.For<ISortTrackData>();
            UUT_ = new DataCalculator(fakeSortTrackData);
        }

        [Test]
        public void calculateCourse_start6000and6000End12000and12000_45degrees()
        {
            UUT_.oldTrackData.Add(new FlightDataDTO("ATR423", 6000, 6000, 7000, new DateTime(2019, 4, 17, 13, 30, 10),0, 0, "" ));

            FlightDataDTO flight = new FlightDataDTO("ATR423", 12000, 12000, 7000,
                new DateTime(2019, 4, 17, 14, 30, 10), 0, 0, "");
            UUT_.calculateCourse(flight);

            Assert.That(flight.Course, Is.EqualTo(45));
        }

        [Test]
        public void calculateVelocity_XYdifference4000HighDifference3000Time20Sec_250meterprsecond()
        {
            UUT_.oldTrackData.Add(new FlightDataDTO("BTU423", 6000, 6000, 7000, new DateTime(2019, 4, 17, 13, 30, 10), 0, 0, ""));
            FlightDataDTO flight = new FlightDataDTO("BTU423", 6000, 10000, 4000,
                new DateTime(2019, 4, 17, 13, 30, 30), 0, 0, "");

            UUT_.CalculateVelocity(flight);

            Assert.That(flight.Velocity, Is.EqualTo(250));
        }

    }
}
