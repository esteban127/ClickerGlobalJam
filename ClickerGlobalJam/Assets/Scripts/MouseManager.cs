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
    
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.visible = false;
            customCursor.SetActive(true);
            customCursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + hammerOffset;
            
            if(Application.platform == RuntimePlatform.Android)
            {
                if (Input.touches.Length >= 0)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        gameManager.Tap();
                    }
                }
            }            
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    gameManager.Tap();
                }
            }
            
        }
        else
        {
            Cursor.visible = true;
            customCursor.SetActive(false);
        }
    }    
}
