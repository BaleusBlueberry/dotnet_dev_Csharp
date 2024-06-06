namespace JokeApp.Functions;

public class Formatter
    {
        public string FormatList(List<string> list)
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }

            return string.Join(",", list);
        }
    }