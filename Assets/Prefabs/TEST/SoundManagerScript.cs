using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    private void Start()
    {
        SoundPlayer(0, 15);
    }


    [Header("Audios")]
    [Tooltip("Here is the SoundList to put all your sound effects")]
    [SerializeField] AudioClip[] _audios;

    enum Audio { playerAttack, jumpSound, hit }

    public void SoundPlayer(int audioNum, float destroyTime)
    {
        GameObject _audiosGameObject = new GameObject();
        _audiosGameObject.AddComponent<AudioSource>();
        GameObject soundObject = Instantiate(_audiosGameObject, transform.position,
Quaternion.identity, null);
        soundObject.GetComponent<AudioSource>().clip = _audios[audioNum];
        soundObject.GetComponent<AudioSource>().Play();
        Destroy(soundObject, destroyTime);
    }

}
