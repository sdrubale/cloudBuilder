using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class textManager : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public List<string> testoDaScrivere = new List<string>();
    public TMP_Text testoOggetto;
    private int iTesto = 0;
    private bool coroutineIniziata = false;

    void Awake()
    {
        if(PlayerPrefs.GetInt("filmatiVisti", 0) == 0)
        {
            PlayerPrefs.SetInt("filmatiVisti", 1);
            PlayerPrefs.SetInt("livelloDaSuperare", 1);
            for(int i = 0; i < staticValue.testoIniziale.Length; i++)
                testoDaScrivere.Add(staticValue.testoIniziale[i]);
        }
        else
            if(PlayerPrefs.GetInt("livelloDaSuperare") == 5)
                for(int i = 0; i < staticValue.testoBoss.Length; i++)
                    testoDaScrivere.Add(staticValue.testoBoss[i]);
            else
                for(int i = 0; i < staticValue.testoFinale.Length; i++)
                    testoDaScrivere.Add(staticValue.testoFinale[i]);
            
        if(!coroutineIniziata)
            StartCoroutine(scrivereTesto(testoDaScrivere[0]));
    }

    private IEnumerator scrivereTesto(string testo)
    {
        coroutineIniziata = true;
        for(int i = 0; i < testo.Length; i++)
        {
            testoOggetto.text += testo[i];
            yield return new WaitForSeconds(0.03f);
        }
        coroutineIniziata = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!coroutineIniziata)
        {
            testoOggetto.text = "";
            iTesto++;
            if(iTesto < testoDaScrivere.Count)
                StartCoroutine(scrivereTesto(testoDaScrivere[iTesto]));
            else
                if(PlayerPrefs.GetInt("filmatiVisti") == 1)
                    SceneManager.LoadScene("Liv1");
                else
                    if(PlayerPrefs.GetInt("livelloDaSuperare") == 5)
                        SceneManager.LoadScene("Liv5");
                    else
                    {
                        PlayerPrefs.SetInt("livelloDaSuperare",5);
                        SceneManager.LoadScene("mainMenu");
                    }
        }
        else
        {
            StopAllCoroutines();
            coroutineIniziata = false;
            testoOggetto.text = testoDaScrivere[iTesto];
        }
        
    }
}
