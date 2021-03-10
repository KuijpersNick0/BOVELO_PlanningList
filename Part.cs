    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOVELO_PlanningList
{
    class Part
    {
        private int nombreGuidon;
        private int nombreChassis;
        private int nombreRoue;

        private string location;

        Random r = new Random();

        public Part(int a, int b, int c)
        {
            a = this.nombreGuidon;
            b = this.nombreChassis;
            c = this.nombreRoue;
            location = String.Format("E{0}", r.Next());
        }


        public Tuple<int, int, int> getNombre()
        {
            return new Tuple<int,int,int>(nombreGuidon, nombreChassis, nombreRoue);
        }

        public string getLocation()
        {
            return location;
        }
    }
}
