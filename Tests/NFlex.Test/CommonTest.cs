using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NFlex;
using System.Linq.Expressions;
using System.Diagnostics;

namespace NFlex.Test
{
    public class CommonTest
    {
        [Fact]
        public void CardCreate()
        {
            List<Test> list = new List<NFlex.Test.Test>();
            list.Add(new Test { Type = 1, Name = "name1" });
            list.Add(new Test { Type = 2, Name = "name2" });
            list.Add(new Test { Type = 1, Name = "name3" });

            var b = list.ToLookup(t => t.Type).Select(s =>s.Aggregate((a1,b1)=>new Test {Type=s.Key,Name=a1.Name+","+b1.Name })).ToList();

            var a = list.GroupBy(t => t.Type).Select(s => new Test
            {
                Type = s.Key,
                Name = string.Join(",", s.Select(t=>t.Name))
            }).ToList();
        }
    }

    public class Test
    {
        public int Type { get; set; }
        public string Name { get; set; }
    }
}
