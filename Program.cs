using ToK_2026;

Console.WriteLine("Running FizzBuzz w. defaults.");
FizzBuzz fizz_buzz = new (["program", "2", "3", "6"]);
if (fizz_buzz.HasValidInput()) fizz_buzz.Run();


Console.WriteLine("Running Wedding Food w. defaults.");
WeddingFood food = new ("/home/amos/Documents/code/ToK-2026/test-data/wedding-food-default");
if (food.HasValidInput()) food.Run();
