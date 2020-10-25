using System.Linq;
using AssemblyLibrary;
using NUnit.Framework;

namespace TestProject1
{

    public class Tests
    {
        private const string PATH = "D:\\5sem\\SPP\\CPP\\Lab_2\\app\\app\\bin\\Debug\\netcoreapp3.1\\app.dll";
        
        [Test]
        public void ShouldReturnNullForInvalidPath()
        {
            Assert.Null(AssemblyLib.GetAssemblyInfo("some"));
        }
        [Test]
        public void NumberOfNamespacesShouldBeCorrect()
        {
            Assert.AreEqual(4, AssemblyLib.GetAssemblyInfo(PATH)?.NamespaceInfos?.Count);
        }
        
        [Test]
        public void NumberOfMethodsShouldBeCorrect()
        {
            Assert.AreEqual(4, AssemblyLib.GetAssemblyInfo(PATH)?.NamespaceInfos.Values.First().DataTypeInfos.First().MethodInfos.Length);
        }
        
        [Test]
        public void NumberOfFieldsShouldBeCorrect()
        {
            Assert.AreEqual(0, AssemblyLib.GetAssemblyInfo(PATH)?.NamespaceInfos.Values.First().DataTypeInfos.First().FieldInfos.Length);
        }
        
        [Test]
        public void NumberOfPropertiesShouldBeCorrect()
        {
            Assert.AreEqual(0, AssemblyLib.GetAssemblyInfo(PATH)?.NamespaceInfos?.Values.First().DataTypeInfos.First().PropertyInfos.Length);
        }
    }
}