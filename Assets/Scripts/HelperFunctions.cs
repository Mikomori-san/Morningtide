namespace StarterAssets
{
    public class HelperFunctions
    {
        public static int GetNumberFromName(string name)
        {
            int number;
            if (int.TryParse(name, out number))
            {
                return number;
            }
            else
            {
                return int.MaxValue;
            }
        }
    }
}