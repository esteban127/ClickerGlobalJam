using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlUpButton : MonoBehaviour
{
    [SerializeField] Text costTxt;
    [SerializeField] Text damageTxt;
    int cost;
    int damage;

    public void Actualizate(int _cost, int _damage)
    {
        cost = _cost;
        damage = _damage;
        costTxt.text = cost.ToString();
        damageTxt.text = damage.ToString();
    }

    internal void SetOnMaxLvl(int _damage)
    {
        cost = 9999999;
        costTxt.text = "MAX";
        damage = _damage;
        damageTxt.text = damage.ToString();
    }
}
