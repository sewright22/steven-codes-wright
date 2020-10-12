using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TypeOneFoodJournal.Entities;

namespace TypeOneFoodJournal.Business
{
    public static class DoseExtensions
    {
        public static decimal GetUpFrontUnits(this Dose dose)
        {
            var upFrontPercentAsDecimal = dose.UpFront / (decimal)100;
            return dose.InsulinAmount * upFrontPercentAsDecimal;
        }

        public static decimal GetExtendedAsUnits(this Dose dose)
        {
            var extendedPercentAsDecimal = dose.Extended / (decimal)100;
            return dose.InsulinAmount * extendedPercentAsDecimal;
        }

        public static int GetTimeExtendedHours(this Dose dose)
        {
            return (int)Math.Floor(dose.TimeExtended);
        }

        public static int GetTimeExtendedMinutes(this Dose dose)
        {
            var extendedHours = dose.GetTimeExtendedHours();
            return (int)((dose.TimeExtended - extendedHours) * 60);
        }
    }
}
