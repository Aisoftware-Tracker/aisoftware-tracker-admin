namespace Aisoftware.Tracker.Borders.Services
{
    public static class StringUtil
    {
        public static string? RemoveAccent(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value?.Replace("ç", "c")
                             ?.Replace("ã", "a")
                             ?.Replace("õ", "o")
                             ?.Replace("á", "a")
                             ?.Replace("é", "e")
                             ?.Replace("í", "i")
                             ?.Replace("ó", "o")
                             ?.Replace("ú", "u")
                             ?.Replace("â", "a")
                             ?.Replace("ê", "e")
                             ?.Replace("ô", "o")
                             ?.Replace("à", "a");
            }

            return string.Empty;

        }
    }

}