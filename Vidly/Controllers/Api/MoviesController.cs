using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context = null;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/Movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        //GET /api/Movies
        public IHttpActionResult GetMovie(int id)
        {
            var MovieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (MovieInDB == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(MovieInDB));
            
        }

        //POST /api/Movie
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var Movie = Mapper.Map<MovieDto, Movie>(MovieDto);
            _context.Movies.Add(Movie);
            _context.SaveChanges();

            MovieDto.Id = Movie.Id;

            return Created(new Uri(Request.RequestUri + "//" + MovieDto.Id), MovieDto);
        }

        //PUT /api/Movie/id
        [HttpPut]
        public void UpdateMovies(int id, MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var MovieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (MovieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(MovieDto, MovieInDb);


            _context.SaveChanges();

        }

        //Delete /api/Movie/id
        [HttpDelete]
        public void DeleteMovies(int id)
        {

            var MovieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (MovieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(MovieInDb);
            _context.SaveChanges();

        }

    }
}
