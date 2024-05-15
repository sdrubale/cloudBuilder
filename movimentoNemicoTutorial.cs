using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoNemicoTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float velocita = 2.5f;
    public int iProssimoPunto = 1;
    public Transform bersaglio;
    public bool muoversi = true;
    private bool coroutineFreezeIniziata = false;

    void Awake()
    {
        bersaglio = levelManagerTutorial.main.punti[iProssimoPunto];
    }
 
    void Update()
    {
        if(GetComponent<attributiNemico>().vita <= 0)
        {
            levelManagerTutorial.main.contaNemici--;
            Destroy(gameObject);
        }
        if(Vector2.Distance(bersaglio.position, transform.position) <= 0.1f)
        {
            iProssimoPunto++;
            if(iProssimoPunto == levelManagerTutorial.main.punti.Length) 
            {
                levelManagerTutorial.main.contaNemici--;
                levelManagerTutorial.main.perdiVita(1);
                Time.timeScale = 0;
                tutorialManager.main.prosegui();
                Destroy(gameObject);
            }
            else
                bersaglio = levelManagerTutorial.main.punti[iProssimoPunto];
        }
    }

    void FixedUpdate()
    {
        Vector2 direzione = (bersaglio.position - transform.position);
        if(muoversi)
        {
            float distanzaDalBersaglio = direzione.magnitude;

            if (distanzaDalBersaglio < 0.1f)
            {
                transform.position = bersaglio.position;
                rb.velocity = Vector2.zero;
            }
            else
                rb.velocity = direzione.normalized * velocita;
        }
        else
            if(!coroutineFreezeIniziata)
            {
                StartCoroutine(aspettareFreeze());
                rb.velocity = direzione.normalized * 0f;
            }
                
            
    }

    private IEnumerator aspettareFreeze()
    {
        coroutineFreezeIniziata = true;
        yield return new WaitForSeconds(5f);
        coroutineFreezeIniziata = false;
        muoversi = true;
    }
}
