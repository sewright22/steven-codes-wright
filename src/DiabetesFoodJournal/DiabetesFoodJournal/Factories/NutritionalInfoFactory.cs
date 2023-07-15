using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.WebApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Factories
{
    public class NutritionalInfoFactory : INutritionalInfoFactory
    {
        public NutritionalInfo Build()
        {
            return new NutritionalInfo();
        }

        public NutritionalInfo Build(NutritionalInfoWebApiModel nutritionalInfo)
        {
            var retVal = Build();

            retVal.Id = nutritionalInfo.Id;
            retVal.Calories = nutritionalInfo.Calories;
            retVal.Carbohydrates = nutritionalInfo.Carbohydrates;
            retVal.Protein = nutritionalInfo.Protein;

            return retVal;
        }
    }

    public interface INutritionalInfoFactory
    {
        NutritionalInfo Build();
        NutritionalInfo Build(NutritionalInfoWebApiModel nutritionalInfo);
    }
}
