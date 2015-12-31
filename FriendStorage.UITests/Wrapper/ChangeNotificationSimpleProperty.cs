using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{   
    [TestClass()]
    public class ChangeNotificationSimpleProperty
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
        public void ShouldRaisePropertyChangedEventOnPropertyChange()
        {
            var eventRaised = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                eventRaised = true;
            };
            wrapper.FirstName = "Raheel";
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void ShouldNotRaisePropertyChangedEventIfPropertyIsSetToSameValue()
        {
            var eventRaised = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                eventRaised = true;
            };
            wrapper.FirstName = "Babar";
            Assert.IsFalse(eventRaised);
        }
    }
}