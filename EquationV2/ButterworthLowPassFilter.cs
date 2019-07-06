using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationV2
{
    class ButterworthLowPassFilter
    {
        //filter fc = 2hz, fs = 10hz

        private const int LowPassOrder = 4;

        private double[] inputValueModifier;
        private double[] outputValueModifier;
        private double[] inputValue;
        private double[] outputValue;
        private int valuePosition;

        public ButterworthLowPassFilter()
        {
            inputValueModifier = new double[LowPassOrder];
            inputValueModifier[0] = 0.098531160923927;
            inputValueModifier[1] = 0.295593482771781;
            inputValueModifier[2] = 0.295593482771781;
            inputValueModifier[3] = 0.098531160923927;

            outputValueModifier = new double[LowPassOrder];
            outputValueModifier[0] = 1.0;
            outputValueModifier[1] = -0.577240524806303;
            outputValueModifier[2] = 0.421787048689562;
            outputValueModifier[3] = -0.0562972364918427;
        }

        public double Filter(double inputValue)
        {
            if (this.inputValue == null && this.outputValue == null)
            {
                this.inputValue = new double[LowPassOrder];
                this.outputValue = new double[LowPassOrder];

                valuePosition = -1;

                for (int i = 0; i < LowPassOrder; i++)
                {
                    this.inputValue[i] = inputValue;
                    this.outputValue[i] = inputValue;
                }

                return inputValue;
            }
            else if (this.inputValue != null && this.outputValue != null)
            {
                valuePosition = IncrementLowOrderPosition(valuePosition);

                this.inputValue[valuePosition] = inputValue;
                this.outputValue[valuePosition] = 0;

                int j = valuePosition;

                for (int i = 0; i < LowPassOrder; i++)
                {
                    this.outputValue[valuePosition] += inputValueModifier[i] * this.inputValue[j] -
                        outputValueModifier[i] * this.outputValue[j];

                    j = DecrementLowOrderPosition(j);
                }

                return this.outputValue[valuePosition];
            }
            else
            {
                throw new Exception("Both inputValue and outputValue should either be null or not null.  This should never be thrown.");
            }
        }

        private int DecrementLowOrderPosition(int j)
        {
            if (--j < 0)
            {
                j += LowPassOrder;
            }
            return j;
        }

        private int IncrementLowOrderPosition(int position)
        {
            return ((position + 1) % LowPassOrder);
        }

    }
}
