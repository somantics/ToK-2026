using System.Linq;
namespace ToK_2026
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

    class FizzBuzzInput
    {
        public int MaxNumber {get; private set;}
        public int SmallerDenominator {get; private set;}
        public int GreaterDenominator {get; private set;}

        public FizzBuzzInput(int x, int y, int n)
        {
            SmallerDenominator = x;
            GreaterDenominator = y;
            MaxNumber = n;
        }

        public static FizzBuzzInput FromArgs(string[] args)
        {
            if (args.Length < 4)
            {
                //freak out
            }

            // Must skip progam name
            string[] inputs = [.. args.Skip(1).Take(3)];
            
            if (!int.TryParse(inputs[0], out int x))
            {
                throw new InputException($"Cannot parse {inputs[0]} as integer");
            }

            if (!int.TryParse(inputs[1], out int y))
            {
                throw new InputException($"Cannot parse {inputs[1]} as integer");
                
            }

            if (!int.TryParse(inputs[2], out int n))
            {
                throw new InputException($"Cannot parse {inputs[2]} as integer");
            }

            // check size constraints

            return new FizzBuzzInput(x, y, n);
        }

        
    }
}