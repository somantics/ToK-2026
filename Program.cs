using ToK_2026.Fizzbuzz;
using ToK_2026.WeddingFood;

FizzBuzz fizzBuzz;
if (args.Length == 4)
{
    Console.WriteLine("Running FizzBuzz.");
    fizzBuzz = new (args);
}
else
{
    Console.WriteLine("Running FizzBuzz w. defaults.");
    fizzBuzz = new (["", "2", "3", "6"]);
}
if (fizzBuzz.HasValidInput()) fizzBuzz.Run();
Console.WriteLine("");


Console.WriteLine("Running Wedding Food w. defaults.");
WeddingFood food = new ("/home/amos/Documents/code/ToK-2026/test-data/wedding-food-default");
if (food.HasValidInput()) food.Run();
