using System.Collections;
using UnityEngine;

public class SmoothHpBar : HPBar
{
    [SerializeField] private float _speedHpBar;

    private Coroutine _smoothlyChangeHPBar;

    protected override void ChangeHPBar(int curentHealth)
    {
        if (_smoothlyChangeHPBar != null)        
            StopCoroutine(_smoothlyChangeHPBar);        

        _smoothlyChangeHPBar = StartCoroutine(SmoothlyChangeHp(curentHealth));
    }

    private IEnumerator SmoothlyChangeHp(int curentHealth)
    {
        while (_hpBar.value != curentHealth)
        {
            _hpBar.value = Mathf.MoveTowards(_hpBar.value, curentHealth, _speedHpBar);
            yield return null;
        }
    }
}
