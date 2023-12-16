namespace PlayoffPool.MVC.Models
{
    public class BreadcrumbItemModel
    {
        public string Text { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
