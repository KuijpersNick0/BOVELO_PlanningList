using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOVELO_PlanningList
{
    class Horaire
    {
        

        //private int temps;

        public Horaire(DateTime a, DateTime b)
        {
            monHoraire = a;
            maTache = b;
        }

        public Horaire()
        {
            //pas encore définie
        }

        private void timer1_Tick(DateTime a)
        {
            a = a.AddMinutes(-1);//au lieu d'ajouter, un retire 1 minute
        }

    }
}
