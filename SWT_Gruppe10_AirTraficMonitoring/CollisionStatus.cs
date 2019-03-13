using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class CollisionStatus : ICollisionStatus
    {
        private bool collisionStatus_;
        private string aircraftsColliding_;

        public CollisionStatus(IDataCalculator iDataCalculator)
        {
            iDataCalculator.DataCalculatedEvent += DetectCollision(); 
        }

        public void RecieveData()
        {

        }

        public void DetectCollision()
        {
            

        }

        public void RaiseEvent()
        {

        }

        public void ShowData()
        {

        }
    }
}
