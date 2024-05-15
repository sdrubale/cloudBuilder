using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attivaAbilitaTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public abilityManagerTutorial codiceAbilityManager;
    public GameObject spiegazione;

    void OnMouseDown()
    {
        codiceAbilityManager.attivaAbilita();
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
