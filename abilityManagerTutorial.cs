using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class abilityManagerTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject oggettiAbilita;
    public int tempoDiRicarica;
    public bool abilitaCarica = false;
    public TMP_Text testiCountdown;
    public static abilityManagerTutorial main;
    public LayerMask layerNemici;

    void Awake()
    {
        main = this;
    }

    public void attivaAbilita()
    {
        if (abilitaCarica)
        {
            Time.timeScale = 1;
            tutorialManager.main.prosegui();
            RaycastHit2D[] nemici = Physics2D.CircleCastAll(transform.position, 50f, (Vector2)transform.position, 0f,  layerNemici);
            for(int j = 0; j < nemici.Length; j++)
                nemici[j].collider.gameObject.GetComponent<movimentoNemicoTutorial>().muoversi = false;
            StartCoroutine(ricaricaECountdown());
        }
    }

    private IEnumerator ricaricaECountdown()
    {
        testiCountdown.gameObject.SetActive(true);
        abilitaCarica = false;
        for(int j = tempoDiRicarica; j > 0; j--)
        {
            testiCountdown.text = j + "s";
            yield return new WaitForSeconds(1f);
        }
        testiCountdown.gameObject.SetActive(false);
        abilitaCarica = true;
    }
}
