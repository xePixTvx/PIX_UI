using System;

namespace PIX_UI_TEST
{
    class Program
    {
        private static TestApp Test;
        static void Main(string[] args)
        {
            Test = new TestApp("data", "config.cfg", "Test App", 1280, 720);
            Test.Start();
        }
    }
}
