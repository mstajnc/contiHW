namespace Backend_Homework.ConsoleApp
{
    internal static class UserActions
    {
        internal static Dictionary<ConsoleKey, string> StorageOptions = new()
        {
            [ConsoleKey.F] = "[F]ile system",
            [ConsoleKey.A] = "[A]zure",
            [ConsoleKey.H] = "[H]ttp"
        };

        internal static Dictionary<ConsoleKey, string> FormatOptions = new()
        {
            [ConsoleKey.X] = "[X]ml",
            [ConsoleKey.J] = "[J]son",
            [ConsoleKey.B] = "[B]son",
            [ConsoleKey.Y] = "[Y]aml",
        };

        internal static string GetFormattedOutput(Dictionary<ConsoleKey, string> dictionary)
        {
            return string.Join(Environment.NewLine, dictionary.Select(x => $"{x.Key} => {x.Value}").ToList());
        }
    }
}
