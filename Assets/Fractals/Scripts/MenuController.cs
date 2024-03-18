using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject panelParameters;
    public GameObject panelInfo;
    public AudioController audioController;
    bool musicActive = true;
    public Button buttonMusic;
    public void ShowPanelParameters()
    {
        panelParameters.SetActive(true);
        audioController.OkSound();
    }
    public void HidePanelParameters()
    {
        panelParameters.SetActive(false);
    }
    public void ClosePanelParameters()
    {
        panelParameters.SetActive(false);
        audioController.CancelSound();
    }
    public void ShowPanelInfo()
    {
        panelInfo.SetActive(true);
        audioController.OkSound();
    }
    public void HidePanelInfo()
    {
        panelInfo.SetActive(false);
        audioController.CancelSound();
    }

    public void ToggleMusic()
    {
        if (musicActive)
        {
            buttonMusic.GetComponent<Image>().color = new Color32(220,200,220,255);
            audioController.StopMusic();
            musicActive = false;
        }
        else
        {
            buttonMusic.GetComponent<Image>().color = new Color32(255, 255, 255,255);
            audioController.PlayMusic();
            musicActive = true;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
