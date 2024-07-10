using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    [HideInInspector]
    public bool copperUnlocked = false;
    [HideInInspector]
    public bool ironUnlocked = false;
    [HideInInspector]
    public bool coilUnlocked = false;
    [HideInInspector]
    public bool copperPlateUnlocked = false;   
    [HideInInspector]
    public bool ironPlateUnlocked = false;
    [HideInInspector]
    public bool circuitUnlocked = false;

    [HideInInspector]
    public bool ironPlateAssemblerInPlace = false;
    [HideInInspector]
    public bool copperPlateAssemblerInPlace = false;
    [HideInInspector]
    public bool coilAssemblerInPlace = false;
    [HideInInspector]
    public bool circuitAssemblerInPlace = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this instance is not destroyed when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
