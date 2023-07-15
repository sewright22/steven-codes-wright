using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DataLayer.Data
{
    public partial class ReadingList
    {
        public string Devices { get; set; } = string.Empty;
        public List<PumpEvent> Event { get; set; } = new List<PumpEvent>();
        public long GapInData { get; set; }
        public long NoData { get; set; }
        public long DataOutOfRange { get; set; }
        public long Dst { get; set; }
    }

    public partial class PumpEvent
    {
        public double? ActualTotalBolusRequested { get; set; }
        public long? Bg { get; set; }
        public BolusRequestOptions? BolusRequestOptions { get; set; }
        public string BolusType { get; set; } = string.Empty;
        public long? CarbSize { get; set; }
        public long? CorrectionBolusSize { get; set; }
        public long? CorrectionFactor { get; set; }
        public long? DeclinedCorrection { get; set; }
        public long? Duration { get; set; }
        public DateTimeOffset EventDateTime { get; set; }
        public string EventHistoryReportDetails { get; set; } = string.Empty;
        public string EventHistoryReportEventDesc { get; set; } = string.Empty;
        public double? FoodBolusSize { get; set; }
        public long? IsQuickBolus { get; set; }
        public Note? Note { get; set; }
        public DateTimeOffset RequestDateTime { get; set; }
        public Standard? Standard { get; set; }
        public long? StandardPercent { get; set; }
        public long? TargetBg { get; set; }
        public long? UserOverride { get; set; }
        public TypeEnum Type { get; set; }
        public BolusRequestOptions Description { get; set; }
        public long SourceRecId { get; set; }
        public long EventTypeId { get; set; }
        public long IndexId { get; set; }
        public long UploadId { get; set; }
        public long Interactive { get; set; }
        public long TempRateId { get; set; }
        public long TempRateCompleted { get; set; }
        public long TempRateActivated { get; set; }
        public double? Iob { get; set; }
        public long? EventId { get; set; }
        public string? DeviceType { get; set; }
        public long? SerialNumber { get; set; }
        public Egv? Egv { get; set; }
    }

    public partial class Egv
    {
        public long EstimatedGlucoseValue { get; set; }
        public long Hypo { get; set; }
        public long BelowTarget { get; set; }
        public long WithinTarget { get; set; }
        public long AboveTarget { get; set; }
        public long Hyper { get; set; }
    }

    public partial class Note
    {
        public long Id { get; set; }
        public long IndexId { get; set; }
        public long EventTypeId { get; set; }
        public long SourceRecordId { get; set; }
        public long EventId { get; set; }
        public bool Active { get; set; }
    }

    public partial class Standard
    {
        public InsulinDelivered? InsulinDelivered { get; set; }
        public double FoodDelivered { get; set; }
        public long CorrectionDelivered { get; set; }
        public double InsulinRequested { get; set; }
        public long CompletionStatusId { get; set; }
        public string CompletionStatusDesc { get; set; } = string.Empty;
        public long BolusIsComplete { get; set; }
        public long BolusRequestId { get; set; }
        public long BolusCompletionId { get; set; }
    }

    public partial class InsulinDelivered
    {
        public DateTimeOffset CompletionDateTime { get; set; }
        public double Value { get; set; }
    }

    public enum BolusRequestOptions { Egv, Standard };

    public enum TypeEnum { Bolus, Cgm };
}
