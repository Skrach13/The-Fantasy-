using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fillRect;
    
    public void UpdateProgressBar(float maxValue,float value)
    {        
        _fillRect.fillAmount = (1/maxValue) * value;    
    }
}
