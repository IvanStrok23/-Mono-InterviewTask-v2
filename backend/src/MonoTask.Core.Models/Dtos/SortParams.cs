using MonoTask.Core.Models.Helpers;

namespace MonoTask.Core.Models.Dtos;

public class SortParams
{
    public SortbyEnum SortBy { get; set; }
    public SortOrderEnum SortOrder { get; set; }
    public string SearchValue { get; set; }

    public SortParams()
    {
        SortBy = SortbyEnum.Name;
        SortOrder = SortOrderEnum.Desc;
        SearchValue = "";
    }
    public SortParams(SortbyEnum sortBy, SortOrderEnum sortOrder, string searchValue)
    {
        SortBy = sortBy;
        SortOrder = sortOrder;
        SearchValue = searchValue;
    }
}
