namespace app.TestClasses
{
    public class Temp
    {
        
    }

    public class Foo
    {
        public Bar BarObj;
        public int IValue;
        public bool BValue;
        public double DValue;
        public string SValue;
        private int pi;
        
        public Foo() {}

        public Foo(int integerValue, bool boolValue, string stringValue)
        {
            IValue = integerValue;
            BValue = boolValue;
            SValue = stringValue;
            
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
        private Bar(){}
    }

    public class A
    {
        public B b;
    }

    public class B
    {
        public C c;
    }

    public class C
    {
        public A a;
    }
}