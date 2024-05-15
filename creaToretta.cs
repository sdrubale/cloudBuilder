using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creaToretta : MonoBehaviour
{
    // Start is called before the first frame update
    public costruisci zonaCostruzione;

    void OnMouseDown()
    {
        zonaCostruzione.costruireTorretta(gameObject);
    }

    void OnMouseOver()
    {
        zonaCostruzione.preVistaAreaToretta(gameObject);
    }

    void OnMouseExit()
    {
        zonaCostruzione.smettiPreVistaAreaToretta();
    }
}
