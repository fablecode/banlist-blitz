using System.Text.RegularExpressions;

namespace BanlistBlitz.Helpers;

public static class StringHelpers
{
    public static string RemoveExtraSpaceBetweenTwoWords(this string? str)
    {
        return Regex.Replace(str ?? throw new ArgumentNullException(nameof(str)), @"\s+", " ");
    }
}