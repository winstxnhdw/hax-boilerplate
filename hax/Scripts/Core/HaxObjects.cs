using GameNetcodeStuff;
using UnityEngine;

namespace Hax;

public class HaxObjects : MonoBehaviour {
    public static HaxObjects? Instance { get; private set; }

    void Awake() {
        HaxObjects.Instance = this;
    }
}
