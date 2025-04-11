using Microsoft.AspNetCore.Mvc.Rendering;
using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Helpers;

public class SelectHelper
{
    public static List<SelectListItem> GetDocumentFLowType(DocumentFlowType? type)
    {
        return ToSelectList<DocumentFlowType>(type);
    }
    public static List<SelectListItem> GetCompanyStatus(CompanyStatus? status)
    {
        return ToSelectList<CompanyStatus>(status);
    }

    public static List<SelectListItem> GetOwnerPositionTypes(PositionType? type)
    {
        return ToSelectList<PositionType>(type,
            filter: x => x == PositionType.Director || x == PositionType.Shareholder || x == PositionType.Other);
    }

    public static List<SelectListItem> GetStaffPositionTypes(PositionType? type)
    {
        return ToSelectList<PositionType>(type,
                    filter: x => x != PositionType.Director && x != PositionType.Shareholder);
    }

    public static List<SelectListItem> GetOwnershipTypes(OwnershipType? type)
    {
        return ToSelectList<OwnershipType>(type);
    }

    private static List<SelectListItem> ToSelectList<TEnum>(
        TEnum? selected = null,
        string defaultText = "Please select",
        Func<TEnum, bool>? filter = null)
        where TEnum : struct, Enum
    {
        var selectList = new List<SelectListItem>();

        if (!string.IsNullOrEmpty(defaultText))
        {
            selectList.Add(new SelectListItem
            {
                Value = "",
                Text = defaultText,
                Selected = selected == null,
                Disabled = true
            });
        }

        var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

        if (filter != null)
            values = values.Where(filter);

        foreach (var value in values)
        {
            var displayName = GetEnumDisplayName(value);

            selectList.Add(new SelectListItem
            {
                Value = value.ToString(),
                Text = displayName,
                Selected = selected != null && value.Equals(selected)
            });
        }

        return selectList;
    }

    private static string GetEnumDisplayName<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var fieldInfo = typeof(TEnum).GetField(value.ToString()!);

        var displayAttribute = fieldInfo?
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .FirstOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? SplitCamelCase(value.ToString()!);
    }

    private static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1");
    }
}
