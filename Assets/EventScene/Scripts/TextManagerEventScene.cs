using TMPro;
using UnityEngine;

public class TextManagerEventScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public TextMeshProUGUI Text { get => _text; set => _text = value; }
}
