#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace NoCheatUtilite.Editor
{
    [CustomPropertyDrawer(typeof(NoCheatInt))]
    public class NoCheatIntDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var encProp = property.FindPropertyRelative("_encryptedValue");
            var keyProp = property.FindPropertyRelative("_encryptionKey");
            var sigProp = property.FindPropertyRelative("_signature");

            int encrypted = encProp.intValue;
            int key = keyProp.intValue;
            int current = encrypted ^ key;

            int updated = EditorGUI.IntField(position, label, current);

            if (updated != current)
            {
                int newKey = (int)typeof(NoCheatInt)
                                  .GetMethod("GenerateKey",
                                             System.Reflection.BindingFlags.NonPublic |
                                             System.Reflection.BindingFlags.Static)!
                                  .Invoke(null, null);

                int newEnc = updated ^ newKey;
                byte[] sig = (byte[])typeof(NoCheatInt)
                                  .GetMethod("ComputeSignature",
                                             System.Reflection.BindingFlags.NonPublic |
                                             System.Reflection.BindingFlags.Static)!
                                  .Invoke(null, new object[] { newEnc, newKey });

                encProp.intValue = newEnc;
                keyProp.intValue = newKey;
                sigProp.arraySize = sig.Length;

                for (int i = 0; i < sig.Length; i++)
                    sigProp.GetArrayElementAtIndex(i).intValue = sig[i];
            }

            EditorGUI.EndProperty();
        }
    }
}

#endif