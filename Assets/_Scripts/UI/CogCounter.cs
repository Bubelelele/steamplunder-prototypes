using TMPro;
using UnityEngine;

public class CogCounter : MonoBehaviour {

    [SerializeField] private TMP_Text amountText;

    public void UpdateCogUI(int amount) {
        amountText.text = amount.ToString();
    }

}