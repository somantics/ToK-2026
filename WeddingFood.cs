
namespace ToK_2026
{
    class WeddingFood
    {
        private WeddingFoodInput? Input {get; set;}

        public WeddingFood() { Input = null; }

        public WeddingFood(string inputpath)
        {
            ReadInput(inputpath);
        }

        public bool HasValidInput()
        {
            return Input != null;
        }

        public void Run()
        {
            if (Input is null)
            {
                Console.WriteLine("Cannot run WeddingFood: No valid input to use.");
                return;
            }
            //rough debugging
            Console.WriteLine(Input);
        }

        public void ReadInput(string inputpath)
        {
            try
            {
                Input = WeddingFoodInput.FromFile(inputpath);
            }
            catch (InputException ex)
            {
                Console.WriteLine($"Incorrect input for WeddingFood: {ex.Message}");
                Input = null;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Could not read file: {ex.Message}");
                Input = null;
            }
        }
    }

    class WeddingFoodInput
    {

        public int TotalPizzaTypes {get; set;}
        public int TotalGuests {get; set;}
        public Dictionary<string, int> OrderedSlices {get; set;}
        public List<string[]> PizzaPreferences {get; set;}

        public WeddingFoodInput()
        {
            TotalPizzaTypes = -1;
            TotalGuests = -1;
            OrderedSlices = [];
            PizzaPreferences = [];
        }

        public WeddingFoodInput(int pizzaTypesAmount, int totalGuests, Dictionary<string, int> slices, List<string[]> preferences)
        {
            TotalPizzaTypes = pizzaTypesAmount;
            TotalGuests = totalGuests;
            OrderedSlices = slices;
            PizzaPreferences = preferences;
        }

        public static WeddingFoodInput FromFile(string filepath)
        {
            WeddingFoodInput input = new();

            try
            {
                using StreamReader reader = new(filepath);

                // Amount of pizza types and amount of guests
                string? firstLine = reader.ReadLine()?.ToLower();
                int[]? typesAndGuests = firstLine?.Split(' ')?.Select(x => Convert.ToInt32(x)).ToArray();
                if (typesAndGuests != null && typesAndGuests.Length == 2)
                {
                    input.TotalPizzaTypes = typesAndGuests[0];
                    input.TotalGuests = typesAndGuests[1];
                }
                else
                {
                    throw new InputException(message: $"Cannot parse first line of file {filepath}");
                }

                // Amount of slices per pizza type
                string? nextLine;
                for (int i = 0; i < input.TotalPizzaTypes; i++)
                {
                    nextLine = reader.ReadLine()?.ToLower();
                    string[]? columns = nextLine?.Split(' ');
                    if (columns == null)
                    {
                        throw new InputException(message: $"Cannot parse line {i + 2} of file {filepath}");
                    }
                    else if (columns.Length != 2)
                    {
                        throw new InputException(message: $"Cannot parse line {i + 2} of file {filepath}: incorrect number of arguments");
                    }
                    else
                    {
                        input.OrderedSlices.Add(columns[0], Convert.ToInt32(columns[1]));
                    }
                }

                // Pizza preferences
                for (int i = 0; i < input.TotalGuests; i++)
                {
                    nextLine = reader.ReadLine()?.ToLower();
                    string[]? columns = nextLine?.Split(' ');
                    if (columns == null)
                    {
                        throw new InputException(message: $"Cannot parse line {i + 2 + input.TotalPizzaTypes} of file {filepath}");
                    }
                    else if (columns.Length != 3)
                    {
                        throw new InputException(message: $"Cannot parse line {i + 2} of file {filepath}: incorrect number of arguments");
                    }
                    else
                    {
                        // First
                        input.PizzaPreferences.Add([.. columns.Skip(1)]);
                    }
                }
            }
            catch (IOException)
            {
                throw;
            }

            return input;
        }

        public override string ToString()
        {
            string output = "";
            output += $"Amount of pizza types: {TotalPizzaTypes}\n";
            output += $"Amount of guests: {TotalGuests}\n";
            foreach ((var key, var item) in OrderedSlices)
            {
                output += $"{key} {item}\n";
            }
            foreach (var line in PizzaPreferences)
            {
                output += $"{string.Join(' ', line)}\n";
            }
            return output;
        }
    }
}