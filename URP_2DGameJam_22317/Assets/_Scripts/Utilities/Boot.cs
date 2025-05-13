using UnityEngine.AddressableAssets;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class Boot : MonoBehaviour
{
    public AssetReference president;
    private void Awake()
    {
        Addressables.LoadSceneAsync(president);
    }
}
