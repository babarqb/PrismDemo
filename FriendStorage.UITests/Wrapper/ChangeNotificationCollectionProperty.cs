using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass]
    public class ChangeNotificationCollectionProperty
    {
        private Friend _friend;
        private FriendEmail _friendEmail;

        [TestInitialize]
        public void Initialize()
        {
            _friendEmail = new FriendEmail() { Email = "babarqb@yahoo.com" };
            _friend = new Friend
            {
                FirstName = "Babar",
                Address = new Address(),
                Emails = new List<FriendEmail>()
                {
                    new FriendEmail() {Email = "babarqb@gmail.com"},
                    _friendEmail
                }
            };
        }
        [TestMethod]
        public void ShouldInitializeEmialProperty()
        {
            var wrapper  = new FriendWrapper(_friend);
            Assert.IsNotNull(wrapper.Emails);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }
        [TestMethod]
        public void ShouldbInSyncAfterAddingEmail()
        {
            _friend.Emails.Remove(_friendEmail);
            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Add(new FriendEmailWrapper(_friendEmail));
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }
        [TestMethod]
        public void ShouldbInSyncAfterRemovingEmail()
        {
            var wrapper = new FriendWrapper(_friend);
            var emailToRemove = wrapper.Emails.Single(ew => ew.Model == _friendEmail);
            wrapper.Emails.Remove(emailToRemove);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        private void CheckIfModelEmailsCollectionIsInSync(FriendWrapper wrapper)
        {
            Assert.AreEqual(_friend.Emails.Count, wrapper.Model.Emails.Count);
            Assert.IsTrue(_friend.Emails.All(e => wrapper.Emails.Any(we => we.Model == e)));
        }
    }
}