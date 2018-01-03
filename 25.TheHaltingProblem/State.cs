namespace TheHaltingProblem
{
    using System;
    using System.Linq;

    public class State
    {
        private Action[] actions;

        public State(char id, Action[] actions)
        {
            this.Id = id;
            this.Actions = actions;
        }

        public char Id { get; }
        
        public Action[] Actions
        {
            get
            {
                return this.actions.ToArray();
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Actions can't be null!");
                }

                if (value.Length != 2)
                {
                    throw new ArgumentException("State must have exactly 2 actions!");
                }

                this.actions = value;
            }
        }
    }
}