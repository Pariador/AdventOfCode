namespace TheHaltingProblem
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class TuringMachine
    {
        private Dictionary<char, State> states;

        private LinkedList<bool> tape;

        public TuringMachine(Dictionary<char, State> states, char currentState = 'A')
        {
            this.States = states;
            this.CurrentState = currentState;

            this.tape = new LinkedList<bool>();

            this.tape.AddFirst(false);

            this.CurrentCell = tape.First;
        }

        public Dictionary<char, State> States
        {
            get
            {
                return this.states;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("States can't be null!");
                }

                this.states = value;
            }
        }

        public char CurrentState { get; set; }

        private LinkedListNode<bool> CurrentCell { get; set; }

        public void Execute()
        {
            State state = this.states[this.CurrentState];
            bool cell = this.CurrentCell.Value;
            Action action = state.Actions[cell ? 1 : 0];

            this.CurrentCell.Value = action.Write;

            if (action.Direction == Direction.Right)
            {
                if (this.CurrentCell.Next == null)
                {
                    this.CurrentCell = this.tape.AddLast(false);
                }
                else
                {
                    this.CurrentCell = this.CurrentCell.Next;
                }
            }
            else
            {
                if (this.CurrentCell.Previous == null)
                {
                    this.CurrentCell = this.tape.AddFirst(false);
                }
                else
                {
                    this.CurrentCell = this.CurrentCell.Previous;
                }
            }

            this.CurrentState = action.Next;
        }

        public void Excute(int times)
        {
            for (int i = 0; i < times; i++)
            {
                this.Execute();
            }
        }

        public int Checksum()
        {
            return this.tape.Count(value => value);
        }
    }
}