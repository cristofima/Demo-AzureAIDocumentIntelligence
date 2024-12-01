namespace AzureDocumentIntelligence.API.Utils
{
    public static class StringUtil
    {
        public static List<string> SplitText(string text, params string[] separator)
        {
            return text.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => s.Trim()).ToList()
                                        .Where(skill => !string.IsNullOrWhiteSpace(skill))
                                        .ToList();
        }
    }
}
