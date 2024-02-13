using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_step_midterm.Calculator
{
    public class Calculator
    {
        public static void RunCalculator()
        {    //supported calculator functionallity
            List<char> mathOperations = new() { '+', '-', '*', '/' };
            double? argA = null;
            double? argB = null;
            char? operation = null;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the first argument: ");
                    argA = TryParseDouble(Console.ReadLine()!);
                    Console.WriteLine("Enter the Second argument: ");
                    argB = TryParseDouble(Console.ReadLine()!);
                    Console.WriteLine($"Enter one from this math operations: {string.Join(", ", mathOperations)}");
                    operation = Console.ReadKey().KeyChar;
                    //checks if we support functinality for this symbol
                    if (mathOperations.Contains((char)operation))
                    {    //checks devide by zero 
                        if (argB == 0 && operation == '/')
                        {
                            throw new Exception("Can't devide by zero");
                        }
                        else
                        {   //sends all arguments to MakeOperation
                            Console.WriteLine($"{argA} {operation} {argB} = {MakeOperation((double)argA, (double)argB, (char)operation)}");
                            argA = null;
                            argB = null;
                            operation = null;
                            Console.Write("if you want to stop calculator press tab key\nelse tap eny key for starting new opration on calculator");
                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                            Console.Write("\n\n");
                            if (keyInfo.Key == ConsoleKey.Tab)
                            {
                                break;
                            }

                        }
                    }
                    else
                    {
                        throw new Exception("Not a valid operation symbol");
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }
        //helper function for parsing strings to doubles
        private static double TryParseDouble(string data)
        {
            bool result = double.TryParse(data, out double value);
            if (result)
            {
                return value;
            }
            else
            {
                throw new Exception("Not a valid type value");
            }
        }
        //calculator functionality
        private static double MakeOperation(double argA, double argB, char operation)
        {
            var result = operation switch
            {
                '+' => argA + argB,
                '-' => argA - argB,
                '*' => argA * argB,
                _ => argA / argB,
            };
            return result;
        }
    }
}
