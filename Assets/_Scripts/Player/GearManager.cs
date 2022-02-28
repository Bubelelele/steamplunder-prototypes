using UnityEngine;

public class GearManager : MonoBehaviour {
    
    public bool ActionOngoing { get; private set; }
    public bool AxeActive { get; private set; } = true;
    public bool HammerActive { get; private set; } = true;
    public bool GunActive { get; private set; } = true;
    public bool SteamerActive { get; private set; } = true;
    public bool GrappleActive { get; private set; } = true;

    private AxeController _axeController;
    private HammerController _hammerController;
    private GunController _gunController;
    private SteamerController _steamerController;
    private GrappleController _grappleController;

    private void Start() {
        _axeController = GetComponent<AxeController>();
        _axeController.onAttackFinished += ActionFinished;
        
        _hammerController = GetComponent<HammerController>();
        _hammerController.onAttackFinished += ActionFinished;

        _gunController = GetComponent<GunController>();
        _gunController.onAttackFinished += ActionFinished;

        _steamerController = GetComponent<SteamerController>();
        _steamerController.onAttackFinished += ActionFinished;
        
        _grappleController = GetComponent<GrappleController>();
        _grappleController.onAttackFinished += ActionFinished;
    }

    private void Update() {
        if (GameManager.instance.state != GameState.Default) return;
        
        if (Input.GetMouseButtonDown(InputManager.instance.AxeMouseBtn) && AxeActive) InvokeAxeAction();
        else if (Input.GetMouseButtonDown(InputManager.instance.GunMouseBtn) && GunActive) InvokeGunAction();
        else if (Input.GetKeyDown(InputManager.instance.HammerBtn) && HammerActive) InvokeHammerAction();
        else if (Input.GetKeyDown(InputManager.instance.SteamerBtn) && SteamerActive) InvokeSteamerAction();
        else if (Input.GetKeyDown(InputManager.instance.GrappleBtn) && GrappleActive) InvokeGrappleAction();
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) ToggleAxe(!AxeActive);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ToggleHammer(!HammerActive);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ToggleGun(!GunActive);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ToggleSteamer(!SteamerActive);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ToggleGrapple(!GrappleActive);
    }
    

    private void ActionFinished() {
        ActionOngoing = false;
        _axeController.SetObjectActive(true);
    }

    private void InvokeAxeAction() {
        if (ActionOngoing || !_axeController.CanAttack) return;
        ActionOngoing = true;
        _axeController.Slash();
    }
    
    private void InvokeGunAction() {
        if (ActionOngoing || !_gunController.CanAttack) return;
        ActionOngoing = true;
        _gunController.Aim();
    }

    private void InvokeHammerAction() {
        if (ActionOngoing || !_hammerController.CanAttack) return;
        ActionOngoing = true;
        _axeController.SetObjectActive(false);
        _hammerController.Smash();
    }
    
    private void InvokeSteamerAction() {
        if (ActionOngoing || !_steamerController.CanAttack) return;
        ActionOngoing = true;
        _axeController.SetObjectActive(false);
        _steamerController.SuckMoisture();
    }
    
    private void InvokeGrappleAction() {
        if (ActionOngoing || !_grappleController.CanAttack) return;
        ActionOngoing = true;
        _axeController.SetObjectActive(false);
        _grappleController.Extend();
    }

    public void ToggleAxe(bool active) {
        AxeActive = active;
        UIManager.instance.cooldownsBar.ToggleAxeUI(active);
    }
    
    public void ToggleHammer(bool active) {
        HammerActive = active;
        UIManager.instance.cooldownsBar.ToggleHammerUI(active);
    }
    
    public void ToggleGun(bool active) {
        GunActive = active;
        UIManager.instance.cooldownsBar.ToggleGunUI(active);
    }
    
    public void ToggleSteamer(bool active) {
        SteamerActive = active;
        UIManager.instance.cooldownsBar.ToggleSteamerUI(active);
    }
    
    public void ToggleGrapple(bool active) {
        GrappleActive = active;
        UIManager.instance.cooldownsBar.ToggleGrappleUI(active);
    }
    
}