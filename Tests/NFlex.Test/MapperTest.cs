using NFlex.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class MapperTest
    {
        [Fact]
        public void SimpleTest()
        {
            var dto = new BookDto
            {
                Description = "描述",
                Language = "中文",
                Price = 29.9m,
                Title = "C++从入门到放弃"
            };
            var book = dto.MapTo<Book>();
            Assert.Equal(dto.Title, book.Title);
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
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
