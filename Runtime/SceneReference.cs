// This file is based on the original script under the MIT License by Gleb Skibitsky https://github.com/skibitsky/scene-reference
using System;
using System.IO;
using UnityEditor;
namespace UnityEngine.SceneManagement
{
    [Serializable]
    public class SceneReference : ISerializationCallbackReceiver, IComparable<SceneReference>
    {
        /// <summary>
        /// Returns the name of the scene.
        /// </summary>
        public string SceneFileName { get => sceneName; private set => sceneName = value; }
        /// <summary>
        /// Returns the full path to the scene asset (.unity).
        /// </summary>
        public string ScenePath { get => assetPath; private set => assetPath = value; }
        /// <summary>
        /// Returns the index of the scene in the Unity build settings.
        /// </summary>
        public int SceneBuildIndex { get => buildIndex; private set => buildIndex = value; }

#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;
#endif
        [SerializeField] private string sceneName;
        [SerializeField] private string assetPath;
        [SerializeField] private int buildIndex;

        public static implicit operator string(SceneReference sceneReference) { return sceneReference.assetPath; }

        public void OnBeforeSerialize() => Validate();
        public void OnAfterDeserialize() { }

        public void Validate()
        {
#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(ScenePath) && sceneAsset != null && ScenePath == AssetDatabase.GetAssetPath(sceneAsset)) return;
            if (sceneAsset == null) { ScenePath = ""; SceneFileName = ""; SceneBuildIndex = -1; return; }

            ScenePath = AssetDatabase.GetAssetPath(sceneAsset);
            SceneFileName = Path.GetFileNameWithoutExtension(assetPath);
            SceneBuildIndex = SceneUtility.GetBuildIndexByScenePath(assetPath);

            if (buildIndex == -1) Debug.LogError($"The scene <b>{SceneFileName}</b> is missing in the Unity build settings");
#endif
        }

        protected bool Equals(SceneReference other) => ScenePath == other.ScenePath;
        public override bool Equals(object obj) => ReferenceEquals(null, obj) ? false : ReferenceEquals(this, obj) ? true : obj.GetType() == GetType() && Equals((SceneReference)obj);
        public override int GetHashCode() => ScenePath != null ? ScenePath.GetHashCode() : 0;    // ReSharper disable twice NonReadonlyMemberInGetHashCode
        public int CompareTo(SceneReference other) => ReferenceEquals(this, other) ? 0 : ReferenceEquals(null, other) ? 1 : string.Compare(assetPath, other.assetPath, StringComparison.Ordinal);
    }
}