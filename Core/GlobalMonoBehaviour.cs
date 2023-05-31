namespace PJL.Core
{
    /// <summary>
    /// MonoBehaviour automatically registered to GameGlobals
    /// </summary>
    public abstract class GlobalMonoBehaviour<T> : InitMonoBehaviour<T> where T : GlobalMonoBehaviour<T>
    {
        protected virtual void Awake()
        {
            GameGlobals.Register((T)this);
        }
    }
}
