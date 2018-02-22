namespace Spinlock
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Spinlock
    {
        private LinkedList<int> buffer;

        public Spinlock(int step)
        {
            this.buffer = new LinkedList<int>();

            this.Step = step;
        }

        public int Step { get; private set; }

        private LinkedListNode<int> Current { get; set; }

        public void Fill(int count, Action<int> report = null)
        {
            int percent = count / 100;

            for (int i = 0; i < count; i++)
            {
                if (report != null && percent != 0 && (i % percent) == 0)
                {
                    report(i / percent);
                }

                this.Insert(i);
            }

            return;
        }

        public void Clear()
        {
            this.buffer.Clear();
        }

        public int GetValueAfter(int value)
        {
            var node = this.buffer.FindLast(value);

            if (this.buffer.Last == node)
            {
                return this.buffer.First.Value;
            }

            return node.Next.Value;
        }

        private void Insert(int value)
        {
            if (!this.buffer.Any())
            {
                this.buffer.AddFirst(value);
                this.Current = this.buffer.First;
                return;
            }

            this.Move(this.Step + 1);

            this.buffer.AddAfter(this.Current, value);
        }

        private void Move(int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (this.Current.Next == null)
                {
                    this.Current = this.buffer.First;
                }
                else
                {
                    this.Current = this.Current.Next;
                }
            }
        }
    }
}