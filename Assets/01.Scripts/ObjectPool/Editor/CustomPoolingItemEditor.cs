using ObjectPooling;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PoolingItemSO))]
public class CustomPoolingItemEditor : Editor
{
    private SerializedProperty enumNameProp;
    private SerializedProperty poolingName;
    private SerializedProperty descriptionProp;
    private SerializedProperty poolCountProp;
    private SerializedProperty prefabProp;

    private GUIStyle textAreaStyle;
    private void OnEnable()
    {
        GUIUtility.keyboardControl = 0;
        
        enumNameProp = serializedObject.FindProperty("enumName");
        poolingName = serializedObject.FindProperty("poolingName");
        descriptionProp = serializedObject.FindProperty("description");
        poolCountProp = serializedObject.FindProperty("poolCount");
        prefabProp = serializedObject.FindProperty("prefab");
        StyleSetup();
        
    }

    private void StyleSetup()
    {
        if (textAreaStyle == null)
        {
            textAreaStyle = new GUIStyle(EditorStyles.textArea);
            textAreaStyle.wordWrap = true;
            textAreaStyle.active.textColor = Color.white;
        }
    }

    public override void OnInspectorGUI()
    {
        //StyleSetup();
        
        serializedObject.Update();
        
        EditorGUILayout.BeginHorizontal("HelpBox");
        {
            EditorGUILayout.BeginVertical();
            {
                
                EditorGUI.BeginChangeCheck(); // 변경을 체크
                string prevName = enumNameProp.stringValue;
                // 엔터가 쳐지거나 포커스가 나갈때 까지 변경을 저장하지 않는다
                EditorGUILayout.DelayedTextField(enumNameProp);

                if (EditorGUI.EndChangeCheck())
                {
                    // 현재 편집중인 에셋의 경로를 알아내기
                    string assetPath = AssetDatabase.GetAssetPath(target);
                    string newName = $"Pool_{enumNameProp.stringValue}";
                    serializedObject.ApplyModifiedProperties();

                    string msg = AssetDatabase.RenameAsset(assetPath, newName);
                    if (string.IsNullOrEmpty(msg))
                    {
                        // 성공적으로 파일명을 변경함
                        target.name = newName;
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndHorizontal();
                        return;
                    }

                    // ㅈ박음
                    enumNameProp.stringValue = prevName;

                }
                
                
                EditorGUILayout.PropertyField(poolingName);

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Description");
                    descriptionProp.stringValue = EditorGUILayout.TextArea(
                        descriptionProp.stringValue, textAreaStyle, GUILayout.Height(60));
                    
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel("PoolSetting");
                    EditorGUILayout.PropertyField(poolCountProp, GUIContent.none);
                    EditorGUILayout.PropertyField(prefabProp, GUIContent.none);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();

    }
    
    
}
