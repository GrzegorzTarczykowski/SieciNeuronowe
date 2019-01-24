using System;
using System.Collections.Generic;
using System.Text;

namespace SieciNeuronowe
{
    abstract class Neuron
    {
        protected double CalculateNetInput(List<Net> listOfNet, double bias)
        {
            double result = 0;
            foreach (var item in listOfNet)
            {
                result += (item.Input * item.Weight);
            }
            result += (bias * 1);
            return result;
        }

        protected double GetOutputByLogisticFunction(double netInput)
        {
            double result = 0;
            result = (1 / (1 + Math.Pow(Math.E, -netInput)));
            return result;
        }
    }
}
