using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core
{
    public abstract class EntityBase<TKey>:IEntity<TKey>
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Required]
        [Key]
        public TKey Id { get; set; }
    }

    public abstract class EntityBase : EntityBase<Guid>
    {
    }
}
