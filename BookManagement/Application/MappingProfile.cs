using Application.DTOs;
using Application.Use_Cases.Commands;
using AutoMapper;
using BookManagement.Models;
using Domain.Entities;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            
            CreateMap<UpdateBookRequest,UpdateBookCommand>();
        }
    }
}