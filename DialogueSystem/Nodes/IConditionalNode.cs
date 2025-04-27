using NaughtyAttributes;

namespace PJL.DialogueSystem
{
    internal interface IConditionalNode
    {
        internal static DropdownList<string> GetPathSelectors(DialogueGraph dg)
        {
            var list = new DropdownList<string>();
            if (dg.RequiredFuncPathSelectors.Length == 0)
            {
                list.Add(DialogueGraph.NullPathSelector, string.Empty);
                return list;
            }

            foreach (var selector in dg.RequiredFuncPathSelectors)
                list.Add(selector, selector);
            return list;
        }
    }
}