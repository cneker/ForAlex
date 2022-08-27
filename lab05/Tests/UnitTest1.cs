using lab05;

namespace Tests
{
    public class UnitTest1
    {
        //[Fact]
        //public void TestSmallInt()
        //{
        //    //arrange
        //    int a = 1;
        //    int b = 2;
        //    int c = 3;
        //    int d = 4;
        //    int expected = 10;
        //    //act
        //    int actual = Sum.Accum(a, b, c, d);
        //    //assert
        //    Assert.Equal(expected, actual);
        //}

        [Fact]
        public void TestBigInt2()
        {
            int a = 2_000_000_000;
            int b = 1_000_000_000;
            long expected = 3_000_000_000;

            long actual = Sum.Accum(a, b);

            Assert.Equal(expected, actual);
        }
    }

    public class UnitTest2
    {
        [Fact]
        public void TestKeepNullAndNull()
        {
            string a = null;
            string b = null;

            Assert.Throws<NullReferenceException>(() => StringUtils.Keep(a, b));
        }

        [Fact]
        public void TestKeep_NullAndAsterisk()
        {
            string a = null;
            string b = "test";
            string expected = null;

            string result = StringUtils.Keep(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestKeep_EmptyStringAndAsterisk()
        {
            string a = "";
            string b = "test";
            string expected = "";

            string result = StringUtils.Keep(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestKeep_AsteriskAndNull()
        {
            string a = "test";
            string b = null;
            string expected = "";

            string result = StringUtils.Keep(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestKeep_AsteriskAndEmptyString()
        {
            string a = "test";
            string b = "";
            string expected = "";

            string result = StringUtils.Keep(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestKeep_helloAndhl()
        {
            string a = "hello";
            string b = "hl";
            string expected = "hll";

            string result = StringUtils.Keep(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestKeep_helloAndle()
        {
            string a = "hello";
            string b = "le";
            string expected = "ell";

            string result = StringUtils.Keep(a, b);

            Assert.Equal(expected, result);
        }
    }

    public class UnitTest3
    {
        private lab05.Stack<string> _stack = new lab05.Stack<string>();

        [Fact]
        public void TestStack_PopEmptyStack()
        {
            Assert.Throws<ArgumentNullException>(() => _stack.Pop());
        }

        [Fact]
        public void TestStack_PeekEmptyStack()
        {
            Assert.Throws<ArgumentNullException>(() => _stack.Peek());
        }

        [Fact]
        public void TestStack_IsEmpty()
        {
            bool expected = true;

            bool actual = _stack.IsEmpty();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestStack_Size()
        {
            int expected = 0;

            int actual = _stack.Size();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestStack_Push555()
        {
            string expected = "555";

            _stack.Push("555");
            string actual = _stack.Pop();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestStack_Peek()
        {
            string expected = "555";

            _stack.Push("555");
            string actual = _stack.Peek();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestStack_ToString()
        {
            string expected = "333 - 444 - 555";

            _stack.Push("555");
            _stack.Push("444");
            _stack.Push("333");
            string actual = _stack.ToString();

            Assert.Equal(expected, actual);
        }
    }
}