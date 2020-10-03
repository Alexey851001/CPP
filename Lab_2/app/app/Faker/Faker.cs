using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace app.Faker
{
    public class Faker
    {
        public Faker(){}

        private List<Object> GenerateParametersForConstructor(ParameterInfo[] parameterInfos)
        {
            const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            const int MAX_STRING_SIZE = 50;
            List<Object> result = new List<Object>();

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                Random random = new Random();
                Type parameterType = parameterInfos[i].ParameterType;
                if (parameterType == typeof(int))
                {
                    int integerValue = random.Next();
                    result.Add(integerValue);
                }
                if (parameterType == typeof(bool))
                {
                    bool boolValue = (random.Next(0, 2) == 0);
                    result.Add(boolValue);
                }
                if (parameterType == typeof(long))
                {
                    long longValue = random.Next();
                    result.Add(longValue);
                }
                
                if (parameterType == typeof(string))
                {
                    int stringLength = random.Next(0,MAX_STRING_SIZE);
                    List<char> charList = new List<char>();
                    for (int j = 0; j < stringLength; j++)
                    {
                        charList.Add(ALPHABET[random.Next(0,ALPHABET.Length + 1)]);
                    }
                    string stringValue = new string(charList.ToArray());
                    result.Add(stringValue);
                }
                
                if (parameterType == typeof(float))
                {
                    int numerator = random.Next();
                    int denominator = random.Next();
                    float floatValue = numerator / denominator;
                    result.Add(floatValue);
                }
                
            }
            return result;
        }

        private T GenerateDTO<T>(ParameterInfo[] parameterInfos, ConstructorInfo constructorInfo )
        {
            List<Object> parametrs = GenerateParametersForConstructor(parameterInfos);
            T obj = (T) constructorInfo.Invoke(parametrs.ToArray());
            return obj;
        }
        public T Create<T>()
        {
            T obj;
            Type type = typeof(T);
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

                obj = GenerateDTO<T>(parameterInfos, constructorInfos[constructorIndex]);
            }
            else
            {
                obj = (T) FormatterServices.GetUninitializedObject(type);
            }

            return obj;
        }
    }
}