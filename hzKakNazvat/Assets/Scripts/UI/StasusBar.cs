using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StasusBar : MonoBehaviour
{
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image ArmorBar;
    [SerializeField] private Image ArmorIcon;
    [SerializeField] private Image HealthIcon;
    [SerializeField] private TextMeshProUGUI HealthText;
    [SerializeField] private TextMeshProUGUI ArmorText;
    private void OnEnable() {
        AbstractPlayer.OnPlayerHealthChanged += ChangeStatusBar;
    }

    private void OnDisable() {
        AbstractPlayer.OnPlayerHealthChanged -= ChangeStatusBar;
    }
    private void ChangeStatusBar(int MaxArmor, int Armor, int MaxHealth, int Health) {
        HealthBar.fillAmount = (float) Health / MaxHealth;
        ArmorBar.fillAmount = (float) Armor / MaxArmor;
        HealthText.text = Health.ToString() + "/" + MaxHealth.ToString();
        ArmorText.text = Armor.ToString() + "/" + MaxArmor.ToString();
        if (Armor <= 0)
            ArmorIcon.enabled = false;
        else
            ArmorIcon.enabled = true;
        if (Health <=0 )    
            HealthIcon.enabled = false;
    }
}
