using System.Text.RegularExpressions;

namespace SlugBasedApi.Models;

public partial class Item
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public required int Year { get; set; }
    public string Slug => GenerateSlug();

    private string GenerateSlug()
    {
        var sluggedTitle = SlugRegex().Replace(Description, string.Empty)
            .ToLower().Replace(" ", "-");
        return $"{sluggedTitle}-{Year}";
    }

    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex SlugRegex();
}
