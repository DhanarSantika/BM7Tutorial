using BM7Tutorial.BLL.CRUD;
using BM7Tutorial.DAL;
using Moq;
using Nexus.Base.CosmosDBRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BM7Tutorial.BLL.Test.CRUD
{
    public class CRUDTest
    {
        [Theory]
        [InlineData("class-1")]
        [InlineData("class-2")]
        public async Task GetClassByIdExists_ReturnClass(string classId)
        {
            // Arrange
            var repo = new Mock<IDocumentDBRepository<Class>>();

            IEnumerable<Class> classes = new List<Class>
                {
                    {new Class() { Id = "class-1", Description = "abcd", ClassCode = "test-class-1"} },
                    {new Class() { Id = "class-2", Description = "xyz0", ClassCode = "test-class-2"} }
                };

            var classData = classes.Where(o => o.Id == classId).FirstOrDefault();
            Dictionary<string, string> pk = null;

            repo.Setup(c => c.GetByIdAsync(
                    It.IsAny<string>(),
                    It.IsAny<Dictionary<string, string>>()
                )).Returns(
                    Task.FromResult(classData)
                );

            var svc = new CRUDService(repo.Object);

            // Act
            var classById = await svc.GetClassById(classId, pk);
            var actual = classById;

            // Assert
            Assert.Equal(classData.Description, actual.Description);
            Assert.Equal(classData.ClassCode, actual.ClassCode);
        }
    }
}
