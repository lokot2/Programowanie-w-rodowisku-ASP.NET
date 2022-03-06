using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IRepository<Genre> repository;
        private readonly IMapper mapper;

        public GenresController(IRepository<Genre> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GenreDto>> GetGenres()
        {
            var genres = repository.FindAll().ToList();
            var genreDtos = mapper.Map<IEnumerable<GenreDto>>(genres);

            return Ok(genreDtos);
        }
    }
}