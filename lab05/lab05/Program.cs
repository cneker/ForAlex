using System.Text;

namespace lab05
{
    public class Sum
    {
        public static long Accum(params int[] values) => values.Select(Convert.ToInt64).Sum();
    }

    internal class Program
    {
        static void Main(String[] arts)
        { }
    }

    public class StringUtils
    {
        public static string Keep(string str, string pattern) => (str, pattern) switch
        {
            (null, null) => throw new NullReferenceException(),
            (null, not null) => null,
            ("", not null) => "",
            (not null, null) => "",
            (not null, "") => "",
            _ => string.Join("", str.Where(s => pattern.Contains(s)))
        };
    }

    public class Stack<T>
    {
        private class Node
        {
            public T Item { get; set; }
            public Node Next { get; set; }
        }

        private Node _first;
        private int _N;
        public Stack()
        {
            _first = null;
            _N = 0;
        }

        public bool IsEmpty() => _N < 1;

        public int Size() => _N;

        public void Push(T item)
        {
            Node oldfirst = _first;
            _first = new Node();
            _first.Item = item;
            _first.Next = oldfirst;
            _N++;
        }

        public T Pop()
        {
            if (Size() == 0)
                throw new ArgumentNullException();
            T item = _first.Item;
            _first = _first.Next;
            _N--;
            return item;
        }

        public T Peek()
        {
            if (Size() == 0)
                throw new ArgumentNullException();
            else
                return _first.Item;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (Node current = _first; current != current.Next; current = current.Next)
            {
                T item = current.Item;
                s.Append(item);
                if (current.Next == null)
                    return s.ToString();
                s.Append(" - ");
            }

            return s.ToString();
        }
    }
}