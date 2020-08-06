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

        private static IFileCabinetService fileCabinetService = new FileCabinetService();
        private static IRecordValidator recordValidator = new DefaultValidator();
        private static CultureInfo cultureInfo = new CultureInfo("en-US");

        /// <summary>
        /// Represents the main method.
        /// </summary>
        /// <param name="args">Arguments of command line.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");

            // TODO : Improve validation-rules command line
            if (args == null || !(args.Length > 0))
            {
                Console.WriteLine("Using default validation rules.");
            }
            else if (args != null &&
                    (args[0] == "--validation-rules=default" ||
                    (args[0] == "-v" && string.Compare(args[1], "default", StringComparison.OrdinalIgnoreCase) == 0)))
            {
                Console.WriteLine("Using default validation rules.");
            }

            if (args != null && args.Length > 0 && (args[0] == "--validation-rules=custom" || (args[0] == "-v" && string.Compare(args[1], "custom", StringComparison.OrdinalIgnoreCase) == 0)))
            {
                recordValidator = new CustomValidator();
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
            var typesConverter = new RecordTypesConverter();

            Console.Write("First name: ");
            string firstName = ReadInput(typesConverter.StringConverter, recordValidator.FirstNameValidator);

            Console.Write("Last name: ");
            string lastName = ReadInput(typesConverter.StringConverter, recordValidator.LastNameValidator);

            Console.Write("Date of birth: ");
            DateTime dateOfBirth = ReadInput(typesConverter.DateTimeConverter, recordValidator.DateOfBirthValidator);

            Console.Write("Age: ");
            short age = ReadInput(typesConverter.ShortConverter, recordValidator.AgeValidator);

            Console.Write("Money: ");
            decimal money = ReadInput(typesConverter.DecimalConverter, recordValidator.MoneyValidator);

            Console.Write("Any letter: ");
            char letter = ReadInput(typesConverter.CharConverter, recordValidator.LetterValidator);

            var record = new FileCabinetRecord()
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Age = age,
                Money = money,
                Letter = letter,
            };

            int id = fileCabinetService.CreateRecord(record);

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
            if (!string.IsNullOrEmpty(parameters) && int.TryParse(parameters, out int id))
            {
                if (id > 0 && id <= fileCabinetService.GetStat())
                {
                    var typesConverter = new RecordTypesConverter();

                    Console.Write("First name: ");
                    string firstName = ReadInput(typesConverter.StringConverter, recordValidator.FirstNameValidator);

                    Console.Write("Last name: ");
                    string lastName = ReadInput(typesConverter.StringConverter, recordValidator.LastNameValidator);

                    Console.Write("Date of birth: ");
                    DateTime dateOfBirth = ReadInput(typesConverter.DateTimeConverter, recordValidator.DateOfBirthValidator);

                    Console.Write("Age: ");
                    short age = ReadInput(typesConverter.ShortConverter, recordValidator.AgeValidator);

                    Console.Write("Money: ");
                    decimal money = ReadInput(typesConverter.DecimalConverter, recordValidator.MoneyValidator);

                    Console.Write("Any letter: ");
                    char letter = ReadInput(typesConverter.CharConverter, recordValidator.LetterValidator);

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

        private static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine();
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
        }
    }
}