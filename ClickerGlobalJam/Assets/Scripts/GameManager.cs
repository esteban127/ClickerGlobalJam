using System;
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
    int currentEnemy = 0;
    int currentEnemyHP;
    int currentGold = 0;
    int currentDamage = 1;
    int hammerLevel;

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
        if(currentEnemyHP<= 0)
        {
            if(currentEnemy < enemiesValues.Length - 1)
            {
                currentEnemy++;
                ChangeEnemy();
            }
            else
            {
                TriggerEndGame();
            }
        }

        ActualizateEnemyHP();
    }

    private void TriggerEndGame()
    {
        endScreen.SetActive(true);
    }

    private void ChangeEnemy()
    {
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
