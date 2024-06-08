namespace ConsoleApp36
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<string, (int, int, int)> getRGB = color =>
            {
                return color.ToLower() switch
                {
                    "red" => (255, 0, 0),
                    "orange" => (255, 165, 0),
                    "yellow" => (255, 255, 0),
                    "green" => (0, 128, 0),
                    "blue" => (0, 0, 255),
                    "indigo" => (75, 0, 130),
                    "violet" => (238, 130, 238),
                    _ => (0, 0, 0)
                };
            };
            string color = "green";
            var rgb = getRGB(color);
            Console.WriteLine($"The RGB value for {color} is ({rgb.Item1}, {rgb.Item2}, {rgb.Item3})");
            Backpack backpack = new Backpack("Black", "Nike", "Nylon", 1.5, 20);
            backpack.AddItem("Laptop", 2);
            backpack.AddItem("Water Bottle", 1);
            try
            {
                backpack.AddItem("Books", 18);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            int[] numbers = { 1, 7, 14, 21, 28, 35, 2, 3, 5 };
            Func<int[], int> countMultiplesOfSeven = arr => arr.Count(x => x % 7 == 0);
            int result = countMultiplesOfSeven(numbers);
            Console.WriteLine($"Count of numbers divisible by 7: {result}");
            int[] numbers2 = { -1, 7, -14, 21, 28, -35, 2, 3, 5 };
            Func<int[], int> countPositiveNumbers = arr => arr.Count(x => x > 0);
            int result2 = countPositiveNumbers(numbers2);
            Console.WriteLine($"Count of positive numbers: {result2}");
            int[] numbers3 = { -1, -7, 7, -14, 21, -14, -35, -2, 3, -5 };
            Action<int[]> displayUniqueNegativeNumbers = arr =>
            {
                var uniqueNegativeNumbers = arr.Where(x => x < 0).Distinct();
                Console.WriteLine("Unique negative numbers: " + string.Join(", ", uniqueNegativeNumbers));
            };
            displayUniqueNegativeNumbers(numbers3);
            string text = "This is a sample text for testing the presence of a specific word.";
            string wordToCheck = "sample";
            Func<string, string, bool> containsWord = (txt, word) => txt.Contains(word);
            bool result3 = containsWord(text, wordToCheck);
            Console.WriteLine($"Does the text contain the word '{wordToCheck}'? {result3}");
        }
    }
}
public class Backpack
{
    public string Color { get; set; }
    public string Brand { get; set; }
    public string Fabric { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    private double currentVolume;
    public List<(string ItemName, double ItemVolume)> Contents { get; private set; }

    public event Action<string, double> ItemAdded;

    public Backpack(string color, string brand, string fabric, double weight, double volume)
    {
        Color = color;
        Brand = brand;
        Fabric = fabric;
        Weight = weight;
        Volume = volume;
        Contents = new List<(string, double)>();
        currentVolume = 0;

        ItemAdded += (itemName, itemVolume) =>
        {
            if (currentVolume + itemVolume > Volume)
            {
                throw new InvalidOperationException("Cannot add item. Exceeds backpack volume.");
            }
            Contents.Add((itemName, itemVolume));
            currentVolume += itemVolume;
            Console.WriteLine($"Item added: {itemName}, Volume: {itemVolume}");
        };
    }

    public void AddItem(string itemName, double itemVolume)
    {
        ItemAdded?.Invoke(itemName, itemVolume);
    }
}