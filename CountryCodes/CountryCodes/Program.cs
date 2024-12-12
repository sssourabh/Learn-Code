namespace CountryNeighborFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            string countriesFilePath = "C:\\Users\\SOURABH.S.M\\source\\repos\\CountryCodes\\CountryCodes\\countries.txt";

            if (!File.Exists(countriesFilePath))
            {
                Console.WriteLine($"The file '{countriesFilePath}' does not exist.");
                return;
            }

            Dictionary<string, (string CountryName, List<string> NeighboringCountries)> countriesData = LoadCountriesFromFile(countriesFilePath);

            Console.Write("Enter a country code (e.g., IN, US, NZ): ");
            string? userInputCountryCode = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrEmpty(userInputCountryCode) || !countriesData.ContainsKey(userInputCountryCode))
            {
                Console.WriteLine("Invalid country code or country not found.");
                return;
            }

            DisplayCountryDetails(countriesData, userInputCountryCode);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static Dictionary<string, (string CountryName, List<string> NeighboringCountries)> LoadCountriesFromFile(string filePath)
        {
            var countryDictionary = new Dictionary<string, (string CountryName, List<string> NeighboringCountries)>();

            foreach (var fileLine in File.ReadAllLines(filePath))
            {
                string[] countryDetails = fileLine.Split(',', 3);
                string countryCode = countryDetails[0].Trim();
                string countryName = countryDetails[1].Trim();
                List<string> neighboringCountries = new List<string>(countryDetails[2].Split(';'));

                countryDictionary[countryCode] = (countryName, neighboringCountries);
            }

            return countryDictionary;
        }

        private static void DisplayCountryDetails(Dictionary<string, (string CountryName, List<string> NeighboringCountries)> countriesData, string countryCode)
        {
            var (countryName, neighboringCountries) = countriesData[countryCode];

            Console.WriteLine($"Country: {countryName}");
            Console.WriteLine("Adjacent Countries: ");

            foreach (string neighbor in neighboringCountries)
            {
                Console.WriteLine($"- {neighbor}");
            }
        }
    }
}
