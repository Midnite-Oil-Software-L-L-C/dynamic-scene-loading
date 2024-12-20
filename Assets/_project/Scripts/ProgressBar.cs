using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] float _progress = 0.5f;
    
    void Start()
    {
        SetProgress(_progress);
    }
    
    public void SetProgress(float progress)
    {
        _progress = Mathf.Clamp01(progress);
        _fillImage.fillAmount = _progress;
    }
}
