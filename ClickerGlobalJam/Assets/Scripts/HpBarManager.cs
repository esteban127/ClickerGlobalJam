using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Text enemyName;
    [SerializeField] Slider slider;
    int currentMaxHP;
    int currentHP;

    public void SetNewEnemy(string name, int health)
    {
        enemyName.text = name;
        currentMaxHP = health;
        currentHP = 0;
        slider.value = 0;
        hpText.text = currentHP.ToString() + " / " + currentMaxHP.ToString();
    }

    public void ActualizateLife(int newHp)
    {
        currentHP = currentMaxHP - newHp;
        slider.value = (float)currentHP/(float)currentMaxHP;
        hpText.text = currentHP.ToString() + " / " + currentMaxHP.ToString();
    }
}
