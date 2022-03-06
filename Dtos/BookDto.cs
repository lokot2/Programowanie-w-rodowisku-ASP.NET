using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Dtos
{
    public class BookDto
    {
		public int? Id { get; set; }

		[Required(ErrorMessage = "Book's name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Author's name is required")]
		public string AuthorName { get; set; }

		[Required(ErrorMessage = "Genre is required")]
		public string GenreId { get; set; }

		[Required(ErrorMessage = "Release Date is required")]
		[Display(Name = "Release Date")]
		public string ReleaseDate { get; set; }

		[Required(ErrorMessage = "Number in Stock is required")]
		[Range(1, 20, ErrorMessage = "The value must be in the range 1-20")]
		[Display(Name = "Number in Stock")]
		public string NumberInStock { get; set; }

		public string DateAdded { get; set; }

		public GenreDto Genre { get; set; }

        public BookDto()
        {
			DateAdded = DateTime.Now.ToShortDateString();
        }
	}
}