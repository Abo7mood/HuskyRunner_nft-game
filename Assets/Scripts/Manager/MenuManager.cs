using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [Header("MenuObjects")]
    public GameObject diePanel; // die panel in canvas, and so on
    public GameObject scorePanel;
    public GameObject healthPanel;
    public GameObject coinsPanel;
    [Space(5)]
    public GameObject micSectionPanel;

    [Space(15)]
    /// 0 = mic
    /// 1 = keyboard
    [SerializeField] Image[] inputImages;
    public void LoadScene(int index) => SceneManager.LoadScene(index);   //load scene 
    public void inputChanger(int index)
    {
        for (int i = 0; i < inputImages.Length; i++) inputImages[i].color = new Color(inputImages[i].color.r, inputImages[i].color.g, inputImages[i].color.b, 1f); // reset the color for images

        inputImages[index].color = new Color(inputImages[index].color.r, inputImages[index].color.g, inputImages[index].color.b, .25f); // set the image to invincible , like choose effect you know
        inputImages[index+1].color = new Color(inputImages[index + 1].color.r, inputImages[index + 1].color.g, inputImages[index + 1].color.b, .25f); // set the image to invincible , like choose effect you know

        if (index == 1)
            micSectionPanel.SetActive(true);
        else
            micSectionPanel.SetActive(false);
    }
}
