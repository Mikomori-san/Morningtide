using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceDisableText : MonoBehaviour
{
    [SerializeField]
    private GameObject combatController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            combatController.GetComponent<EnemyLookAtPointLogic>().SetStartFalse();
            gameObject.SetActive(false);
        }
    }
}
