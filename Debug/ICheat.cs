using System.Collections.Generic;

namespace PJL.Debug
{
    public interface ICheat
    {
        public string Command { get; }
        public int NumArgs { get; }

        public bool TryExecute(IEnumerable<string> arguments);
    }
}