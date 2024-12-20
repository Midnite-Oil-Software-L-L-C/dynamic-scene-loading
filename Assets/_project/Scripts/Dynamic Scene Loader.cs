using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

public class DynamicSceneLoader : MonoBehaviour
{
    [SerializeField] List<MapLevelDefinitionSO> _levels;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Transform _buttonPanel;
    [SerializeField] ProgressBar _progressBar;
    [SerializeField] SelectLevelButton _buttonPrefab;
    [SerializeField] Toggle _clearCacheToggle;

    void Start()
    {
        _buttonPanel.gameObject.SetActive(true);
        _loadingScreen.SetActive(false);
        foreach(var level in _levels)
        {
            var button = Instantiate(_buttonPrefab, _buttonPanel);
            button.SetText(level.SceneName);
            button.AddListener(() => LoadScene(level));
        }
        _clearCacheToggle.isOn = bool.Parse(PlayerPrefs.GetString("ClearCache", "false"));
    }

    void LoadScene(MapLevelDefinitionSO levelDefinition)
    {
        PlayerPrefs.SetString("ClearCache", _clearCacheToggle.isOn.ToString());
        PlayerPrefs.Save();
        _buttonPanel.gameObject.SetActive(false);
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneWithProgressAsync(levelDefinition.SceneReference));
    }

    IEnumerator LoadSceneWithProgressAsync(AssetReference sceneReference)
    {
        _progressBar.SetProgress(0f);
        if (_clearCacheToggle.isOn)
        {
            var clearCacheHandle = Addressables.ClearDependencyCacheAsync(sceneReference, false);
            yield return clearCacheHandle;

            if (clearCacheHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("Failed to clear dependency cache.");
                _loadingScreen.SetActive(false);
                yield break;
            }
        }
       
        AsyncOperationHandle<SceneInstance> handle = default;
        try
        {
            handle = sceneReference.LoadSceneAsync();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
        if (handle.Equals(default))
        {
            yield break;
        }
        
        while (!handle.IsDone)
        {
            _progressBar.SetProgress(handle.PercentComplete);
            yield return null;
        }
        _progressBar.SetProgress(1f);
        _loadingScreen.SetActive(false);
    }
}