using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> repository;
        private readonly IMapper mapper;

        public BooksController(IRepository<Book> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET api/books/
        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooks()
        {
            var books = repository.FindAll()
                .Include(b => b.Genre)
                .ToList();

            var bookDtos = mapper.Map<IEnumerable<BookDto>>(books);

            return Ok(bookDtos);
        }

        // GET api/books/{id}
        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBook(int id)
        {
            var book = repository.FindByCondition(b => b.Id == id)
                .Include(b => b.Genre)
                .FirstOrDefault();

            var bookDto = mapper.Map<BookDto>(book);

            return Ok(bookDto);
        }

        // POST api/books/
        [HttpPost]
        public ActionResult CreateBook(BookDto book)
        {
            var bookEntity = mapper.Map<Book>(book);
            repository.Create(bookEntity);

            return Created($"api/books/{bookEntity.Id}", null);
        }

        // PUT api/books/
        [HttpPut]
        public ActionResult UpdateBook(BookDto book)
        {
            var bookInDb = repository.FindObject(b => b.Id == book.Id.Value);
            mapper.Map(book, bookInDb);
            repository.Update(bookInDb);

            return NoContent();
        }

        // DELETE /api/books/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var bookInDb = repository.FindObject(c => c.Id == id);
            repository.Delete(bookInDb);

            return NoContent();
        }
    }
}