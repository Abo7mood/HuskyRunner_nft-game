using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    #region Constructer
    public static SaveManager instance;
    MenuManager _menu;
    #endregion
    #region Booleans

    #endregion
    [HideInInspector] public bool isMic;

    private void Awake()
    {
        instance = this;
        _menu = FindObjectOfType<MenuManager>().GetComponent<MenuManager>();
        Getter();

    }



    public void Setter()
    {
        PlayerPrefs.SetInt("MicBoolean", isMic ? 1 : 0);
        PlayerPrefs.SetFloat("MicBoolean", isMic ? 1 : 0);

    }
    public void Getter()
    {
        isMic = PlayerPrefs.GetInt("MicBoolean") == 1 ? true : false;

        if (isScene(1)) return;

        if (isMic)
            _menu.micSectionPanel.SetActive(true);
        else
            _menu.micSectionPanel.SetActive(false);

    }
    public void MicSetter(bool condition) => isMic = condition;
    bool isScene(int index) => SceneManager.GetActiveScene().buildIndex == index;

}
