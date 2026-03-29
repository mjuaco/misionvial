using UnityEngine;
using UnityEngine.UI;

public class UICars : MonoBehaviour
{
    [SerializeField] private SCBCarsEntity SCBCars;
    
    [SerializeField] private Image background;
    [SerializeField] private Image CarSprite;
    [SerializeField] private Image isBlock;

    private void Start()
    {
       

        background.sprite = SCBCars.sprite;
        CarSprite.sprite = SCBCars.sprite;
        isBlock.sprite = SCBCars.isblock;
    }

    private void Update()
    {
        
    }
}
