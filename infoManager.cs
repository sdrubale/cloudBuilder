using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class infoManager : MonoBehaviour
{
    // Start is called before the first frame update
    public abilityManager codiceAbilityManager;
    public GameObject menuInfo;
    public GameObject[] versioniNemiciSbloccate;
    public TMP_Text descrizioneT1;
    public string[] descrizioniT1;
    public Image immagineT1;
    public Sprite[] immaginiT1;
    public TMP_Text descrizioneT2;
    public string[] descrizioniT2;
    public Image immagineT2;
    public Sprite[] immaginiT2;
    public TMP_Text descrizioneT3;
    public string[] descrizioniT3;
    public Image immagineT3;
    public Sprite[] immaginiT3;
    public int[] iT;
    public GameObject[] bottoni;

    void OnMouseDown()
    {
        if(codiceAbilityManager.iconaZonaTorretta == null)
        {
            Time.timeScale = 1 - Time.timeScale;
            menuInfo.SetActive(!menuInfo.activeSelf);
            if(PlayerPrefs.GetInt("livelloDaSuperare") == 1)
                PlayerPrefs.SetInt("livelloDaSuperare",2);
            for(int i = 0; i < PlayerPrefs.GetInt("livelloDaSuperare") && i < versioniNemiciSbloccate.Length; i++)
                versioniNemiciSbloccate[i].SetActive(true);
            if(PlayerPrefs.GetInt("livelloDaSuperare") == 1)
                for(int i = 0; i < 3; i++)
                {
                    bottoni[i].GetComponent<Image>().color = Color.red;
                    bottoni[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            for(int  i = 0; i < 3; i++)
            {
                bottoni[i+3].GetComponent<Image>().color = Color.red;
                bottoni[i+3].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void aumentareLivello(GameObject bottone)
    {
        int i = Array.IndexOf(bottoni, bottone);
        if(iT[i] + 1 < PlayerPrefs.GetInt("livelloDaSuperare") && iT[i] + 1 < 4)
        {
            iT[i]++;
            switch (i)
            {
                case 0:
                    immagineT1.sprite = immaginiT1[iT[i]];
                    descrizioneT1.text = descrizioniT1[iT[i]];
                    break;
                case 1:
                    immagineT2.sprite = immaginiT2[iT[i]];
                    descrizioneT2.text = descrizioniT2[iT[i]];
                    break;
                case 2:
                    immagineT3.sprite = immaginiT3[iT[i]];
                    descrizioneT3.text = descrizioniT3[iT[i]];
                    break;
            }
            if(iT[i] + 1 == PlayerPrefs.GetInt("livelloDaSuperare") || iT[i] + 1 == 4)
            {
                bottoni[i].GetComponent<Image>().color = Color.red;
                bottoni[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            if(iT[i] == 1)
            {
                bottoni[i+3].GetComponent<Image>().color = Color.white;
                bottoni[i+3].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void diminuireLivello(GameObject bottone)
    {
        int i = Array.IndexOf(bottoni, bottone) % 3;
        if(iT[i] > 0)
        {
            iT[i]--;
            switch (i)
            {
                case 0:
                    immagineT1.sprite = immaginiT1[iT[i]];
                    descrizioneT1.text = descrizioniT1[iT[i]];
                    break;
                case 1:
                    immagineT2.sprite = immaginiT2[iT[i]];
                    descrizioneT2.text = descrizioniT2[iT[i]];
                    break;
                case 2:
                    immagineT3.sprite = immaginiT3[iT[i]];
                    descrizioneT3.text = descrizioniT3[iT[i]];
                    break;
            }
            if(iT[i] == 0)
            {
                bottoni[i + 3].GetComponent<Image>().color = Color.red;
                bottoni[i + 3].transform.GetChild(0).gameObject.SetActive(false);
            }
            if(iT[i] == PlayerPrefs.GetInt("livelloDaSuperare") - 2 || iT[i]  == 2)
            {
                bottoni[i].GetComponent<Image>().color = Color.white;
                bottoni[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
