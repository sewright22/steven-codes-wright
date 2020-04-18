using DiabetesFoodJournal.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.DataModels
{
    public class TagDataModel : ObservableObject, IDataModel<Tag>
    {
        private int id;
        private string description;

        [JsonIgnore]
        public Tag Model
        {
            get;
            protected set;

        }
        public int Id { get { return this.id; } set { SetProperty(ref this.id, value); } }
        public string Description { get { return this.description; } set { SetProperty(ref this.description, value); } }

        public bool IsChanged
        {
            get
            {
                return this.Model.Id != this.id ||
                this.Model.Description != this.description;
            }
        }

        public void Load(Tag model)
        {
            this.id = model.Id;
            this.description = model.Description;
            Model = model;
        }

        public Tag Save()
        {
            if (Model == null)
            {
                Model = new Tag();

            }

            this.Model.Id = this.id;
            this.Model.Description = this.description;

            return this.Model;
        }
    }

}
