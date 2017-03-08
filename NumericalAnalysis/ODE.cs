using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis
{
    public delegate double Equation(double x, params double[] args);

    public class ODE
    {
        public Equation Equation { get; set; }
        public double x0 { get; set; }
        public double y0 { get; set; }
        public Equation[] Deriatives { get; set; }

        public ODE(Equation equation, double x0, double y0)
        {
            this.Equation = equation;
            this.x0 = x0;
            this.y0 = y0;
        }

        public ODE(Equation equation, double x0, double y0, params Equation[] deriatives)
        {
            this.Equation = equation;
            this.Deriatives = deriatives;
            this.x0 = x0;
            this.y0 = y0;
        }
    }
}
