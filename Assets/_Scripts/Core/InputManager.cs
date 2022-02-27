using System;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager instance;
    private void Awake() => instance = this;

    // -----
    // Keybindings
    // - Player
    public int AxeMouseBtn => 0;
    public int GunMouseBtn => 1;
    public KeyCode HammerBtn => KeyCode.F;
    public KeyCode GrappleBtn => KeyCode.R;
    public KeyCode SteamerBtn => KeyCode.Q;
    // - Misc
    public KeyCode InteractBtn => KeyCode.E;
    public KeyCode ReloadBtn => KeyCode.K;
    public KeyCode PauseBtn => KeyCode.Escape;
    // -----
    
    public Vector2 InputDir { get; private set; }
    
    public Vector3 InputDir3 => new Vector3(InputDir.x, 0, InputDir.y);
    
    public Vector3 MousePos { get; private set; }
    
    private void Update() {
        var hori = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");
        InputDir = new Vector2(hori, vert).normalized;

        MousePos = Input.mousePosition;
    }

    
}