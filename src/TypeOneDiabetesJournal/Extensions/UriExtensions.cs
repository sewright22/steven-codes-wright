namespace Extensions
{
    public static class UriExtensions
    {
        /// <summary>
        /// Builds a URL for an API request using the provided base URL, endpoint, and query parameters.
        /// </summary>
        /// <param name="builder">The <see cref="UriBuilder"/> instance to use for constructing the URL.</param>
        /// <param name="endpoint">The API endpoint to call.</param>
        /// <param name="queryParams">Optional query parameters to include in the URL.</param>
        /// <returns>A string representing the constructed URL.</returns>
        public static string BuildUrl(this string baseUrl, string endpoint, IDictionary<string, string>? queryParams = null, params string[] additionalPath)
        {
            var builder = new UriBuilder(baseUrl);
            builder.Path = Path.Combine(builder.Path, endpoint, Path.Combine(additionalPath));
            if (queryParams != null)
            {
                builder.Query = new FormUrlEncodedContent(queryParams).ReadAsStringAsync().Result;
            }
            return builder.Uri.ToString();
        }

        /// <summary>
        /// Builds a URL for an API request using the provided base URL, endpoint, date range, and query parameters.
        /// </summary>
        /// <param name="builder">The <see cref="UriBuilder"/> instance to use for constructing the URL.</param>
        /// <param name="endpoint">The API endpoint to call.</param>
        /// <param name="startDate">The start date of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <param name="queryParams">Optional query parameters to include in the URL.</param>
        /// <returns>A string representing the constructed URL.</returns>
        public static string BuildUrlWithDates(this string baseUrl, string endpoint, DateTime startDate, DateTime endDate, IDictionary<string, string>? queryParams = null, params string[] additionalPath)
        {
            var formattedStartDate = startDate.ToString("yyyy-MM-dd");
            var formattedEndDate = endDate.ToString("yyyy-MM-dd");

            var queryString = queryParams != null
                ? string.Join("&", queryParams.Select(q => $"{Uri.EscapeDataString(q.Key)}={Uri.EscapeDataString(q.Value)}"))
                : null;

            var path = $"{endpoint}/{formattedStartDate}/{formattedEndDate}";
            return baseUrl.BuildUrl(path, queryParams, additionalPath);
        }
    }
}