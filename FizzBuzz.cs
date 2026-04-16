namespace ToK_2026.Fizzbuzz
{
    class FizzBuzz
    {
        private FizzBuzzInput? Input {get; set;}

        public FizzBuzz() { Input = null; }

        public FizzBuzz(string[] args)
        {
            ReadInput(args);
        }

        public bool HasValidInput()
        {
            return Input != null;
        }

        public void Run()
        {
            // validate we have input
            if (Input is null)
            {
                Console.WriteLine("Cannot run FizzBuzz: No valid input to use.");
                return;
            }

            List<string> output_strings = [];
            int current = 1;
            while (current <= Input.MaxNumber) 
            {
                string output = FilterNumber(current, Input.SmallerDenominator, Input.GreaterDenominator);
                output_strings.Add(output);
                current ++;
            }

            Console.WriteLine(string.Join('\n', output_strings));
        }

        public void ReadInput(string[] args)
        {
            try
            {
                Input = FizzBuzzInput.FromArgs(args);
            }
            catch (InputException ex)
            {
                Console.WriteLine($"Incorrect input for FizzBuzz: {ex.Message}");
                Input = null;
            }
        }

        private static string FilterNumber(int number, int small, int large)
        {
            if (number % (small * large) == 0)
            {
                return "FizzBuzz";
            } 
            else if (number % small == 0)
            {
                return "Fizz";
            }
            else if (number % large == 0)
            {
                return "Buzz";
            }
            else
            {
                return number.ToString();
            }
        }
    }

    
}