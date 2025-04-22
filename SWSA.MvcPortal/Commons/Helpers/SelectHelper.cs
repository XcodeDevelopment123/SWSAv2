using Microsoft.AspNetCore.Mvc.Rendering;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Extensions;

namespace SWSA.MvcPortal.Commons.Helpers;

public class SelectHelper
{
    public static List<SelectListItem> GetEnumSelectList<TEnum>(TEnum? selected = null, string defaultText = "Please select")
        where TEnum : struct, Enum
    {
        return ToSelectList(selected, defaultText);
    }

    public static List<SelectListItem> GetEnableDisabledSelection(bool? isEnabled)
    {
        return new List<SelectListItem>
        {
            new SelectListItem { Text = "Please select", Value = "", Selected = isEnabled == null,Disabled=true },
            new SelectListItem { Text = "Enable", Value = "true", Selected = isEnabled == true },
            new SelectListItem { Text = "Disable", Value = "false", Selected = isEnabled  == false }
        };
    }

    public static List<SelectListItem> GetYesNoSelection(bool? yesOrNo)
    {
        return new List<SelectListItem>
        {
            new SelectListItem { Text = "Please select", Value = "", Selected = yesOrNo == null,Disabled=true },
            new SelectListItem { Text = "Yes", Value = "true", Selected = yesOrNo == true },
            new SelectListItem { Text = "No", Value = "false", Selected = yesOrNo  == false }
        };
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
            var displayName = value.GetDisplayName();

            selectList.Add(new SelectListItem
            {
                Value = value.ToString(),
                Text = displayName,
                Selected = selected != null && value.Equals(selected)
            });
        }

        return selectList;
    }
}
