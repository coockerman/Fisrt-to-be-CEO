using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider UIExp;
    [SerializeField] private GameObject[] UIHpPlayer = new GameObject[5];
    [SerializeField] private TextMeshProUGUI UITimeClock;

    [SerializeField] private GameObject UiSettingMenu;
    [SerializeField] private TextMeshProUGUI TxtSettingAudioSource;
    [SerializeField] private TextMeshProUGUI TxtSettingAudioMusic;

    private void OnEnable()
    {
        EventPlayer.OnUIUpdateExp += UpdateUIExp;
        EventPlayer.OnUIUpdateHp += UpdateUIHpPlayer;
        EventManager.OnUIUpdateTimeClock += UpdateUITimeClock;
    }

    private void OnDisable()
    {
        EventPlayer.OnUIUpdateExp -= UpdateUIExp;
        EventPlayer.OnUIUpdateHp -= UpdateUIHpPlayer;
        EventManager.OnUIUpdateTimeClock -= UpdateUITimeClock;
    }

    void UpdateUIExp(float lv, float exp, float defaultExp)
    {
        UIExp.maxValue = defaultExp;
        UIExp.value = exp;
    }

    void UpdateUIHpPlayer(float hp, float defaultHp)
    {
        foreach (GameObject player in UIHpPlayer)
        {
            player.GetComponent<Image>().enabled = false;
        }

        int healthToDisplay = Mathf.Clamp(Mathf.FloorToInt(hp), 0, UIHpPlayer.Length); 
        for (int i = 0; i < healthToDisplay; i++)
        {
            UIHpPlayer[i].GetComponent<Image>().enabled = true;
        }
    }


    void UpdateUITimeClock(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);  // Tính phút
        int seconds = Mathf.FloorToInt(time % 60);  // Tính giây
        
        UITimeClock.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnSettingMenu()
    {
        SceneGame.Instance.PauseScene();
        UiSettingMenu.SetActive(true);
    }
    public void OffSettingMenu()
    {
        SceneGame.Instance.ContinueScene();
        UiSettingMenu.SetActive(false);
    }

    public void ExitGamePlay()
    {
        SceneGame.Instance.LoadScene(0);
    }

    public void ChangeAudioSource()
    {
        TxtSettingAudioSource.text = SoundManager.Instance.ChangeVolumeAudioSource();
    }
    public void ChangeAudioMusic()
    {
        TxtSettingAudioMusic.text = SoundManager.Instance.ChangeVolumeAudioMusic();
    }
}
