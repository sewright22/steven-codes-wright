using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.WebApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Factories
{
    public class DoseFactory : IDoseFactory
    {
        public Dose Build()
        {
            return new Dose();
        }

        public Dose Build(DoseWebApiModel doseWebApiModel)
        {
            var retVal = Build();

            retVal.Id = doseWebApiModel.Id;
            retVal.InsulinAmount = doseWebApiModel.InsulinAmount;
            retVal.TimeExtended = doseWebApiModel.TimeExtended;
            retVal.TimeOffset = doseWebApiModel.TimeOffset;
            retVal.UpFront = doseWebApiModel.UpFront;
            retVal.Extended = doseWebApiModel.Extended;

            return retVal;
        }
    }

    public interface IDoseFactory
    {
        Dose Build();
        Dose Build(DoseWebApiModel doseWebApiModel);
    }
}
