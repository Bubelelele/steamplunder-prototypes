using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStats : MonoBehaviour, IDamageable {

    [Header("Health")]
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private Healthbar healthbar;

    [Header("Low Health FX")]
    [SerializeField] private PostProcessVolume ppVolume;
    [SerializeField] private int lowHealthAt = 30;
    [SerializeField] private Animation lowHealthAnim;

    public int Cogs { get; private set; }

    private int _health;
    private Vignette _vignette;
    private ColorGrading _colorGrading;
    private float _vignetteTarget;
    private float _saturationTarget;

    private void Start() {
        _health = maxHealth;
        ppVolume.profile.TryGetSettings(out _vignette);
        ppVolume.profile.TryGetSettings(out _colorGrading);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha5)) Damage(25);

        _vignette.intensity.value = 
            Mathf.MoveTowards(_vignette.intensity.value, _vignetteTarget, Time.deltaTime/2);
        _colorGrading.saturation.value = 
            Mathf.MoveTowards(_colorGrading.saturation.value, _saturationTarget, Time.deltaTime/2);
    }

    public void Damage(int amount) {
        _health -= amount;
        if (_health <= 0) Die();
        
        healthbar.UpdateHealthbar(_health, maxHealth);
        
        LowHealthFX();
    }

    public void AddCogs(int amount) {
        Cogs += amount;
        UIManager.instance.cogCounter?.UpdateCogUI(Cogs);
    }

    private void Die() {
        GameManager.instance.UpdateGameState(GameState.NoMove);
        EffectManager.instance.DeathEffect(transform.position);
        GameManager.instance.WaitReloadScene(1f);
        gameObject.SetActive(false);
        
        
    }

    private void LowHealthFX() {
        if (_health <= lowHealthAt) {
            float fraction = 1 - (float) _health / lowHealthAt;
            
            _vignetteTarget = .6f * fraction;
            _saturationTarget = -60f * fraction;
            lowHealthAnim.Play();
        } else {
            _vignetteTarget = 0f;
            _saturationTarget = 0f;
            if (lowHealthAnim.isPlaying) lowHealthAnim.Stop();
        }
    }
    
}