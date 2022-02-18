using UnityEngine;

public class GearManager : MonoBehaviour {

    private AxeController _axeController;
    private HammerController _hammerController;
    private GunController _gunController;
    private SteamerController _steamerController;
    private GrappleController _grappleController;

    public bool ActionOngoing { get; private set; }
    public bool no;
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
        if (Input.GetMouseButtonDown(InputManager.instance.AxeMouseBtn)) InvokeAxeAction();
        else if (Input.GetMouseButtonDown(InputManager.instance.GunMouseBtn)) InvokeGunAction();
        else if (Input.GetKeyDown(InputManager.instance.HammerBtn)) InvokeHammerAction();
        else if (Input.GetKeyDown(InputManager.instance.SteamerBtn)) InvokeSteamerAction();
        else if (Input.GetKeyDown(InputManager.instance.GrappleBtn)) InvokeGrappleAction();
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
        if (ActionOngoing || !_hammerController.CanAttack || !no) return;
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
    
}