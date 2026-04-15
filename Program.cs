using ToK_2026;

Console.WriteLine("Running FizzBuzz.");
FizzBuzz fizz_buzz = new (args);
if (fizz_buzz.HasValidInput()) fizz_buzz.Run();



/*
FizzBuzz algorithm

Input: X, Y, N

For each integer < N: 
    if divisible by X and Y:
        map to FizzBuzz
    else if divisible by X:
        map to Fizz
    else if divisible by Y:
        map to Buzz
*/