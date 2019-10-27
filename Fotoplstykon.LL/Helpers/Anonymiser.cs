using Fotoplastykon.DAL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Fotoplastykon.BLL.Helpers
{
    public class Anonymiser<T> where T : class
    {
        public T Anonymise(T item)
        {
            var properties = typeof(T).GetProperties().Where(p => p.GetCustomAttribute(typeof(AnonymiseAttribute)) != null);

            foreach (var property in properties)
            {
                if(property.PropertyType == typeof(string))
                {
                    using (var sha256 = SHA256.Create())
                    {
                        property.SetValue(item, sha256.ComputeHash(Guid.NewGuid().ToByteArray()));
                    }
                }
                else
                {
                    property.SetValue(item, property.GetConstantValue());
                }
            }

            return item;
        }
    }
}
