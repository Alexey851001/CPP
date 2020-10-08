using System;
using System.Collections.Generic;

namespace app.TestClasses
{
    public class Temp {}

    public class Foo
    {
        public Bar BarObj;
        public int IValue;
        public bool BValue;
        public double DValue;
        public string SValue;
        public List<int> ListValue;
        private int pi;
        
        public Foo() {}

        public Foo(int integerValue, bool boolValue, string stringValue)
        {
            IValue = integerValue;
            BValue = boolValue;
            SValue = stringValue;
            
        }

        public Foo(double doubleValue)
        {
            DValue = doubleValue;
        }
        public int Pi
        {
            get => pi;
            set => pi = value;
        }
    }

    public class Bar
    {
        public int IValue;
        public DateTime DateTimeValue;
        public char CValue;
        private Bar(){}
    }

    public class A
    {
        public B b;
        public byte ByValue;
    }

    public class B
    {
        public C c;
        public byte ByValue;
    }

    public class C
    {
        public A a;
        public byte ByValue;
    }
}