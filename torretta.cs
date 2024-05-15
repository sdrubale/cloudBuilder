using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class torretta : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> bersagli = new List<Transform>();
    public float[] range;
    public LayerMask layerNemici;
    public int costoCostruzione;
    public float[] pas;//proiettili al secondo
    public List<float> tempoDaScorsoProiettile = new List<float>();
    public int[] dannoTorretta;
    public int iMiglioramenti = 0;
    public int[] costoMigliorementi;
    public bool applicaDebuf = false;
    private bool coroutineIniziata = false;
    public bool attaccaInfetti = false;
    public bool sparare = true;
    public GameObject proiettilePrefab;
    public Transform puntoDiFuoco;
    public Sprite[] immaginiTorretta;
    public int nBersagli;
    private int moltiplicatoreDanniAggiuntivi = 0;
    public int[] valoreDanniAggiuntivi;
    public Transform exBersaglio;
    public bool aggiungereDanni;
    public Sprite immagineProittile;

    void Awake()
    {
        for(int i = 0; i < nBersagli; i++)
            tempoDaScorsoProiettile.Add(0f);
    }

    void Update()
    {
        for(int i = 0; i < nBersagli; i++)
        {
            if(bersagli.Count <= i)
                trovaBersaglio();
            else
                if(bersagli[i] == null)
                 {
                    bersagli.RemoveAt(i);
                    tempoDaScorsoProiettile.RemoveAt(i);
                    i--;
                }
                else   
                    if(Vector2.Distance(bersagli[i].position, transform.position) > range[iMiglioramenti])
                    {
                        bersagli.RemoveAt(i);
                        tempoDaScorsoProiettile.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        tempoDaScorsoProiettile[i] += Time.deltaTime;
                        if(tempoDaScorsoProiettile[i] >= 1f/pas[iMiglioramenti])
                        {
                            GameObject proiettile = Instantiate(proiettilePrefab, puntoDiFuoco.position, Quaternion.identity);
                            proiettile codiceProiettile = proiettile.GetComponent<proiettile>();
                            Transform bersaglio = bersagli[i];
                            codiceProiettile.impostaBersaglio(bersaglio);
                            codiceProiettile.danno = dannoTorretta[iMiglioramenti];
                            if(aggiungereDanni)
                                if(exBersaglio == bersagli[i])
                                {
                                    codiceProiettile.danno += moltiplicatoreDanniAggiuntivi * valoreDanniAggiuntivi[iMiglioramenti];
                                    moltiplicatoreDanniAggiuntivi++;
                                    proiettile.transform.localScale *= (1 + moltiplicatoreDanniAggiuntivi/5f);
                                }
                                else
                                    moltiplicatoreDanniAggiuntivi = 0;
                            proiettile.GetComponent<SpriteRenderer>().sprite = immagineProittile;
                            exBersaglio = bersagli[i];
                            codiceProiettile.applicaDebuf = applicaDebuf;
                            if(applicaDebuf && !coroutineIniziata)
                                StartCoroutine(fineApplicazioneDebuf());
                            tempoDaScorsoProiettile[i] = 0f;
                        }
                    }        
        }
    }

    private IEnumerator fineApplicazioneDebuf()
    {
        coroutineIniziata = true;
        yield return new WaitForSeconds(3f);
        applicaDebuf = false;
        coroutineIniziata = false;
    }

    private void trovaBersaglio()
    {
        RaycastHit2D[] bersagliColpiti = Physics2D.CircleCastAll(transform.position, range[iMiglioramenti], (Vector2)transform.position, 0f,  layerNemici);
        
        if(bersagliColpiti.Length > 0)
            for(int iBersaglio = 0; iBersaglio < bersagliColpiti.Length; iBersaglio++)
            {
                bool bersaglioDoppione = false;
                for(int i = 0; i < bersagli.Count; i++)
                    if(bersagli[i] == bersagliColpiti[iBersaglio].transform)
                    {
                        bersaglioDoppione = true;
                        break;
                    }
                if(!bersaglioDoppione)
                    if(attaccaInfetti || !bersagliColpiti[iBersaglio].collider.gameObject.GetComponent<attributiNemico>().virus)
                    {
                        tempoDaScorsoProiettile.Add(0f);
                        bersagli.Add(bersagliColpiti[iBersaglio].transform);
                        break;
                    }
            }     
    }

    public int calcolareSoldiVendita()
    {
        int sommaCosti = 0;
        for(int i = 0; i < iMiglioramenti; i++)
            sommaCosti += costoMigliorementi[i];
        if(attaccaInfetti)
            sommaCosti += costoMigliorementi[costoMigliorementi.Length - 1];
        return (costoCostruzione + sommaCosti)/2;
    }
}
