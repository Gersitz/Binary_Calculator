using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Calculator
{
    public class Calculator
    {
        public static void Calculate()
        {
            char[] validOperators = { '+', '-', '*', '/' };
            double num1;
            double num2;
            char calcOperator;
            double result = 0;
            int intResult = 0;

            Console.WriteLine("If you want to calculate binary numbers, write 'bin' in front of the number.");
            Console.WriteLine("Example: 'bin100101'");

            do
            {
                Console.Write("Please enter the first number: ");
                string input = Console.ReadLine();
                num1 = CheckForValidInput(input);
            } while (double.IsNaN(num1));

            do
            {
                Console.Write("Please enter the operator (+, -, *, /): ");
                calcOperator = Console.ReadKey().KeyChar;
                if (!Array.Exists(validOperators, op => op == calcOperator))
                {
                    Console.WriteLine("\nPlease enter a valid operator!");
                }

            } while (!Array.Exists(validOperators, op => op == calcOperator));

            do
            {
                Console.Write("\nPlease enter the second number: ");
                string input = Console.ReadLine();
                num2 = CheckForValidInput(input);
            } while (double.IsNaN(num2));

            switch (calcOperator)
            {
                case '+':
                    result = Calculator.Add(num1, num2);
                    intResult = (int)result;
                    break;
                case '-':
                    result = Calculator.Subtract(num1, num2);
                    intResult = (int)result;
                    break;
                case '*':
                    result = Calculator.Multiply(num1, num2);
                    intResult = (int)result;
                    break;
                case '/':
                    result = Calculator.Divide(num1, num2);
                    intResult = (int)result;
                    break;
                default:
                    Console.WriteLine("Invalid operator!");
                    break;
            }
            Console.Clear();
            Console.WriteLine($"Decimal result: {result}");
            Console.WriteLine($"Binary result: {ConvertIntToBin(intResult)}");

            Console.Write("\nPress Enter to go back to the menu...");
            Console.Read();
            Console.Clear();
        }

        private static double CheckForValidInput(string num)
        {
            bool isBinary;
            string binaryString = num.StartsWith("bin") ? num.Substring(3) : num;

            if (num.StartsWith("bin"))
            {
                foreach (char c in binaryString)
                {
                    if (c != '0' && c != '1')
                    {
                        Console.WriteLine("This is not a valid binary number!");
                        return double.NaN;
                    }
                }
                return Calculator.ConvertBinToInt(binaryString);
            }
            else
            {
                double result;
                if (!double.TryParse(num, out result))
                {
                    Console.WriteLine("Invalid number!");
                    return double.NaN;
                }
                return result;
            }
        }

        public static string ConvertIntToBin(int num)
        {
            string binaryNumber;
            if (num < 0)
            {
                Console.WriteLine("Please only enter positive numbers.");
                return "-1";
            }
            else
            {
                binaryNumber = Convert.ToString(num, 2);
                List<string> binChunks = FormatBinary(binaryNumber);
                return string.Join(" ", binChunks).Trim();
            }
        }

        public static List<string> FormatBinary(string binaryNumber)
        {
            while (binaryNumber.Length % 4 != 0)
            {
                binaryNumber = binaryNumber.Insert(0, "0");
            }

            List<string> binChunks = new List<string>();

            for (int i = binaryNumber.Length; i >= 0; i -= 4)
            {
                string chunk = binaryNumber.Substring(i, Math.Min(4, binaryNumber.Length - i));
                binChunks.Add(chunk);
            }

            binChunks.Reverse();
            return binChunks;
        }

        public static int ConvertBinToInt(string bin)
        {
            return Convert.ToInt32(bin, 2);
        }

        public static double Add(double num1, double num2)
        {
            return num1 + num2;
        }

        public static double Subtract(double num1, double num2)
        {
            return num1 - num2;
        }

        public static double Multiply(double num1, double num2)
        {
            return num1 * num2;
        }

        public static double Divide(double num1, double num2)
        {
            if (!(num2 == 0))
                return num1 / num2;
            else
                Console.WriteLine("You can't divide by 0!");
            return -1;
        }

        public static string AddBin(string bin1, string bin2)
        {
            int num1 = ConvertBinToInt(bin1);
            int num2 = ConvertBinToInt(bin2);
            int result = Convert.ToInt32(Add(num1, num2));
            return Convert.ToString(result, 2);
        }

        public static string SubtractBin(string bin1, string bin2)
        {
            int num1 = ConvertBinToInt(bin1);
            int num2 = ConvertBinToInt(bin2);
            if (num2 > num1)
            {
                Console.WriteLine("You can't create negative binary numbers!");
                return "-1";
            }
            else
            {
                int result = Convert.ToInt32(Subtract(num1, num2));
                return Convert.ToString(result, 2);
            }
        }

        public static string MultiplyBin(string bin1, string bin2)
        {
            int num1 = ConvertBinToInt(bin1);
            int num2 = ConvertBinToInt(bin2);
            int result = Convert.ToInt32(Multiply(num1, num2));
            return Convert.ToString(result, 2);
        }

        public static string DivideBin(string bin1, string bin2)
        {
            int num1 = ConvertBinToInt(bin1);
            int num2 = ConvertBinToInt(bin2);
            int result = Convert.ToInt32(Divide(num1, num2));

            if (!(num2 == 0))
            {
                if (num1 % num2 == 0)
                    return Convert.ToString(result, 2);
                else
                    Console.WriteLine("The result would be a floating point number. Only integers can be displayed as a binary number.");
                return "-1";
            }
            else
            {
                Console.WriteLine("The second binary number resembles 0. You can't divide by 0!");
                return "-1";
            }
        }
    }
}
