using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class costruisciTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject torretta;
    public GameObject[] oggettiCerchio;
    private int iCerchio = 0;
    public GameObject[] iconeTorrette;
    public GameObject[] opzioniTorrette;
    public GameObject[] torrette;
    private bool areaVisualizzata = false;
    public bool freeze;
    public GameObject areaTorretta;
    public TMP_Text testoRimozione;
    public TMP_Text testoMiglioramento;
    public SpriteRenderer iconaMiglioramento;

    void OnMouseDown()
    {
        if(!freeze)
            oggettiCerchio[iCerchio].SetActive(!oggettiCerchio[iCerchio].activeSelf);
    }

    public void costruireTorretta(GameObject iconaTorretta)
    {
        int i = Array.IndexOf(iconeTorrette, iconaTorretta);
        if(i == 0)
        {
            levelManagerTutorial.main.guadagnaSoldi(-150);
            if(areaVisualizzata)
                smettiPreVistaAreaToretta();
            torretta = Instantiate(torrette[i], transform.position, Quaternion.identity);
            torretta codiceTorretta = torretta.GetComponent<torretta>();
            tutorialManager.main.prosegui();
            oggettiCerchio[iCerchio].SetActive(false);
            iCerchio++;
            cambiareTesto(testoMiglioramento, codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti]);
            cambiareTesto(testoRimozione, codiceTorretta.calcolareSoldiVendita());
        }
            
    }

    void OnMouseOver()
    {
        if(torretta == null) 
            return;
        visualizzaAreaTorretta();
    }

    void OnMouseExit()
    {
        if(torretta == null) 
            return;
        smettiVisualizzareAreaTorretta();
    }

   public void preVistaAreaToretta(GameObject iconaTorretta)
    {
        if(!areaVisualizzata)
        {
            int i = Array.IndexOf(iconeTorrette, iconaTorretta);
            if(i == 0)
            {
                torretta = Instantiate(torrette[i], transform.position, Quaternion.identity);
                Destroy(torretta.GetComponent<torretta>());
                Color newColor = torretta.GetComponent<SpriteRenderer>().color;
                newColor.a = 0.5f; // Assuming you want to change the alpha value
                torretta.GetComponent<SpriteRenderer>().color = newColor;
                visualizzaAreaTorretta();
            }
            
        }
    }
    private void visualizzaAreaTorretta()
    {
        if(!areaVisualizzata)
        {
            float nuovaScala = torretta.GetComponent<torretta>().range[torretta.GetComponent<torretta>().iMiglioramenti];
            Vector3 newPos = areaTorretta.transform.position; // Ottieni la posizione attuale
            newPos.z = 0.1f; // Modifica solo la coordinata Z
            areaTorretta.transform.position = newPos; // Imposta la nuova posizione
            areaTorretta.transform.localScale = new Vector3(nuovaScala, nuovaScala, 1f);
            areaTorretta.SetActive(true);
            areaVisualizzata = true;
        }
    }

    public void smettiVisualizzareAreaTorretta()
    {
        if(areaVisualizzata)
        {
            areaTorretta.SetActive(false);
            areaVisualizzata = false;
        }
        
    }

    public void smettiPreVistaAreaToretta()
    {
        if(areaVisualizzata)
        {
            DestroyImmediate(torretta);
            smettiVisualizzareAreaTorretta();
        }
    }

    public void migliorareTorretta()
    {
        torretta codiceTorretta = torretta.GetComponent<torretta>();
        if(codiceTorretta.iMiglioramenti < codiceTorretta.costoMigliorementi.Length-1)
        {
            if(levelManagerTutorial.main.soldi >= codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti])
            {
                levelManagerTutorial.main.guadagnaSoldi(-codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti]);
                torretta.GetComponent<SpriteRenderer>().sprite = codiceTorretta.immaginiTorretta[codiceTorretta.iMiglioramenti];
                codiceTorretta.iMiglioramenti++;
                tutorialManager.main.prosegui();
                cambiareTesto(testoMiglioramento, codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti]);
                if(codiceTorretta.iMiglioramenti == codiceTorretta.costoMigliorementi.Length-1)
                    iconaMiglioramento.color = new Color(0.5f, 0f, 0.5f);
                cambiareTesto(testoRimozione, codiceTorretta.calcolareSoldiVendita());
            }
        }
        else if(!codiceTorretta.attaccaInfetti)
            if(levelManager.main.soldi >= codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti])
            {
                torretta.GetComponent<SpriteRenderer>().sprite = codiceTorretta.immaginiTorretta[codiceTorretta.iMiglioramenti];
                levelManager.main.soldi -= codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti];
                codiceTorretta.attaccaInfetti = true;
                testoMiglioramento.gameObject.SetActive(false);
                iconaMiglioramento.color = Color.gray;
                cambiareTesto(testoRimozione, codiceTorretta.calcolareSoldiVendita());
            }
    }

    public void cambiareTesto(TMP_Text testoDaModificare, int valoreNuovo)
    {
        testoDaModificare.text = string.Format("{0}$", valoreNuovo);
    }
}
