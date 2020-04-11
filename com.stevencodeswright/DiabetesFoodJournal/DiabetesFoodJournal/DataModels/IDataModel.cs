using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.DataModels
{
    public interface IDataModel<TModel>
    {
        TModel Model { get; }
        void Load(TModel model);
        TModel Save();
        bool IsChanged { get; }
    }
}
