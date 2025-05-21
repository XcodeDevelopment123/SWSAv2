using Microsoft.AspNetCore.Mvc.Rendering;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Filters;

namespace SWSA.MvcPortal.Commons.Helpers;

public class SelectHelper
{
    public static List<SelectListItem> GetEnumSelectList<TEnum>(TEnum? selected = null, string defaultText = "Please select")
        where TEnum : struct, Enum
    {
        return ToSelectList(selected, defaultText);
    }

    public static List<SelectListItem> GetDepartments(string? type)
    {
        return new List<SelectListItem>
        {
            new SelectListItem { Text = "Please select", Value = "", Selected = false, Disabled=true },
            new SelectListItem { Text = DepartmentType.Account, Value = DepartmentType.Account, Selected = type == DepartmentType.Account },
            new SelectListItem { Text = DepartmentType.Audit, Value = DepartmentType.Audit, Selected = type == DepartmentType.Audit },
            new SelectListItem { Text = DepartmentType.Tax, Value = DepartmentType.Tax, Selected = type == DepartmentType.Tax },
            new SelectListItem { Text = DepartmentType.Secretary, Value = DepartmentType.Secretary, Selected = type == DepartmentType.Secretary },
        };
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

    public static List<SelectListItem> GetUserRoleList(UserRole? role, UserRole loggedUserRole)
    {
        if (loggedUserRole != UserRole.SuperAdmin)
            return ToSelectList(role, filter: x => x != UserRole.SuperAdmin);

        return ToSelectList(role);
    }

    public static List<SelectListItem> GetOwnerPositionTypes(PositionType? type)
    {
        return ToSelectList<PositionType>(type,
            filter: x => x == PositionType.Director || x == PositionType.Shareholder || x == PositionType.Other);
    }

    public static List<SelectListItem> GetCompanyCommunicationContactPositionTypes(PositionType? type)
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
            var fieldInfo = typeof(TEnum).GetField(value.ToString());
            var ignore = fieldInfo?.GetCustomAttributes(typeof(EnumIgnoreAttribute), false).Any() ?? false;
            if (ignore)
                continue;

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
