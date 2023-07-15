using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Services.External
{
    public partial class FitbitProfile
    {
        [JsonProperty("user")]
        public FitbitUser User { get; set; }
    }

    [JsonObject("user")]
    public partial class FitbitUser
    {
        [JsonProperty("age")]
        public long Age { get; set; }

        [JsonProperty("ambassador")]
        public bool Ambassador { get; set; }

        [JsonProperty("autoStrideEnabled")]
        public bool AutoStrideEnabled { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("avatar150")]
        public Uri Avatar150 { get; set; }

        [JsonProperty("avatar640")]
        public Uri Avatar640 { get; set; }

        [JsonProperty("averageDailySteps")]
        public long AverageDailySteps { get; set; }

        [JsonProperty("challengesBeta")]
        public bool ChallengesBeta { get; set; }

        [JsonProperty("clockTimeDisplayFormat")]
        public string ClockTimeDisplayFormat { get; set; }

        [JsonProperty("corporate")]
        public bool Corporate { get; set; }

        [JsonProperty("corporateAdmin")]
        public bool CorporateAdmin { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTimeOffset DateOfBirth { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("displayNameSetting")]
        public string DisplayNameSetting { get; set; }

        [JsonProperty("distanceUnit")]
        public string DistanceUnit { get; set; }

        [JsonProperty("encodedId")]
        public string EncodedId { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("foodsLocale")]
        public string FoodsLocale { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("glucoseUnit")]
        public string GlucoseUnit { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("heightUnit")]
        public string HeightUnit { get; set; }

        [JsonProperty("isBugReportEnabled")]
        public bool IsBugReportEnabled { get; set; }

        [JsonProperty("isChild")]
        public bool IsChild { get; set; }

        [JsonProperty("isCoach")]
        public bool IsCoach { get; set; }

        [JsonProperty("languageLocale")]
        public string LanguageLocale { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("legalTermsAcceptRequired")]
        public bool LegalTermsAcceptRequired { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("memberSince")]
        public DateTimeOffset MemberSince { get; set; }

        [JsonProperty("mfaEnabled")]
        public bool MfaEnabled { get; set; }

        [JsonProperty("offsetFromUTCMillis")]
        public long OffsetFromUtcMillis { get; set; }

        [JsonProperty("sdkDeveloper")]
        public bool SdkDeveloper { get; set; }

        [JsonProperty("sleepTracking")]
        public string SleepTracking { get; set; }

        [JsonProperty("startDayOfWeek")]
        public string StartDayOfWeek { get; set; }

        [JsonProperty("strideLengthRunning")]
        public double StrideLengthRunning { get; set; }

        [JsonProperty("strideLengthRunningType")]
        public string StrideLengthRunningType { get; set; }

        [JsonProperty("strideLengthWalking")]
        public double StrideLengthWalking { get; set; }

        [JsonProperty("strideLengthWalkingType")]
        public string StrideLengthWalkingType { get; set; }

        [JsonProperty("swimUnit")]
        public string SwimUnit { get; set; }

        [JsonProperty("temperatureUnit")]
        public string TemperatureUnit { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("topBadges")]
        public TopBadge[] TopBadges { get; set; }

        [JsonProperty("visibleUser")]
        public bool VisibleUser { get; set; }

        [JsonProperty("waterUnit")]
        public string WaterUnit { get; set; }

        [JsonProperty("waterUnitName")]
        public string WaterUnitName { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("weightUnit")]
        public string WeightUnit { get; set; }
    }

    public partial class Features
    {
        [JsonProperty("exerciseGoal")]
        public bool ExerciseGoal { get; set; }
    }

    public partial class TopBadge
    {
        [JsonProperty("badgeGradientEndColor")]
        public string BadgeGradientEndColor { get; set; }

        [JsonProperty("badgeGradientStartColor")]
        public string BadgeGradientStartColor { get; set; }

        [JsonProperty("badgeType")]
        public string BadgeType { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("cheers")]
        public object[] Cheers { get; set; }

        [JsonProperty("dateTime")]
        public DateTimeOffset DateTime { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("earnedMessage")]
        public string EarnedMessage { get; set; }

        [JsonProperty("encodedId")]
        public string EncodedId { get; set; }

        [JsonProperty("image100px")]
        public Uri Image100Px { get; set; }

        [JsonProperty("image125px")]
        public Uri Image125Px { get; set; }

        [JsonProperty("image300px")]
        public Uri Image300Px { get; set; }

        [JsonProperty("image50px")]
        public Uri Image50Px { get; set; }

        [JsonProperty("image75px")]
        public Uri Image75Px { get; set; }

        [JsonProperty("marketingDescription")]
        public string MarketingDescription { get; set; }

        [JsonProperty("mobileDescription")]
        public string MobileDescription { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shareImage640px")]
        public Uri ShareImage640Px { get; set; }

        [JsonProperty("shareText")]
        public string ShareText { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("timesAchieved")]
        public long TimesAchieved { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }
    }
}
