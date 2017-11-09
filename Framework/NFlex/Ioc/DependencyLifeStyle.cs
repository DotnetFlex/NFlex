using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Ioc
{
    /// <summary>
    /// 依赖注入实例的生命周期枚举
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// 每个Web请求实例
        /// </summary>
        PerWebRequest,

        /// <summary>
        /// 单例
        /// </summary>
        Singleton,

        /// <summary>
        /// 生命周期实例
        /// </summary>
        Transient
    }
}
