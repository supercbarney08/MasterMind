
Console.WriteLine(@"Welcome to MasterMIND!
You must correctly guess four (4) number of numerical value 1-6.
For a correct guess in the correct position a plus (+) will be printed.
For a correct guess in the wrong position a minus (-) will be printed.
For an incorrect guess nothing will be printed.

For example:
If the secret answer were: 1234
And the user guessed: 4233
++- will be printed

You will have 10 guess to correctly decipher the secret answer.");

while (true)
{
    if (StartGame())
    {
        PlayGame();
    }
    else
    {
        break;
    }
}

bool StartGame()
{
    Console.WriteLine("Are you ready to play? ( Y/N )");
    var playing = Console.ReadLine();
    return string.Equals("Y", playing, StringComparison.InvariantCultureIgnoreCase);
}

void PlayGame()
{
    Random rnd = new Random();
    int[] solution = [rnd.Next(1, 6), rnd.Next(1, 6), rnd.Next(1, 6), rnd.Next(1, 6)];
    var solved = false;
    var turns = 10;
    while (!solved && turns > 0)
    {
        var input = GetInput(turns);
        int[] inputArray;
        if (!ValidInput(input, out inputArray))
        {
            continue;
        }
        turns--;
        var output = CheckAnswer(solution, inputArray);
        solved = string.Equals(output, "++++");
        if (!solved)
        {
            Console.WriteLine("INCORRECT!!");
            Console.WriteLine(output);
        }
    }
    if (solved)
    {
        Console.WriteLine("YOU DID IT!!");
    }
    else
    {
        Console.WriteLine(@"You have run out of tries... ;(
                            The correct answer was " + string.Concat(solution));

    }
}

string? GetInput(int tries)
{
    var inputArray = new int[4];
    Console.WriteLine($"Tries remaining {tries}. Enter you guess: ");
    return Console.ReadLine();
}

bool ValidInput(string? input, out int[] inputArray)
{
    inputArray = new int[4];

    if (string.IsNullOrWhiteSpace(input) || input.Length > 4)
    {
        Console.WriteLine("Input must be exaclty 4 numbers 1-6 in length.");
        return false;
    }
    var stringArr = input.ToCharArray();
    for (var i = 0; i < 4; i++)
    {
        if (!int.TryParse(stringArr[i].ToString(), out inputArray[i]))
        {
            Console.WriteLine("All inputs must be a numeric value numbered 1-6.");
            return false;
        }
        if (inputArray[i] > 6 || inputArray[i] < 0)
        {
            Console.WriteLine("All inputs must be a numeric value numbered 1-6.");
            return false;
        }
    }
    return true;
}

string? CheckAnswer(int[] solution, int[] guesses)
{
    string[] answers = ["", "", "", ""];
    
    for (int g = 0; g < 4; g++)
    {
        if (solution[g] == guesses[g])
        {
            answers[g] = "+";
        }
        else {
            for (int s = 0; s < 4; s++)
            {
                if (s == g) continue;
                if (solution[s] == guesses[g] && string.IsNullOrWhiteSpace(answers[s]))
                {
                    answers[s] = "-";
                    break;
                }
            } 
        }
    }

    return string.Concat(string.Concat(answers).Order());
}
