using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Responses
{
    public partial class FoodLogResponse
    {
        [JsonProperty("foods")]
        public Food[] Foods { get; set; }

        [JsonProperty("summary")]
        public Summary Summary { get; set; }
    }

    public partial class Food
    {
        [JsonProperty("isFavorite")]
        public bool IsFavorite { get; set; }

        [JsonProperty("logDate")]
        public DateTimeOffset LogDate { get; set; }

        [JsonProperty("logId")]
        public long LogId { get; set; }

        [JsonProperty("loggedFood")]
        public LoggedFood LoggedFood { get; set; }

        [JsonProperty("nutritionalValues", NullValueHandling = NullValueHandling.Ignore)]
        public Summary NutritionalValues { get; set; }
    }

    public partial class LoggedFood
    {
        [JsonProperty("accessLevel")]
        public string AccessLevel { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("calories")]
        public long Calories { get; set; }

        [JsonProperty("foodId")]
        public long FoodId { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("mealTypeId")]
        public long MealTypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unit")]
        public Unit Unit { get; set; }

        [JsonProperty("units")]
        public long[] Units { get; set; }
    }

    public partial class Unit
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("plural")]
        public string Plural { get; set; }
    }

    public partial class Summary
    {
        [JsonProperty("calories")]
        public long Calories { get; set; }

        [JsonProperty("carbs")]
        public double Carbs { get; set; }

        [JsonProperty("fat")]
        public double Fat { get; set; }

        [JsonProperty("fiber")]
        public double Fiber { get; set; }

        [JsonProperty("protein")]
        public double Protein { get; set; }

        [JsonProperty("sodium")]
        public double Sodium { get; set; }

        [JsonProperty("water", NullValueHandling = NullValueHandling.Ignore)]
        public long? Water { get; set; }
    }
}
