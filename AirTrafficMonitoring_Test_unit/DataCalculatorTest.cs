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



        [TestCase(6000, 6000, 12000, 12000, 45)]
        [TestCase(12000, 12000, 6000, 6000, 225)]
        [TestCase(6000, 6000, 6000, 12000, 0)]
        [TestCase(6000, 12000, 6000, 6000, 180)]
        [TestCase(6000, 6000, 12000, 6000, 90)]
        [TestCase(12000, 6000, 6000, 6000, 270)]
        [TestCase(12000, 12000, 6000, 18000, 315)]
        [TestCase(12000, 12000, 18000, 6000, 135)]
        [TestCase(1, 1, 8000, 24000, 18)]
        public void calculateCourse_startandEnd_degrees(int s1, int s2, int s3, int s4, int expectedResult)
        {
            UUT_.oldTrackData.Add(new FlightDataDTO("ATR423", s1, s2, 7000, new DateTime(2019, 4, 17, 13, 30, 10),0, 0, "" ));

            FlightDataDTO flight = new FlightDataDTO("ATR423", s3, s4, 7000,
                new DateTime(2019, 4, 17, 14, 30, 10), 0, 0, "");
            UUT_.calculateCourse(flight);

            Assert.That(flight.Course, Is.EqualTo(expectedResult));
        }

        [TestCase(6000, 6000, 7000, 6000, 10000, 4000, 250)]
        [TestCase(6000, 6000, 4000, 6000, 10000, 7000, 250)]
        [TestCase(6000, 10000, 7000, 6000, 6000, 4000, 250)]
        [TestCase(6000, 10000, 4000, 6000, 6000, 7000, 250)]
        public void calculateVelocity_XYdifferenceHighDifferenceTimeDifference_meterprsecond(int s1, int s2, int s3, int s4, int s5, int s6, int expectedResult)
        {
            UUT_.oldTrackData.Add(new FlightDataDTO("BTU423", s1, s2, s3, new DateTime(2019, 4, 17, 13, 30, 10), 0, 0, ""));
            FlightDataDTO flight = new FlightDataDTO("BTU423", s4, s5, s6,
                new DateTime(2019, 4, 17, 13, 30, 30), 0, 0, "");

            UUT_.CalculateVelocity(flight);

            Assert.That(flight.Velocity, Is.EqualTo(expectedResult));
        }


        [Test]
        public void testReception()
        {
            List<FlightDataDTO> Data_ = new List<FlightDataDTO>();
            
            Data_.Add(new FlightDataDTO("ABCD",10,10,10,DateTime.Now, 10,10,"true"));

            fakeSortTrackData.SortDataEvent += Raise.EventWith(this, new AirTrafficEvent(Data_));

            Assert.That(UUT_.newTrackData,Is.EqualTo(Data_));

        }

    }
}
