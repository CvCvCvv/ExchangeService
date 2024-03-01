using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ExchangeService.Client.Extensions;

public static class RequestHelper
{
    public static string GenerateParametersString(this IDictionary<string, string> properties)
    {
        return properties != null && properties.Count != 0 ? "?" + string.Join("&", properties.Select(a => $"{a.Key}={a.Value}")) : string.Empty;
    }

    public static void AddStringIfNotEmptyOrWhiteSpace( this IDictionary<string, string> dictionary,
           string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return;

        dictionary.Add(key, value);
    }

    public static void AddSimpleStruct<T>(this IDictionary<string, string> dictionary,
        string key, T value) where T : struct
        => dictionary.Add(key, value.ToString()!);

    public static void AddSimpleStructIfNotNull<T>(this IDictionary<string, string> dictionary,
        string key, T? value) where T : struct
    {
        if (!value.HasValue) return;

        dictionary.Add(key, value.Value.ToString()!);
    }

    public static void AddEnum<T>(this IDictionary<string, string> dictionary,
        string key, T value) where T : struct
    {
        dictionary.Add(key, (Convert.ToDecimal(Convert.ToInt32(value))).ToFormattedString());
        //dictionary.Add( key, value.ToString());
    }


    public static void AddEnumIfNotNull<T>(this IDictionary<string, string> dictionary,
        string key, T? value) where T : struct
    {
        if (!value.HasValue) return;

        dictionary.Add(key, (Convert.ToInt32(value)).ToString());
    }

    public static void AddArray(this IDictionary<string, string> dictionary,
        string key, string[] value)
    {
        if (value == null) return;

        var nextProperty = string.Empty;

        for (int i = 0; i < value.Length; i++)
        {
            if (i == 0)
            {
                nextProperty += value[i].ToString();
            }
            else
            {
                nextProperty += "&" + key + "=" + value[i].ToString();
            }
        }

        dictionary.Add(key, nextProperty);
    }

    public static void AddDateTime(this IDictionary<string, string> dictionary,
      string key, DateTime? value)
    {
        if (value is null) return;

        //2023-06-22T10:51:38.585Z
        dictionary.Add(key, $"{value.Value.ToString("yyyy-MM-dd")}T{value.Value.ToString("HH:mm:ss.fffZ")}");
    }

    public static void AddDecimal(this IDictionary<string, string> dictionary,
        string key, decimal value) =>
        dictionary.Add(key, value.ToFormattedString());

    public static void AddDecimalIfNotNull(this IDictionary<string, string> dictionary,
        string key, decimal? value)
    {
        if (!value.HasValue) return;

        dictionary.Add(key, value.Value.ToFormattedString());
    }

    private static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo { NumberDecimalSeparator = @"." };

    private static string ToFormattedString(this decimal value) => value.ToString(NumberFormat);
}
