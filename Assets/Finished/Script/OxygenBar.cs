using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Slider oxygenSlider; 
    public float maxOxygen; 
    private void Start()
    {
        
        maxOxygen = LifeManager.instance.gasMaxTime; 
        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.minValue = 0;


        //UpdateOxygenBar(maxOxygen);
        oxygenSlider.value = maxOxygen;
    }

    private void Update()
    {
        //if (!JournalMenu.instance.oxygenPanel.activeSelf )
        //{

        //        float currentOxygen = LifeManager.instance.gasTimer; 
        //                UpdateOxygenBar(currentOxygen);
        //}
        oxygenSlider.value = Mathf.Clamp(LifeManager.instance.gasTimer, 0, maxOxygen);
    }

    //private void UpdateOxygenBar(float currentOxygen)
    //{
        
    //    oxygenSlider.value = Mathf.Clamp(currentOxygen, 0, maxOxygen);


    //}
}
