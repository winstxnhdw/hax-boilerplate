sealed class HaxObject : MonoBehaviour {
    internal static HaxObject? Instance { get; private set; }

    void Awake() => HaxObject.Instance = this;
}
