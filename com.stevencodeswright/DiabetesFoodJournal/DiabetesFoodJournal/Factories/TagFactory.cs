using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.WebApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Factories
{
    public class TagFactory : ITagFactory
    {
        public Tag Build()
        {
            return new Tag();
        }
        public Tag Build(TagWebApiModel tag)
        {
            var retVal = Build();

            retVal.Id = tag.Id;
            retVal.Description = tag.Description;

            return retVal;
        }
    }

    public interface ITagFactory
    {
        Tag Build();
        Tag Build(TagWebApiModel tag);
    }
}
