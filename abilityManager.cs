using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class abilityManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] oggettiAbilita;
    public int[] tempiDiRicarica;
    private bool[] abilitaCarica;
    public TMP_Text[] testiCountdown;
    public wavesManager codiceWavesManager;
    public LayerMask layerNemici;
    public LayerMask layerIcone;
    public List<costruisci> codiceCostruisci = new List<costruisci>();
    public Sprite[] immaginiNormalizzate;
    private bool visuallizzareIcona = false;
    private bool bloccareAbilita;
    private bool coroutineIniziata;
    private bool ricaricare = true;
    private float tempoPassatoDaAttivazione = 0f;
    public GameObject zonaTorrettaPrefab;
    public GameObject iconaZonaTorretta;
    public Sprite immaginiOrginaleArchitect;
    public Sprite annulla;
    
    void Awake()
    {
        abilitaCarica = new bool[tempiDiRicarica.Length];
        for(int i = 0; i < abilitaCarica.Length;i++)
            abilitaCarica[i] = true;
    }

    public IEnumerator attivaAbilita(GameObject abilita)
    {
        int i = Array.IndexOf(oggettiAbilita, abilita);
        if (abilitaCarica[i] && !bloccareAbilita)
        {
            switch (i)
            {
                case 0:
                    codiceWavesManager.spawnare = false;
                    RaycastHit2D[] nemici = Physics2D.CircleCastAll(transform.position, 50f, (Vector2)transform.position, 0f,  layerNemici);
                    for(int j = 0; j < nemici.Length; j++)
                        if(nemici[j].collider.gameObject.GetComponent<movimentoNemici>() != null)
                        nemici[j].collider.gameObject.GetComponent<movimentoNemici>().muoversi = false;
                    break;
                case 1:
                    for(int j = 0; j < codiceCostruisci.Count; j++)
                        if(codiceCostruisci[j].torretta != null)
                            codiceCostruisci[j].torretta.GetComponent<torretta>().applicaDebuf = true;
                    break;
                case 2:
                    List<int> iTorretteTemporanee = new List<int>();
                    for(int j = 0; j < codiceCostruisci.Count; j++)
                        if(codiceCostruisci[j].torretta == null)
                        {
                            codiceCostruisci[j].oggettiCerchio[0].SetActive(false);
                            codiceCostruisci[j].torretta = Instantiate(codiceCostruisci[j].torrette[UnityEngine.Random.Range(0,3)], codiceCostruisci[j].gameObject.transform.position, Quaternion.identity);
                            codiceCostruisci[j].freeze = true;
                            Color coloreTorrettaTemporanea = codiceCostruisci[j].torretta.GetComponent<SpriteRenderer>().color;
                            coloreTorrettaTemporanea.a = 0.75f; // Assuming you want to change the alpha value
                            codiceCostruisci[j].torretta.GetComponent<SpriteRenderer>().color = coloreTorrettaTemporanea;
                            iTorretteTemporanee.Add(j);
                            
                        }
                    StartCoroutine(aspettaEDistruggi(iTorretteTemporanee));   
                    break;
                case 3:
                    RaycastHit2D[] nemiciVirus = Physics2D.CircleCastAll(transform.position, 50f, (Vector2)transform.position, 0f,  layerNemici);
                    for(int j = 0; j < nemiciVirus.Length; j++)
                        if(nemiciVirus[j].collider.gameObject.GetComponent<attributiNemico>().virus && nemiciVirus[j].collider.gameObject.GetComponent<movimentoNemici>() != null)
                        {
                            nemiciVirus[j].collider.gameObject.GetComponent<attributiNemico>().virus = false;
                            switch (nemiciVirus[j].collider.gameObject.name)
                            {
                                case "nemicoBaseInfetto(Clone)":
                                    nemiciVirus[j].collider.gameObject.GetComponent<SpriteRenderer>().sprite = immaginiNormalizzate[0];
                                    break;
                                case "nemicoTxtInfetto(Clone)":
                                    nemiciVirus[j].collider.gameObject.GetComponent<SpriteRenderer>().sprite = immaginiNormalizzate[1];
                                    break;
                                case "nemicoMovInfetto(Clone)":
                                    nemiciVirus[j].collider.gameObject.GetComponent<SpriteRenderer>().sprite = immaginiNormalizzate[2];
                                    break;
                            }
                        }                      
                    break;
                case 4:
                    if(iconaZonaTorretta == null)
                    {
                        iconaZonaTorretta = Instantiate(zonaTorrettaPrefab, new Vector3(0, 0, 0.1f), Quaternion.identity);
                        iconaZonaTorretta.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("icone");
                        Destroy(iconaZonaTorretta.GetComponent<costruisci>());
                        Color newColor = Color.white;
                        newColor.a = 0.5f; // Assuming you want to change the alpha value
                        iconaZonaTorretta.GetComponent<SpriteRenderer>().color = newColor;
                        tempoPassatoDaAttivazione = 0f;
                        visuallizzareIcona = true;
                        oggettiAbilita[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = annulla;
                        while(iconaZonaTorretta != null)
                            yield return null;
                        visuallizzareIcona = false;
                    }
                    else
                        ricaricare = false;
                    break;
            }
            if(ricaricare)
                StartCoroutine(ricaricaECountdown(i));
            else
                ricaricare = true;
        }
    }

    private IEnumerator aspettaEDistruggi(List<int> iTorretteTemporanee)
    {
        yield return new WaitForSeconds(7f);
        for(int j = 0; j < iTorretteTemporanee.Count; j++)
        {
            Destroy(codiceCostruisci[iTorretteTemporanee[j]].torretta);
            codiceCostruisci[iTorretteTemporanee[j]].oggettiCerchio[0].SetActive(false);
            codiceCostruisci[iTorretteTemporanee[j]].freeze = false;
            codiceCostruisci[iTorretteTemporanee[j]].smettiVisualizzareAreaTorretta();
        } 
    }

    void Update()
    {
        if(visuallizzareIcona)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 10; // imposta la posizione z a un valore positivo
            iconaZonaTorretta.transform.position = worldPos;
            tempoPassatoDaAttivazione += Time.deltaTime;
            if(tempoPassatoDaAttivazione > 0.2f)
                if(Input.GetMouseButtonDown(0))
                {
                    Collider2D hitCollider = Physics2D.OverlapCircle(worldPos, iconaZonaTorretta.GetComponent<CircleCollider2D>().radius*2f, LayerMask.NameToLayer("UI")); // Controlla se c'è un collider nella posizione del mouse
                    if(hitCollider.gameObject != iconaZonaTorretta)
                    {
                        if (hitCollider.gameObject == gameObject || hitCollider.gameObject == oggettiAbilita[4]) // Se non c'è un collider, piazza il cerchio
                        {
                            ricaricare = false;
                            Destroy(iconaZonaTorretta);
                            oggettiAbilita[4].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = immaginiOrginaleArchitect;
                        } 
                        else
                            if(!coroutineIniziata)
                                StartCoroutine(cambiareColoreIcona());
                    }
                    else
                    {
                        codiceCostruisci.Add(Instantiate(zonaTorrettaPrefab, iconaZonaTorretta.transform.position, Quaternion.identity).GetComponent<costruisci>());
                        codiceCostruisci[codiceCostruisci.Count - 1].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                        Destroy(iconaZonaTorretta);
                        oggettiAbilita[4].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = immaginiOrginaleArchitect;
                    }
                }
                
        }
    }

    private IEnumerator cambiareColoreIcona()
    {
        coroutineIniziata = true;
        SpriteRenderer spriteIcona = iconaZonaTorretta.GetComponent<SpriteRenderer>();
        spriteIcona.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        coroutineIniziata = false;
        spriteIcona.color = Color.white;
    }

    private IEnumerator ricaricaECountdown(int i)
    {
        testiCountdown[i].gameObject.SetActive(true);
        abilitaCarica[i] = false;
        for(int j = tempiDiRicarica[i]; j > 0; j--)
        {
            testiCountdown[i].text = j + "s";
            yield return new WaitForSeconds(1f);
        }
        testiCountdown[i].gameObject.SetActive(false);
        abilitaCarica[i] = true;
    }
}
