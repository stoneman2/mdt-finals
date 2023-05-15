using System;
namespace StonemanFinals2
{
    public struct Water
    {
        public int max;
        public int fill;
        public int drink;
        public int breakTime;
        public int fillTime;
        public int eventTime;
        public Water(int max, int fill, int drink, int breakTime, int fillTime, int eventTime)
        {
            this.max = max;
            this.fill = fill;
            this.drink = drink;
            this.breakTime = breakTime;
            this.fillTime = fillTime;
            this.eventTime = eventTime;
        }
    }
    class ExamIDACamp
    {
        static int AskDrink(int bucketCapacity)
        {
            Console.Write("How much can you drink?: "); // V Drink
            int drink = int.Parse(Console.ReadLine());

            if (drink <= bucketCapacity)
            {
                Console.WriteLine("You can't drink that much.");
                return AskDrink(bucketCapacity);
            }
            else

            return drink;
        }
        static void FillWater(ref Water water)
        {

        }
        static void CalculateWater(Water water)
        {
            // The 
        }
        static void IdiaCamp()
        {
            Console.Write("Insert bucket capacity: "); // V Max
            int bucketCapacity = int.Parse(Console.ReadLine());

            int drink = AskDrink(bucketCapacity);

            Console.Write("How much can you fill?: "); // V Fill
            int fill = int.Parse(Console.ReadLine());

            Console.Write("Break Time: ");
            int breakTime = int.Parse(Console.ReadLine()); // T Drink

            Console.Write("Fill Time:");
            int fillTime = int.Parse(Console.ReadLine()); // T Fill

            Console.Write("Event Time: ");
            int eventTime = int.Parse(Console.ReadLine()); // T max

            Water water = new Water(bucketCapacity, fill, drink, breakTime, fillTime, eventTime);

            CalculateWater(water);
        }
        static void Main(string[] args)
        {
            IdiaCamp();
        }
    }
}