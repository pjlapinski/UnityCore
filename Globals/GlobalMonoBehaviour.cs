namespace PJL.Globals
{
    public abstract class GlobalMonoBehaviour<T> : InitMonoBehaviour<T> where T : GlobalMonoBehaviour<T>
    {
        protected virtual void Awake()
        {
            GameGlobals.Register((T)this);
        }
    }
}
