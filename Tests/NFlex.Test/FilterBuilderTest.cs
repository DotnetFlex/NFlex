using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class FilterBuilderTest
    {
        //[Fact]
        //public void DefaultTest()
        //{
        //    var list = GetDataList();
        //    FilterBuilder<UserInfo> filter = new FilterBuilder<UserInfo>();

        //    var all = list.Where(filter);
        //    Assert.Equal(list.Count, all.Count());
        //}

        //[Fact]
        //public void SimpleTest()
        //{
        //    var list = GetDataList();
        //    FilterBuilder<UserInfo> filter = new FilterBuilder<UserInfo>();
        //    filter.And(t => t.Age > 20);

        //    var result = list.Where(filter);
        //    Assert.Equal(3, result.Count());
        //}

        //[Fact]
        //public void MultTest()
        //{
        //    var list = GetDataList();
        //    FilterBuilder<UserInfo> filter = new FilterBuilder<UserInfo>();
        //    filter.And(t => t.Age < 25);
        //    filter.And(t => t.Sex == "女");

        //    var result = list.Where(filter);
        //    Assert.Equal(2, result.Count());
        //}

        //[Fact]
        //public void MultFilterTest()
        //{
        //    var list = GetDataList();
        //    FilterBuilder<UserInfo> filter1 = new FilterBuilder<UserInfo>(t => t.Name == "张无忌");
        //    FilterBuilder<UserInfo> filter2 = new FilterBuilder<UserInfo>(t => t.Name == "赵敏");
        //    filter2.Or(filter1);

        //    var result = list.Where(filter2);
        //    Assert.Equal(2, result.Count());
        //}

        //[Fact]
        //public void MultFilterTest2()
        //{
        //    var list = GetDataList();
        //    FilterBuilder<UserInfo> filter1 = new FilterBuilder<UserInfo>(t => t.Name == "张无忌");
        //    FilterBuilder<UserInfo> filter2 = new FilterBuilder<UserInfo>(t => t.Name == "赵敏");
        //    filter2.And(filter1);

        //    var result = list.Where(filter2);
        //    Assert.Equal(0, result.Count());
        //}

        [Fact]
        public void QueryableTest()
        {
            var list = GetDataList();
            var queryable = list.AsQueryable();
            FilterBuilder<UserInfo> filter = new FilterBuilder<UserInfo>(t => t.Sex == "男");

            var result=queryable.Where(filter);
            Assert.Equal(3, result.Count());
        }

        private List<UserInfo> GetDataList()
        {
            List<UserInfo> list = new List<UserInfo>
            {
                new UserInfo {Name="张三丰",Sex="男",Age=120 },
                new UserInfo {Name="张无忌",Sex="男",Age=25 },
                new UserInfo {Name="赵敏",Sex="女",Age=23 },
                new UserInfo {Name="鲁班",Sex="男",Age=18 },
                new UserInfo {Name="貂蝉",Sex="女",Age=18 }
            };
            return list;
        }

        public class UserInfo
        {
            public string Name { get; set; }
            public string Sex { get; set; }
            public int Age { get; set; }
        }
    }
}
