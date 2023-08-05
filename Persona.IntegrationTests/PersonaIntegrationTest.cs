using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Persona.IntegrationTests;

[TestClass]
public class PersonaIntegrationTest
{
    private IWebDriver? _webDriver;
    private const string BaseUrl = "https://localhost:7142";

    [TestInitialize]
    public void SetUp()
    {
        //setup
        new DriverManager().SetUpDriver(new ChromeConfig());
        _webDriver = new ChromeDriver();
    }
    
    //test title of page
    [TestMethod]
    public void TitleShouldHaveHomeInIt()
    {
        _webDriver?.Navigate().GoToUrl(BaseUrl);
        Assert.IsTrue(_webDriver?.Title.Contains("Home"));
    }
    
    //test the data displayed in index page
    [TestMethod]
    public void IndexPageDisplaysListOfJournals()
    {
        _webDriver?.Navigate().GoToUrl(BaseUrl);
        var content = _webDriver.PageSource;
        
        //assert test cases
        Assert.IsTrue(content.Contains("My First Journal"));
        Assert.IsTrue(content.Contains("This is my first journal entry."));
        Assert.IsTrue(content.Contains("Second Journal"));
        Assert.IsTrue(content.Contains("Another journal entry."));
    }
    
    //test navigation from index page to post journal
    [TestMethod]
    public void NavigatesFromIndexPageToPostJournalDetailsPage()
    {
        
        _webDriver.Navigate().GoToUrl(BaseUrl);

        var createNewEntryButton = _webDriver.FindElement(By.XPath("//button[contains(., 'Create New Entry')]"));
        createNewEntryButton.Click();

        // assert test case
        Assert.IsTrue(_webDriver.Url.Contains("/Home/PostJournal"));
    }

    [TestCleanup]
    public void TearDown()
    {
        //tear down
        _webDriver?.Quit();   
    }
}
