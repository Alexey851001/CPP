using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace AssemblyLibrary
{
    public class AssemblyLib
    {
        public static AssemblyInfo GetAssemblyInfo(string path)
        {
            try
            {
                AssemblyInfo assemblyInfo = new AssemblyInfo();
                Assembly assembly = Assembly.LoadFile(path);
                foreach (Type type in assembly.GetTypes())
                {
                    string currentNamespace = type.Namespace;
                    if (currentNamespace != null)
                    {
                        if (!assemblyInfo.NamespaceInfos.ContainsKey(currentNamespace))
                        {
                            assemblyInfo.NamespaceInfos.Add(currentNamespace, new NamespaceInfo());
                        }

                        assemblyInfo.NamespaceInfos[currentNamespace].DataTypeInfos.AddLast(
                            new DataTypeInfo(type.GetFields(),
                                type.GetProperties(),
                                type.GetMethods(), type.Name));
                    }
                }

                return assemblyInfo;
            } catch (Exception e)
            {
                return null;
            }
        }
        public static TreeViewItem BuildTree(AssemblyInfo assemblyInfo)
        {
            var root = new TreeViewItem();

            foreach (var keyValuePair in assemblyInfo.NamespaceInfos)
            {
                string namespaceName = keyValuePair.Key;
                ObservableCollection<TreeViewItem> namespaceList = root.Inner;
                TreeViewItem namespaceNode = new TreeViewItem();
                namespaceNode.Name = "Namespace " + namespaceName;
                namespaceList.Add(namespaceNode);

                foreach (DataTypeInfo dataTypeInfo in keyValuePair.Value.DataTypeInfos)
                {
                    TreeViewItem classInfo = new TreeViewItem();
                    classInfo.Name = "class " + dataTypeInfo.Name;

                    var methods = dataTypeInfo.MethodInfos;
                    foreach (var method in methods)
                    {
                        var methodInfo = new TreeViewItem();
                        string parametersString = null;
                        var parameters = method.GetParameters();
                        foreach (var parameter in parameters)
                        {
                            parametersString += parameter.ParameterType.Name + " " + parameter.Name + ", ";
                        }
                        if (!string.IsNullOrEmpty(parametersString))
                        {
                            parametersString = parametersString.Substring(0, parametersString.Length - 2);
                        }

                        methodInfo.Name = "Method " + (method.IsPublic ? "public " : "") + (method.IsPrivate ? "private " : "") + (method.IsStatic ? "static " : "") + method.ReturnType.Name + " " + method.Name + "(" + parametersString + ")";
                        classInfo.Inner.Add(methodInfo);
                    }

                    var properties = dataTypeInfo.PropertyInfos;
                    foreach (PropertyInfo property in properties)
                    {
                        TreeViewItem propertyInfo = new TreeViewItem();
                        propertyInfo.Name = "Property " + (property.CanWrite ? "set " : "") + (property.CanRead ? "get " : "") + property.PropertyType.Name + " " + property.Name;
                        classInfo.Inner.Add(propertyInfo);
                    }

                    var fields = dataTypeInfo.FieldInfos;
                    foreach (FieldInfo field in fields)
                    {
                        TreeViewItem fieldInfo = new TreeViewItem();
                        fieldInfo.Name = "Field "  + (field.IsPublic ? "public " : "") + (field.IsPrivate ? "private " : "") + (field.IsStatic ? "static " : "") + field.FieldType.Name + " " + field.Name;
                        classInfo.Inner.Add(fieldInfo);
                    }

                    namespaceNode.Inner.Add(classInfo);
                }
            }
            return root;
        }
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
    
    public class TreeViewItem
    {
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Inner { get; set; }

        public TreeViewItem()
        {
            Inner = new ObservableCollection<TreeViewItem>();
        }
    }
}
