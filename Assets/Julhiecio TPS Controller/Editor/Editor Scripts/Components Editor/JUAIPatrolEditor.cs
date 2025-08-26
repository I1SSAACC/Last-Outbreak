using UnityEngine;
using UnityEditor;
using JU.CharacterSystem.AI;
namespace JUTPSEditor
{
    [CustomEditor(typeof(JU_AI_PatrolCharacter))]
    public class JU_AI_PatrolCharacterEditor : Editor
    {
        SerializedProperty MoveEnabled;
        SerializedProperty NavigationSettings;
        SerializedProperty General;
        SerializedProperty AimSpeed;
        SerializedProperty FieldOfView;
        SerializedProperty HearSensor;
        SerializedProperty PatrolPath;
        SerializedProperty PatrolArea;
        SerializedProperty PatrolRandomlyIfNotHavePath;
        SerializedProperty MoveRandom;
        SerializedProperty FollowPatrolPath;
        SerializedProperty MoveRandomPatrolArea;
        SerializedProperty MoveToPossibleTargetPosition;
        SerializedProperty SearchLosedTarget;
        SerializedProperty DamageDetector;
        SerializedProperty Attack;
        SerializedProperty EscapeAreas;

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
            HearSensor = serializedObject.FindProperty(nameof(HearSensor));

            PatrolPath = serializedObject.FindProperty(nameof(PatrolPath));
            PatrolArea = serializedObject.FindProperty(nameof(PatrolArea));

            PatrolRandomlyIfNotHavePath = serializedObject.FindProperty(nameof(PatrolRandomlyIfNotHavePath));
            MoveRandom = serializedObject.FindProperty(nameof(MoveRandom));
            FollowPatrolPath = serializedObject.FindProperty(nameof(FollowPatrolPath));
            MoveRandomPatrolArea = serializedObject.FindProperty(nameof(MoveRandomPatrolArea));
            MoveToPossibleTargetPosition = serializedObject.FindProperty(nameof(MoveToPossibleTargetPosition));
            SearchLosedTarget = serializedObject.FindProperty(nameof(SearchLosedTarget));
            DamageDetector = serializedObject.FindProperty(nameof(DamageDetector));
            Attack = serializedObject.FindProperty(nameof(Attack));
            EscapeAreas = serializedObject.FindProperty(nameof(EscapeAreas));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var toolbarStyle = JUTPSEditor.CustomEditorStyles.Toolbar();

            JUTPSEditor.CustomEditorUtilities.JUTPSTitle("Patrol AI Behaviour");

            // Patrol AI
            showPatrolAI = GUILayout.Toggle(showPatrolAI, "Patrol AI", toolbarStyle);
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
                EditorGUILayout.PropertyField(HearSensor, true);
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
                EditorGUILayout.PropertyField(MoveRandomPatrolArea, true);
                EditorGUILayout.PropertyField(MoveToPossibleTargetPosition, true);
                EditorGUILayout.PropertyField(SearchLosedTarget, true);
                EditorGUILayout.PropertyField(DamageDetector, true);
                EditorGUILayout.PropertyField(Attack, true);
                EditorGUILayout.PropertyField(EscapeAreas, true);
                EditorGUILayout.Space();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}