using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context = null;

        public MoviesController()
        {
            _context = new Models.ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(c => c.Genre).ToList<Movie>();


            return View(movies);
        }

        public ActionResult Details(int Id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(i => i.Id == Id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewmodel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.NumberInStock = movie.NumberInStock;
                movieInDB.GenreId = movie.GenreId;
            }


            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}

        //[Route("movies/released/{year}/{month:regex(\\d{4})}:range(1,12)")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year+"/"+month);
        //}

        //[Route("movies/random")]
        //public ActionResult Random(int pageIndex = 2, string MovieName = "Shrek!")
        //{
        //    Movie M = new Movie()
        //    {
        //        Id = pageIndex,
        //        Name = MovieName
        //    };

        //    var Customers = new List<Customer>()
        //    {
        //         new Customer() { Name="Customer1", Id=1},
        //         new Customer() { Name="Customer2", Id=2}

        //    };

        //    var RandomMovieViewModel = new RandomMovieViewModel()
        //    {
        //        Movie = M,
        //        Customers = Customers

        //    };

        //    return View(RandomMovieViewModel);
        //}

    }
}