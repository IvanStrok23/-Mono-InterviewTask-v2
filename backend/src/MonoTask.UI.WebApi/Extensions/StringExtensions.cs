using MonoTask.Core.Models.Helpers;

namespace MonoTask.UI.WebApi.Extensions;

public static class StringExtensions
{

    public static SortbyEnum ToSortByEnum(this String str)
    {
        switch (str)
        {
            case "Name":
                return SortbyEnum.Name;
            case "Country":
                return SortbyEnum.Country;
            case "Make":
                return SortbyEnum.MakeName;
            case "Year":
                return SortbyEnum.Year;
            default:
                return SortbyEnum.Name;
        }
    }

    public static SortOrderEnum ToSortOrderEnum(this String str)
    {
        switch (str)
        {
            case "asc":
                return SortOrderEnum.Asc;
            case "desc":
                return SortOrderEnum.Desc;
            default:
                return SortOrderEnum.Asc;
        }
    }
}
