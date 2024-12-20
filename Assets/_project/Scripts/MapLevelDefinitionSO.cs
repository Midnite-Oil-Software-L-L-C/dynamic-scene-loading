using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "MapLevelDefinition", menuName = "Map Level Definition")]
public class MapLevelDefinitionSO : ScriptableObject
{
   [SerializeField] AssetReference _sceneReference;
   [SerializeField] string _sceneName;
   
   public AssetReference SceneReference => _sceneReference;
   public string SceneName => _sceneName;
}
