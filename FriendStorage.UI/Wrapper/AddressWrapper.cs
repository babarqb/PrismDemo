using FriendStorage.Model;

namespace FriendStorage.UI.Wrapper
{
    public class AddressWrapper : ModelWrapper<Address>
    {
        public AddressWrapper(Address model) : base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public string City
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Street
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string StreetNumber 
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}