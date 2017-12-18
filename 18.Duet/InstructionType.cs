namespace Duet
{
    using System;

    public enum InstructionType
    {
        Set = 0,
        Add = 1,
        Multiply = 2,
        Modulo = 3,
        ConditionalJump = 4,
        Play = 5,
        Recover = 6,
        Send = 7,
        Recieve = 8
    }
}