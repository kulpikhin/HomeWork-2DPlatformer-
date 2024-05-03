using TMPro;
using UnityEngine;

public class BaseHPBar : HPBar
{
    [SerializeField] private TMP_Text _hpText;

    protected override void ChangeHPBar(int currentHealth)
    {
        _hpText.text = currentHealth + "/" + _maxHealth;
        _hpBar.value = currentHealth;
    }
}
