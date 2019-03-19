using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace SWT_Gruppe10_AirTraficMonitoring
{
    class SortTrackData : ISortTrackData
    {

        // lige nu står en liste af fligtdata til at være lig med et event , men det er også forkert. 
        


        // Vores DLL skal ligges ind i vores github repository, eller hvad det nu hedder. 
        // Har bare lavet en reference til den lokalt, ved ikke om i får det med ved push/commit 


        //Dette skal til for at hooke sig til DLL
        private ITransponderReceiver reciever_;

        public SortTrackData(ITransponderReceiver reciever)//Her skal den hooke sig på DLL. interfacet i DLL
        {
            //Dette er taget direkte fra kode eksemplet
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


        //Tror det kommende kode skal til for at lave denne klasse til source

        //Ved ikke om denne linje skal bruges. Den står i event source slidet. 


        public event EventHandler<AirTrafficEvent> SortDataEvent;
        public List<FlightDataDTO> data { get; set; }

        public void SortData(object sender, RawTransponderDataEventArgs e)
        {

            string[] inputfields;
            DateTime Timestamp = new DateTime();
            

            foreach (var flightData in e.TransponderData)
            {
                inputfields = flightData.Split(';');

                Timestamp = Convert.ToDateTime(inputfields[4]);

                data.Add(new FlightDataDTO(inputfields[0],Convert.ToInt16(inputfields[1]),Convert.ToInt16(inputfields[2]),
                    Convert.ToInt16(inputfields[3]),Timestamp,0,0,""));
                Console.WriteLine(Timestamp);
            }
            
            // måske
            AirTrafficEvent airTrafficEvent = new AirTrafficEvent();
            airTrafficEvent.AirTrafficList = data;
            SortDataEvent?.Invoke(this,airTrafficEvent);

            // i denne klasse skal de forskellige pladser i den string der kommer ind vel deles ud i vores fligtDataDTO 
        }



        // ud fra slide 3 skal der vel også en metode til at invoke vores SortDataEvent 
    }
}
