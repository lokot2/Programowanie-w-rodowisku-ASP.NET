using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibApp.ViewModels;
using AutoMapper;
using LibApp.Dtos;
using Flurl.Http;
using Flurl;
using Microsoft.AspNetCore.Authorization;

namespace LibApp.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IMapper mapper;
        private const string booksUrl = "https://localhost:44352/api/books";
        private const string genresUrl = "https://localhost:44352/api/genres";

        public BooksController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Authorize(Roles = "User,StoreManager,Owner")]
        public async Task<IActionResult> Index()
        {
            var books = await booksUrl.GetJsonAsync<IEnumerable<BookDto>>();

            return View(books);
        }

        [Authorize(Roles = "User,StoreManager,Owner")]
        public async Task<IActionResult> Details(int id)
        {
            var book = await booksUrl.AppendPathSegment(id)
                .GetJsonAsync<BookDto>();

            return View(book);
        }

        [Authorize(Roles = "StoreManager,Owner")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await booksUrl.AppendPathSegment(id)
                .GetJsonAsync<BookDto>();

            if (book == null)
            {
                return NotFound();
            }

            var genres = await genresUrl.GetJsonAsync<IEnumerable<GenreDto>>();
            var viewModel = new BookFormViewModel
            {
                Book = mapper.Map<BookDto>(book),
                Genres = genres
            };

            return View("BookForm", viewModel);
        }

        [Authorize(Roles = "StoreManager,Owner")]
        public async Task<IActionResult> New()
        {
            var genres = await genresUrl.GetJsonAsync<IEnumerable<GenreDto>>();
            var viewModel = new BookFormViewModel
            {
                Genres = genres
            };

            return View("BookForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "StoreManager,Owner")]
        public async Task<IActionResult> Save(BookDto book)
        {
            if (!ModelState.IsValid)
            {
                var genres = await genresUrl.GetJsonAsync<IEnumerable<GenreDto>>();

                var viewModel = new BookFormViewModel
                {
                    Genres = genres,
                    Book = book
                };

                return View("BookForm", viewModel);
            }

            if (!book.Id.HasValue || book.Id == 0)
            {
                await booksUrl.PostJsonAsync(book);
            }
            else
            {
                await booksUrl.PutJsonAsync(book);
            }

            return RedirectToAction("Index", "Books");
        }
    }
}
