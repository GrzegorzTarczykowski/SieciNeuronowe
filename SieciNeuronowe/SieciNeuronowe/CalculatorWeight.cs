using System;
using System.Collections.Generic;
using System.Text;

namespace SieciNeuronowe
{
    abstract class CalculatorWeight
    {
        protected double learnigRate = 0.5;
        protected double GetTotalErrorDivOutputOutput(double outputTarget, double outputOutput)
        {
            double result = 0;
            result = (outputOutput - outputTarget);
            return result;
        }

        protected double GetOutputOutputDivNetInputOutput(double outputOutput)
        {
            double result = 0;
            result = outputOutput * (1 - outputOutput);
            return result;
        }

        protected double GetNetInputOutputDivWeight(double outputHidden)
        {
            double result = 0;
            result = outputHidden;
            return result;
        }

        protected double GetTotalErrorDivWeight(double totalErrorDivOutputOutput, double outputOutputDivNetInputOutput, double netInputOutputDivWeight)
        {
            double result = 0;
            result = totalErrorDivOutputOutput * outputOutputDivNetInputOutput * netInputOutputDivWeight;
            return result;
        }

        protected double DecreaseTheError(double weight, double learnigRate, double totalErrorDivWeight)
        {
            double result = 0;
            result = weight - learnigRate * totalErrorDivWeight;
            return result;
        }
    }
}
