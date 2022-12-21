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
        //SantaDbContext _context { get; set; }

        //public SantaTests(SantaDbContext dbcontext)
        //{
        //    _context = dbcontext;
        //}
        //[TestMethod]
        //public void CanRegisterAccounts()
        //{
        //    //Arrange
        //    AccountInput input = new AccountInput();
        //    input.Name = "unittest";
        //    input.Password = "unittest";
        //    input.IsNice = true;

        //    //Act

        //    //Assert
        //}

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
            SantaController controller = new SantaController();
            string names = "hi, hi";

            //Act
            string[] list = controller.SplitNames(names);

            //Assert
            Assert.IsTrue(list.Length == 2);
        }

        [TestMethod]
        public void SplittingNames_OneName_ValidOption2()
        {
            //Arrange
            SantaController controller = new SantaController();
            string names = "hi,hi";

            //Act
            string[] list = controller.SplitNames(names);

            //Assert
            Assert.IsTrue(list.Length == 2);
        }

        [TestMethod]
        public void SplittingNames_OneName()
        {
            //Arrange
            SantaController controller = new SantaController();
            string names = "hi";

            //Act
            string[] list = controller.SplitNames(names);

            //Assert
            Assert.IsTrue(list.Length == 1);
        }
    }
}