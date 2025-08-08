namespace Hax;

public class HaxObject : MonoBehaviour {
    public static HaxObject? Instance { get; private set; }

    void Awake() => HaxObject.Instance = this;
}
