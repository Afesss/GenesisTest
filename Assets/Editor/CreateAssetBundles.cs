using UnityEditor;

namespace Editor
{
    public class CreateAssetBundles
    {
        private const string AssetsBundlePath = "Assets/AssetBundles";
        
        [MenuItem("Assets/Build AssetsBundle")]
        private static void BuildAllBundles()
        {
            BuildPipeline.BuildAssetBundles(AssetsBundlePath, BuildAssetBundleOptions.None,
                BuildTarget.Android);
        }
    }
}
