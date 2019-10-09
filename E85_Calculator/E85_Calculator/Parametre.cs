using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E85_Calculator
{
    [Serializable]
    class Parametre
    {
        public double PrixE85 { get; set; }
        public double PrixSP95 { get; set; }
        public double Reservoir { get; set; }
    }
}
