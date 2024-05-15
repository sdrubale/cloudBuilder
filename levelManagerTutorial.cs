using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class levelManagerTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public static levelManagerTutorial main;
    public Transform[] punti;
    public TMP_Text testoVita;
    public TMP_Text testoOndata;
    public TMP_Text testoSoldi;
    private int vita = 20;
    private int contaOndata = 0;
    public float soldi = 0;
    public int maxWave = 3;
    public GameObject winScreen;
    public int contaNemici;

    void Awake()
    {
        guadagnaSoldi(0);
        main = this;
    }

    public void perdiVita(int vitaPersa)
    {
        vita -= vitaPersa;
        if(vita <= 0)
            testoVita.text = "0/20";
        else
            testoVita.text = vita + "/20";
    }

    public void aggiornaOndata()
    {
        contaOndata++;
        testoOndata.text = "" + string.Format("{0}/{1}", contaOndata, maxWave);
    }

    void Update()
    {
        if(contaNemici == 0 && vita > 0)
            partitaVinta();
    }

    public void partitaVinta()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void tornareAlMenuPrincipale()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void guadagnaSoldi(int guadagno)
    {
        soldi += guadagno;
        testoSoldi.text = string.Format("{0}$", soldi);
    }
}
