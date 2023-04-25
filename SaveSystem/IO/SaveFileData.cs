namespace PJL.SaveSystem.IO
{
    public struct SaveFileData
    {
        public int Index { get; }
        public string Preamble { get; }

        public SaveFileData(int index, string preamble)
        {
            Index = index;
            Preamble = preamble;
        }
    }
}