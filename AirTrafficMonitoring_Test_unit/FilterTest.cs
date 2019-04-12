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



    }
}
