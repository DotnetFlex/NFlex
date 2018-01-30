using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Collections;

namespace NFlex
{
    public class DynamicObject : System.Dynamic.DynamicObject, IDictionary<string, object>
    {
        private readonly IDictionary<string, object> _values= new Dictionary<string, object>();

        public ICollection<string> Keys
        {
            get
            {
                return _values.Keys;
            }
        }

        public ICollection<object> Values
        {
            get
            {
                return _values.Values;
            }
        }

        public int Count
        {
            get
            {
                return _values.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _values.IsReadOnly;
            }
        }

        public DynamicObject()
        {
            _values = new Dictionary<string, object>();
        }
        public object this[string key]
        {
            get
            {
                if (_values.ContainsKey(key))
                    return _values[key];
                return null;
            }

            set
            {
                if (_values.ContainsKey(key))
                    _values[key] = value;
                else
                    _values.Add(key, value);
            }
        }

        /// <summary>  
        /// 获取属性值  
        /// </summary>  
        /// <param name="propertyName"></param>  
        /// <returns></returns>  
        public object GetPropertyValue(string propertyName)
        {
            if (_values.ContainsKey(propertyName) == true)
            {
                return _values[propertyName];
            }
            return null;
        }

        /// <summary>  
        /// 设置属性值  
        /// </summary>  
        /// <param name="propertyName"></param>  
        /// <param name="value"></param>  
        public void SetPropertyValue(string propertyName, object value)
        {
            if (_values.ContainsKey(propertyName) == true)
            {
                _values[propertyName] = value;
            }
            else
            {
                _values.Add(propertyName, value);
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            SetPropertyValue(binder.Name, value);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetPropertyValue(binder.Name);
            return result != null;
        }

        public string ToXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (var kv in _values)
            {
                sb.Append(string.Format("<{0}>{1}</{0}>", kv.Key, ToXml(kv.Value)));
            }
            sb.Append("</xml>");
            return sb.ToString();
        }

        private string ToXml(object obj)
        {
            StringBuilder sb = new StringBuilder();
            Type type = obj.GetType();
            switch (type.Name)
            {
                case "String":
                case "Int32":
                case "Decimal":
                case "Float":
                case "Double":
                case "DateTime":
                    sb.Append(obj.ToString());
                    break;
                case "List`1":
                    var emValue = obj as IEnumerable<object>;
                    if (emValue != null)
                    {
                        foreach (var v in emValue)
                        {
                            var vType = v.GetType();
                            var name = vType.Name;
                            var attr = vType.CustomAttributes.FirstOrDefault(t => t.AttributeType.Name == "XmlRootAttribute");
                            if (attr != null && attr.ConstructorArguments.Count > 0)
                                name = attr.ConstructorArguments.First().Value.ToString();
                            sb.Append(string.Format("<{0}>{1}</{0}>", name, ToXml(v)));
                        }
                    }
                    break;
                default:
                    var propertyList = type.GetProperties();
                    foreach (var p in propertyList)
                    {
                        string name = p.Name;
                        object value = p.GetValue(obj);
                        sb.Append(string.Format("<{0}>{1}</{0}>", name, ToXml(value)));
                    }
                    break;
            }
            return sb.ToString();
        }

        public bool ContainsKey(string key)
        {
            return _values.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            _values.Add(key, value);
        }

        public bool Remove(string key)
        {
            return _values.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _values.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            _values.Add(item);
        }

        public void Clear()
        {
            _values.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _values.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _values.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}
