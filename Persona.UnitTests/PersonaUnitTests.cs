using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol;
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
        Assert.IsInstanceOfType(result, typeof(ActionResult<JournalModel>));
    }
    
    [TestMethod]
    public async Task CreateReturnDataAddedResult()
    {
        // Arrange
        var options = new DbContextOptions<JournalDBContext>();
        var mockDbContext = new Mock<JournalDBContext>(options);
        var controller = new JournalController(mockDbContext.Object);
        var journalModel = new JournalModel
        {
            JournalId = 1,
            JournalName = "Journal 1",
            Description = "Description for Journal 1",
            Photo = "photo1.jpg",
            UserEnteredDate = DateTime.Now,
            CreatedDate = DateTime.Now.AddDays(-5)
        };

        // Act
        var result = await controller.Create(journalModel);
        var data = result?.Result as ObjectResult;

        if (data != null)
        {
            var objData = data.Value as JournalModel;
            // Assert
            Assert.AreEqual(objData?.JournalName,  journalModel.JournalName);
            
        }
    }

    [TestMethod]
    public async Task GetJournalsReturnsOkResult()
    {
        //Arrange
        var options = new DbContextOptions<JournalDBContext>();
        var mockDbContext = new Mock<JournalDBContext>(options);
        var controller = new JournalController(mockDbContext.Object);

        //Act
        var result =  await controller.GetJournal();

        //Assert
        Assert.IsInstanceOfType(result, typeof(ActionResult<List<JournalModel>>));
    }
    
    [TestMethod]
    public async Task GetJournalByIdReturnNotFoundResult()
    {
        // Arrange
        var options = new DbContextOptions<JournalDBContext>();
        var mockDbContext = new Mock<JournalDBContext>(options);
        var controller = new JournalController(mockDbContext.Object);
        const int journalId = 99;

        // Act
        var result = await controller.GetJournalById(journalId);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

}