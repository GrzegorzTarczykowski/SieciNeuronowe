using System;
using System.Collections.Generic;
using System.Text;

namespace SieciNeuronowe
{
    class NeuralNetwork
    {
        List<double> listOfWeightHiddenLayer;
        List<double> listOfWeightOutputLayer;
        int inputCount;
        int hiddenNeuronCount;
        int outputNeuronCount;
        List<double> listOfInput;
        List<double> listOfOutputTarget;
        List<HiddenNeuron> listOfHiddenNeuron;
        List<OutputNeuron> listOfOutputNeuron;
        double totalError;
        List<CalculatorWeightOfOutputLayer> listOfCalculatorWeightOfOutputLayer; 
        List<CalculatorWeightOfInputLayer> listOfCalculatorWeightOfInputLayer;
        public NeuralNetwork(int inputCount, int hiddenNeuronCount, int outputNeuronCount)
        {
            this.inputCount = inputCount;
            this.hiddenNeuronCount = hiddenNeuronCount;
            this.outputNeuronCount = outputNeuronCount;
            List<double> listOfWeight = new List<double>() { 0.05, 0.1, 0.15, 0.2, 0.25, 0.3, 0.35,
                                                             0.4, 0.45, 0.50, 0.55, 0.60, 0.65, 0.70,
                                                             0.75, 0.8, 0.85, 0.9, 0.95};
            Random random = new Random();
            listOfWeightHiddenLayer = new List<double>();
            for (int i = 0; i < (inputCount * hiddenNeuronCount); i++)
            {
                listOfWeightHiddenLayer.Add(listOfWeight[random.Next(0, listOfWeight.Count - 1)]);
            }

            listOfWeightOutputLayer = new List<double>();
            for (int i = 0; i < (hiddenNeuronCount * outputNeuronCount); i++)
            {
                listOfWeightOutputLayer.Add(listOfWeight[random.Next(0, listOfWeight.Count - 1)]);
            }
        }

        public List<double> GetListOfOutputValues(List<double> listOfInput)
        {
            this.listOfInput = listOfInput;
            TheForwardPass();
            List<double> listOfOutputValues = new List<double>();
            foreach(var item in listOfOutputNeuron)
            {
                listOfOutputValues.Add(item.Output);
            }
            return listOfOutputValues;
        }

        public void TeachNeuralNetwork(List<double> listOfInput, List<double> listOfOutputTarget)
        {
            this.listOfInput = listOfInput;
            this.listOfOutputTarget = listOfOutputTarget;
            TheForwardPass();
            CalculatingTheTotalError();
            TheBackwardsPass();
            ChangeWeight();
        }

        private void TheForwardPass()
        {
            List<Net> listOfNetBetweenInputAndHiddenNeuron
                = new List<Net>();

            listOfHiddenNeuron = new List<HiddenNeuron>();
            int k = 0;
            for (int i = 0; i < hiddenNeuronCount; i++)
            {
                for (int j = 0; j < inputCount; j++)
                {
                    listOfNetBetweenInputAndHiddenNeuron
                        .Add(new Net() { Input = listOfInput[j], Weight = listOfWeightHiddenLayer[k] });
                    k++;
                }
                listOfHiddenNeuron.Add(new HiddenNeuron(listOfNetBetweenInputAndHiddenNeuron));
                listOfNetBetweenInputAndHiddenNeuron.Clear();
            }

            List<Net> listOfNetBetweenInputAndOutputNeuron
                = new List<Net>();

            listOfOutputNeuron = new List<OutputNeuron>();
            k = 0;
            for (int i = 0; i < outputNeuronCount; i++)
            {
                for (int j = 0; j < listOfHiddenNeuron.Count; j++)
                {
                    listOfNetBetweenInputAndOutputNeuron
                        .Add(new Net() { Input = listOfHiddenNeuron[j].Output, Weight = listOfWeightOutputLayer[k] });
                    k++;
                }
                listOfOutputNeuron.Add(new OutputNeuron(listOfNetBetweenInputAndOutputNeuron));
                listOfNetBetweenInputAndOutputNeuron.Clear();
            }
        }

        private void CalculatingTheTotalError()
        {
            int k = 0;
            List<double> listOfErrorOutput = new List<double>();
            foreach (OutputNeuron item in listOfOutputNeuron)
            {
                item.CalculatingErrorForOutputNeuron(listOfOutputTarget[k]);
                listOfErrorOutput.Add(listOfOutputNeuron[k].Error);
                k++;
            }

            totalError = GetTotalError(listOfErrorOutput);
        }

        private double GetTotalError(List<double> listOfErrorOutput)
        {
            double result = 0;
            foreach (double errorOutput in listOfErrorOutput)
            {
                result += errorOutput;
            }
            return result;
        }

        private void TheBackwardsPass()
        {
            //Output Layer

            listOfCalculatorWeightOfOutputLayer = new List<CalculatorWeightOfOutputLayer>();
            int k = 0;
            for (int i = 0; i < listOfOutputNeuron.Count; i++)
            {
                for (int j = 0; j < listOfHiddenNeuron.Count; j++)
                {
                    listOfCalculatorWeightOfOutputLayer.Add(new CalculatorWeightOfOutputLayer(listOfOutputNeuron[i].Output
                        , listOfHiddenNeuron[j].Output, listOfOutputTarget[i], listOfWeightOutputLayer[k]));
                    k++;
                }
            }
            foreach (CalculatorWeightOfOutputLayer item in listOfCalculatorWeightOfOutputLayer)
            {
                item.CalculatingNewWeight();
            }

            //Hidden Layer

            listOfCalculatorWeightOfInputLayer = new List<CalculatorWeightOfInputLayer>();
            k = 0;
            for (int i = 0; i < listOfHiddenNeuron.Count; i++)
            {
                for (int j = 0; j < listOfInput.Count; j++)
                {
                    CalculatorWeightOfInputLayer calculatorWeightOfInputLayer = new CalculatorWeightOfInputLayer(listOfHiddenNeuron[i].Output
                        , listOfInput[j], listOfWeightHiddenLayer[k]);
                    int g = 0;
                    for (int h = 0; h < listOfOutputNeuron.Count; h++)
                    {
                        calculatorWeightOfInputLayer.AddToListOfTotalErrorDivOutputHidden(
                            listOfCalculatorWeightOfOutputLayer[i + g].TotalErrorDivOutputNeuronOutput
                          , listOfCalculatorWeightOfOutputLayer[i + g].OutputNeuronOutputDivOutputNeuronNetInput
                          , listOfWeightOutputLayer[i + g]);
                        g += listOfHiddenNeuron.Count;
                    }
                    listOfCalculatorWeightOfInputLayer.Add(calculatorWeightOfInputLayer);
                    k++;
                }
            }
            foreach (CalculatorWeightOfInputLayer item in listOfCalculatorWeightOfInputLayer)
            {
                item.CalculatingNewWeight();
            }
        }

        private void ChangeWeight()
        {
            int f = 0;
            foreach (var item in listOfCalculatorWeightOfInputLayer)
            {
                listOfWeightHiddenLayer[f] = item.NewWeight;
                f++;
            }
            f = 0;
            foreach (var item in listOfCalculatorWeightOfOutputLayer)
            {
                listOfWeightOutputLayer[f] = item.NewWeight;
                f++;
            }
        }
    }
}
