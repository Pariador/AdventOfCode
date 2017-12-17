namespace Spinlock
{
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class Spinlock
    {
        private List<int> buffer;

        public Spinlock(int step)
        {
            this.buffer = new List<int>();

            this.Step = step;
        }

        public int Step { get; private set; }

        public int Position { get; private set; }

        public void Fill(int count, Action<int> report = null)
        {
            for (int i = 0; i < count; i++)
            {
                if (report != null && i % 100000 == 0)
                {

                    report(i);
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
            //var node = this.buffer.FindLast(value);

            //if (this.buffer.Last == node)
            //{
            //    return this.buffer.First.Value;
            //}

            //return node.Next.Value;

            int index = this.buffer.LastIndexOf(value);

            if (index < 0)
            {
                return index;
            }

            index = (index + 1) % this.buffer.Count;

            return this.buffer[index];
        }

        private void Insert(int value)
        {
            if (!this.buffer.Any())
            {
                this.buffer.Add(value);
                //this.buffer.AddFirst(value);
                return;
            }

            this.Position = (this.Position + this.Step + 1) % this.buffer.Count;

            //var node = this.buffer.NodeAt(this.Position);

            //this.buffer.AddBefore(node, value);

            this.buffer.Insert(this.Position, value);
        }
    }
}