namespace TaskManagement.Core.Common
{
    public static class Utility
    {
        public static List<string> GetSeparatedValues(string filter)
        {
            return filter?.Split("|")?.ToList();
        }
    }
}
