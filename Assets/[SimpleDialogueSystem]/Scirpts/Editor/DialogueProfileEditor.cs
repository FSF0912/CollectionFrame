using UnityEngine;
using UnityEditor;

namespace FSF.DialogueSystem
{
    [CustomEditor(typeof(DialogueProfile))]
    public class DialogueProfileEditor : UnityEditor.Editor
    {
        private SerializedProperty actionsProperty;
        private Vector2 scrollPosition;
        private const float elementWidth = 240f; // 调整单个元素的宽度
        private const float elementPadding = 10f; // 元素间的间隔

        private void OnEnable()
        {
            actionsProperty = serializedObject.FindProperty("actions");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(Screen.height - 100), GUILayout.MaxHeight(Screen.height - 100));

            if (actionsProperty != null && actionsProperty.isArray)
            {
                EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);

                float availableWidth = Screen.width - 60; // 留出边距
                int elementsPerRow = Mathf.Max(1, Mathf.FloorToInt(availableWidth / (elementWidth + elementPadding)));

                for (int i = 0; i < actionsProperty.arraySize; i++)
                {
                    if (i % elementsPerRow == 0)
                    {
                        EditorGUILayout.BeginHorizontal();
                    }

                    EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(elementWidth), GUILayout.MinWidth(elementWidth)); // 设置垂直框的最大宽度和最小宽度
                    SerializedProperty actionProperty = actionsProperty.GetArrayElementAtIndex(i);
                    SerializedProperty nameProperty = actionProperty.FindPropertyRelative("name");
                    SerializedProperty dialogueProperty = actionProperty.FindPropertyRelative("dialogue");
                    SerializedProperty audioProperty = actionProperty.FindPropertyRelative("audio");
                    SerializedProperty spriteProperty = actionProperty.FindPropertyRelative("image");

                    // 显示序号
                    EditorGUILayout.LabelField($"Action {i + 1}", EditorStyles.boldLabel);
                    
                    // 设置自适应宽度
                    EditorGUILayout.PropertyField(nameProperty, new GUIContent("Name"), GUILayout.ExpandWidth(true));
                    EditorGUILayout.LabelField("Dialogue");
                    float textAreaHeight = Mathf.Max(60, EditorStyles.textArea.CalcHeight(new GUIContent(dialogueProperty.stringValue), elementWidth - 20));
                    dialogueProperty.stringValue = EditorGUILayout.TextArea(dialogueProperty.stringValue, EditorStyles.textArea, GUILayout.Height(textAreaHeight));
                    EditorGUILayout.PropertyField(audioProperty, new GUIContent("Audio"), GUILayout.ExpandWidth(true));
                    EditorGUILayout.PropertyField(spriteProperty, new GUIContent("Sprite"), GUILayout.ExpandWidth(true));

                    if (spriteProperty.objectReferenceValue != null)
                    {
                        var sprite = spriteProperty.objectReferenceValue as Sprite;
                        if (sprite != null)
                        {
                            float aspectRatio = sprite.rect.width / sprite.rect.height;
                            float width = elementWidth - 20;
                            float height = width / aspectRatio;

                            Rect spriteRect = GUILayoutUtility.GetRect(width, height, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                            Texture2D texture = sprite.texture;
                            Rect textureRect = new Rect(sprite.rect.x / texture.width, sprite.rect.y / texture.height, sprite.rect.width / texture.width, sprite.rect.height / texture.height);
                            GUI.DrawTextureWithTexCoords(spriteRect, texture, textureRect, true);
                        }
                    }
                    else
                    {
                        // 绘制一个空的大复选框
                        float width = elementWidth - 20; // 留出一些边距
                        float height = width; // 高度和宽度相同，形成一个正方形

                        Rect spriteRect = GUILayoutUtility.GetRect(width, height);
                        EditorGUI.DrawRect(spriteRect, new Color(0.9f, 0.9f, 0.9f)); // 绘制浅灰色背景
                        EditorGUI.LabelField(spriteRect, "No Sprite Assigned", new GUIStyle { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Italic });
                    }

                    // 在右下角添加 "Remove Action" 按钮
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Remove Action", GUILayout.Width(120)))
                    {
                        actionsProperty.DeleteArrayElementAtIndex(i);
                        break; // 立即退出循环，防止索引错误
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();

                    GUILayout.Space(elementPadding);

                    if ((i + 1) % elementsPerRow == 0 || i == actionsProperty.arraySize - 1)
                    {
                        EditorGUILayout.EndHorizontal();
                    }
                }

                // 添加 "Add Action" 按钮并放大
                if (GUILayout.Button("Add Action", GUILayout.Height(30)))
                {
                    actionsProperty.InsertArrayElementAtIndex(actionsProperty.arraySize);
                    SerializedProperty newAction = actionsProperty.GetArrayElementAtIndex(actionsProperty.arraySize - 1);
                    // 初始化新元素的字段（可选）
                    newAction.FindPropertyRelative("name").stringValue = "New Action";
                    newAction.FindPropertyRelative("dialogue").stringValue = "";
                    newAction.FindPropertyRelative("audio").objectReferenceValue = null;
                    newAction.FindPropertyRelative("image").objectReferenceValue = null;
                }
            }
            else
            {
                Debug.LogError("Failed to find 'actions' property or it is not an array.");
            }

            EditorGUILayout.EndScrollView();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
