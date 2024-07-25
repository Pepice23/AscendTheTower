using System.Globalization;

namespace AscendTheTower.Helper;

public static class HelperFunctions
{
    public static string FormatNumberCompact(double number)
    {
        return number switch
        {
            // Check the range and format accordingly
            < 1000 => number.ToString(CultureInfo.InvariantCulture),
            < 1_000_000 => (number / 1_000).ToString("0.00K", CultureInfo.InvariantCulture),
            < 1_000_000_000 => (number / 1_000_000).ToString("0.00M", CultureInfo.InvariantCulture),
            < 1_000_000_000_000 => (number / 1_000_000_000).ToString("0.00B", CultureInfo.InvariantCulture),
            _ => (number / 1_000_000_000_000).ToString("0.00T", CultureInfo.InvariantCulture)
        };
    }

}
