using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass()]
    public class ChangeTrackingSimpleProperty
    {
        private Friend _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new Friend()
            {
                FirstName = "Babar",
                LastName = "Raheel",
                Address = new Address(),
                Emails = new List<FriendEmail>()
            };
        }
        [TestMethod]
        public void ShouldStoreOringnalValue()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.AreEqual("Babar",wrapper.FirstNameOrignalValue);

            wrapper.FirstName = "Raheel";
            Assert.AreEqual("Babar", wrapper.FirstNameOrignalValue);
           
            Assert.AreEqual("Raheel", wrapper.FirstName);
        }
    }
}