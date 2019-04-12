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
    class FilterTest
    {
        private Filter UUT_;
        private ISortTrackData sortTrackData_;
        private AirTrafficEvent event_;

        [SetUp]
        public void Setup()
        {

            event_ = null;
            sortTrackData_ = NSubstitute.Substitute.For<ISortTrackData>();
            UUT_ = new Filter(sortTrackData_);

            UUT_.FiltratedEvent += (e, args) => { event_ = args; };
        }


        [Test]
        public void EventRecieved()
        {
            List<FlightDataDTO> flightlist = new List<FlightDataDTO>();
            FlightDataDTO flight1 = new FlightDataDTO("ABC123",10000,10000,10000,DateTime.Now,200,90,new CollidingFlightsDTO("","",""));

            flightlist.Add(flight1);

            sortTrackData_.SortDataEvent += Raise.EventWith(new AirTrafficEvent(flightlist));

            Assert.That(event_,Is.Not.Null);

        }

        [Test]
        public void FiltrateArea_AreaFlightLimit_within()
        {
            List<FlightDataDTO> flightlist = new List<FlightDataDTO>();

            FlightDataDTO flight1 = new FlightDataDTO("ABC123", 10000, 10000, 10000, DateTime.Now, 200, 90, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight2 = new FlightDataDTO("DEF456", 70000, 15000, 10000, DateTime.Now, 200, 90, new CollidingFlightsDTO("", "", ""));

            flightlist.Add(flight1);
            flightlist.Add(flight2);

            //flightlist = UUT_.SortedDataTracks;
            sortTrackData_.SortDataEvent += Raise.EventWith(new AirTrafficEvent(flightlist));

            //Assert.That(UUT_.FiltratedDataTracks,Is.EqualTo(flightlist));
            Assert.That(event_, Is.Not.Null);

        }

        [Test]
        public void AreaFligtLimit_outside()
        {
            List<FlightDataDTO> flightlist = new List<FlightDataDTO>();

            FlightDataDTO flight1 = new FlightDataDTO("ABC123", 90000, 4000, 10000, DateTime.Now, 200, 90, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight2 = new FlightDataDTO("ABC123", 90000, 40000, 10000, DateTime.Now, 200, 90, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight3 = new FlightDataDTO("ABC123", 80000, 4000, 10000, DateTime.Now, 200, 90, new CollidingFlightsDTO("", "", ""));

            flightlist.Add(flight1);
            flightlist.Add(flight2);
            flightlist.Add(flight3);

            //flightlist = UUT_.SortedDataTracks;

            sortTrackData_.SortDataEvent += Raise.EventWith(new AirTrafficEvent(flightlist));

            //Assert.That(UUT_.FiltratedDataTracks,Is.Null);
            Assert.That(event_, Is.Null);

        }

        [Test]
        public void TestestesVois()
        {
            Assert.That(event_, Is.Null);
        }

    }
}