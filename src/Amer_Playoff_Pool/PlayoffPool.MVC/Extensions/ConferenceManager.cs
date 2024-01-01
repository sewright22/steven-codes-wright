namespace PlayoffPool.MVC.Extensions
{
    using AmerFamilyPlayoffs.Data;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class ConferenceManager
    {
        public static IQueryable<SelectListItem> GetConferences(this AmerFamilyPlayoffContext context)
        {
            return context.Conferences
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
        }
    }
}
