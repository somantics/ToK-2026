
namespace ToK_2026.WeddingFood
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
            int result = DistributeGreedily();
            Console.WriteLine(result);
            //rough debugging
            Console.WriteLine(Input);

            /*
            order of priority:
                do not serve unwanted pizza
                serve appreciated pizza
                neutral
            
            approaches
                brute force all options and reduce(max) 
                distribute greedily and then swap when unhappy assignment happens
                    how does this ensure correct solution? 
                distribute so that no one is unhappy, then swap when it increases score
                distribute randomly, for each guest swap if it increases score
            
            observations
                score is capped at number of guests
                pizza types is capped at number of guests
            
            edge cases
                there is only one type of pizza --> there is only one solution
                there are an equal amount of types and guests

            */
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

        private int BruteForce()
        {
            /*
            construct all solutions
                pick one starting guest:
                    per pizza type: 
                        pick one starting guest, with 1 pizza spent
                        ...until 1 guest left (which means 1 pizza) then march back up the tree
            */
            return -1;
        }

        private int DistributeGreedily()
        {
            if (Input is null)
            {
                Console.WriteLine("Cannot run WeddingFood: No valid input to use.");
                return -1;
            }

            Dictionary<string, int> remainingPizzas = new(Input.OrderedSlices);
            List<Guest> happyGuests = [];
            List<Guest> neutralGuests = [];
            List<Guest> sadGuests = [];

            // Supplying happy pizzas
            List<Guest> remainingGuests = [];
            foreach (var guest in Input.Guests)
            {
                if (remainingPizzas[guest.PreferedPizza] > 0 )
                {
                    remainingPizzas[guest.PreferedPizza] -= 1;
                    guest.UsedPizza = guest.PreferedPizza;
                    happyGuests.Add(guest);
                }
                else
                {
                    remainingGuests.Add(guest);
                }
            }

            // Supplying neutral pizzas
            foreach (var guest in remainingGuests)
            {
                if (FindNeutralPizza(guest, out string neutralPizza))
                {
                    remainingPizzas[neutralPizza] -= 1;
                    guest.UsedPizza = neutralPizza;
                    neutralGuests.Add(guest);
                }
                else // sad pizza
                {
                    sadGuests.Add(guest);
                }
            }

            // Handling sad pizzas
            remainingGuests = [.. sadGuests];
            sadGuests = [];
            foreach (var guest in remainingGuests)
            {
                (Guest? Candidate, string Pizza) bestSwap = (null, "");
                
                // Can I swap with a neutral guest?
                List<Guest> neutralSwapCandidates = neutralGuests.FindAll(x => x.UsedPizza != guest.UnhappyPizza);
                foreach (var candidate in neutralSwapCandidates)
                {
                    if (FindNeutralPizzaFrom(candidate, remainingPizzas, out string neutralPizza))
                    {
                        bestSwap = (candidate, neutralPizza);
                        if (candidate.UsedPizza == guest.PreferedPizza) break; // best case scenario
                    }
                }

                if (bestSwap.Candidate != null)
                {
                    guest.UsedPizza = bestSwap.Candidate.UsedPizza;
                    bestSwap.Candidate.UsedPizza = bestSwap.Pizza;
                    remainingPizzas[bestSwap.Pizza] -= 1;

                    if (bestSwap.Candidate.UsedPizza == guest.PreferedPizza)
                    {
                        happyGuests.Add(guest);
                    }
                    else
                    {
                        neutralGuests.Add(guest);
                    }

                    break;
                }

                // Can I swap with a happy guest?
                List<Guest> happySwapCandidates = neutralGuests.FindAll(x => x.UsedPizza != guest.UnhappyPizza);
                foreach (var candidate in happySwapCandidates)
                {
                    if (remainingPizzas[candidate.PreferedPizza] > 0)
                    {
                        bestSwap = (candidate, candidate.PreferedPizza); // good enough swap

                    }
                    else if (FindNeutralPizzaFrom(candidate, remainingPizzas, out string neutralPizza))
                    {
                        bestSwap = (candidate, neutralPizza); // good enough swap
                        if (candidate.UsedPizza == guest.PreferedPizza) break; // best case scenario
                    }
                }

                if (bestSwap.Candidate != null)
                {
                    guest.UsedPizza = bestSwap.Candidate.UsedPizza;
                    bestSwap.Candidate.UsedPizza = bestSwap.Pizza;
                    remainingPizzas[bestSwap.Pizza] -= 1;

                    if (bestSwap.Candidate.UsedPizza == guest.PreferedPizza)
                    {
                        happyGuests.Add(guest);
                    }
                    else
                    {
                        neutralGuests.Add(guest);
                    }
                    
                    break;
                }
                
                // Cannot swap for improvement
                sadGuests.Add(guest);
            }

            int score = happyGuests.Count - sadGuests.Count * 2;
            Console.WriteLine(happyGuests.Count);
            Console.WriteLine(neutralGuests.Count);
            Console.WriteLine(sadGuests.Count);
            return score;


            /*
                Algorithm:

                list wants per pizza
                fullfill wants at first come, first serve
                list neutrals per pizza
                fullfill neutrals at first come, first serve
                
                for each (to be unhappy) guest remaining: 
                    can I swap with a neutral to create happy + happy? <-- this should not be possible at this stage
                        if so this is +4
                    can I swap with a neutral to create happy + neutral?
                        if so this is +3
                    can I swap with a neutral to create neutral + neutral?
                        if so this is +2
                    can I swap with a happy to create happy + happy?
                        if so this is +2
                    can I swap with a happy to create happy + neutral?
                        if so this is +2
                    can I swap with a happy to create neutral + neutral?
                        if so this is +1

                    if no to all, put on really unhappy list
                
                assign pizzas in order to really unhappy guests

                calculate and return score


                Weakness: potential local maxima based on initial choices (at first come, first serve stages) 
            */
        }

        private bool FindNeutralPizza(Guest guest, out string neutralPizza)
        {
            if (Input is null)
            {
                Console.WriteLine("Cannot run WeddingFood: No valid input to use.");
                neutralPizza = "";
                return false;
            }

            return FindNeutralPizzaFrom(guest, Input.OrderedSlices, out neutralPizza);
        }

        private static bool FindNeutralPizzaFrom(Guest guest, Dictionary<string, int> availablePizzas, out string neutralPizza)
        {
            neutralPizza = "";
            return false;
        }
    }

}