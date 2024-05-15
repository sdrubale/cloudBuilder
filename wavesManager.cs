using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavesManager : MonoBehaviour
{
    public List<List<int>> nNemici = new List<List<int>>();
    public List<List<GameObject>> prefabNemiciOrde = new List<List<GameObject>>();
    public List<List<float>> gestoreSpawnNemici = new List<List<float>>();
    public float[] tempoTraWave;
    public List<GameObject> tipiPrefab;
    public bool spawnare = true;
    private bool coroutineIniziata = false;
    public int guadagnoWave;

    void Start()
    {
        // Definizione dei valori delle liste
        switch (levelManager.main.livello)
        {
            case 1:
                nNemici.Add(new List<int>() {10});
                nNemici.Add(new List<int>() {15});
                nNemici.Add(new List<int>() {20});
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0]}); 
                gestoreSpawnNemici.Add(new List<float>() {5f, 1f, 2f});
                gestoreSpawnNemici.Add(new List<float>() {2f, 1f, 2f});
                gestoreSpawnNemici.Add(new List<float>() {2f, 0.8f, 1.5f});
                break;
            case 2:
                nNemici.Add(new List<int>() {5});
                nNemici.Add(new List<int>() {12});
                nNemici.Add(new List<int>() {10, 3});
                nNemici.Add(new List<int>() {13, 6});
                nNemici.Add(new List<int>() {15, 8});
                nNemici.Add(new List<int>() {20, 9, 5});
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[1]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[1]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[1]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[1], tipiPrefab[1]}); 
                gestoreSpawnNemici.Add(new List<float>() {5f, 1.5f, 2f});
                gestoreSpawnNemici.Add(new List<float>() {8f, 1f, 1.5f});
                gestoreSpawnNemici.Add(new List<float>() {8f, 0.8f, 1.5f, 7f, 3f, 5f});
                gestoreSpawnNemici.Add(new List<float>() {10f, 0.5f, 2.5f, 11f, 2f, 4.5f});
                gestoreSpawnNemici.Add(new List<float>() {12f, 1f, 3f, 15f, 1f, 10f});
                gestoreSpawnNemici.Add(new List<float>() {9f, 0.8f, 1.5f, 7f, 3f, 5f, 17f, 0.5f, 1f});
                break;
            case 3:
                nNemici.Add(new List<int>() {8, 4});
                nNemici.Add(new List<int>() {28});
                nNemici.Add(new List<int>() {25, 7, 4});
                nNemici.Add(new List<int>() {2});
                nNemici.Add(new List<int>() {30,8, 5});
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[0]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[1], tipiPrefab[1]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[2]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[1], tipiPrefab[2]}); 
                gestoreSpawnNemici.Add(new List<float>() {5f, 1f, 3f, 6.5f, 1.5f, 2f});
                gestoreSpawnNemici.Add(new List<float>() {7f, 1f, 1.5f});
                gestoreSpawnNemici.Add(new List<float>() {8f, 0.8f, 1.5f, 15f, 2f, 6f, 18f, 3f, 5f});
                gestoreSpawnNemici.Add(new List<float>() {2f, 10f, 11f});
                gestoreSpawnNemici.Add(new List<float>() {6f, 1.25f, 3f, 8f, 13f, 19f, 10f, 10f, 12f});
                break;
            case 4:
                nNemici.Add(new List<int>() {8, 2});
                nNemici.Add(new List<int>() {15, 5});
                nNemici.Add(new List<int>() {7});
                nNemici.Add(new List<int>() {7, 3});
                nNemici.Add(new List<int>() {5});
                nNemici.Add(new List<int>() {2});
                nNemici.Add(new List<int>() {1});
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[1]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0], tipiPrefab[1]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[1]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[1], tipiPrefab[2]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[3]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[4]}); 
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[5]}); 
                gestoreSpawnNemici.Add(new List<float>() {5f, 1f, 3f, 10f, 5f, 10f});
                gestoreSpawnNemici.Add(new List<float>() {7f, 0.7f, 1.3f, 15f, 2f, 3f});
                gestoreSpawnNemici.Add(new List<float>() {7f, 1f, 1.5f});
                gestoreSpawnNemici.Add(new List<float>() {17f, 2.5f, 5f, 4f, 5f, 7f});
                gestoreSpawnNemici.Add(new List<float>() {10f, 3f, 6f});
                gestoreSpawnNemici.Add(new List<float>() {7f, 7f, 8f});
                gestoreSpawnNemici.Add(new List<float>() {3f, 0f, 0f});
                break;
            case 5:
                nNemici.Add(new List<int>() {1});
                prefabNemiciOrde.Add(new List<GameObject>() {tipiPrefab[0]}); 
                gestoreSpawnNemici.Add(new List<float>() {1.5f, 0f, 0f});
                break;
        }
        // Avvia la gestione delle onde
        for(int i = 0; i < nNemici.Count; i++)
            for(int j = 0; j < nNemici[i].Count; j++)
                levelManager.main.contaNemici += nNemici[i][j];
        StartCoroutine(gestoreOrde());
        
    }

    public IEnumerator gestoreOrde()
    {
        for(int i = 0; i < nNemici.Count; i++)
        {
            while(!spawnare)
            {
                if(!coroutineIniziata)
                    StartCoroutine(aspettareFreeze());
                yield return null; // aspetta un frame
            }
            levelManager.main.aggiornaOndata();
            StartCoroutine(generaOrda(nNemici[i], prefabNemiciOrde[i], gestoreSpawnNemici[i]));
            levelManager.main.guadagnaSoldi(guadagnoWave * i);
            if(nNemici.Count - 1 != i)
                yield return new WaitForSeconds(tempoTraWave[i]);
        }
    }

    public IEnumerator generaOrda(List<int> numeroNemici, List<GameObject> prefabNemici, List<float> tempiSpawn)
    {
        for(int i = 0; i < numeroNemici.Count; i++)
        {
            while(!spawnare)
            {
                if(!coroutineIniziata)
                    StartCoroutine(aspettareFreeze());
                yield return null; // aspetta un frame
            }
            StartCoroutine(generaMostri(numeroNemici[i], prefabNemici[i], tempiSpawn[i * 3], tempiSpawn[i * 3 + 1], tempiSpawn[i * 3 + 2]));
        }
    }


    public IEnumerator generaMostri(int nNemici, GameObject prefabNemico, float tempoPreSpawn, float tempoTraSpawnMin, float tempoTraSpawnMax)
    {
        yield return new WaitForSeconds(tempoPreSpawn);
        for(int i = 0; i < nNemici; i++)
        {
            while(!spawnare)
            {
                if(!coroutineIniziata)
                    StartCoroutine(aspettareFreeze());
                yield return null; // aspetta un frame
            }
            Instantiate(prefabNemico, new Vector3(levelManager.main.punti[0].position.x, levelManager.main.punti[0].position.y, levelManager.main.punti[0].position.z + 10), Quaternion.identity);
            yield return new WaitForSeconds(UnityEngine.Random.Range(tempoTraSpawnMin, tempoTraSpawnMax));
        }
    }

    private IEnumerator aspettareFreeze()
    {
        coroutineIniziata = true;
        yield return new WaitForSeconds(5f);
        coroutineIniziata = false;
        spawnare = true;
    }
}