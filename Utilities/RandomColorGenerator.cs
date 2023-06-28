namespace Scorekeeper.Utilities
{
    public class RandomColorGenerator
    {
        private Random _random;

        public RandomColorGenerator(int? seed = null)
        {
            if (seed != null)
            {
                _random = new Random((int)seed);
            }
            else
            {
                _random = new Random();
            }
        }

        public string GetRandomColor()
        {
            string color = string.Format("#{0:X6}", _random.Next(0x1000000));
            return color;
        }
    }
}
