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
    public class DataCalculatorTest
    {
        private DataCalculator UUT_;
        private IFilter fakeFilter;
        private AirTrafficEvent ATEvent;

        [SetUp]
        public void setUp()
        {
            ATEvent = null;
            fakeFilter = Substitute.For<IFilter>();
            UUT_ = new DataCalculator(fakeFilter);

            UUT_.DataCalculatedEvent += (o, args) => { ATEvent = args; };
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
            UUT_.oldTrackData.Add(new FlightDataDTO("ATR423", s1, s2, 7000, new DateTime(2019, 4, 17, 13, 30, 10),0, 0, new CollidingFlightsDTO("", "", "")));

            FlightDataDTO flight = new FlightDataDTO("ATR423", s3, s4, 7000,
                new DateTime(2019, 4, 17, 14, 30, 10), 0, 0, new CollidingFlightsDTO("", "", ""));
            UUT_.calculateCourse(flight);

            Assert.That(flight.Course, Is.EqualTo(expectedResult));
        }

        [TestCase(6000, 6000, 7000, 6000, 10000, 4000, 250)]
        [TestCase(6000, 6000, 4000, 6000, 10000, 7000, 250)]
        [TestCase(6000, 10000, 7000, 6000, 6000, 4000, 250)]
        [TestCase(6000, 10000, 4000, 6000, 6000, 7000, 250)]
        public void calculateVelocity_XYdifferenceHighDifferenceTimeDifference_meterprsecond(int s1, int s2, int s3, int s4, int s5, int s6, int expectedResult)
        {
            UUT_.oldTrackData.Add(new FlightDataDTO("BTU423", s1, s2, s3, new DateTime(2019, 4, 17, 13, 30, 10), 0, 0, new CollidingFlightsDTO("", "", "")));
            FlightDataDTO flight = new FlightDataDTO("BTU423", s4, s5, s6,
                new DateTime(2019, 4, 17, 13, 30, 30), 0, 0, new CollidingFlightsDTO("", "", ""));

            UUT_.CalculateVelocity(flight);

            Assert.That(flight.Velocity, Is.EqualTo(expectedResult));
        }

        [Test]
        public void testReception()
        {
            List<FlightDataDTO> Data_ = new List<FlightDataDTO>();

            Data_.Add(new FlightDataDTO("ABCD", 10, 10, 10, DateTime.Now, 10, 10, new CollidingFlightsDTO("", "", "")));

            fakeFilter.FiltratedEvent += Raise.EventWith(this, new AirTrafficEvent(Data_));

            Assert.That(UUT_.newTrackData, Is.EqualTo(Data_));

        }

        [Test]
        public void Calculator_callOneEvents_firstTimeEqualsFalse()
        {
            List<FlightDataDTO> Data1 = new List<FlightDataDTO>();
            
            Data1.Add(new FlightDataDTO("ABCD", 6000, 6000, 7000, DateTime.Now, 0, 0, new CollidingFlightsDTO("", "", "")));

            fakeFilter.FiltratedEvent += Raise.EventWith(this, new AirTrafficEvent(Data1));
           
            Assert.That(UUT_.firstTime, Is.EqualTo(false));

        }

        [Test]
        public void Calculator_callTwoEvents_getVelocityCalculated()
        {
            List<FlightDataDTO> Data1 = new List<FlightDataDTO>();
            List<FlightDataDTO> Data2 = new List<FlightDataDTO>();

            Data1.Add(new FlightDataDTO("ABCD", 6000, 6000, 7000, new DateTime(2019, 4, 17, 13, 30, 10), 0, 0, new CollidingFlightsDTO("", "", "")));
            Data2.Add(new FlightDataDTO("ABCD", 6000, 10000, 4000, new DateTime(2019, 4, 17, 13, 30, 30), 0, 0, new CollidingFlightsDTO("", "", "")));

            fakeFilter.FiltratedEvent += Raise.EventWith(this, new AirTrafficEvent(Data1));
            fakeFilter.FiltratedEvent += Raise.EventWith(this, new AirTrafficEvent(Data2));

            Assert.That(UUT_.newTrackData[0].Velocity, Is.EqualTo(250));
            
        }

        [Test]
        public void Calculator_callTwoEvents_getCourseCalculated()
        {
            List<FlightDataDTO> Data1 = new List<FlightDataDTO>();
            List<FlightDataDTO> Data2 = new List<FlightDataDTO>();

            Data1.Add(new FlightDataDTO("ABCD", 6000, 6000, 7000, new DateTime(2019, 4, 17, 13, 30, 10), 0, 0, new CollidingFlightsDTO("", "", "")));
            Data2.Add(new FlightDataDTO("ABCD", 12000, 12000, 4000, new DateTime(2019, 4, 17, 13, 30, 30), 0, 0, new CollidingFlightsDTO("", "", "")));

            fakeFilter.FiltratedEvent += Raise.EventWith(this, new AirTrafficEvent(Data1));
            fakeFilter.FiltratedEvent += Raise.EventWith(this, new AirTrafficEvent(Data2));

            Assert.That(UUT_.newTrackData[0].Course, Is.EqualTo(45));

        }

       //Nye test til genaflevering 

        [Test]
        public void NEWtestReception()
        {
            List<FlightDataDTO> testList = new List<FlightDataDTO>();

            FlightDataDTO FDDTO = new FlightDataDTO("DTO345", 35000, 40000, 10000, new DateTime(2019, 4, 17, 13, 30, 30),0,0, new CollidingFlightsDTO("", "", ""));

            testList.Add(FDDTO);

            fakeFilter.FiltratedEvent += Raise.EventWith(new AirTrafficEvent(testList));

            Assert.That(ATEvent, Is.Not.Null);
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
        public void NEWcalculateCourse_startandEnd_degrees(int s1, int s2, int s3, int s4, int expectedResult)
        {
            List<FlightDataDTO> testList1 = new List<FlightDataDTO>();
            List<FlightDataDTO> testList2 = new List<FlightDataDTO>();

            FlightDataDTO Track1 = new FlightDataDTO("ATR423", s1, s2, 7000, new DateTime(2019, 4, 17, 13, 30, 10), 0,
                0, new CollidingFlightsDTO("", "", ""));
            
            testList1.Add(Track1);

            FlightDataDTO Track2 = new FlightDataDTO("ATR423", s3, s4, 7000,
                new DateTime(2019, 4, 17, 14, 30, 10), 0, 0, new CollidingFlightsDTO("", "", ""));

            testList2.Add(Track2);

            fakeFilter.FiltratedEvent += Raise.EventWith(new AirTrafficEvent(testList1));
            fakeFilter.FiltratedEvent += Raise.EventWith(new AirTrafficEvent(testList2));
            
            Assert.That(ATEvent.AirTrafficList[0].Course, Is.EqualTo(expectedResult));
        }

        [TestCase(6000, 6000, 7000, 6000, 10000, 4000, 250)]
        [TestCase(6000, 6000, 4000, 6000, 10000, 7000, 250)]
        [TestCase(6000, 10000, 7000, 6000, 6000, 4000, 250)]
        [TestCase(6000, 10000, 4000, 6000, 6000, 7000, 250)]
        public void NEWcalculateVelocity_XYdifferenceHighDifferenceTimeDifference_meterprsecond(int s1, int s2, int s3, int s4, int s5, int s6, int expectedResult)
        {
            List<FlightDataDTO> testList1 = new List<FlightDataDTO>();
            List<FlightDataDTO> testList2 = new List<FlightDataDTO>();

            FlightDataDTO Track1 = new FlightDataDTO("BTU423", s1, s2, s3, new DateTime(2019, 4, 17, 13, 30, 10), 0, 0, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO Track2 = new FlightDataDTO("BTU423", s4, s5, s6,
                new DateTime(2019, 4, 17, 13, 30, 30), 0, 0, new CollidingFlightsDTO("", "", ""));

            testList1.Add(Track1);
            testList2.Add(Track2);

            fakeFilter.FiltratedEvent += Raise.EventWith(new AirTrafficEvent(testList1));
            fakeFilter.FiltratedEvent += Raise.EventWith(new AirTrafficEvent(testList2));

            Assert.That(ATEvent.AirTrafficList[0].Velocity, Is.EqualTo(expectedResult));
        }
    }
}
