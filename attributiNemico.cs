using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attributiNemico : MonoBehaviour
{
    public int vita;
    public float velocita;
    public bool virus;
    public int soldiAllaMorte;
    private bool isDestroyed = false;

    public void riceviDanno(int danno)
    {
        vita -= danno;
        if(vita <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            levelManager.main.contaNemici--;
            levelManager.main.guadagnaSoldi(soldiAllaMorte);
            Destroy(gameObject);
        }
    }
}

