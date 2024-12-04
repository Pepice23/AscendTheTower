using System.Globalization;
using System.IO;

namespace AscendTheTower.Helper;

/// <summary>
/// Provides utility functions for formatting numbers and handling image resources.
/// </summary>
public static class HelperFunctions
{
    private static readonly Random Random = new Random();
    private const string WwwrootPath = "wwwroot";
    
    /// <summary>
    /// Formats a number into a compact representation (e.g., 1K, 1M, 1B, 1T)
    /// </summary>
    /// <param name="number">The number to format</param>
    /// <returns>A formatted string representation of the number</returns>
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

    /// <summary>
    /// Gets a random image from the specified folder with proper error handling
    /// </summary>
    /// <param name="folder">The folder path relative to wwwroot</param>
    /// <param name="filePrefix">The prefix to use for the image file name</param>
    /// <param name="urlPrefix">Optional URL prefix to prepend to the result</param>
    /// <returns>The path to a random image</returns>
    private static string GetRandomImageFromFolder(string folder, string filePrefix, string? urlPrefix = null)
    {
        var imageFolder = Path.Combine(WwwrootPath, folder);
        
        if (!Directory.Exists(imageFolder))
            throw new DirectoryNotFoundException($"Image folder not found: {imageFolder}");
            
        var imageFiles = Directory.EnumerateFiles(imageFolder).ToList();
        if (!imageFiles.Any())
            throw new InvalidOperationException($"No images found in folder: {imageFolder}");
            
        var randomIndex = Random.Next(imageFiles.Count);
        var imagePath = $"{filePrefix}{randomIndex + 1}.png";
        
        return urlPrefix != null ? $"{urlPrefix}({imagePath})" : imagePath;
    }

    /// <summary>
    /// Gets a random background image
    /// </summary>
    /// <returns>URL string for a random background image</returns>
    public static string GetRandomBackgroundImage()
    {
        return GetRandomImageFromFolder(
            "images/backgrounds",
            "images/backgrounds/bg",
            "url");
    }

    /// <summary>
    /// Gets a random enemy image
    /// </summary>
    /// <returns>Path to a random enemy image</returns>
    public static string GetRandomEnemyImage()
    {
        return GetRandomImageFromFolder(
            "images/enemies",
            "images/enemies/enemy");
    }

    /// <summary>
    /// Gets a random weapon image based on rarity
    /// </summary>
    /// <param name="rarity">The rarity level of the weapon</param>
    /// <returns>Path to a random weapon image of the specified rarity</returns>
    public static string GetRandomWeaponImage(string rarity)
    {
        var (folder, prefix) = rarity.ToLower() switch
        {
            "poor" => ("weapons/poor", "images/weapons/poor/p"),
            "uncommon" => ("weapons/uncommon", "images/weapons/uncommon/u"),
            "rare" => ("weapons/rare", "images/weapons/rare/r"),
            "epic" => ("weapons/epic", "images/weapons/epic/e"),
            "legendary" => ("weapons/legendary", "images/weapons/legendary/l"),
            _ => throw new ArgumentException($"Invalid rarity level: {rarity}")
        };

        return GetRandomImageFromFolder(
            Path.Combine("images", folder),
            prefix);
    }
}
