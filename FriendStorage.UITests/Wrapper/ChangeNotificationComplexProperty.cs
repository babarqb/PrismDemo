using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass()]
    public class ChangeNotificationComplexProperty
    {
        private Friend _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new Friend()
            {
                FirstName = "Babar",
                Address = new Address(),
                Emails = new List<FriendEmail>()

            };
        }
        [TestMethod]
        public void ShouldIntializeAddressProperty()
        {
            var warpper = new FriendWrapper(_friend);

            Assert.IsNotNull(warpper.Address);
            Assert.AreEqual(_friend.Address, warpper.Model.Address);
        }  
    }
}