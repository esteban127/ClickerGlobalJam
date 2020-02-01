using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int[] hammerCost;
    [SerializeField] int[] hammerDamage;
    [SerializeField] int[] enemieValues;

    [SerializeField] Sprite[] hammerSprite;
    [SerializeField] Sprite[] enemies;
    int currentEnemy = 0;
    int currentEnemyHP;
    int currentGold = 0;
    int currentDamage = 1;
    int hammerLevel;

    const int MAX_HAMMER_LVL = 13;

    [SerializeField] GameObject enemy;
    [SerializeField]LvlUpButton lvlUpButton;
    public Sprite GetCurrentHammer()
    {
        if (hammerLevel == MAX_HAMMER_LVL)
        {
            return hammerSprite[3];
        }
        if( hammerLevel >= 10)
        {
            return hammerSprite[2];
        }
        if (hammerLevel >= 5)
        {
            return hammerSprite[1];
        }
        return hammerSprite[0];
    }
    private void Start()
    {
        currentEnemyHP = enemieValues[currentEnemy];
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
            if(currentEnemy < enemieValues.Length - 1)
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
        throw new NotImplementedException();
    }

    private void ChangeEnemy()
    {
        throw new NotImplementedException();
    }

    private void ActuaulizateGold()
    {
        Debug.Log("Not implemented");
        //throw new NotImplementedException();
    }

    private void ActualizateEnemyHP()
    {
        Debug.Log("Not implemented");
        //throw new NotImplementedException();
    }

    public void LevelUpHammer()
    {
        if(currentGold >= hammerCost[hammerLevel + 1])
        {
            hammerLevel++;
            currentGold -= hammerCost[hammerLevel];
            ActuaulizateGold();
            currentDamage = hammerDamage[hammerLevel];            
            ActualizateButton();
        }
    }
    private void AddGold(int ammount)
    {
        currentGold += ammount;
        if(hammerLevel != MAX_HAMMER_LVL && currentGold >= hammerCost[hammerLevel + 1])
        {
            lvlUpButton.GetComponent<Button>().enabled = true;
        }
        ActuaulizateGold();
    }

    private void ActualizateButton()
    {
        lvlUpButton.GetComponent<Button>().enabled = false;
        if (hammerLevel != MAX_HAMMER_LVL)
        {
            lvlUpButton.Actualizate(hammerCost[hammerLevel + 1], currentDamage);

            if (currentGold >= hammerCost[hammerLevel + 1])
            {
                lvlUpButton.GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            lvlUpButton.SetOnMaxLvl(currentDamage);
        }
    }

}
