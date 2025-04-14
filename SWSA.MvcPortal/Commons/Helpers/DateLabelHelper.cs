using SWSA.MvcPortal.Commons.Constants;

namespace SWSA.MvcPortal.Commons.Helpers;

public class DateLabelHelper
{
    public static string GetDateOnlyLabel(DateTime? date)
    {
        if (date == null)
        {
            return AppSettings.NotAvailable;
        }

        return date.Value.ToString("dd/MM/yyyy");
    }
}
