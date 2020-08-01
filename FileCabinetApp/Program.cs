using System;
using System.Globalization;

namespace FileCabinetApp
{
    /// <summary>
    /// Represent the main class.
    /// </summary>
    public static class Program
    {
        private const string DeveloperName = "Artem Halvita";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static bool isRunning = true;

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("stat", Stat),
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("list", List),
            new Tuple<string, Action<string>>("edit", Edit),
            new Tuple<string, Action<string>>("find", Find),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "stat", "prints record statistics", "The 'stat' command prints record statistics." },
        };

        private static string[][] findProperties = new string[][]
        {
            new string[] { "firstname" },
            new string[] { "lastname" },
            new string[] { "dateOfBirth" },
        };

        private static FileCabinetService fileCabinetService = new FileCabinetDefaultService();
        private static CultureInfo cultureInfo = new CultureInfo("en-US");

        /// <summary>
        /// Represents the main method.
        /// </summary>
        /// <param name="args">Arguments of command line.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");

            // TODO : Improve validation-rules command line
            if (args != null && args.Length > 0)
            {
                if (args[0] == "--validation-rules=default" || (args[0] == "-v" && string.Compare(args[1], "default", StringComparison.OrdinalIgnoreCase) == 0))
                {
                }

                if (args[0] == "--validation-rules=custom" || (args[0] == "-v" && string.Compare(args[1], "custom", StringComparison.OrdinalIgnoreCase) == 0))
                {
                    fileCabinetService = new FileCabinetCustomService();
                }
            }

            if (fileCabinetService is FileCabinetDefaultService)
            {
                Console.WriteLine("Using default validation rules.");
            }
            else if (fileCabinetService is FileCabinetCustomService)
            {
                Console.WriteLine("Using custom validation rules.");
            }

            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            do
            {
                Console.Write("> ");
                var inputs = Console.ReadLine().Split(' ', 2);
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                var index = Array.FindIndex(commands, 0, commands.Length, i => i.Item1.Equals(command, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    const int parametersIndex = 1;
                    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                    commands[index].Item2(parameters);
                }
                else
                {
                    PrintMissedCommandInfo(command);
                }
            }
            while (isRunning);
        }

        private static void PrintMissedCommandInfo(string command)
        {
            Console.WriteLine($"There is no '{command}' command.");
            Console.WriteLine();
        }

        private static void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(helpMessages, 0, helpMessages.Length, i => string.Equals(i[Program.CommandHelpIndex], parameters, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(helpMessages[index][Program.ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (var helpMessage in helpMessages)
                {
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[Program.CommandHelpIndex], helpMessage[Program.DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static void Stat(string parameters)
        {
            var recordsCount = fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static void Create(string parameters)
        {
            int id = default;
            bool done = false;

            while (!done)
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("Incorrect entry. Try again.");
                    Console.Write("First name: ");
                    firstName = Console.ReadLine();
                }

                Console.Write("Last name: ");
                string lastName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Incorrect entry. Try again.");
                    Console.Write("Last name: ");
                    lastName = Console.ReadLine();
                }

                Console.Write("Date of birth: ");
                string dateInput = Console.ReadLine();
                DateTime dateOfBirth;
                while (!DateTime.TryParse(dateInput, out dateOfBirth))
                {
                    Console.WriteLine("Incorrect entry. Try again.");
                    Console.Write("Date of birth: ");
                    dateInput = Console.ReadLine();
                }

                Console.Write("Age: ");
                string ageInput = Console.ReadLine();
                short age;
                while (!short.TryParse(ageInput, out age))
                {
                    Console.WriteLine("Incorrect entry. Try again.");
                    Console.Write("Age: ");
                    ageInput = Console.ReadLine();
                }

                Console.Write("Money: ");
                string moneyInput = Console.ReadLine();
                decimal money;
                while (!decimal.TryParse(moneyInput, out money))
                {
                    Console.WriteLine("Incorrect entry. Try again.");
                    Console.Write("Money: ");
                    moneyInput = Console.ReadLine();
                }

                Console.Write("Any letter: ");
                string letterInput = Console.ReadLine();
                char letter;
                while (!char.TryParse(letterInput, out letter))
                {
                    Console.WriteLine("Incorrect entry. Try again.");
                    Console.Write("Letter: ");
                    letterInput = Console.ReadLine();
                }

                var record = new FileCabinetRecord()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Age = age,
                    Money = money,
                    Letter = letter,
                };

                try
                {
                    id = fileCabinetService.CreateRecord(record);
                    done = true;
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Try again.");
                }
            }

            Console.WriteLine($"Record #{id} is created.");
        }

        private static void List(string parameters)
        {
            var records = fileCabinetService.GetRecords();

            foreach (var item in records)
            {
                Console.WriteLine($"#{item.Id}, {item.FirstName}, {item.LastName}, {item.DateOfBirth.ToString("yyyy-MMM-dd", cultureInfo)}, {item.Age}, {item.Money}, {item.Letter}");
            }
        }

        private static void Edit(string parameters)
        {
            int id;
            if (!string.IsNullOrEmpty(parameters) && int.TryParse(parameters, out id))
            {
                if (id > 0 && id <= fileCabinetService.GetStat())
                {
                    Console.Write("First name: ");
                    string firstName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 60)
                    {
                        Console.WriteLine("Incorrect entry. Try again.");
                        Console.Write("First name: ");
                        firstName = Console.ReadLine();
                    }

                    Console.Write("Last name: ");
                    string lastName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 60)
                    {
                        Console.WriteLine("Incorrect entry. Try again.");
                        Console.Write("Last name: ");
                        lastName = Console.ReadLine();
                    }

                    Console.Write("Date of birth: ");
                    string dateInput = Console.ReadLine();
                    DateTime dateOfBirth;
                    while (!DateTime.TryParse(dateInput, out dateOfBirth) || dateOfBirth <= new DateTime(1950, 1, 1) || dateOfBirth >= DateTime.Now)
                    {
                        Console.WriteLine("Incorrect entry. Try again.");
                        Console.Write("Date of birth: ");
                        dateInput = Console.ReadLine();
                    }

                    Console.Write("Age: ");
                    string ageInput = Console.ReadLine();
                    short age;
                    while (!short.TryParse(ageInput, out age) || age < 12 || age > 99)
                    {
                        Console.WriteLine("Incorrect entry. Try again.");
                        Console.Write("Age: ");
                        ageInput = Console.ReadLine();
                    }

                    Console.Write("Money: ");
                    string moneyInput = Console.ReadLine();
                    decimal money;
                    while (!decimal.TryParse(moneyInput, out money) || money < 0)
                    {
                        Console.WriteLine("Incorrect entry. Try again.");
                        Console.Write("Money: ");
                        moneyInput = Console.ReadLine();
                    }

                    Console.Write("Any letter: ");
                    string letterInput = Console.ReadLine();
                    char letter;
                    while (!char.TryParse(letterInput, out letter) || !char.IsLetter(letter))
                    {
                        Console.WriteLine("Incorrect entry. Try again.");
                        Console.Write("Letter: ");
                        letterInput = Console.ReadLine();
                    }

                    var newRecord = new FileCabinetRecord()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        DateOfBirth = dateOfBirth,
                        Age = age,
                        Money = money,
                        Letter = letter,
                    };

                    fileCabinetService.EditRecord(id, newRecord);

                    Console.WriteLine($"Record #{id} is edited.");
                }
                else
                {
                    Console.WriteLine($"#{id} not found.");
                }
            }
            else
            {
                Console.WriteLine($"There is no explanation for '{parameters}' command.");
            }
        }

        private static void Find(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(findProperties, 0, findProperties.Length, i => string.Equals(i[Program.CommandHelpIndex], parameters.Split(' ')[0], StringComparison.InvariantCultureIgnoreCase));

                if (index == 0)
                {
                    foreach (var item in fileCabinetService.FindByFirstName(parameters.Split(' ')[1]))
                    {
                        Console.WriteLine($"#{item.Id}, {item.FirstName}, {item.LastName}, {item.DateOfBirth.ToString("yyyy-MMM-dd", cultureInfo)}, {item.Age}, {item.Money}, {item.Letter}");
                    }
                }

                if (index == 1)
                {
                    foreach (var item in fileCabinetService.FindByLastName(parameters.Split(' ')[1]))
                    {
                        Console.WriteLine($"#{item.Id}, {item.FirstName}, {item.LastName}, {item.DateOfBirth.ToString("yyyy-MMM-dd", cultureInfo)}, {item.Age}, {item.Money}, {item.Letter}");
                    }
                }

                if (index == 2)
                {
                    foreach (var item in fileCabinetService.FindByDateOfBirth(parameters.Split(' ')[1]))
                    {
                        Console.WriteLine($"#{item.Id}, {item.FirstName}, {item.LastName}, {item.DateOfBirth.ToString("yyyy-MMM-dd", cultureInfo)}, {item.Age}, {item.Money}, {item.Letter}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Enter property and query.");
            }
        }
    }
}