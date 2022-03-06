using AutoMapper;
using LibApp.Dtos;
using LibApp.Models;
using System;

namespace LibApp.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.ReleaseDate, map => map.MapFrom(src => src.ReleaseDate.ToShortDateString()))
                .ForMember(dest => dest.GenreId, map => map.MapFrom(src => src.GenreId.ToString()))
                .ForMember(dest => dest.NumberInStock, map => map.MapFrom(src => src.NumberInStock.ToString()));

            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.ReleaseDate, map => map.MapFrom(src => DateTime.Parse(src.ReleaseDate)))
                .ForPath(dest => dest.GenreId, map => map.MapFrom(src => byte.Parse(src.GenreId)))
                .ForMember(dest => dest.NumberInStock, map => map.MapFrom(src => int.Parse(src.NumberInStock)));
        }
    }
}
