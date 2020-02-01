using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    [SerializeField] GameObject customCursor;
    [SerializeField] Sprite DefaultPointer;
    [SerializeField] GameManager gameManager;
    [SerializeField] Vector3 hammerOffset;
    [SerializeField] Vector3 explosionOffset;
    [SerializeField] GameObject explosionPool;
    [SerializeField] AudioManager audioManager;
    
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.visible = false;          
            
            if(Application.platform == RuntimePlatform.Android)
            {
                if (Input.touches.Length >= 0)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        gameManager.Tap();
                        TriggerExplosion(Camera.main.ScreenToWorldPoint(Input.touches[0].position) + explosionOffset);
                    }
                }
            }            
            else
            {
                customCursor.GetComponent<SpriteRenderer>().enabled = true;
                customCursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + hammerOffset;
                if (Input.GetMouseButtonDown(0))
                {
                    customCursor.GetComponent<Animator>().SetTrigger("Tap");
                    gameManager.Tap();
                    TriggerExplosion(Camera.main.ScreenToWorldPoint(Input.mousePosition) + explosionOffset);
                }
            }            
        }
        else
        {
            Cursor.visible = true;
            customCursor.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void TriggerExplosion(Vector3 pos)
    {
        GameObject explosion = GetExplosion();

        if(explosion!= null)
        {
            explosion.SetActive(true);
            explosion.transform.position = pos;
            explosion.GetComponent<ExplosionBehaviour>().Explode();
        }
    }

    private GameObject GetExplosion()
    {
        for (int i = 0; i < explosionPool.transform.childCount; i++)
        {
            if (!explosionPool.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return explosionPool.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }
}
