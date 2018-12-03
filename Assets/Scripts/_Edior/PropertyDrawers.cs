using System;
using UnityEditor;
using UnityEngine;

public class PropertyDrawers : MonoBehaviour {

    public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

    // custion serializable class
    [Serializable]
    public class Ingredient {
        public string name;
        public int amount = 1;
        public IngredientUnit unit;
    }
    // IngredientDrawer
    [CustomPropertyDrawer(typeof(Ingredient))]
    public class IngredientDrawer : PropertyDrawer {
        // Draw the propery inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var amountRect = new Rect(position.x, position.y, 30, position.height);
            var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
            var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // Draw fields - pass GUIContent.none to each so they can draw with labels
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

            // Set indent back to what is was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }

    public class MyRangeAttribute : PropertyAttribute {
        public readonly float min;
        public readonly float max;

        public MyRangeAttribute(float min, float max) {
            this.min = min;
            this.max = max;
        }
    }

    [CustomPropertyDrawer(typeof(MyRangeAttribute))]
    public class RangeDrawer : PropertyDrawer {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // First get the attribute since it contains the range for the slider
            MyRangeAttribute range = (MyRangeAttribute) attribute;

            // Now draw the property as a Slider or an IntSlider based on whether it's a float or Integer
            if (property.propertyType == SerializedPropertyType.Float)
                EditorGUI.Slider(position, property, range.min, range.max, label);
            else if (property.propertyType == SerializedPropertyType.Integer)
                EditorGUI.Slider(position, property, (int) range.min, (int)range.max, label);
            else
            EditorGUI.LabelField(position, label.text, "Use MyRange with float or int.");

        }
    }

    // show this float in the Inpeactor as a slider between 0 and 10
    [MyRangeAttribute(-10f, 10f)]
    public float myFloat = 0f;

    public Ingredient potionResult;
    public Ingredient[] potionIngredients;
}