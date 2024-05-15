using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    public GameObject[] spiegazione;
    public costruisciTutorial[] codiciCostruisci;
    public int iSpiegazione = 0;
    public static tutorialManager main;
    public GameObject prefabNemico;
    public GameObject nemico;

    void Awake()
    {
        main = this;
        Time.timeScale = 0;
        for(int i = 0; i < codiciCostruisci.Length; i++)
            codiciCostruisci[i].freeze = true;
    }

    public void prosegui()
    {
        spiegazione[iSpiegazione].SetActive(false);
        iSpiegazione++;
        if(iSpiegazione < spiegazione.Length)
        {
            spiegazione[iSpiegazione].SetActive(true);
            if(iSpiegazione == 3)
            {
                Time.timeScale = 1;
                levelManagerTutorial.main.aggiornaOndata();
                nemico = Instantiate(prefabNemico, levelManagerTutorial.main.punti[0].transform.position, Quaternion.identity);
                return;
            }

            if(iSpiegazione == 5)
            {
                codiciCostruisci[0].freeze = false;
                return;
            }
                
            if(iSpiegazione == 7)
            {
                codiciCostruisci[0].freeze = true;
                Time.timeScale = 1;
                levelManagerTutorial.main.aggiornaOndata();
                nemico = Instantiate(prefabNemico, levelManagerTutorial.main.punti[0].transform.position, Quaternion.identity);
                return;
            }

            if(iSpiegazione == 8)
            {
                codiciCostruisci[0].freeze = false;
                Time.timeScale = 0;
                return;
            }

            if(iSpiegazione == 10)
            {
                codiciCostruisci[0].oggettiCerchio[1].SetActive(false);
                codiciCostruisci[0].freeze = true;
                Time.timeScale = 1;
                levelManagerTutorial.main.aggiornaOndata();
                nemico = Instantiate(prefabNemico, levelManagerTutorial.main.punti[0].transform.position, Quaternion.identity);
                return;
            }

            if(iSpiegazione == 11)
            {
                Time.timeScale = 0;
                abilityManagerTutorial.main.abilitaCarica = true;
                return;
            }
        }
    }

    void Update()
    {
        if(nemico != null)
        {
            if(iSpiegazione == 3 && nemico.GetComponent<movimentoNemicoTutorial>().iProssimoPunto == 7)
                prosegui();
            if(iSpiegazione == 10 && nemico.GetComponent<movimentoNemicoTutorial>().iProssimoPunto == 4)
                prosegui();
        }
            
        if(codiciCostruisci[0].oggettiCerchio[0].activeSelf && iSpiegazione == 5)
            prosegui();
        if(!codiciCostruisci[0].oggettiCerchio[0].activeSelf && iSpiegazione == 6)
        {
            spiegazione[iSpiegazione].SetActive(false);
            iSpiegazione -= 2;
            prosegui();
        }

        if(codiciCostruisci[0].oggettiCerchio[1].activeSelf && iSpiegazione == 8)
            prosegui();
        if(!codiciCostruisci[0].oggettiCerchio[1].activeSelf && iSpiegazione == 9)
        {
            spiegazione[iSpiegazione].SetActive(false);
            iSpiegazione -= 2;
            prosegui();
        }
    }
}
