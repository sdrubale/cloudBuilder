using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class costruisci : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject torretta;
    public GameObject[] oggettiCerchio;
    private int iCerchio = 0;
    public GameObject[] iconeTorrette;
    public GameObject[] opzioniTorrette;
    public GameObject[] torrette;
    private bool areaVisualizzata = false;
    private bool rilevaClick = false;
    private bool coroutineIniziata = false;
    public bool freeze = false;
    public GameObject areaTorretta;
    public TMP_Text testoRimozione;
    public TMP_Text testoMiglioramento;
    public SpriteRenderer iconaMiglioramento;

    void OnMouseDown()
    {
        if(!freeze)
        {
            oggettiCerchio[iCerchio].SetActive(!oggettiCerchio[iCerchio].activeSelf);
            if(oggettiCerchio[iCerchio].activeSelf)
            {
                if(!coroutineIniziata)
                    StartCoroutine(iniziaRilevareClick());
            }
            else
                rilevaClick = false;
        }
        
        
    }

    private IEnumerator iniziaRilevareClick()
    {
        coroutineIniziata = true;
        yield return new WaitForSeconds(0.2f);
        rilevaClick = true;
        coroutineIniziata = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && rilevaClick)
        {
            bool clickEsterno = true;
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if(iCerchio == 0)
                {
                    for(int i = 0; i < iconeTorrette.Length; i++)
                        if(hit.collider.gameObject == iconeTorrette[i])
                        {
                            clickEsterno = false;
                            break;
                        }
                }
                else
                    for(int i = 0; i < opzioniTorrette.Length; i++)
                        if(hit.collider.gameObject == opzioniTorrette[i])
                        {
                            clickEsterno = false;
                            break;
                        }
            }
            if(clickEsterno && oggettiCerchio[iCerchio].activeSelf)
            {
                oggettiCerchio[iCerchio].SetActive(false);
                rilevaClick = false;
            }
                
        }
    }



    public void costruireTorretta(GameObject iconaTorretta)
    {
        int i = Array.IndexOf(iconeTorrette, iconaTorretta);
        if(levelManager.main.soldi >= torrette[i].GetComponent<torretta>().costoCostruzione)
        {
            if(areaVisualizzata)
                smettiPreVistaAreaToretta();
            torretta = Instantiate(torrette[i], transform.position, Quaternion.identity);
            torretta codiceTorretta = torretta.GetComponent<torretta>();
            levelManager.main.guadagnaSoldi(-codiceTorretta.costoCostruzione);
            oggettiCerchio[iCerchio].SetActive(false);
            rilevaClick = false;
            iCerchio++;
            cambiareTesto(testoMiglioramento, codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti]);
            cambiareTesto(testoRimozione, codiceTorretta.calcolareSoldiVendita());
            if(codiceTorretta.iMiglioramenti + 1 >= PlayerPrefs.GetInt("livelloDaSuperare"))
            {
                testoMiglioramento.gameObject.SetActive(false);
                iconaMiglioramento.color = Color.gray;
                cambiareTesto(testoRimozione, codiceTorretta.calcolareSoldiVendita());
            }
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
            torretta = Instantiate(torrette[i], transform.position, Quaternion.identity);
            Destroy(torretta.GetComponent<torretta>());
            Color newColor = torretta.GetComponent<SpriteRenderer>().color;
            newColor.a = 0.75f; // Assuming you want to change the alpha value
            torretta.GetComponent<SpriteRenderer>().color = newColor;
            visualizzaAreaTorretta();
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

    public void vendereTorretta()
    {
        levelManager.main.guadagnaSoldi(torretta.GetComponent<torretta>().calcolareSoldiVendita());
        Destroy(torretta);
        testoMiglioramento.gameObject.SetActive(true);
        iconaMiglioramento.color = Color.green;
        oggettiCerchio[iCerchio].SetActive(false);
        rilevaClick = false;
        iCerchio--;
    }

    public void migliorareTorretta()
    {
        torretta codiceTorretta = torretta.GetComponent<torretta>();
        if(codiceTorretta.iMiglioramenti + 1 < PlayerPrefs.GetInt("livelloDaSuperare"))
        {
            if(codiceTorretta.iMiglioramenti < codiceTorretta.costoMigliorementi.Length-1)
                {
                    if(levelManager.main.soldi >= codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti])
                    {
                        levelManager.main.guadagnaSoldi(-codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti]);
                        torretta.GetComponent<SpriteRenderer>().sprite = codiceTorretta.immaginiTorretta[codiceTorretta.iMiglioramenti];
                        codiceTorretta.iMiglioramenti++;
                        cambiareTesto(testoMiglioramento, codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti]);
                        if(codiceTorretta.iMiglioramenti + 1 >= PlayerPrefs.GetInt("livelloDaSuperare"))
                        {
                            testoMiglioramento.gameObject.SetActive(false);
                            iconaMiglioramento.color = Color.gray;
                            cambiareTesto(testoRimozione, codiceTorretta.calcolareSoldiVendita());
                        }
                        else
                        {
                            if(codiceTorretta.iMiglioramenti == codiceTorretta.costoMigliorementi.Length-1)
                                iconaMiglioramento.color = new Color(0.5f, 0f, 0.5f);
                            cambiareTesto(testoRimozione, codiceTorretta.calcolareSoldiVendita());
                        }
                        
                    }
                }
                else if(!codiceTorretta.attaccaInfetti)
                    if(levelManager.main.soldi >= codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti])
                    {
                        torretta.GetComponent<SpriteRenderer>().sprite = codiceTorretta.immaginiTorretta[codiceTorretta.iMiglioramenti];
                        levelManager.main.guadagnaSoldi(-codiceTorretta.costoMigliorementi[codiceTorretta.iMiglioramenti]);
                        codiceTorretta.attaccaInfetti = true;
                        testoMiglioramento.gameObject.SetActive(false);
                        iconaMiglioramento.color = Color.gray;
                        cambiareTesto(testoRimozione, codiceTorretta.calcolareSoldiVendita());
                    }
        }          
    }

    public void cambiareTesto(TMP_Text testoDaModificare, int valoreNuovo)
    {
        testoDaModificare.text = string.Format("{0}$", valoreNuovo);
    }
}
