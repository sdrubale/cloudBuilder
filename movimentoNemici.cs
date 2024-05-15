using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoNemici : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocita;
    public int iProssimoPunto = 1;
    public Transform bersaglio;
    public bool muoversi = true;
    private bool coroutineFreezeIniziata = false;
    public bool applicaSlow = false;
    private bool coroutineSlowIniziata = false;

    void Awake()
    {
        velocita = gameObject.GetComponent<attributiNemico>().velocita;
        bersaglio = levelManager.main.punti[iProssimoPunto];
    }
    void Update()
    {
        if(applicaSlow && !coroutineSlowIniziata)
            StartCoroutine(applicaEffettoSlow());
        if(Vector2.Distance(bersaglio.position, transform.position) <= 0.1f)
        {
            iProssimoPunto++;
            if(iProssimoPunto == levelManager.main.punti.Length) 
            {
                levelManager.main.contaNemici--;
                levelManager.main.perdiVita(1);
                Destroy(gameObject);
            }
            else
                bersaglio = levelManager.main.punti[iProssimoPunto];
        }
    }

    public IEnumerator applicaEffettoSlow()
    {
        coroutineSlowIniziata = true;
        velocita -= 0.5f;
        applicaSlow = false;
        yield return new WaitForSeconds(2.5f);
        coroutineSlowIniziata = false;
        velocita += 0.5f;
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
