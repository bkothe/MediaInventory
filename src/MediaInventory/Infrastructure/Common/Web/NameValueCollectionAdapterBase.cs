using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace MediaInventory.Infrastructure.Common.Web
{
    public abstract class NameValueCollectionAdapterBase : IDictionary<string, string>
    {
        private readonly Lazy<NameValueCollection> _values;

        protected NameValueCollectionAdapterBase(Func<NameValueCollection> values)
        {
            _values = new Lazy<NameValueCollection>(values);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return KeyAndValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, string> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _values.Value.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            Array.Copy(KeyAndValues.ToArray(), array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return Remove(item.Key);
        }

        public int Count
        {
            get { return _values.Value.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool ContainsKey(string key)
        {
            return Keys.Contains(key);
        }

        public void Add(string key, string value)
        {
            _values.Value.Add(key, value);
        }

        public bool Remove(string key)
        {
            var exists = ContainsKey(key);
            if (exists) _values.Value.Remove(key);
            return exists;
        }

        public bool TryGetValue(string key, out string value)
        {
            var exists = ContainsKey(key);
            value = exists ? this[key] : null;
            return exists;
        }

        public string this[string key]
        {
            get { return _values.Value[key]; }
            set { _values.Value[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return _values.Value.Keys.Cast<string>().ToList(); }
        }

        public ICollection<string> Values
        {
            get { return Keys.Select(x => this[x]).ToList(); }
        }

        public IEnumerable<KeyValuePair<string, string>> KeyAndValues
        {
            get { return Keys.Select(x => new KeyValuePair<string, string>(x, this[x])); }
        }

        public override string ToString()
        {
            return this.ToUrlEncodedString();
        }
    }
}
