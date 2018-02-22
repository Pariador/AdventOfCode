namespace Duet
{
    using System;

    public enum InstructionType
    {
        Set,
        Add,
        Subtract,
        Multiply,
        Modulo,
        JumpIfPositive,
        JumpIfNotZero,
        Play,
        Recover,
        Send,
        Recieve
    }
}