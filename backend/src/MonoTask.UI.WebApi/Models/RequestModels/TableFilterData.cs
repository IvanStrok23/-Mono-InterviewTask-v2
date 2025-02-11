namespace MonoTask.UI.WebApi.Models.RequestModels;

public class TableFilterData
{
    public int Page { get; set; }
    public string SortBy { get; set; }
    public string SortOrder { get; set; }
    public string SearchValue { get; set; }

    public TableFilterData()
    {
        Page = 1;
        SortBy = "Name";
        SortOrder = "asc";
        SearchValue = "";
    }

    /// <summary>
    /// Constructor to make new object with custom Search Value
    /// </summary>
    /// <param name="searchValue">Set search value.</param>
    public TableFilterData(string searchValue)
    {
        Page = 1;
        SortBy = "Name";
        SortOrder = "asc";
        SearchValue = searchValue;
    }
}
