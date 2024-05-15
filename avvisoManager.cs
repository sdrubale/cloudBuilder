using System.Collections;
using UnityEngine;

public class avvisoManager : MonoBehaviour
{
    public float[] tempoPreSpawn;
    public GameObject[] oggetti;

    void Start()
    {
        StartCoroutine(visuallizza());
    }

    private IEnumerator visuallizza()
    { 
        // Nascondi l'oggetto all'inizio
        for(int i = 0; i < tempoPreSpawn.Length; i++)
        {
            yield return new WaitForSeconds(tempoPreSpawn[i]);
            oggetti[i].SetActive(true);
            yield return new WaitForSeconds(5f);
            Destroy(oggetti[i]);
        }
    }    
}