using NFlex.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;

namespace NFlex.Test
{
    public class MapperTest
    {
        [Fact]
        public void SimpleTest()
        {
            var dto = new Book
            {
                Description = "描述",
                Language = "中文",
                Price = 29.9m,
                Title = "C++从入门到放弃"
            };
            var book = dto.MapTo<BookDto>();
            Assert.Equal(dto.Title, book.Title);
        }

        [Fact]
        public void CustomTest()
        {
            MapperConfig.Initialize();
            var book = new Book
             {
                 Description = "描述",
                 Language = "中文",
                 Price = 29.9m,
                 Title = "C++从入门到放弃"
             };
            var dto = book.MapTo<BookDto>();
        }
    }

    public class BookMapper : IMap
    {
        public void Register(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Book, BookDto>()
                .ForMember(t => t.Desc, o => o.MapFrom(s => s.Description));
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class BookDto
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
    }
}
