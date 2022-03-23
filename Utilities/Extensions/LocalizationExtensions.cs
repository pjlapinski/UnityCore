using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace PJL.Utilities.Extensions
{
    public static class LocalizationExtensions
    {
        public static void SetSmartValue(this LocalizedString localizedString, string key, IVariable value)
        {
            localizedString.Remove(key);
            localizedString[key] = value;
        }
    }
}