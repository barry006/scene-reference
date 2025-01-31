using UnityEditor;
namespace UnityEngine.SceneManagement
{
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferencePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sceneAssetProp = property.FindPropertyRelative("sceneAsset");
            EditorGUI.BeginProperty(position, label, sceneAssetProp);
            EditorGUI.PropertyField(position, sceneAssetProp, label);
            EditorGUI.EndProperty();
        }
    }
}