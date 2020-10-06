using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace app.Generate
{
    public class ListGenerate: IGenerate
    {
        private Dictionary<Type,IGenerate> _dictionary = new Dictionary<Type,IGenerate>();
        public object Generate()
        {
            return null;
        }

        public object GenericGenerate(Type type, Dictionary<Type,IGenerate> dictionary)
        {
            Random random = new Random();
            int listLenght = random.Next(0,10);
            List<Object> result = new List<object>();
            for (int i = 0; i < listLenght; i++)
            {
                if (dictionary.ContainsKey(type))
                {
                    result.Add(dictionary[type].Generate());
                }
                else
                {
                    result.Add(FormatterServices.GetUninitializedObject(type));
                }
            }

            object list = null;
            if (type == typeof(int))
            {
                list = result.Cast<int>().ToList();
            }
            if (type == typeof(bool))
            {
                list = result.Cast<bool>().ToList();
            }
            if (type == typeof(float))
            {
                list = result.Cast<float>().ToList();
            }
            if (type == typeof(DateTime))
            {
                list = result.Cast<DateTime>().ToList();
            }
            if (type == typeof(long))
            {
                list = result.Cast<long>().ToList();
            }
            if (type == typeof(string))
            {
                list = result.Cast<string>().ToList();
            }
            return list;
        }
        
    }
}