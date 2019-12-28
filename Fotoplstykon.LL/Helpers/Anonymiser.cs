using Fotoplastykon.DAL.Attributes;
using Fotoplastykon.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Fotoplastykon.BLL.Helpers
{
    public class Anonymiser<T> where T : class, IAnonymisationable
    {
        public T Anonymise(T item)
        {
            var properties = typeof(T).GetProperties().Where(p => p.GetCustomAttribute(typeof(AnonymiseAttribute)) != null);

            foreach (var property in properties)
            {
                if(property.PropertyType == typeof(string))
                {
                    using (var md5 = MD5.Create())
                    {
                        var hash = md5.ComputeHash(Guid.NewGuid().ToByteArray());
                        var sBuilder = new StringBuilder();

                        for (int i = 0; i < hash.Length; i++) sBuilder.Append(hash[i].ToString("x2"));

                        property.SetValue(item, sBuilder.ToString());
                    }
                }
                else
                {
                    property.SetValue(item, property.GetConstantValue());
                }
            }

            item.AnonimisationDate = DateTime.Now;

            return item;
        }
    }
}
