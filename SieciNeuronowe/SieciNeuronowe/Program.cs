using System;
using System.Collections.Generic;

namespace SieciNeuronowe
{
    class Program
    {
        static void Main(string[] args)
        {
            double input1 = 0;
            double input2 = 1;
            double input3 = 0;
            double input4 = 0;
            double input5 = 0;
            double input6 = 0;
            double output1target = 0.01;
            double output2target = 0.99;
            double output3target = 0.01;
            double output4target = 0.01;
            double output5target = 0.01;
            double output6target = 0.01;
            double output7target = 0.01;
            double output8target = 0.01;
            double output9target = 0.01;
            double output10target = 0.01;

            List<double> listOfInput = new List<double>();
            listOfInput.Add(input1);
            listOfInput.Add(input2);
            listOfInput.Add(input3);
            listOfInput.Add(input4);
            listOfInput.Add(input5);
            listOfInput.Add(input6);

            List<double> listOfOutputTarget = new List<double>();
            listOfOutputTarget.Add(output1target);
            listOfOutputTarget.Add(output2target);
            listOfOutputTarget.Add(output3target);
            listOfOutputTarget.Add(output4target);
            listOfOutputTarget.Add(output5target);
            listOfOutputTarget.Add(output6target);
            listOfOutputTarget.Add(output7target);
            listOfOutputTarget.Add(output8target);
            listOfOutputTarget.Add(output9target);
            listOfOutputTarget.Add(output10target);

            List<double> listOfInput2 = new List<double>();
            listOfInput2.Add(input1);
            listOfInput2.Add(input6);
            listOfInput2.Add(input3);
            listOfInput2.Add(input4);
            listOfInput2.Add(input5);
            listOfInput2.Add(input2);

            List<double> listOfOutputTarget2 = new List<double>();
            listOfOutputTarget2.Add(output1target);
            listOfOutputTarget2.Add(output10target);
            listOfOutputTarget2.Add(output3target);
            listOfOutputTarget2.Add(output4target);
            listOfOutputTarget2.Add(output5target);
            listOfOutputTarget2.Add(output6target);
            listOfOutputTarget2.Add(output7target);
            listOfOutputTarget2.Add(output8target);
            listOfOutputTarget2.Add(output9target);
            listOfOutputTarget2.Add(output2target);

            

            int hiddenNeuronCount = listOfInput.Count;
            int outputNeuronCount = listOfOutputTarget.Count;
            NeuralNetwork neuralNetwork = new NeuralNetwork(listOfInput.Count, hiddenNeuronCount, outputNeuronCount);
            var listBeforeTeach = neuralNetwork.GetListOfOutputValues(listOfInput);
            var list2BeforeTeach = neuralNetwork.GetListOfOutputValues(listOfInput2);
            for (int i = 0; i < 10000; i++)
            {
                neuralNetwork.TeachNeuralNetwork(listOfInput, listOfOutputTarget);
                neuralNetwork.TeachNeuralNetwork(listOfInput2, listOfOutputTarget2);
            }
            var listAfterTeach = neuralNetwork.GetListOfOutputValues(listOfInput);
            var listAfterTeach2 = neuralNetwork.GetListOfOutputValues(listOfInput2);
            Console.ReadKey();
        }
    }
}
