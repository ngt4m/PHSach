using Microsoft.AspNetCore.Mvc.Rendering;

namespace PHSach.Helper
{
    public static class HtmlHelpers
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string paths, string cssClass = "active", bool matchStart = false)
        {
            var httpContext = htmlHelper.ViewContext.HttpContext;
            var currentPath = httpContext.Request.Path.Value?.TrimEnd('/');

            if (string.IsNullOrEmpty(currentPath)) currentPath = "/";

            var acceptedPaths = paths.Split(',').Select(p => p.TrimEnd('/'));

            if (matchStart)
            {
                return acceptedPaths.Any(p => currentPath.StartsWith(p, StringComparison.OrdinalIgnoreCase)) ? cssClass : "";
            }
            else
            {
                return acceptedPaths.Contains(currentPath, StringComparer.OrdinalIgnoreCase) ? cssClass : "";
            }
        }
    }

}
