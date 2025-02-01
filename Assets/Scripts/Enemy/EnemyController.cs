using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Collections;
using Spine.Unity;
using Spine.Unity.AnimationTools;
using Spine;
public class EnemyController : MonoBehaviour
{
    SkeletonAnimation skeleton;
    SkeletonDataModifierAsset skeletonData;
    int[] array1 = new int[] { 1, 3, 4, 7, 8 };
    string[] Skins = new string[] { "V1", "V2", "V3", "V4" };
    const string GREENENEMY = "GreenEnemy", BIRDENEMY = "BirdEnemy"; // enemies names
    const string GENERATED = "CanGenerate", GREEN="Green"; // enemies tags
    [SerializeField] GameObject[] GreenEnemyList;
    [SerializeField] GameObject[] BirdEnemyList;
    
    
    GameObject Bird;
    int rand;
    private void Awake()
    {
        int rand1 = Random.Range(0, GreenEnemyList.Length);
        int rand2 = Random.Range(0, BirdEnemyList.Length);

        if (gameObject.CompareTag(GREEN))
        {
            GameObject Green = Instantiate(GreenEnemyList[rand1], transform.position, Quaternion.identity,transform);
            skeleton = Green.GetComponent<SkeletonAnimation>();
            if (Green == null)
                Debug.LogError("x");
        }
        else if (gameObject.name == BIRDENEMY)
        {
            Bird = Instantiate(BirdEnemyList[rand2], transform.position, Quaternion.identity, transform);
            skeleton = Bird.GetComponent<SkeletonAnimation>();

        }else
            Debug.LogError("x");

    }
    private void Start() =>
       ChangeSkin();
 
    private void ChangeSkin()
    {
        if (skeleton != null&&gameObject.CompareTag(GENERATED))
        {
            GenerateSkin();
            skeleton.skeleton.SetSkin(Skins[rand]);
            Debug.Log(rand);
        }
    }
   private int GenerateSkin()
    {
         rand = Random.Range(0, Skins.Length);
        return rand;
    }
                     

}
