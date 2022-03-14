using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessFakes;
using DataAccessInterfaces;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        IUserManager userManager;

        [TestInitialize]
        public void TestSetup()
        {
            userManager = new UserManager(new UserAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsCorrectHashValue()
        {
            const string source = "newuser";
            const string expectedResult = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            string actualResult = "";

            actualResult = userManager.HashSha256(source);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectEmailPasswordHash()
        {
            // Arrange
            const string email = "tess@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAuthenticateUserFailsWithIncorrectEmail()
        {
            // Arrange
            const string email = "tess-x@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool expectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAuthenticateUserFailsWithIncorrectPasswordHash()
        {
            // Arrange
            const string email = "tess@company.com";
            const string passwordHash = "x9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool expectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAuthenticateUserFailsWithDuplicateEmails()
        {
            // Arrange
            const string email = "duplicate@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool expectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestSelectUserByEmailReturnsCorrectUser()
        {
            // Arrange
            User user = null;
            const int expectedUserId = 999999;
            const string expectedUserEmail = "tess@company.com";
            int actualUserId = 0;

            // Act
            user = userManager.GetUserByEmail(expectedUserEmail);
            actualUserId = user.UserID;

            // Assert
            Assert.AreEqual(expectedUserId, actualUserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectUserByEmailReturnsApplicationException()
        {
            // Arrange
            User user = null;
            const string badUserEmail = "xtess@company.com";

            // Act
            user = userManager.GetUserByEmail(badUserEmail);

            // Assert
            // nothing to do, checking for exception

        }

        [TestMethod]
        public void TestGetRolesForUserReturnsCorrectList()
        {

            var expectedRoles = new List<string>();
            expectedRoles.Add("Logged in");
            expectedRoles.Add("Admin");
            List<string> actualRoles = null;

            actualRoles = userManager.GetRolesForUser(999999);

            CollectionAssert.AreEquivalent(expectedRoles, actualRoles);
        }

        [TestMethod]
        public void TestGetRolesForUserFailsWithIncorrectList()
        {
            var expectedRoles = new List<string>();
            expectedRoles.Add("xRental");
            expectedRoles.Add("Prep");
            List<string> actualRoles = null;

            actualRoles = userManager.GetRolesForUser(999999);

            CollectionAssert.AreNotEquivalent(expectedRoles, actualRoles);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetRolesForBadUserIDThrowsApplicationException()
        {
            const int badUserID = -1;

            userManager.GetRolesForUser(badUserID);
        }

        //[TestMethod]
        //public void TestAuthenticateUsernameAndEmailNotTakenReturnsTrue()
        //{
        //    var expectedResult = true;
        //    const string username = "NotDuplicateMan";
        //    const string email = "notduplicate@company.com";

        //    var actualResult = userManager.AuthenticateUsernameOrEmailNotTaken(username, email);

        //    Assert.AreEqual(expectedResult, actualResult);
        //}

        //[TestMethod]
        //public void TestAuthenticateUsernameAndEmailNotTakenWithDuplicateUsernameReturnsFalse()
        //{
        //    var expectedResult = false;
        //    const string username = "DuplicateMan";
        //    const string email = "notduplicate@company.com";

        //    var actualResult = userManager.AuthenticateUsernameOrEmailNotTaken(username, email);

        //    Assert.AreEqual(expectedResult, actualResult);
        //}

        //[TestMethod]
        //public void TestAuthenticateUsernameAndEmailNotTakenWithDuplicateEmailReturnsFalse()
        //{
        //    var expectedResult = false;
        //    const string username = "NotDuplicateMan";
        //    const string email = "duplicate@company.com";

        //    var actualResult = userManager.AuthenticateUsernameOrEmailNotTaken(username, email);

        //    Assert.AreEqual(expectedResult, actualResult);
        //}

        [TestMethod]
        public void TestInsertNewUserWithNoDuplicatesReturnsCorrectValue()
        {
            var expectedResult = 1;
            const string username = "NotDuplicateMan";
            const string email = "notduplicate@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            List<String> roles = new List<String>();
            roles.Add("Logged in");

            var actualResult = userManager.InsertNewUser(username, email, passwordHash, roles);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertNewUserFailsWithDuplicateUsername()
        {
            const string username = "DuplicateMan";
            const string email = "notduplicate@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            List<String> roles = new List<String>();
            roles.Add("Logged in");

            userManager.InsertNewUser(username, email, passwordHash, roles);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertNewUserFailsWithDuplicateEmail()
        {
            const string username = "NotDuplicateMan";
            const string email = "duplicate@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            List<String> roles = new List<String>();
            roles.Add("Logged in");

            userManager.InsertNewUser(username, email, passwordHash, roles);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertNewUserFailsWithDuplicateUsernameAndEmail()
        {
            const string username = "DuplicateMan";
            const string email = "duplicate@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            List<String> roles = new List<String>();
            roles.Add("Logged in");

            userManager.InsertNewUser(username, email, passwordHash, roles);
        }
    }
}
