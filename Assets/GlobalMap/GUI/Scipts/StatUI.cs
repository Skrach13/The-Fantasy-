using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private ProgressBar _progressBar;
        
    public TextMeshProUGUI Value { get => _value;}
    public ProgressBar ProgressBar { get => _progressBar;}
}
