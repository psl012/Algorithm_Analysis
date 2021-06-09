using System;
using System.Collections.Generic;

namespace Week_11_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            WIS wis = new WIS();
            float[] test = new float[10] {280, 618, 762, 908, 409, 34, 59, 277, 246, 779}; 
            
            float[] hello = wis.Apply(test);
           // Console.WriteLine(hello);
            List<float> bye = wis.Reconstruction(hello, test);
            foreach(float f in bye)
            {
                Console.WriteLine(f);
            }

            Console.ReadKey();
        }
    }

    class FileReader
    {
        public void Scan(string dir)
        {
            
        }
    }

    class Huffman
    {
        public (string, Dictionary<string, Tree>) Apply(float[] sigma)
        {
            Dictionary<string, Tree> F = new Dictionary<string, Tree>();
            string rootKey = "";

            for(int i = 0; i < sigma.Length; i++)
            {
                F.Add(i.ToString(), new Tree(sigma[i]));
            }
            
            // Main Loop

            int counter = 25;
            Dictionary<string, Tree> newF = new Dictionary<string, Tree>();
            while(F.Count >= 2 && counter > 0)
            {
                counter--;
                ((Tree, string), (Tree, string)) minTrees = Get2MinNumber(F);
                Tree firstMinTree = minTrees.Item1.Item1;
                string firstMinIndex = minTrees.Item1.Item2;
                float firstMinTreeFreq = firstMinTree._frequency;

                Tree secondMinTree = minTrees.Item2.Item1;
                string secondMinIndex = minTrees.Item2.Item2;
                float secondMinTreeFreq = secondMinTree._frequency;
                
                firstMinTree = new Tree(firstMinTreeFreq);
                secondMinTree = new Tree(secondMinTreeFreq);

                
                float combinedFrequency = firstMinTreeFreq + secondMinTreeFreq;

                F.Remove(firstMinIndex);
                F.Remove(secondMinIndex);

                Tree T3 = new Tree(combinedFrequency);
                
                T3._leftChild = firstMinTree;
                T3._rightChild = secondMinTree;

                string combinedKey = firstMinIndex + "&" + secondMinIndex;
                rootKey = combinedKey;
                F.Add(combinedKey, T3);

                Console.WriteLine("===============");
                foreach(KeyValuePair<string, Tree> ss in F)
                {
                 //   Console.WriteLine(ss.Value._frequency);
                }

            }
            int minHuffLength = 0;
            int maxHuffLength = 0;

            int leftCounter = 100;     
            int rightCounter = 100;   
            Tree temp = F[rootKey];

            Console.WriteLine("LeftGuy");
            while(temp != null && leftCounter > 0)
            {
                leftCounter --;
                Console.WriteLine(temp._frequency);
                temp = temp._leftChild;
                minHuffLength++;
            }


            Console.WriteLine("RightGuy");
            temp = F[rootKey];
            while(temp != null && rightCounter > 0)
            {
                rightCounter --;
                Console.WriteLine(temp._frequency);
                temp = temp._rightChild;
                maxHuffLength++;
            }
          //  Console.WriteLine("Min: " + minHuffLength);
           // Console.WriteLine("Max: " + maxHuffLength);
            return (rootKey, F);
        }

        public ((Tree, string), (Tree, string)) Get2MinNumber(Dictionary<string,Tree> dictOfTrees)
        {
            float firstMin = 999999999999999;
            float secondMin = 999999999999999;

            string firstIndex = "";
            string secondIndex = "";

            Tree firstMinTree = null;
            Tree secondMinTree = null;

            foreach(KeyValuePair<string, Tree> entry in dictOfTrees)
            {
                if(entry.Value._frequency < firstMin)
                {
                    secondMinTree = firstMinTree;
                    secondMin = firstMin;
                    secondIndex = firstIndex;

                    firstMinTree = entry.Value;
                    firstMin = entry.Value._frequency;
                    firstIndex = entry.Key;
                }
                else
                {
                    if(entry.Value._frequency < secondMin)
                    {
                        secondMinTree = entry.Value;
                        secondMin = entry.Value._frequency;
                        secondIndex = entry.Key;
                    }
                }
            }

            return ((firstMinTree, firstIndex), (secondMinTree, secondIndex));
        }
    }

    class Tree
    {
        public float _frequency;
        public Tree _leftChild;
        public Tree _rightChild;

        public Tree(float root, Tree leftChild = null, Tree rightChild = null)
        {
            _frequency = root;
            _leftChild = leftChild;
            _rightChild = rightChild;
        }
    }
}
