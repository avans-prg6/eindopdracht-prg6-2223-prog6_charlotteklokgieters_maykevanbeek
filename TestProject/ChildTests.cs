using LogicLayer.KidValidation;
using LogicLayer.Santa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
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
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void CertainGiftAmount_LiedAboutNiceness_Invalid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void CertainGiftAmount_OnlyOnePresent_Invalid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void CertainGiftAmount_OnlyThreePresents_Invalid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void GiftCombinations_Valid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void GiftCombinations_Lego_InValid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void GiftCombinations_NightLight_InValid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void GiftCombinations_Music_InValid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void DivertFromAgeRating_Valid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void DivertFromAgeRating_InValid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void GiftAvailabilityInList_Valid()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void GiftAvailabilityInList_InValid()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}

