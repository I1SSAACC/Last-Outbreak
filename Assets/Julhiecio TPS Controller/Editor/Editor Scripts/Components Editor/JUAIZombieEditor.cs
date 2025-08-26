using UnityEngine;
using UnityEditor;
using JU.CharacterSystem.AI;
namespace JUTPSEditor
{
    [CustomEditor(typeof(JU_AI_Zombie))]
    public class JU_AI_ZombieCharacterEditor : Editor
    {
        SerializedProperty MoveEnabled;
        SerializedProperty NavigationSettings;
        SerializedProperty General;
        SerializedProperty AimSpeed;

        SerializedProperty FieldOfView;
        SerializedProperty DamageDetector;
        SerializedProperty Hear;

        SerializedProperty PatrolPath;
        SerializedProperty PatrolArea;

        SerializedProperty PatrolRandomlyIfNotHavePath;
        SerializedProperty MoveRandom;
        SerializedProperty FollowPatrolPath;
        SerializedProperty PatrolInsideArea;
        SerializedProperty MoveToLastTargetPosition;
        SerializedProperty SearchLastTarget;
        SerializedProperty Attack;

        bool showPatrolAI;
        bool showSensors;
        bool showPatrolAreas;
        bool showStates;

        void OnEnable()
        {
            MoveEnabled = serializedObject.FindProperty(nameof(MoveEnabled));
            NavigationSettings = serializedObject.FindProperty(nameof(NavigationSettings));
            General = serializedObject.FindProperty(nameof(General));
            AimSpeed = serializedObject.FindProperty(nameof(AimSpeed));

            FieldOfView = serializedObject.FindProperty(nameof(FieldOfView));            
            DamageDetector = serializedObject.FindProperty(nameof(DamageDetector));
            Hear = serializedObject.FindProperty(nameof(Hear));

            PatrolPath = serializedObject.FindProperty(nameof(PatrolPath));
            PatrolArea = serializedObject.FindProperty(nameof(PatrolArea));

            PatrolRandomlyIfNotHavePath = serializedObject.FindProperty(nameof(PatrolRandomlyIfNotHavePath));
            MoveRandom = serializedObject.FindProperty(nameof(MoveRandom));
            FollowPatrolPath = serializedObject.FindProperty(nameof(FollowPatrolPath));
            PatrolInsideArea = serializedObject.FindProperty(nameof(PatrolInsideArea));
            MoveToLastTargetPosition = serializedObject.FindProperty(nameof(MoveToLastTargetPosition));
            SearchLastTarget = serializedObject.FindProperty(nameof(SearchLastTarget));
            Attack = serializedObject.FindProperty(nameof(Attack));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //DrawDefaultInspector();
            //return;

            var toolbarStyle = JUTPSEditor.CustomEditorStyles.Toolbar();

            JUTPSEditor.CustomEditorUtilities.JUTPSTitle("Zombie AI Behaviour");

            // Patrol AI
            showPatrolAI = GUILayout.Toggle(showPatrolAI, "Zombie AI", toolbarStyle);
            if (showPatrolAI)
            {
                EditorGUILayout.PropertyField(MoveEnabled);
                EditorGUILayout.PropertyField(NavigationSettings, true);
                EditorGUILayout.PropertyField(General, true);
                EditorGUILayout.PropertyField(AimSpeed);
                EditorGUILayout.Space();
            }

            // Sensors
            showSensors = GUILayout.Toggle(showSensors, "Sensors", toolbarStyle);
            if (showSensors)
            {
                EditorGUILayout.PropertyField(FieldOfView, true);
                EditorGUILayout.PropertyField(DamageDetector, true);
                EditorGUILayout.PropertyField(Hear, true);
                EditorGUILayout.Space(20);
            }

            // Patrol Areas
            showPatrolAreas = GUILayout.Toggle(showPatrolAreas, "Patrol Areas", toolbarStyle);
            if (showPatrolAreas)
            {
                EditorGUILayout.PropertyField(PatrolPath);
                EditorGUILayout.PropertyField(PatrolArea);
                EditorGUILayout.Space(20);
            }

            // States
            showStates = GUILayout.Toggle(showStates, "States", toolbarStyle);
            if (showStates)
            {
                EditorGUILayout.PropertyField(PatrolRandomlyIfNotHavePath);
                EditorGUILayout.PropertyField(MoveRandom, true);
                EditorGUILayout.PropertyField(FollowPatrolPath, true);
                EditorGUILayout.PropertyField(PatrolInsideArea, true);
                EditorGUILayout.PropertyField(MoveToLastTargetPosition, true);
                EditorGUILayout.PropertyField(SearchLastTarget, true);
                EditorGUILayout.PropertyField(Attack, true);
                EditorGUILayout.Space();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}