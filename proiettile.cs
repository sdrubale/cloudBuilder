using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proiettile : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform bersaglio;
    public float velocita = 5f;
    public Rigidbody2D rb;
    public int danno;
    public bool applicaDebuf;

    public void impostaBersaglio(Transform bersaglioProiettile)
    {
        bersaglio = bersaglioProiettile;
    }

    void FixedUpdate()
    {
        if(bersaglio == null)
            Destroy(gameObject);
        Vector2 direzione = (bersaglio.position - transform.position);
        float distanzaDalBersaglio = direzione.magnitude;

        if (distanzaDalBersaglio < 0.1f)
        {
            bersaglio.gameObject.GetComponent<attributiNemico>().riceviDanno(danno);
            if(applicaDebuf && bersaglio.gameObject.GetComponent<movimentoNemici>() != null)
                bersaglio.gameObject.GetComponent<movimentoNemici>().applicaSlow = true;
            Destroy(gameObject);
        }
        else
            rb.velocity = direzione.normalized * velocita;
    }
}
