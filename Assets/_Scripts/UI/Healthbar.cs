using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    
    [SerializeField] private Image healthbarImage;
    [SerializeField] private float reduceSpeed = 2f;
    [SerializeField] private TMP_Text healthText;
    
    private float _target = 1;
    
    public void UpdateHealthbar(int currentHealth, int maxHealth) {
        _target = (float) currentHealth / maxHealth;
        if (healthText != null) healthText.text = currentHealth + "/" + maxHealth;
    }

    private void Update() {
        healthbarImage.fillAmount = Mathf.MoveTowards(healthbarImage.fillAmount, _target, reduceSpeed * Time.deltaTime);
    }
}