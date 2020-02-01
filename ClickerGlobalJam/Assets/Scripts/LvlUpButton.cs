using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlUpButton : MonoBehaviour
{
    [SerializeField] Text costTxt;
    [SerializeField] Text damageTxt;
    [SerializeField] GameObject coin;
    [SerializeField] Image hammerImg;
    [SerializeField] Sprite[] hammersSprite;
    int cost;
    int damage;

    public void Actualizate(int _cost, int _damage)
    {
        cost = _cost;
        damage = _damage;
        costTxt.text = cost.ToString();
        damageTxt.text = "Current repair: " + damage.ToString();
    }
    public void ChangeHammer(int hammerEvolution)
    {
        hammerImg.sprite = hammersSprite[hammerEvolution];
    }

    internal void SetOnMaxLvl(int _damage)
    {
        cost = 9999999;
        costTxt.text = "MAX";
        coin.SetActive(false);
        damage = _damage;
        damageTxt.text = "Current repair: " + damage.ToString();
    }
}
