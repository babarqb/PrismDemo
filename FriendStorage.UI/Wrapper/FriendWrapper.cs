using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FriendStorage.Model;
using FriendStorage.UI.ViewModel;

namespace FriendStorage.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Friend>
    {
        public FriendWrapper(Friend model):base(model)
        {
            IntializeComplexProperties(model);
            IntializeCollectionProperties(model);
        }

        private void IntializeCollectionProperties(Friend model)
        {
            if (model.Emails == null)
            {
                throw new ArgumentException("Emails cannot b null");
            }
            Emails = new ObservableCollection<FriendEmailWrapper>(model.Emails.Select(e => new FriendEmailWrapper(e)));
            RegisterCollection(Emails, model.Emails);
        }

        

        private void IntializeComplexProperties(Friend model)
        {
            if (model.Address == null)
            {
                throw new ArgumentException("Address cannot b null");
            }
            Address = new AddressWrapper(model.Address);
        }

        //With ModelWrapper Methods
        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FriendGroupId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value);}
        }
        
        public DateTime? Birthday 
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public bool IsDeveloper
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public AddressWrapper Address { get; private set; }
        public ObservableCollection<FriendEmailWrapper> Emails { get; private set; }



        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string FirstNameOrignalValue => GetOrignalValue<string>(nameof(FirstName));

        //Without ModelWrapper Methods
        //public string FirstName
        //{
        //    get { return Model.FirstName; }
        //    set
        //    {
        //        if (!Equals(Model.FirstName, value))
        //        {
        //            Model.FirstName = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
    }
}