using System;
using System.Reflection;

namespace app.Faker
{
    public class Faker
    {
        public Faker(){}

        private T GenerateDTO<T>(ParameterInfo[] parameterInfos, ConstructorInfo constructorInfo )
        {

            return default(T);
        }
        public T Create<T>()
        {
            Type type = typeof(T);
            ConstructorInfo[] constructorInfos = type.GetConstructors();
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
            
            T obj = GenerateDTO<T>(parameterInfos, constructorInfos[constructorIndex]);
            
            return obj;
        }
    }
}