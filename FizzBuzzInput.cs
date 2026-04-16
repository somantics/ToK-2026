namespace ToK_2026.Fizzbuzz
{
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