using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class Filter : IFilter
    {

        public List<FlightDataDTO> SortedDataTracks;
        public List<FlightDataDTO> FiltratedDataTracks;

        public Filter(ISortTrackData sortTrackData)
        {
            sortTrackData.SortDataEvent += FiltrateArea;
        }


        public event EventHandler<AirTrafficEvent> FiltratedEvent;

        public void FiltrateArea(object sender, AirTrafficEvent e)
        {
            SortedDataTracks = new List<FlightDataDTO>();
            SortedDataTracks = e.AirTrafficList;

            FiltratedDataTracks = new List<FlightDataDTO>();

            foreach (var c in SortedDataTracks)
            {
                if
                (Convert.ToInt32(c.XCor) <= 85000 && Convert.ToInt32(c.XCor) >= 5000 &&
                 Convert.ToInt32(c.YCor) <= 85000 && Convert.ToInt32(c.YCor) >= 5000)
                {
                    FiltratedDataTracks.Add(c);
                }
            }

            if (FiltratedDataTracks.Count != 0) //!FiltratedDataTracks.Contains(null))
            {
                AirTrafficEvent airTrafficEvent = new AirTrafficEvent(FiltratedDataTracks);
                FiltratedEvent?.Invoke(this, airTrafficEvent);
            }

        }


    }
}
