using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using app.Generate;

namespace app.Faker
{
    public class Faker
    {
        private Dictionary<Type,IGenerate> _dictionary = new Dictionary<Type,IGenerate>();
        private Stack<Type> _stack = new Stack<Type>();

        public Faker()
        {
            _dictionary.Add(typeof(int),new IntGenerate());
            _dictionary.Add(typeof(bool),new BoolGenerate());
            _dictionary.Add(typeof(long),new LongGenerate());
            _dictionary.Add(typeof(string),new StringGenerate());
            _dictionary.Add(typeof(float),new FloatGenerate());
            _dictionary.Add(typeof(DateTime), new DateGenerate());
            _dictionary.Add(typeof(List<>), new ListGenerate());
        }

        private bool isDTO(Type type)
        {
            return type.IsClass && !type.IsValueType && !type.IsGenericType;
        }

        private List<Object> GenerateParametersForConstructor(ParameterInfo[] parameterInfos)
        {
            List<Object> result = new List<Object>();

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                Type parameterType = parameterInfos[i].ParameterType;
                if (!isDTO(parameterType))
                {
                    if (_dictionary.ContainsKey(parameterType))
                    {
                        if (parameterType.IsGenericType)
                        {
                            result.Add(_dictionary[parameterType].GenericGenerate(parameterType.GenericTypeArguments[0]));
                        }
                        else
                        {
                            result.Add(_dictionary[parameterType].Generate());
                        }
                    }
                    else
                    {
                        result.Add(FormatterServices.GetSafeUninitializedObject(parameterType));
                    }
                }
                else
                {
                    result.Add(this.CreateInner(parameterType));
                }
            }
            return result;
        }

        private void GenerateFields(Object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                Type parameterType = fields[i].FieldType;
                if (!isDTO(parameterType))
                {
                    if (_dictionary.ContainsKey(parameterType))
                    {
                        if (parameterType.IsGenericType)
                        {
                            fields[i].SetValue(obj,_dictionary[parameterType].GenericGenerate(parameterType.GenericTypeArguments[0]));
                        }
                        else
                        {
                            fields[i].SetValue(obj, _dictionary[parameterType].Generate());
                        }
                    }
                    else
                    {
                        fields[i].SetValue(obj, FormatterServices.GetSafeUninitializedObject(parameterType));
                    }
                }
                else
                {
                    fields[i].SetValue(obj,this.CreateInner(parameterType));
                }
            }

            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                if (propertyInfos[i].CanWrite)
                {
                    Type parameterType = fields[i].FieldType;
                    if (!isDTO(parameterType))
                    {
                        if (_dictionary.ContainsKey(parameterType))
                        {
                            if (parameterType.IsGenericType)
                            {
                                propertyInfos[i].SetValue(obj,_dictionary[parameterType].GenericGenerate(parameterType.GenericTypeArguments[0]));
                            }
                            else
                            {
                                propertyInfos[i].SetValue(obj, _dictionary[parameterType].Generate());
                            }
                        }
                        else
                        {
                            propertyInfos[i].SetValue(obj, FormatterServices.GetSafeUninitializedObject(parameterType));
                        }
                    }
                    else
                    {
                        propertyInfos[i].SetValue(obj,this.CreateInner(parameterType));
                    }
                }
            }
        }
        private Object GenerateDTO(ParameterInfo[] parameterInfos, ConstructorInfo constructorInfo )
        {
            List<Object> parametrs = GenerateParametersForConstructor(parameterInfos);
            Object obj =  constructorInfo.Invoke(parametrs.ToArray());
            return obj;
        }

        private Object CreateInner(Type type)
        {
            if (!_stack.Contains(type))
            {
                _stack.Push(type);
                Object obj;
                ConstructorInfo[] constructorInfos = type.GetConstructors();
                if (constructorInfos.Length != 0)
                {
                    ParameterInfo[] parameterInfos = constructorInfos[0].GetParameters();
                    int constructorIndex = 0;
                    for (int i = 1; i < constructorInfos.Length; i++)
                    {
                        if (parameterInfos.Length < constructorInfos[i].GetParameters().Length)
                        {
                            parameterInfos = constructorInfos[i].GetParameters();
                            constructorIndex = i;
                        }
                    }

                    obj = GenerateDTO(parameterInfos, constructorInfos[constructorIndex]);
                }
                else
                {
                    obj = FormatterServices.GetUninitializedObject(type);
                }

                GenerateFields(obj);
                _stack.Pop();
                return obj;
            }
            else
            {
                return null;
            }
        }
        public T Create<T>()
        {
            Type type = typeof(T);
            return (T) CreateInner(type);
        }
    }
}