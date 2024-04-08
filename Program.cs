using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Binary_Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string calculatorOperation = "";
            while (true)
            {
                calculatorOperation = SelectOperation();

                switch (calculatorOperation)
                {
                    case "1":
                        Console.Clear();
                        ConvertToBinary();
                        break;
                    case "2":
                        Console.Clear();
                        ConvertToDecimal();
                        break;
                    case "3":
                        Console.Clear();
                        Calculator.Calculate();
                        break;
                    case "4":
                        return;
                }
            }
        }

        private static void ConvertToBinary()
        {
            int decimalNum = 0;
            Console.WriteLine("Please enter the number you want to convert.");
            Console.WriteLine("Keep in mind: You should only enter whole and non-negative numbers.");
            while (!int.TryParse(Console.ReadLine(), out decimalNum) || decimalNum < 0)
                Console.WriteLine("Please enter a whole or non-negative number!");
            string binaryNum = Calculator.ConvertIntToBin(decimalNum);
            Console.WriteLine($"\nThe number {decimalNum} converts to {binaryNum}.");
            Console.Write("\nPress Enter to go back to the menu...");
            Console.Read();
            Console.Clear();
        }

        private static void ConvertToDecimal()
        {
            string input = "";
            string binaryNum = "";
            bool isBinary = false;
            Console.WriteLine("Please enter the binary number you want to convert.");
            Console.WriteLine("Keep in mind: You can leave out leading zeros and spaces (e.g. 0001 1101 can also be 11101)");
            while (!isBinary)
            {
                input = Console.ReadLine();
                binaryNum = Regex.Replace(input, @"^\s*0+", "");

                foreach (char character in binaryNum)
                {
                    if (character == '0' || character == '1')
                        isBinary = true;
                    else
                    {
                        Console.WriteLine("This is not a binary number!");
                        isBinary = false;
                        break;
                    }
                }
            }
            List<string> BinaryChunks = Calculator.FormatBinary(binaryNum);
            string formattedBinary = string.Join(" ", BinaryChunks);
            Console.Clear();
            Console.WriteLine($"Binary number: {formattedBinary}");
            int decimalNum = Calculator.ConvertBinToInt(binaryNum);
            Console.WriteLine($"Result in Decimal: {decimalNum}");

            Console.Write("\nPress Enter to go back to the menu...");
            Console.Read();
            Console.Clear();
        }

        private static string SelectOperation()
        {
            Console.WriteLine("Welcome to the Binary Calculator.");
            Console.WriteLine("Please select what you want to do:");
            Console.WriteLine("1: Decimal to Binary Conversion");
            Console.WriteLine("2: Binary to Decimal Conversion");
            Console.WriteLine("3: Calculations");
            Console.WriteLine("4: Quit");

            string selection = Console.ReadLine();

            while (selection != "1" && selection != "2" && selection != "3" && selection != "4")
            {
                Console.WriteLine("Please select one of the given options! (Enter a number)");
                selection = Console.ReadLine();
            }

            return selection;
        }
    }
}
