using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject healthbar;
    
    private BaseStats baseStats;
    private Slider healthBarSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        baseStats = GetComponent<BaseStats>();
        healthBarSlider = healthbar.GetComponent<Slider>();
        
        healthBarSlider.maxValue = baseStats.MaxHealth;
        healthBarSlider.value = baseStats.Health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = baseStats.Health;
    }
}
