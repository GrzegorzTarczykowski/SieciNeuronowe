using System;
using System.Collections.Generic;
using System.Text;

namespace SieciNeuronowe
{
    class CalculatorWeightOfOutputLayer : CalculatorWeight
    {
        public double OutputNeuronOutput { get; set; }
        public double HiddenNeuronOutput { get; set; }
        public double OutputTarget { get; set; }
        public double CurrentWeight { get; set; }
        public double NewWeight { get; set; }
        public double TotalErrorDivOutputNeuronOutput { get; set; }
        public double OutputNeuronOutputDivOutputNeuronNetInput { get; set; }
        public CalculatorWeightOfOutputLayer(double outputNeuronOutput, double hiddenNeuronOutput, double outputTarget,
            double currentWeight)
        {
            OutputNeuronOutput = outputNeuronOutput;
            HiddenNeuronOutput = hiddenNeuronOutput;
            OutputTarget = outputTarget;
            CurrentWeight = currentWeight;
        }
        public void CalculatingNewWeight()
        {
            TotalErrorDivOutputNeuronOutput = GetTotalErrorDivOutputOutput(OutputTarget, OutputNeuronOutput);

            OutputNeuronOutputDivOutputNeuronNetInput = GetOutputOutputDivNetInputOutput(OutputNeuronOutput);

            double OutputNeuronNetInputDivWeight = GetNetInputOutputDivWeight(HiddenNeuronOutput);

            double TotalErrorDivWeight = GetTotalErrorDivWeight(TotalErrorDivOutputNeuronOutput
                , OutputNeuronOutputDivOutputNeuronNetInput, OutputNeuronNetInputDivWeight);

            NewWeight = DecreaseTheError(CurrentWeight, learnigRate, TotalErrorDivWeight);
        }
    }
}
