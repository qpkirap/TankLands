using Game.Configs.Data;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Game.Configs
{
    [CustomEditor(typeof(UnitsConfig))]
    public class UnitsConfigEditor : UnityEditor.Editor
    {
        private ReorderableList list;
        
        private void OnEnable()
        {
            var listProperty = serializedObject.FindProperty(UnitsConfig.UnitDatasKey);
            
            list = new ReorderableList(serializedObject, listProperty, true, true, true, true)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Unit Data"),
                drawElementCallback = DrawElement,
                onAddCallback = OnAddElement,
                onRemoveCallback = OnRemoveElement,
                onReorderCallback = list => EditorUtility.SetDirty(target),
                onSelectCallback = null
            };
        }

        private void OnDisable()
        {
            AssetDatabase.SaveAssets();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            
            serializedObject.ApplyModifiedProperties();

            if (target != null && list.serializedProperty is { serializedObject: not null })
            {
                EditorGUILayout.Space();
            
                list?.DoLayoutList();
            }
        }

        private void OnAddElement(ReorderableList reorderableList)
        {
            var config = target as UnitsConfig;
            
            var newData = ScriptableEntity.Create<UnitData>();
            
            config.Add(newData);
            
            Undo.RecordObject(this, "Add Unit Data");

            AssetDatabase.AddObjectToAsset(newData, config);
            
            Undo.RegisterCreatedObjectUndo(newData, "Add Unit Data");
            
            EditorUtility.SetDirty(this);
        }
        
        private void OnRemoveElement(ReorderableList reorderableList)
        {
            var config = target as UnitsConfig;

            Undo.RecordObject(config, "Remove Unit Data");

            var indexToRemove = reorderableList.index;

            if (indexToRemove >= 0 && indexToRemove < config.UnitDatas.Count)
            {
                var removedData = config.GetViewData(indexToRemove);
                config.RemoveViewData(indexToRemove);

                Undo.DestroyObjectImmediate(removedData);

                AssetDatabase.SaveAssets(); 
            }
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);

            var item = (UnitData)element.objectReferenceValue;
            
            rect.y += 2;
            
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element,
                new GUIContent(item.Title));
            
            rect.y += EditorGUIUtility.singleLineHeight + 2;
        }
    }
}