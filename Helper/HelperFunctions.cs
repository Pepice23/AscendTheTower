using System;
using System.Globalization;
using System.IO;
using System.Linq;

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
        var imageFiles = Directory.EnumerateFiles(imageFolder);
        var randomIndex = new Random().Next(imageFiles.Count());
        var randomImage = $"url(images/backgrounds/bg{randomIndex + 1}.png)";
        return randomImage;
    }

    public static string GetRandomEnemyImage()
    {
        const string imageFolder = "wwwroot/images/enemies";
        var imageFiles = Directory.EnumerateFiles(imageFolder);
        var randomIndex = new Random().Next(imageFiles.Count());
        var randomImage = $"images/enemies/enemy{randomIndex + 1}.png";
        return randomImage;
    }

    public static string GetRandomWeaponImage(string rarity)
    {
        var imageFolder = "";
        var imgPath = "";
        switch (rarity)
        {
            case "poor":
                imageFolder = "wwwroot/images/weapons/poor";
                imgPath = "images/weapons/poor/p";
                break;
            case "uncommon":
                imageFolder = "wwwroot/images/weapons/uncommon";
                imgPath = "images/weapons/uncommon/u";
                break;
            case "rare":
                imageFolder = "wwwroot/images/weapons/rare";
                imgPath = "images/weapons/rare/r";
                break;
            case "epic":
                imageFolder = "wwwroot/images/weapons/epic";
                imgPath = "images/weapons/epic/e";
                break;
            case "legendary":
                imageFolder = "wwwroot/images/weapons/legendary";
                imgPath = "images/weapons/legendary/l";
                break;
        }

        var imageFiles = Directory.EnumerateFiles(imageFolder);
        var randomIndex = new Random().Next(imageFiles.Count());
        var randomImage = $"{imgPath}{randomIndex + 1}.png";
        return randomImage;
    }
}
