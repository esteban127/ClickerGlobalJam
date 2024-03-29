﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int[] hammerCost;
    [SerializeField] int[] hammerDamage;
    [SerializeField] int[] enemiesValues;
    [SerializeField] string[] enemiesNames;
    [SerializeField] Sprite[] enemiesSprites;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject[] cracks;
    [SerializeField] GameObject[] bigExplosions;
    [SerializeField] AudioManager audioManager;
    int currentEnemy = 0;
    int currentEnemyHP;    
    int currentGold = 0;
    int currentDamage = 1;
    int hammerLevel;

    public bool canTap = true;

    const int MAX_HAMMER_LVL = 12;

    [SerializeField] Text goldText;
    [SerializeField] GameObject enemy;
    [SerializeField] HpBarManager hpBar;
    [SerializeField] LvlUpButton lvlUpButton;
    [SerializeField] GameObject customCursor;

    private void Start()
    {
        currentEnemyHP = enemiesValues[currentEnemy];
        ChangeEnemy();
        ActualizateGold();
        ActualizateButton();

    }

    public void Tap()
    {
        enemy.GetComponent<Animator>().SetTrigger("Clicked");
        if (currentEnemyHP >= currentDamage)
        {
            currentEnemyHP -= currentDamage;
            AddGold(currentDamage);
        }
        else
        {
            int actualDamage = currentEnemyHP;
            currentEnemyHP -= actualDamage;
            AddGold(actualDamage);

        }
        if (currentEnemyHP < (float)enemiesValues[currentEnemy] / 3.0f)
        {
            cracks[0].SetActive(false);
        }
        if (currentEnemyHP < ((float)enemiesValues[currentEnemy] / 3.0f)*2.0f)
        {
            cracks[1].SetActive(false);
        }
        if(currentEnemyHP<= 0)
        {
            cracks[2].SetActive(false);
            if(currentEnemy < enemiesValues.Length - 1)
            {
                currentEnemy++;
                StartCoroutine(ChangeEnemyAnimation(false));
            }
            else
            {
                StartCoroutine(ChangeEnemyAnimation(true));
            }
        }

        ActualizateEnemyHP();
    }

    IEnumerator ChangeEnemyAnimation(bool end)
    {
        canTap = false;
        foreach(GameObject bigExplosion in bigExplosions)
        {
            bigExplosion.SetActive(true);
            bigExplosion.GetComponent<ExplosionBehaviour>().Explode();
        }
        yield return new WaitForSeconds(1.0f);
        audioManager.PlaySfx(1, 1, 1);
        enemy.GetComponent<Animator>().SetTrigger("Done");
        yield return new WaitForSeconds(1.0f);

        if (!end)
        {
            ChangeEnemy();
        }
        else
        {
            TriggerEndGame();
        }
        yield return new WaitForSeconds(1.0f);
        ReturnInput();
    }

    private void TriggerEndGame()
    {
        endScreen.SetActive(true);
    }
    private void ReturnInput()
    {

        canTap = true;
    }
    private void ChangeEnemy()
    {
        enemy.GetComponent<Animator>().SetTrigger("Reset");
        foreach (GameObject crack in cracks)
        {
            crack.SetActive(true);
        }
        currentEnemyHP = enemiesValues[currentEnemy];
        enemy.GetComponent<SpriteRenderer>().sprite = enemiesSprites[currentEnemy];
        hpBar.SetNewEnemy(enemiesNames[currentEnemy], enemiesValues[currentEnemy]);
    }

    private void ActualizateGold()
    {
        goldText.text = currentGold.ToString();
    }

    private void ActualizateEnemyHP()
    {
        hpBar.ActualizateLife(currentEnemyHP);
    }

    public void LevelUpHammer()
    {
        if(currentGold >= hammerCost[hammerLevel + 1])
        {
            hammerLevel++;
            customCursor.GetComponent<Animator>().SetInteger("CurrentHammerLvl", hammerLevel);
            currentGold -= hammerCost[hammerLevel];
            ActualizateGold();
            currentDamage = hammerDamage[hammerLevel];            
            ActualizateButton();
        }
    }
    private void AddGold(int ammount)
    {
        currentGold += ammount;
        if(hammerLevel != MAX_HAMMER_LVL && currentGold >= hammerCost[hammerLevel + 1])
        {
            lvlUpButton.GetComponent<Button>().interactable = true;
        }
        ActualizateGold();
    }

    private void ActualizateButton()
    {
        lvlUpButton.GetComponent<Button>().interactable = false;
        if (hammerLevel != MAX_HAMMER_LVL)
        {
            lvlUpButton.Actualizate(hammerCost[hammerLevel + 1], currentDamage);
            if(hammerLevel == 4)
            {
                lvlUpButton.ChangeHammer(1);
            }
            if (hammerLevel == 9)
            {
                lvlUpButton.ChangeHammer(2);
            }
            if (currentGold >= hammerCost[hammerLevel + 1])
            {
                lvlUpButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            lvlUpButton.SetOnMaxLvl(currentDamage);
            lvlUpButton.ChangeHammer(3);
        }
    }

}
