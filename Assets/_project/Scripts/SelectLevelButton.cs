using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TMP_Text _text;
    
    public void AddListener(UnityEngine.Events.UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
    
    public void SetText(string text)
    {
        _text.text = text;
    }
}