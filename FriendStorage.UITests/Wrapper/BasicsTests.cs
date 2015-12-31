using System;
using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass()]
    public class BasicsTests    
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

        [TestMethod()]
        public void ShouldContainModelInModelProperty()
        {
            //Act
            var wrapper = new FriendWrapper(_friend);
            //Assert
            Assert.AreEqual(_friend, wrapper.Model);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        {
            try
            {
                var wrapper = new FriendWrapper(null);
            }
            catch (ArgumentNullException exp)
            {
                Assert.AreEqual("model", exp.ParamName);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentNullExceptionIfAddressIsNull()
        {
            try
            {
                _friend.Address = null;
                var wrapper = new FriendWrapper(_friend);
            }
            catch (ArgumentException exp)
            {
                Assert.AreEqual("Address cannot b null", exp.Message);
                throw;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentNullExceptionIfEmailsCollectionIsNull()
        {
            try
            {
                _friend.Emails = null;
                var wrapper = new FriendWrapper(_friend);
            }
            catch (ArgumentException exp)
            {
                Assert.AreEqual("Emails cannot b null", exp.Message);
                throw;
            }
        }

        [TestMethod]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend) {FirstName = "Raheel"};

            Assert.AreEqual("Raheel",_friend.FirstName);
        }
        [TestMethod]
        public void ShouldGetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);

            Assert.AreEqual(wrapper.FirstName, _friend.FirstName);
        }
    }
}