using NFlex.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Models
{
    public class UserInfo:AggregateRoot<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public int Age { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
