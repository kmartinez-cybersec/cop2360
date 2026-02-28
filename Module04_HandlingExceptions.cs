using System;

class DivisionProgram
{
    static void Main(string[] args)
    {

        Console.WriteLine("DivisionProgram: A program to divide integer x by integer y");

        Func <object, string> errorMsgGeneric = e =>
            $"ERROR INFO:\n-------------------------------------\n{e}\n-------------------------------------\n\nPlease try again";

        while (true)
        {
            try
            {
                Console.WriteLine("Enter the first number (dividend):");
                string strDividend = Console.ReadLine();

                Console.WriteLine("Enter the second number (divisor):");
                string strDivisor = Console.ReadLine();

                int intDividend = Convert.ToInt32(strDividend);
                int intDivisor = Convert.ToInt32(strDivisor);

                int intQuotient = Divide(intDividend, intDivisor);
                Console.WriteLine($"The quotient of {intDividend} divided by {intDivisor} is {intQuotient}");

                break;
            }
            catch (FormatException e)
            {
                Console.WriteLine("\nERROR: One or both inputs are not valid integers; please enter only number digits.");
                Console.WriteLine(errorMsgGeneric(e));
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("\nERROR: Dividing by zero is not allowed; please enter another number for the second input.");
                Console.WriteLine(errorMsgGeneric(e));
            }
            catch (Exception e)
            {
                Console.WriteLine("\nERROR: Unexpected error occurred.");
                Console.WriteLine(errorMsgGeneric(e));
            }
        }

    }
    static int Divide(int dividend, int divisor)
    {
        return dividend / divisor;
    }
}