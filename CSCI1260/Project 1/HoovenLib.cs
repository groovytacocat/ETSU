namespace MyDLL
{

    public class HoovenLib
    {
        //Generic Parse method implemented by IParsable<T>
        public static T Parse<T>(string s, IFormatProvider provider) where T : IParsable<T>
        {
            return T.Parse(s, provider);
        }

        ///<summary>
        /// Generic Validate Method that takes a prompt, an error message, and a generic List of values to compare the input read from console
        /// If the user input throws an exception when parsing errormessage is displayed and the validate method gets a recursive call
        /// Concept in mind for this method is for when desired input must match pre-determined values
        ///     E.g. Y/N questions, Multiple Choice questions, Number values where they must match a set of values known prior to running program
        ///</summary>
        public static T Validate<T>(string prompt, string errorMessage, List<T> accepted) where T : IParsable<T>
        {
            T input;

            Console.Write(prompt);

            try
            {
                input = Parse<T>(Console.ReadLine(), null);
            }
            catch
            {
                Console.Write(errorMessage);
                input = Validate(prompt, errorMessage, accepted);
            }

            if (!(accepted.Contains(input)))
            {
                Console.Write(errorMessage);
                input = Validate(prompt, errorMessage, accepted);
            }

            return input;
        }

        ///<summary>
        /// Generic Validate Method that takes a prompt, an error message
        /// If the user input throws an exception when parsing errormessage is displayed and the validate method gets a recursive call
        /// Similar to the above however this works for when you have numeric inputs that need to be within a range
        ///     E.g. A < input < B / input > 0 / or where a list of accepted values may be so large that it would not be sensible to declare/initialize
        ///</summary>
        public static T Validate<T>(string prompt, string errorMessage) where T : IParsable<T>
        {
            Console.Write(prompt);

            try
            {
                return Parse<T>(Console.ReadLine(), null);
            }
            catch
            {
                Console.Write(errorMessage);
                return Validate<T>(prompt, errorMessage);
            }
        }

        /// <summary>
        /// Generic Validate method as above but intended for use with numeric data types
        /// Takes a min and max argument and compares user input to these values.
        /// Displays error message if input is not within the upper/lower bounds
        /// </summary>
        /// <typeparam Name="T"></typeparam>
        /// <param Name="prompt">String to prompt user for input</param>
        /// <param Name="errorMessage">String displayed when parse failed or input not within bounds</param>
        /// <param Name="min">Minimum acceptable value</param>
        /// <param Name="max">Maximum acceptable value</param>
        /// <returns>A number of type T that is within the provided boundary</returns>
        public static T Validate<T>(string prompt, string errorMessage, T min, T max) where T : IParsable<T>, IComparable<T>
        {
            T input;

            do
            {
                Console.Write(prompt);

                try
                {
                    input = Parse<T>(Console.ReadLine(), null);
                }
                catch
                {
                    Console.Write(errorMessage);
                    input = Validate<T>(prompt, errorMessage, min, max);
                }

                if (input.CompareTo(min) < 0 || input.CompareTo(max) > 0)
                {
                    Console.Write(errorMessage);
                }

            } while (input.CompareTo(min) < 0 || input.CompareTo(max) > 0);

            return input;
        }

        ///<summary>
        /// Method that returns a boolean to be used when prompting a user if they would like to repeat a program/method/action
        /// Only accepts y, Y, n, or N as values
        /// Returns true for user input of y/Y 
        ///</summary>
        public static bool Repeat(string prompt, string errorMessage)
        {
            char repeat;

            do
            {
                repeat = char.ToUpper(Validate<char>(prompt, errorMessage));

                if (!(repeat == 'Y' || repeat == 'N'))
                {
                    Console.Write(errorMessage);
                }
            } while (!(repeat == 'Y' || repeat == 'N'));

            return repeat == 'Y' ? true : false;
        }

        /// <summary>
        /// Generic that swaps the valuables of 2 variables
        /// </summary>
        /// <typeparam Name="T">Type of values being swapped</typeparam>
        /// <param Name="x">Value 1 to be swapped</param>
        /// <param Name="y">Value 2 to be swapped</param>
        public static void Swap<T> (ref T x, ref T y)
        {
            T swap = y;

            y = x;

            x = swap;
        }
    }
}