using UnityEngine;
using UnityEditor;

namespace JUTPSEditor
{
    public class MessageWindow : EditorWindow
    {       
        private static Texture2D s_banner;

        private static string s_title;
        private static string s_message;
        private static string s_buttonText;
        private static int s_fontSize;
        private static MessageType s_messageTypeIcon;

        public static void ShowMessage(
            string message, 
            string title = "Message", 
            string buttonText = "OK", 
            int height = 256, 
            int width = 512, 
            int fontSize = 12, 
            MessageType messageType = MessageType.None)
        {
            s_title = title;
            s_message = message;
            s_buttonText = buttonText;
            s_fontSize = fontSize;
            s_messageTypeIcon = messageType;

            MessageWindow window = GetWindow<MessageWindow>();
            window.titleContent = new GUIContent(title);
            window.titleContent.text = s_title;

            float x = (Screen.currentResolution.width - width) / 2;
            float y = (Screen.currentResolution.height - height) / 2;

            window.position = new Rect(x, y, width, height);
        }

        private void OnGUI()
        {
            if (s_banner == null) 
                s_banner = CustomEditorUtilities.GetImage("JUTPSLOGO");
            
            if (s_banner != null)
            {
                GUILayout.BeginHorizontal();

                CustomEditorUtilities.RenderImageWithResize(s_banner, new Vector2(64, 40));
                GUILayout.Label("| " + s_title, CustomEditorStyles.Title(16), GUILayout.Height(30));

                GUILayout.EndHorizontal();
            }

            GUIStyle style = new(EditorStyles.label);

            switch (s_messageTypeIcon)
            {
                case MessageType.None:
                    break;
                case MessageType.Info:
                    style = new(EditorStyles.helpBox);
                    break;
                case MessageType.Warning:
                    break;
                case MessageType.Error:
                    break;
            }

            style.fontSize = s_fontSize;
            style.wordWrap = true;

            if (s_messageTypeIcon == MessageType.None)
                GUILayout.Label(s_message, style);
            else
                EditorGUILayout.HelpBox(s_message, s_messageTypeIcon, true);

            GUILayout.Space(15);

            if (GUILayout.Button(s_buttonText))
                GetWindow<MessageWindow>().Close();
        }
    }
}