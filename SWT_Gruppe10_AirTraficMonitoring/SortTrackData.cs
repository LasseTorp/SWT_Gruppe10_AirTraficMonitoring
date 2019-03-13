using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace SWT_Gruppe10_AirTraficMonitoring
{
    class SortTrackData : ISortTrackData
    {
        
        // Vores DLL skal ligges ind i vores github repository, eller hvad det nu hedder. 
        // Har bare lavet en reference til den lokalt, ved ikke om i får det med ved push/commit 


        //Dette skal til for at hooke sig til DLL
        private ITransponderReceiver reciever_;

        public SortTrackData(ITransponderReceiver reciever)//Her skal den hooke sig på DLL. interfacet i DLL
        {
            //Dette er taget direkte fra kode eksemplet
            reciever_ = reciever;
            reciever_.TransponderDataReady += RecieverOnTransponderDataReady; 
        }

        private void RecieverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            //et eller andet skal være lige med        e.Transponderdata
            
        }






        //Tror det kommende kode skal til for at lave denne klasse til source

        //Ved ikke om denne linje skal bruges. Den står i event source slidet. 
        public event EventHandler<AirTrafficEvent> SortDataEvent;


        public void SortData()
        {
            if ()
            {
                
            }
        }
    }
}
