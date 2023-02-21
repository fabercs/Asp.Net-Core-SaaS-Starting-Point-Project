using System;

namespace EMSApp.Shared.Extensions;

public static class StringExtensions
{
    public static string RemovePrefix(this string str, params string[] preFixes)
    {
        return str.RemovePrefix(StringComparison.Ordinal, preFixes);
    }
    
    public static string RemovePrefix(this string str, StringComparison comparisonType, params string[] preFixes)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        if (preFixes.Length == 0)
        {
            return str;
        }

        foreach (var preFix in preFixes)
        {
            if (str.StartsWith(preFix, comparisonType))
            {
                return str.Right(str.Length - preFix.Length);
            }
        }

        return str;
    }
    public static string Right(this string str, int len)
    {
        if (string.IsNullOrEmpty(str))
            throw new ArgumentNullException(nameof(str));
        
        if (str.Length < len)
        {
            throw new ArgumentException("len argument can not be bigger than given string's length!");
        }

        return str.Substring(str.Length - len, len);
    }

}