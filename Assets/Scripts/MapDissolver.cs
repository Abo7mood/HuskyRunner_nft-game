using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDissolver : MonoBehaviour
{
    [SerializeField] GameObject[] BackGroundsGroups;

    private void Awake()
    {
        for (int i = 0; i < BackGroundsGroups.Length; i++)
                    BackGroundsGroups[i].SetActive(false);
        
        int rand = Random.Range(0, BackGroundsGroups.Length);
        BackGroundsGroups[rand].SetActive(true);
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        Destroy(gameObject,LevelManager.instance.destroyTimeMap);
        else
            return;
         
    }
}
