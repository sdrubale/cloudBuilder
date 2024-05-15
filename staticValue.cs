using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticValue : MonoBehaviour
{
    public static string[] testoIniziale;
    public static string[] testoBoss;
    public static string[] testoFinale;
    public static bool aprireMenuLivelli = false;
    public static bool vedereLoghi = true;

    void Awake()
    {
        testoIniziale = new string[] 
        { 
            "Ciao sono mr. Cloud!",
            "Il tuo compito é quello di aiutarci nella costruzione del cloud, una potente infrastruttura simile a un computer molto potente.",
            "Esso é accesibile tramite connessione internet da ovunque e che, per esempio, puó essere usato per caricarci dei file.",
            "I file caricati saranno salvati lí e potranno essere recuperati con un telefono, tablet o computer e una connessione a internet",
            "Per quanto riguarda il tuo compito, esso é quello di costruire dei cloud in grado di archiviare i file caricati.",
            "Ad aiutarti ci saranno anche degli specialisti del settore che incontrerai man mano che avanzi nel tuo percorso.",
            "Buon divertimento!" 
        };

        testoBoss = new string[] 
        { 
            "Ciao, é bello rivederti!",
            "Innanzitutto complimenti per essere attivato fin qui.",
            "Ora peró é arrivato il momento dell'ultima sfida, una sfida abbastanza difficile.",
            "Il nemico che stai per affrontare é in grado di caricare dei file autonomamente, bloccare per breve tempo il funzionamento delle torri ed é immune alle abilitá dei tuoi specialisti",
            "Non ti preoccupare, so che puoi farcela! Buona fortuna!",
        };

        testoFinale = new string[] 
        { 
            "Complimenti!",
            "Deve essere stato uno scontro difficile ma ci sei riuscito!",
            "Grazie a te ora tutti potranno beneficiare degli utilizzi del cloud!",
            "Questa esperienza é stata possibile grazie a:\nLorenzo Luciano Sasso, Samuel Colucci, Christian Giuliana, Francesco Cupito",
            "Per realizzarlo sono stati usati\nGame engine: unity\nDisegni: procreate, paint",
            "Infine ringrazio fondazione mondo digitale per aver dato l'opportunitá al Maxwell di partecipare."
        };
    }
}
