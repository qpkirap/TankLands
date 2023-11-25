using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Game.Configs
{
    [CustomEditor(typeof(UnitsViewConfig))]
    public class UnitViewConfigEditor : UnityEditor.Editor
    {
        private ReorderableList list;
        
        private void OnEnable()
        {
            var listProperty = serializedObject.FindProperty(UnitsViewConfig.ViewDatasKey);
            
            list = new ReorderableList(serializedObject, listProperty, true, true, true, true)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Unit View Data"),
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
            var config = target as UnitsViewConfig;
            
            var newData = ScriptableEntity.Create<UnitViewData>();
            
            config.AddViewData(newData);
            
            Undo.RecordObject(this, "Add Unit View Data");

            AssetDatabase.AddObjectToAsset(newData, config);
            
            Undo.RegisterCreatedObjectUndo(newData, "Add Unit View Data");
            
            EditorUtility.SetDirty(this);
        }
        
        private void OnRemoveElement(ReorderableList reorderableList)
        {
            var config = target as UnitsViewConfig;

            Undo.RecordObject(config, "Remove Unit View Data");

            var indexToRemove = reorderableList.index;

            if (indexToRemove >= 0 && indexToRemove < config.ViewDatas.Count)
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
            
            var item = (UnitViewData)element.objectReferenceValue;
            
            rect.y += 2;
            
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element,
                new GUIContent(item.Title));
            
            rect.y += EditorGUIUtility.singleLineHeight + 2;
        }
    }
}