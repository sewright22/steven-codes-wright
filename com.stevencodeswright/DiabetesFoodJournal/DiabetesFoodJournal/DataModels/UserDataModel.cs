using DiabetesFoodJournal.Entities;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.DataModels
{
    public class UserDataModel : ObservableObject, IDataModel<User>
    {
        private int id;
        private string email;
        private string password;

        [JsonIgnore]
        public User Model { get; protected set; }

        public int Id { get { return this.id; } set { SetProperty(ref this.id, value); } }
        public string Email { get { return this.email; } set { SetProperty(ref this.email, value); } }
        public string Password { get { return this.password; } set { SetProperty(ref this.password, value); } }

        [JsonIgnore]
        public bool IsChanged
        {
            get
            {
                return this.Model.Id != this.id ||
                       this.Model.Email != this.email ||
                       this.Model.Password != this.password;
            }
        }

        public User Copy()
        {
            throw new NotImplementedException();
        }

        public void Load(User model)
        {
            Model = model;

            this.Id = model.Id;
            this.Email = model.Email;
            this.Password = model.Password;
        }

        public User Save()
        {
            this.Model.Id = this.id;
            this.Model.Email = this.email;
            this.Model.Password = this.password;

            return Model;
        }
    }
}
