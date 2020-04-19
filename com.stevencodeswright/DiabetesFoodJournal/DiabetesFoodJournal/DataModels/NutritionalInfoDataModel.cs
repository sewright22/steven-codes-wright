using DiabetesFoodJournal.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.DataModels
{
    public class NutritionalInfoDataModel : ObservableObject, IDataModel<NutritionalInfo>
    {
        private int id;
        private int calories;
        private int protein;
        private int carbohydrates;

        [JsonIgnore]
        public NutritionalInfo Model
        {
            get;
            protected set;

        }
        public int Id { get { return this.id; } set { SetProperty(ref this.id, value); } }
        public int Calories { get { return this.calories; } set { SetProperty(ref this.calories, value); } }
        public int Protein { get { return this.protein; } set { SetProperty(ref this.protein, value); } }
        public int Carbohydrates { get { return this.carbohydrates; } set { SetProperty(ref this.carbohydrates, value); } }

        public bool IsChanged
        {
            get
            {
                return this.Model.Id != this.id ||
                this.Model.Calories != this.calories ||
                this.Model.Protein != this.protein ||
                this.Model.Carbohydrates != this.carbohydrates;
            }
        }

        public void Load(NutritionalInfo model)
        {
            this.id = model.Id;
            this.calories = model.Calories;
            this.protein = model.Protein;
            this.carbohydrates = model.Carbohydrates;
            Model = model;
        }

        public NutritionalInfo Copy()
        {
            var retVal = new NutritionalInfo();

            retVal.Calories = this.calories;
            retVal.Protein = this.protein;
            retVal.Carbohydrates = this.carbohydrates;

            return retVal;
        }
        public NutritionalInfo Save()
        {
            if (Model == null)
            {
                Model = new NutritionalInfo();

            }

            this.Model.Id = this.id;
            this.Model.Calories = this.calories;
            this.Model.Protein = this.protein;
            this.Model.Carbohydrates = this.carbohydrates;

            return this.Model;
        }
    }

}
