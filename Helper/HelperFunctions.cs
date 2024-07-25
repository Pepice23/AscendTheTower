using System;
using System.Globalization;
using System.IO;

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

    public static string GetRandomBackgroundImage()
    {
        const string imageFolder = "wwwroot/images/backgrounds";
        var imageFiles = Directory.GetFiles(imageFolder);
        var randomIndex = new Random().Next(imageFiles.Length);
        var randomImage = $"url(images/backgrounds/bg{randomIndex + 1}.png)";
        return randomImage;
    }

}
