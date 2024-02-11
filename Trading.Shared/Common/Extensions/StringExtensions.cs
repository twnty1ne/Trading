using System.Globalization;
using System.Linq;

namespace Trading.Shared.Common.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string text)
    {
        return string.Concat(text.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }
    
    public static string ToPascalCase(this string text)
    {
        var textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(text)
            .Replace(" ", string.Empty)
            .Replace("_", string.Empty);
    }
}