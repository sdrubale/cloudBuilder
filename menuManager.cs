using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class menuManager : MonoBehaviour
{
    public GameObject titolo;
    public GameObject exitButton;
    public GameObject schermataPrincipale;
    public GameObject mappaLivelli;
    public GameObject loghi;
    public GameObject logoMaxwell;
    public GameObject logoFondazioneMondoDigitale;
    public GameObject[] bottoniStartLivello;
    
    void Awake()
    {
        if(staticValue.vedereLoghi)
            StartCoroutine(visualizzareLoghi());

        for(int i = 4; PlayerPrefs.GetInt("livelloDaSuperare", 1) - 1 < i;i--)
        {
            bottoniStartLivello[i].GetComponent<Image>().color = Color.red;
            bottoniStartLivello[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        if(staticValue.aprireMenuLivelli)
        {
            apriMappaLivelli();
            staticValue.aprireMenuLivelli = false;
        }
    }

private IEnumerator visualizzareLoghi()
{
    staticValue.vedereLoghi = false;
    loghi.SetActive(true);
    yield return new WaitForSeconds(0.5f);
    logoMaxwell.SetActive(true);
    float startTime = Time.time;
    float duration = 1f;
    while (Time.time < startTime + duration)
    {
        Color colore = logoMaxwell.GetComponent<Image>().color;
        colore.a = Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
        logoMaxwell.GetComponent<Image>().color = colore;
        yield return null;
    }
    yield return new WaitForSeconds(1f);
    startTime = Time.time;
    while (Time.time < startTime + duration)
    {
        Color colore = logoMaxwell.GetComponent<Image>().color;
        colore.a = 1 - Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
        logoMaxwell.GetComponent<Image>().color = colore;
        yield return null;
    }
    logoMaxwell.SetActive(false);
    startTime = Time.time;
    logoFondazioneMondoDigitale.SetActive(true);
    while (Time.time < startTime + duration)
    {
        Color colore = logoFondazioneMondoDigitale.GetComponent<Image>().color;
        colore.a = Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
        logoFondazioneMondoDigitale.GetComponent<Image>().color = colore;
        yield return null;
    }
    yield return new WaitForSeconds(1f);
    startTime = Time.time;
    while (Time.time < startTime + duration)
    {
        Color colore = logoMaxwell.GetComponent<Image>().color;
        colore.a = 1 - Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
        logoMaxwell.GetComponent<Image>().color = colore;
        yield return null;
    }
    logoFondazioneMondoDigitale.SetActive(false);
    yield return new WaitForSeconds(0.5f);
    startTime = Time.time;
    while (Time.time < startTime + duration)
    {
        Color colore = loghi.GetComponent<Image>().color;
        colore.a = 1 - Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
        loghi.GetComponent<Image>().color = colore;
        yield return null;
    }
    loghi.SetActive(false);
}

    public void chiudiGioco()
    {
        Application.Quit();
    }

    public void indetro(GameObject schermataDaChiudere)
    {
        titolo.SetActive(true);
        exitButton.SetActive(true);
        schermataDaChiudere.SetActive(false);
        schermataPrincipale.SetActive(true);
    }

    public void apriMappaLivelli()
    {
        titolo.SetActive(false);
        exitButton.SetActive(false);
        schermataPrincipale.SetActive(false);
        mappaLivelli.SetActive(true);
    }

    public void startLivello(GameObject bottoneSchiacciato)
    {
        int i = Array.IndexOf(bottoniStartLivello, bottoneSchiacciato);
        if(i == 0 && PlayerPrefs.GetInt("filmatiVisti", 0) == 0)
            SceneManager.LoadScene("filmatoIniziale");
        else
            if(i == 4)
                SceneManager.LoadScene("filmatoIniziale");
            else
                if(i < PlayerPrefs.GetInt("livelloDaSuperare"))
                    SceneManager.LoadScene("Liv" + (i+1));
                
    }

    public void avviareTutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

}
