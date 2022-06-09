using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jopalesha.Common.Domain.Models;
using Xunit;
using Xunit.Sdk;

namespace Jopalesha.Common.Domain.Tests
{
    public class QueryTests
    {
        [Fact]
        public void Skip_IncreaseOffset()
        {
            var lst = new List<Myclass>();

            for (int i = 0; i < 10; i++)
            {
                lst.Add(new Myclass() { Value = i });
            }

            var res = lst.Skip(3).Where(it => it.Value > 5).ToList();

            var query = new Query<TestClass>().Skip(10);

            Assert.Equal(10, query.Offset);
        }

        private class  Myclass
        {
            public int Value { get; set; }
        }
    }
}
