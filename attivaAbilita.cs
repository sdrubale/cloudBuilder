using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attivaAbilita : MonoBehaviour
{
    // Start is called before the first frame update
    public abilityManager codiceAbilityManager;
    public GameObject spiegazione;

    void OnMouseDown()
    {
        StartCoroutine(codiceAbilityManager.attivaAbilita(gameObject));
    }

    void OnMouseOver()
    {
        spiegazione.SetActive(true);
    }

    void OnMouseExit()
    {
        spiegazione.SetActive(false);
    }
}
