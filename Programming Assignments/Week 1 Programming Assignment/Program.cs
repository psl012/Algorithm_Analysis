using System;
using System.Numerics;
using System.IO;

namespace C_Sharp_Programs
{
    class Program
    {
        static void Main(string[] args)
        {        
            Multiplication _multiplication = new Multiplication();
            BigInteger test = _multiplication.Multiply("3141592653589793238462643383279502884197169399375105820974944592", "2718281828459045235360287471352662497757247093699959574966967627");
            Console.WriteLine(test);

            using (StreamWriter sw = new StreamWriter("answer.txt"))
            {
                sw.WriteLine(test);
            }

            Console.ReadKey();

        }

        class Multiplication
        {
            public BigInteger Multiply(string numString1, string numString2)
            {
                if (numString1.Length % 2 != 0)
                {
                    numString1 = numString1.Insert(0,"0");
                }
                if (numString2.Length % 2 != 0)
                {
                    numString2 = numString2.Insert(0,"0");
                }

                if (numString1.Length < numString2.Length)
                {
                    int diff = numString2.Length - numString1.Length;
                    for (int i = 0; i < diff; i++)
                    {
                        numString1 = numString1.Insert(0, "0");
                    }
                }
                else if (numString2.Length < numString1.Length)
                {
                    int diff = numString1.Length - numString2.Length;
                    for (int i = 0; i < diff; i++)
                    {
                        numString2 = numString2.Insert(0, "0");
                    }

                }

                Console.WriteLine("num1: " + numString1);
                Console.WriteLine("num2: " + numString2);

                BigInteger num1 = BigInteger.Parse(numString1);
                BigInteger num2 = BigInteger.Parse(numString2);
                
                int midPoint1 = numString1.Length/2;
                int midPoint2 = numString2.Length/2;

                string numA = numString1.Substring(0, midPoint1);
                string numB = numString1.Substring(midPoint1);
                
                string numC = numString2.Substring(0, midPoint2);
                string numD = numString2.Substring(midPoint2);

                BigInteger A = BigInteger.Parse(numA);
                BigInteger B = BigInteger.Parse(numB);
                BigInteger C = BigInteger.Parse(numC);
                BigInteger D = BigInteger.Parse(numD);

                Console.WriteLine("The A, B, C, D for");
                Console.WriteLine(numString1);
                Console.WriteLine(numString2);
                Console.WriteLine("string A: " + A);
                Console.WriteLine("string B: " + B);
                Console.WriteLine("string C: " + C);
                Console.WriteLine("string D: " + D);
                Console.WriteLine("End for this set");
                Console.WriteLine("");

                BigInteger AC;
                BigInteger BD;
                BigInteger ABCD;
                BigInteger ADBC;

                if(numA.Length <= 1)
                {
                    AC = A*C;
                    BD = B*D;
                    ABCD = (A+B) * (C+D);
                    ADBC = ABCD - AC - BD;
                }
                else 
                {
                    AC = Multiply(numA, numC);
                    BD = Multiply(numB, numD);

                    string num_A_plus_B = (BigInteger.Parse(numA) + BigInteger.Parse(numB)).ToString();
                    string num_C_plus_D = (BigInteger.Parse(numC) + BigInteger.Parse(numD)).ToString();

                    Console.WriteLine("A+B =" + num_A_plus_B);
                    Console.WriteLine("C+D =" + num_C_plus_D);

                    ABCD = Multiply(num_A_plus_B, num_C_plus_D);
                    ADBC = ABCD - AC - BD;
                }

                int n = numString1.Length;
                int n_half = numString1.Length/2;

                BigInteger ten_n = BigInteger.Pow(10,n);
                BigInteger ten_n_half = BigInteger.Pow(10, n_half);

                return ten_n * AC + ten_n_half * (ADBC) + BD;
            }
        }

    }

}
