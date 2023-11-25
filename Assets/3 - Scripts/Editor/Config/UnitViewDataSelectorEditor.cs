using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Configs
{
    [CustomEditor(typeof(UnitData))]
    public class UnitViewDataSelectorEditor : UnityEditor.Editor
    {
        private UnitsViewConfig unitsViewConfig;
        private SerializedProperty viewDataProperty;
        private SerializedProperty idProperty;
        private SerializedProperty indexProperty;

        private int selectedViewDataIndex = 0;
        
        private void OnEnable()
        {
            idProperty = serializedObject.FindProperty("id");
            indexProperty = serializedObject.FindProperty(UnitData.IndexViewKey);

            selectedViewDataIndex = indexProperty.intValue;
            
            var guid = AssetDatabase.FindAssets("Unit View Config");
            
            if (guid.Length > 0 && unitsViewConfig == null)
            {
                foreach (var s in guid)
                {
                    unitsViewConfig = FindAssetByGUID(s) as UnitsViewConfig;
                    
                    if (unitsViewConfig != null) break;
                }
            }

            if (unitsViewConfig == null) return;
            
            viewDataProperty = serializedObject.FindProperty(UnitData.ViewDataKey);
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Id", EditorStyles.boldLabel);

            EditorGUILayout.TextField(idProperty.stringValue);
            
            base.OnInspectorGUI();

            if (unitsViewConfig == null)
            {
                EditorGUILayout.HelpBox("Unit View Config not found.", MessageType.Error);
                return;
            }

            var viewDatas = unitsViewConfig.ViewDatas.ToArray();

            var viewDataNames = new string[viewDatas.Length];
            
            for (int i = 0; i < viewDatas.Length; i++)
            {
                viewDataNames[i] = viewDatas[i].Title;
            }

            selectedViewDataIndex = EditorGUILayout.Popup("Select View Data", selectedViewDataIndex, viewDataNames);

            indexProperty.intValue = selectedViewDataIndex;

            var selectedViewData = viewDatas[selectedViewDataIndex];

            viewDataProperty.stringValue = selectedViewData.Id;
            
            serializedObject.ApplyModifiedProperties();
        }

        private Object FindAssetByGUID(string assetGUID)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);

            if (!string.IsNullOrEmpty(assetPath))
            {
                var loadedAsset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);

                if (loadedAsset != null)
                {
                    Debug.Log("Asset found: " + loadedAsset.name);
                }
                else
                {
                    Debug.LogError("Failed to load asset.");
                }

                return loadedAsset;
            }
            
            Debug.LogError("Asset path not found for GUID: " + assetGUID);

            return null;
        }
    }
}