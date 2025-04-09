namespace SWSA.MvcPortal.Commons.Helpers;


//No going to use
public class HeaderHelper(string controller, string action)
{
    private string Controller { get; set; } = controller;

    private string Action { get; set; } = action;

    public string GetHeader()
    {
        return $"""
             <div class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1 class="m-0">Dashboard</h1>
                        </div><!-- /.col -->
                        <div class="col-sm-6">
                         {GetBreadcrumb()}
                        </div><!-- /.col -->
                    </div><!-- /.row -->
                </div><!-- /.container-fluid -->
            </div>
            """;
    }

    private string GetBreadcrumb()
    {
        return @"""
                 <ol class=""breadcrumb float-sm-right"">
                    <li class=""breadcrumb-item""><a href=""#"">Home</a></li>
                    <li class=""breadcrumb-item active"">Dashboard v1</li>
                 </ol>
     
                """;
    }
}

public class PageMeta
{
    public string Title { get; set; } = string.Empty; 
    public List<BreadcrumbItem> Breadcrumbs { get; set; } = new();
}

public class BreadcrumbItem
{
    public string Label { get; set; } = string.Empty;
    public string? Url { get; set; } // null 表示当前页面（不可点）

    public BreadcrumbItem(string label, string? url = null)
    {
        Label = label;
        Url = url;
    }
}