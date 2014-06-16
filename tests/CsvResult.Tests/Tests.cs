using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace CsvResult.Tests
{
    public class TestController : Controller
    {

        public class TestClass
        {
            public string FirstName { get; set; }
            public string Surname { get; set; }
        }
        

        public CsvResult Index()
        {

            var data = new[]
            {
                new TestClass
                {
                    FirstName = "firstName",
                    Surname = "Surname"
                },
                new TestClass
                {
                    FirstName = "2ndFirstName",
                    Surname = "2ndsurname"
                }
            };

            return new CsvResult(data);

        }


    }

    public class Tests
    {
        [Fact]
        public void TestControllerShouldReturnTypeOfCsvResult()
        {
            //arrange
            var controller = new TestController();
            //act
            var result = controller.Index();
            //assert
            Assert.IsType<CsvResult>(result);
        }

        [Fact]
        public void TestControllerDefaultMimeTypeShouldBeTextCsv()
        {
            //arrange
            var controller = new TestController();
            //act
            var result = controller.Index();
            //assert
            Assert.Equal("text/csv", result.ContentType);
        }

        [Fact]
        public void TestControllerShouldHaveTwoRows()
        {
            //arrange
            var controller = new TestController();
            //act
            var result = controller.Index();
            //assert
            Assert.Equal(2, result.Data.OfType<TestController.TestClass>().Count());
        }

        [Fact]
        public void TestControllerShouldHaveCorrectData()
        {
            //arrange
            var controller = new TestController();
            //act
            var result = controller.Index();
            //assert
            Assert.Equal("2ndFirstName", result.Data.OfType<TestController.TestClass>().Last().FirstName);
        }


    }
}
