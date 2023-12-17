using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PlayoffPool.MVC.Extensions
{
	public static class StringExtensions
	{
		public static string GetControllerNameForUri(this string controllerName)
		{
			return controllerName.Replace("Controller", string.Empty);
		}

		public static bool HasValue(this string? value, params string[] ignoreStrings)
		{
			string adjustedString = value?.Trim() ?? string.Empty;

			foreach (var ignoreString in ignoreStrings)
			{
				adjustedString = adjustedString.Replace(ignoreString, string.Empty);
			}

            return !string.IsNullOrWhiteSpace(adjustedString);
        }
	}
}
