using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persona.WebAPI.Controllers;
using PersonaClassLibrary;

namespace Persona.UnitTests;

[TestClass]
public class PersonaUnitTests
{
    //here, i am trying to test if the create method will return status code 200 when it
    //adds data to dbcontext but it does not give positive response,
    //i also tried using other methods of Assert class but it still does not work
    [TestMethod]
    public async Task CreateReturnOkResult()
    {
        // Arrange
        var options = new DbContextOptions<JournalDBContext>();
        var mockDbContext = new Mock<JournalDBContext>(options);
        var controller = new JournalController(mockDbContext.Object);
        var journalModel = new JournalModel();

        // Act
        var result = await controller.Create(journalModel);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

    }
}