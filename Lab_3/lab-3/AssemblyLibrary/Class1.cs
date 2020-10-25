﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyLibrary
{
    public class AssemblyLib
    {
        public static AssemblyInfo GetAssemblyInfo(string path)
        {
            AssemblyInfo assemblyInfo = new AssemblyInfo();
            Assembly assembly = Assembly.LoadFile(path);
            foreach (Type type in assembly.GetTypes())
            {
                string currentNamespace = type.Namespace;
                if (!assemblyInfo.NamespaceInfos.ContainsKey(currentNamespace))
                {
                    assemblyInfo.NamespaceInfos.Add(currentNamespace, new NamespaceInfo());
                }
                NamespaceInfo namespaceInfo = assemblyInfo.NamespaceInfos[currentNamespace];

                namespaceInfo.DataTypeInfos.AddLast(new DataTypeInfo(type.GetFields(), type.GetProperties(),
                    type.GetMethods(), type.Name));
            }

            return assemblyInfo;
        } 
        
        public class AssemblyInfo
        {
            public Dictionary<string, NamespaceInfo> NamespaceInfos = new Dictionary<string, NamespaceInfo>();
        }
        
        public class NamespaceInfo
        {
            public LinkedList<DataTypeInfo> DataTypeInfos = new LinkedList<DataTypeInfo>();
        }

        public class DataTypeInfo
        {
            public string Name;
            public FieldInfo[] FieldInfos;
            public PropertyInfo[] PropertyInfos;
            public MethodInfo[] MethodInfos;

            public DataTypeInfo(FieldInfo[] fieldInfos, PropertyInfo[] propertyInfos, MethodInfo[] methodInfos, string name)
            {
                FieldInfos = fieldInfos;
                PropertyInfos = propertyInfos;
                MethodInfos = methodInfos;
                Name = name;
            }
        }

    }
}