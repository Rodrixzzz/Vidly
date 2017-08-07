using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;


namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _contex;
        public MoviesController()
        {
            _contex = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _contex.Dispose();
        }
        public ViewResult Index()
        {
            var movies = _contex.Movies.Include(m => m.Genre).ToList();
            return View(movies);    
        }

        public ActionResult Details(int id)

        {
            var movie = _contex.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name="Shrek!"};
            List<Customer> customers = new List<Customer>()
            {
                new Customer {Name="Customer 1"},
                new Customer {Name="Customer 2"}
            };
            RandomMovieViewModel viewModel = new RandomMovieViewModel
            {
                Movie=movie,
                Customers=customers
            };
            return View(movie);
        }
        public ActionResult New()
        {
            var genres = _contex.Genre.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                     Genres = _contex.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.AddedDate= DateTime.Now;
                _contex.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _contex.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.Stock = movie.Stock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _contex.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Edit(int id)
        {
            var movie = _contex.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _contex.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }
    }
}