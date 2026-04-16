namespace ToK_2026.WeddingFood
{
    class Guest
    {
        public string PreferedPizza {get; private set;}
        public string UnhappyPizza {get; private set;}
        public string? UsedPizza {get; set;}

        public Guest(string[] preferences)
        {
            PreferedPizza = preferences[0];
            UnhappyPizza = preferences[1];
        }
    }
}