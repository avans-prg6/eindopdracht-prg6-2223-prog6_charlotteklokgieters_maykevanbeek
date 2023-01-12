using LogicLayer.KidValidation;
using LogicLayer.Santa;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using SantasWishlist.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class ChildTests
    {
        [TestMethod]
        public void AddError_valid()
        {
            //Arrange
            string error = "test";
            List<string> list = new List<string>();

            //Act
            WishlistValidation.AddError(error, list);

            //Assert
            Assert.IsTrue(list.Count == 1);
        }

        [TestMethod]
        public void CertainGiftAmount_valid()
        {
            List<string> presents = new List<string>();
            string present1 = "Lego";
            string present2 = "Ondergoed";
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.CertainGiftAmount(presents, Santa_WishList.Models.Enums.Niceness.Very_Nice, true, "gerrie", "ik ben lief");

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 0);
        }

        [TestMethod]
        public void CertainGiftAmount_LiedAboutNiceness_Invalid()
        {
            //Arrange
            List<string> presents = new List<string>();
            string present1 = "Lego";
            string present2 = "Ondergoed";
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.CertainGiftAmount(presents, Santa_WishList.Models.Enums.Niceness.Very_Nice, false, "gerrie", "ik ben lief");

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void CertainGiftAmount_OnlyOnePresent_Invalid()
        {
            List<string> presents = new List<string>();
            string present1 = "Lego";
            string present2 = "Roblox";
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.CertainGiftAmount(presents, Santa_WishList.Models.Enums.Niceness.Naughty, false, "gerrie", "ik ben lief");

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void CertainGiftAmount_OnlyThreePresents_Invalid()
        {
            List<string> presents = new List<string>();
            string present1 = "Lego";
            string present2 = "Roblox";
            string present3 = "Poppen";
            string present4 = "Stikkers";
            presents.Add(present1);
            presents.Add(present2);
            presents.Add(present3);
            presents.Add(present4);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.CertainGiftAmount(presents, Santa_WishList.Models.Enums.Niceness.Very_Nice, true, "gerrie", "ik ben lief");

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void GiftCombinations_Valid()
        {
            //Arrange
            List<string> presents = new List<string>();
            string present1 = "Lego";
            string present2 = "Ondergoed";
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.GiftCombinations(presents);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 0);
        }

        [TestMethod]
        public void GiftCombinations_Lego_InValid()
        {
            //Arrange
            List<string> presents = new List<string>();
            string present1 = "Lego";
            string present2 = "K`nex";
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.GiftCombinations(presents);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void GiftCombinations_LegoDummies_InValid()
        {
            //Arrange
            List<string> presents = new List<string>();
            string present1 = "lego for dummies";
            string present2 = "Knex for dummies";
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.GiftCombinations(presents);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void GiftCombinations_NightLight_InValid()
        {
            //Arrange
            List<string> presents = new List<string>();
            string present1 = "Nachtlampje";
            string present2 = "Lego";
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.GiftCombinations(presents);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void GiftCombinations_Music_InValid()
        {
            //Arrange
            List<string> presents = new List<string>();
            string present1 = "Muziekinstrument";
            string present2 = "Lego";
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.GiftCombinations(presents);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void DivertFromAgeRating_Valid()
        {
            //Arrange
            int age = 18;
            string present1 = "Roblox";
            string present2 = "Computerspel";
            List<string> presents = new List<string>();
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.DivertFromAgeRating(presents, age);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 0);
        }

        [TestMethod]
        public void DivertFromAgeRating_InValid()
        {
            //Arrange
            int age = 3;
            string present1 = "Roblox";
            string present2 = "Computerspel";
            List<string> presents = new List<string>();
            presents.Add(present1);
            presents.Add(present2);
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.DivertFromAgeRating(presents, age);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void DivertFromAgeRating_InValidAge()
        {
            //Arrange
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);
            List<string> presents = new List<string>();

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.DivertFromAgeRating(presents, null);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void GiftAvailabilityInList_Valid()
        {
            //Arrange
            string[] presents = new string[1];
            string gift = "Pannenkoeken";
            presents[0] = gift;
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.GiftAvailibilityInList(presents);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 0);
        }

        [TestMethod]
        public void GiftAvailabilityInList_InValid()
        {
            //Arrange
            string[] presents = new string[1];
            string gift = "Lego";
            presents[0] = gift;
            IGiftRepository repository = new GiftRepository();
            WishlistValidation validation = new WishlistValidation(repository);

            //Act
            validation.ResetErrors(validation.GetErrors());
            validation.GiftAvailibilityInList(presents);

            //Assert
            Assert.IsTrue(validation.GetErrorCount() == 1);
        }

        [TestMethod]
        public void Rule9_Valid()
        {
            //Arrange
            string name = "mayke";
            Mock<IGiftRepository> giftMock = new Mock<IGiftRepository>();
            WishlistValidation validation = new WishlistValidation(giftMock.Object);

            //Act
            bool test = validation.Rule9(name);

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void Rule9_InValid()
        {
            //Arrange
            string name = "stijn";
            Mock<IGiftRepository> giftMock = new Mock<IGiftRepository>();
            WishlistValidation validation = new WishlistValidation(giftMock.Object);

            //Act
            bool test = validation.Rule9(name);

            //Assert
            Assert.IsTrue(!test);
        }
    }
}

