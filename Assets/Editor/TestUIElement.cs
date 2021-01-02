using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class TestUIElement : EditorWindow
{
    [MenuItem("Window/UIElements/TestUIElement")]
    public static void ShowExample()
    {
        TestUIElement wnd = GetWindow<TestUIElement>();
        wnd.titleContent = new GUIContent("TestUIElement");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        
        Button button = rootVisualElement.Q<Button>("Test");

        // 2
        button.clickable.clicked += () => 
        {
            Debug.Log("youre clicked !!");
            // if (presetManager != null)
            // {
            //     Preset newPreset = new Preset();
            //     presetManager.presets.Add(newPreset);

            //     EditorUtility.SetDirty(presetManager);
                        
            //     PopulatePresetList();
            //     BindControls();
            // }
        };



        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/TestUIElement.uxml");
        VisualElement labelFromUXML = visualTree.CloneTree();
        root.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/TestUIElement.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        root.Add(labelWithStyle);
    }
}