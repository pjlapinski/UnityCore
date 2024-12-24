namespace PJL.Core {
/// <summary>
///     MonoBehaviour automatically registered to GameGlobals
/// </summary>
public abstract class GlobalMonoBehaviour : InitMonoBehaviour {
    protected virtual void Awake() { GameGlobals.Register(GetType(), this); }
}
}
