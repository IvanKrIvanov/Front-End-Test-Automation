using MovieCatalogPomTests.Pages;
using OpenQA.Selenium.Interactions;

namespace MovieCatalogPomTests.Tests
{
    public class MovieCatalogTests : BaseTest
    {
        private string lastMovieTitle;
        private string lastMovieDescription;
	

		[Test, Order(1)]
        public void AddMovieWithoutTitle()
        {
            addMoviePage.OpenPage();

            addMoviePage.TitleInput.Clear();


            actions.ScrollToElement(addMoviePage.AddButton).Perform();

            addMoviePage.AddButton.Click();

            addMoviePage.AssertEmptyTitleMessage();
        }

        [Test, Order(2)]
        public void AddMovieWithoutDescription()
        {
            lastMovieTitle = GenerateRandomTitle();

            addMoviePage.OpenPage();

            addMoviePage.TitleInput.SendKeys(lastMovieTitle); 

			actions.ScrollToElement(addMoviePage.AddButton).Perform();

			addMoviePage.AddButton.Click();

            addMoviePage.AssertEmptyDescriptionMessage();
        }

        [Test, Order(3)]
        public void AddMovieWithTitleAndDescription()
        { 
            lastMovieTitle= GenerateRandomTitle();
            lastMovieDescription= GenerateRandomDescription();
            addMoviePage.OpenPage();

            addMoviePage.TitleInput.Clear();
            addMoviePage.TitleInput.SendKeys(lastMovieTitle);
            addMoviePage.DescriptionInput.Clear();
            addMoviePage.DescriptionInput.SendKeys(lastMovieDescription);
			actions.ScrollToElement(addMoviePage.AddButton).Perform();
			addMoviePage.AddButton.Click();

            allMoviesPage.NavigateToLastPage();

            Assert.That(allMoviesPage.LastMovieTitle.Text.Trim, Is.EqualTo(lastMovieTitle), "The title is not as expected");
        }

        [Test, Order(4)]
        public void EditLastMovie()
        {
            lastMovieTitle = GenerateRandomTitle() + "EDITED";
            lastMovieDescription = GenerateRandomDescription();

            allMoviesPage.OpenPage();
            allMoviesPage.NavigateToLastPage();
            allMoviesPage.LastMovieEditButton.Click();

            editMoviePage.TitleInput.Clear();
            editMoviePage.TitleInput.SendKeys(lastMovieTitle);
			actions.ScrollToElement(editMoviePage.EditButton).Perform();
			editMoviePage.EditButton.Click();

            editMoviePage.AssertRecordEdited();
        }

        [Test, Order(5)]
        public void MarkLastMovieAsWatched()
        {
            allMoviesPage.OpenPage();
            allMoviesPage.NavigateToLastPage();
            allMoviesPage.LastMovieMarkAsWatchedButton.Click();

            watchedMoviesPage.OpenPage();
            watchedMoviesPage.NavigateToLastPage();

            Assert.That(watchedMoviesPage.LastMovieTitle.Text.Trim, Is.EqualTo(lastMovieTitle), "The movie was not added to watched");
        }

        [Test, Order(6)]
        public void DeleteMovie()
        {
            allMoviesPage.OpenPage();
            allMoviesPage.NavigateToLastPage();
            allMoviesPage.LastMovieDeleteButton.Click();

            deletePage.YesButton.Click();

            Assert.That(deletePage.ToastMessage.Text.Trim(), Is.EqualTo("The Movie is deleted successfully!"), "The movie was not deleted");
        }
    }
}