using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TimeManager : MonoBehaviour
{
    [Header("TimeEdit")]
    [SerializeField] float timeMulitplier;
    [SerializeField] float timereal;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Update()
    {
        if(Time.timeScale<70)
        Time.timeScale += timeMulitplier * Time.deltaTime;
        Time.timeScale= Mathf.Clamp(Time.timeScale, 0, 1.4f);
        timereal = Time.timeScale;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        Time.timeScale = 1;
    }
}
