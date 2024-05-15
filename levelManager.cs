using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class levelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static levelManager main;
    public Transform[] punti;
    public TMP_Text testoVita;
    public TMP_Text testoOndata;
    public TMP_Text testoSoldi;
    public TMP_Text testoOndataPerdita;
    private int vita = 20;
    private int contaOndata = 0;
    public float soldi = 0;
    public int maxWave;
    public GameObject endScreen;
    public GameObject winScreen;
    public int contaNemici = 0;
    public int livello;

    void Awake()
    {
        guadagnaSoldi(0);
        main = this;
        Time.timeScale = 1;
    }

    public void perdiVita(int vitaPersa)
    {
        vita -= vitaPersa;
        if(vita <= 0)
        {
            testoVita.text = "0/20";
            partitaPersa();
        }   
        else
            testoVita.text = vita + "/20";
    }

    public void aggiornaOndata()
    {
        contaOndata++;
        testoOndata.text = "" + string.Format("{0}/{1}", contaOndata, maxWave);
    }

    public void partitaPersa()
    {
        Time.timeScale = 0;
        endScreen.SetActive(true);
        testoOndataPerdita.text = string.Format("Sei arrivato all'ondata {0}", contaOndata);
    }

    void Update()
    {
        if(contaNemici == 0 && vita > 0)
            partitaVinta();
    }

    public void rincomincia()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void partitaVinta()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void tornareAlMenuPrincipale(bool livelloSuperato)
    {
        if(livelloSuperato && PlayerPrefs.GetInt("livelloDaSuperare") == livello)
            PlayerPrefs.SetInt("livelloDaSuperare", (PlayerPrefs.GetInt("livelloDaSuperare") + 1));
        staticValue.aprireMenuLivelli = true;
        SceneManager.LoadScene("mainMenu");
    }

    public void guadagnaSoldi(int guadagno)
    {
        soldi += guadagno;
        testoSoldi.text = string.Format("{0}$", soldi);
    }
}
