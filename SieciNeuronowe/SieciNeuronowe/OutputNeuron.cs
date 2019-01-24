using System;
using System.Collections.Generic;
using System.Text;

namespace SieciNeuronowe
{
    class OutputNeuron : Neuron
    {
        public double NetInput { get; set; }
        public double Output { get; set; }
        public double Error { get; set; }
        public OutputNeuron(List<Net> listOfNetBetweenInputAndOutputNeuron)
        {
            NetInput = CalculateNetInput(listOfNetBetweenInputAndOutputNeuron, 0.65);
            Output = GetOutputByLogisticFunction(NetInput);
        }

        public void CalculatingErrorForOutputNeuron(double target)
        {
            Error = (Math.Pow((target - Output), 2) / 2);
        }
    }
}
