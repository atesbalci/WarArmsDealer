using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.Editor
{
    public class AtesToolsWindow : EditorWindow
    {
        private Font _font;
        private Color _color;

        [MenuItem("WarArmsDealer/Ates Tools")]
        public static void OpenEditorWindow()
        {
            GetWindow<AtesToolsWindow>().Show();
        }

        public AtesToolsWindow()
        {
            titleContent = new GUIContent("Ates Tools");
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            _color = EditorGUILayout.ColorField(_color, GUILayout.Width(50));
            _font = (Font)EditorGUILayout.ObjectField("", _font, typeof(Font), false);
            if (GUILayout.Button("Set Fonts"))
            {
                foreach (var text in Selection.activeGameObject.GetComponentsInChildren<Text>(true))
                {
                    Undo.RecordObject(text, "Font Change");
                    text.color = _color;
                    text.font = _font;
                }
                Undo.IncrementCurrentGroup();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
