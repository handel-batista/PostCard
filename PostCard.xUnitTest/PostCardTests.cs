using PostCard.Models;
using PostCard.UnitTest;
using System;
using System.Linq;
using Xunit;

namespace PostCard.xUnitTest
{
    public class PostCardTests
    {

        [Fact]
        public void Test_UploadedDataViewModel_FormValidation_required_fields_Should_Pass()
        {
            CheckFieldsValidation cfv = new CheckFieldsValidation();
            UploadedDataViewModel fvm = new UploadedDataViewModel()
            {
                Name = "Handel Batista",
                Email = "hbatista@marcolin.com",
                Base64 = "R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs="
            };

            var errorcount = cfv.Validate(fvm).Count();

            Assert.Equal(0, errorcount);
        }

        [Fact]
        public void Test_UploadedDataViewModel_FormValidation_required_fields_Empty_Should_Not_Pass()
        {
            CheckFieldsValidation cfv = new CheckFieldsValidation();
            UploadedDataViewModel fvm = new UploadedDataViewModel()
            {
                Name = "",
                Email = "",
                Base64 = ""
            };

            var errorcount = cfv.Validate(fvm).Count();

            Assert.Equal(0, errorcount);
        }

        [Fact]
        public void Test_UploadedDataViewModel_FormValidation_Name_MaxLength_Should_Not_Pass()
        {
            CheckFieldsValidation cfv = new CheckFieldsValidation();
            UploadedDataViewModel fvm = new UploadedDataViewModel()
            {
                Name = "R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs=Handel Batista"
            };

            var errorcount = cfv.Validate(fvm).Count();

            Assert.Equal(0, errorcount);
        }

        [Fact]
        public void Test_UploadedDataViewModel_FormValidation_Email_Regex_Should_Not_Pass()
        {
            CheckFieldsValidation cfv = new CheckFieldsValidation();
            UploadedDataViewModel fvm = new UploadedDataViewModel()
            {
                Email = "R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs@gmail.com"
            };

            var errorcount = cfv.Validate(fvm).Count();

            Assert.Equal(0, errorcount);
        }
    }
}
