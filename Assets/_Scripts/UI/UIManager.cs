using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    private void Awake() => instance = this;

    public CooldownsBar cooldownsBar;
    public CogCounter cogCounter;

}
