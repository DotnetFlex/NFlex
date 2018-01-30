using System;

namespace NFlex
{
    /// <summary>
    /// 排序
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class SortAttribute:Attribute
    {
        /// <summary>
        /// 初始化排序
        /// </summary>
        /// <param name="sortId">排序号</param>
        public SortAttribute(int sortId)
        {
            SortId = sortId;
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }
    }
}
