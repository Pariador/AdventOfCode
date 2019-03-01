namespace MemoryManeuver
{
    using System.Collections.Generic;

    class Node<T>
    {
        public Node()
        {
            this.Children = new List<Node<T>>();
        }

        public Node(T value)
            : this()
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public List<Node<T>> Children { get; }
    }
}