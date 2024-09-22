using System;

namespace social_app_backend.Utils;

public class GetOptions
{
    public int Page { get; set; } = 1;
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public string SortOrder { get; set; } = "DESC";
    public string? SearchKey { get; set; }
}
