using System;
using System.Collections.Generic;
using System.Text;

namespace SieciNeuronowe
{
    class CalculatorWeightOfInputLayer : CalculatorWeight
    {
        private List<double> listOfTotalErrorDivHiddenNeuronOutput;
        public double HiddenNeuronOutput { get; set; }
        public double Input { get; set; }
        public double CurrentWeight { get; set; }
        public double NewWeight { get; set; }
        public CalculatorWeightOfInputLayer(double hiddenNeuronOutput, double input, double currentWeight)
        {
            listOfTotalErrorDivHiddenNeuronOutput = new List<double>();
            HiddenNeuronOutput = hiddenNeuronOutput;
            Input = input;
            CurrentWeight = currentWeight;
        }

        public void CalculatingNewWeight()
        {
            double TotalErrorDivHiddenNeuronOutput = GetTotalErrorDivOutputHiddenConsiderAllOutputNeurons(listOfTotalErrorDivHiddenNeuronOutput);

            double HiddenNeuronOutputDivHiddenNeuronNetInput = GetOutputOutputDivNetInputOutput(HiddenNeuronOutput);

            double HiddenNeuronNetInputDivWeight = GetNetInputOutputDivWeight(Input);

            double TotalErrorDivWeight = GetTotalErrorDivWeight(TotalErrorDivHiddenNeuronOutput, HiddenNeuronOutputDivHiddenNeuronNetInput, HiddenNeuronNetInputDivWeight);

            NewWeight = DecreaseTheError(CurrentWeight, learnigRate, TotalErrorDivWeight);
        }

        public void AddToListOfTotalErrorDivOutputHidden(double totalErrorDivOutputNeuronOutput
            , double outputNeuronOutputDivOutputNeuronNetInput, double weight)
        {
            listOfTotalErrorDivHiddenNeuronOutput
                .Add(GetTotalErrorDivHiddenNeuronOutput(totalErrorDivOutputNeuronOutput
                , outputNeuronOutputDivOutputNeuronNetInput, weight));
        }

        private double GetTotalErrorDivHiddenNeuronOutput(double totalErrorDivOutputNeuronOutput
            , double outputNeuronOutputDivOutputNeuronNetInput, double weight)
        {
            double result = 0;
            result = totalErrorDivOutputNeuronOutput * outputNeuronOutputDivOutputNeuronNetInput * weight;
            return result;
        }

        private double GetTotalErrorDivOutputHiddenConsiderAllOutputNeurons(List<double> listOfTotalErrorDivOutputHidden)
        {
            double result = 0;
            foreach (double item in listOfTotalErrorDivOutputHidden)
            {
                result += item;
            }
            return result;
        }
    }
}
