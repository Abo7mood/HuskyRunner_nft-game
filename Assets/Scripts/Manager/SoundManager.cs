using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  

    public static SoundManager instance;
     [HideInInspector] public SoundObjecter soundObjecter;
    private void Awake()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
            instance = this;
        }
        soundObjecter = new SoundObjecter();
    }
    //the main sound 
    [SerializeField] AudioSource mainSource;
   
    //list of sound here 
   
    public AudioClip[] audios;
    public GameObject audiosGameObject;

    //nothing , just to make sure I'll not forgot the array
    enum Audio {jump, hit, die, kill, coins}

    public void SoundPlayer(GameObject soundObject , GameObject audiosGameObjects,int audioNum)
    {
            soundObject = Instantiate(audiosGameObjects, transform.position,
       Quaternion.identity, null); 
        soundObject.GetComponent<AudioSource>().clip = audios[audioNum];
        soundObject.GetComponent<AudioSource>().Play();
      Destroy(soundObject, 2f);

    }
   

    
    //public void JumpSound()
    //{
    //    if (jumpsoundPrefab == null)
    //    {
    //        jumpsoundPrefab = Instantiate(audiosGameObjects[1], transform.position,
    //   Quaternion.identity, null); Destroy(jumpsoundPrefab, 1f);
    //    }
    //}
    //public void HitSound()
    //{
    //    if (hitSoundPrefab == null)
    //    {
    //        hitSoundPrefab = Instantiate(audiosGameObjects[2], transform.position,
    //   Quaternion.identity, null); Destroy(hitSoundPrefab, 1f);
    //    }
    //}

    //public void DieSound()
    //{
    //    if (dieSoundPrefab == null)
    //    {
    //        dieSoundPrefab = Instantiate(audiosGameObjects[3], transform.position,
    //   Quaternion.identity, null); Destroy(dieSoundPrefab, 1f);
    //    }
    //}
    //public void KillSound()
    //{
    //    if (killSoundPrefab == null)
    //    {
    //        killSoundPrefab = Instantiate(audiosGameObjects[4], transform.position,
    //   Quaternion.identity, null); Destroy(killSoundPrefab, 1f);
    //    }

    //}
    //public void CoinsSound()
    //{
    //    if (coinsSoundPrefab == null)
    //    {
    //        coinsSoundPrefab = Instantiate(audiosGameObjects[5], transform.position,
    //   Quaternion.identity, null); Destroy(coinsSoundPrefab, 1f);
    //    }
    //}

}
public class SoundObjecter:SoundManager
{ 
  
  [HideInInspector] public GameObject walkSoundPrefab;
    [HideInInspector] public GameObject jumpsoundPrefab;
    [HideInInspector] public GameObject hitSoundPrefab;
    [HideInInspector] public GameObject dieSoundPrefab;
    [HideInInspector] public GameObject killSoundPrefab;
    [HideInInspector] public GameObject coinsSoundPrefab;
    
    public void Ref()
    {
        if (instance == null)
            instance = this;
       
    }

}


