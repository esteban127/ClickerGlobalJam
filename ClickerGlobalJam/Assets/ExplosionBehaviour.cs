using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public void Explode()
    {
        GetComponent<Animator>().SetTrigger("Explode");
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        yield return null;
    }
}
