using System;
using System.Collections.Generic;
using System.Text;

namespace SieciNeuronowe
{
    class HiddenNeuron : Neuron
    {
        public double NetInput { get; set; }
        public double Output { get; set; }
        public HiddenNeuron(List<Net> listOfNetBetweenInputAndHiddenNeuron)
        {
            NetInput = CalculateNetInput(listOfNetBetweenInputAndHiddenNeuron, 0.35);
            Output = GetOutputByLogisticFunction(NetInput);
        }
    }
}
