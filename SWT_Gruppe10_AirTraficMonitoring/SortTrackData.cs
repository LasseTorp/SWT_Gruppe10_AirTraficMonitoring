using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class SortTrackData : ISortTrackData
    {
        //Dette skal til for at hooke sig til DLL
        private ITransponderReceiver reciever_;

        public SortTrackData(ITransponderReceiver reciever)//Her skal den hooke sig på DLL. interfacet i DLL
        {
            DataRecieved_ = new List<string>();
            reciever_ = reciever;

            //reciever_.TransponderDataReady += RecieverOnTransponderDataReady;
            reciever_.TransponderDataReady += SortData;
        }


        // Klassen blev tilføjet for at se om der kunne printes fra dll filen. 
        private void RecieverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Dette blev gjort for at se om dataen kom ind og printet på konsollen 
           
            // foreach (var c in e.TransponderData)
            //{
            //  Console.WriteLine(c);   
            //}
        }


        public event EventHandler<AirTrafficEvent> SortDataEvent;
        public List<FlightDataDTO> data = new List<FlightDataDTO>();
        public List<string> DataRecieved_ { set; get; }

        public void SortData(object sender, RawTransponderDataEventArgs e)
        {
            
            DataRecieved_ = e.TransponderData;

            string[] inputfields;
           
            foreach (var flightData in e.TransponderData)
            {
                inputfields = flightData.Split(';');
                
                data.Add(new FlightDataDTO(inputfields[0],Convert.ToInt32(inputfields[1]),Convert.ToInt32(inputfields[2]),
                    Convert.ToInt32(inputfields[3]),DateTime.ParseExact(inputfields[4], "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture),0,0,""));

                //Console.WriteLine(DateTime.ParseExact(inputfields[4], "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture));
            }
            
            // måske
            AirTrafficEvent airTrafficEvent = new AirTrafficEvent();
            airTrafficEvent.AirTrafficList = data;
            SortDataEvent?.Invoke(this,airTrafficEvent);
            
        }
        
    }
}
