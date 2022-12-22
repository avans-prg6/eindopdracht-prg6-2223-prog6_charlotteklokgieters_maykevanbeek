using LogicLayer.General;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Santa_WishList.Controllers;
using Santa_WishList.Models;
using Santa_WishList.Models.Viewmodels;
using SantasWishlist_Data;
using System.ComponentModel.DataAnnotations;

namespace TestProject
{
    [TestClass]
    public class SantaTests
    {
        [TestMethod]
        public void NoDubbleNamesAllowed_Invalid()
        {
            //Arrange
            string KidsNames = "hi, hi";

            //Act
            ValidationResult result = ValidationMethods.CheckDubbleNames(KidsNames, new ValidationContext(KidsNames, null, null));

            //Assert
            Assert.IsTrue(result != ValidationResult.Success);
        }

        [TestMethod]
        public void NoDubbleNamesAllowed_Valid()
        {
            //Arrange
            string KidsNames = "hi, hello";

            //Act
            ValidationResult result = ValidationMethods.CheckDubbleNames(KidsNames, new ValidationContext(KidsNames, null, null));

            //Assert
            Assert.IsTrue(result == ValidationResult.Success);
        }

        [TestMethod]
        public void SplittingNames_OneName_ValidOption1()
        {
            ////Arrange
            string names = "hi, hi";

            //Act
            string[] list = General.SplitString(names);

            //Assert
            Assert.IsTrue(list.Length == 2);
        }

        [TestMethod]
        public void SplittingNames_OneName_ValidOption2()
        {
            //Arrange
            string names = "hi,hi";

            //Act
            string[] list = General.SplitString(names);

            //Assert
            Assert.IsTrue(list.Length == 2);
        }

        [TestMethod]
        public void SplittingNames_OneName()
        {
            //Arrange
            string names = "hi";

            //Act
            string[] list = General.SplitString(names);

            //Assert
            Assert.IsTrue(list.Length == 1);
        }
    }
}