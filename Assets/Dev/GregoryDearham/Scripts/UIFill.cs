//using UnityEngine.UI;
//using UnityEngine;

//public class UIFill : MonoBehaviour
//{
//    public int maxValue;
//    public Image fill;

//    private int currentValue;

//    void Start()
//    {
//        currentValue = 0;
//        fill.fillAmount = 1;


//    }

//    void Update()
//    {
        

//        if (Input.GetKeyDown(KeyCode.Escape)) {

//    }

//    public void Add(int i)

//    {

//        currentValue += i;

//        if (currentValue >= maxValue)
//        {
//            currentValue = maxValue;
//        }

//        fill.fillAmount = (float)currentValue/maxValue;

//    }


//    public void Deduct(int i)
//    {

//        currentValue -= i;

//        if (currentValue >= 0)
//        {
//            currentValue = 0;
//        }

//        fill.fillAmount = (float)currentValue / maxValue;


//    }

//}
