using NPC; 
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NPCData))]  // Target NPCData as the base class
public class NPCPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw the label and field
        EditorGUI.BeginProperty(position, label, property);

        // Get the assigned ScriptableObject
        Object obj = property.objectReferenceValue;

        // Check if the assigned object is not of type AttackingNPCData
        if (obj != null && !(obj is AttackingNPCData))  // Ensure only AttackingNPCData is allowed
        {
            Debug.LogWarning($"Invalid assignment! Only AttackingNPCData ScriptableObjects are allowed for {property.name}.");
            property.objectReferenceValue = null; // Reset if it's invalid
        }

        // Draw the object field, but only allow valid AttackingNPCData types
        property.objectReferenceValue = EditorGUI.ObjectField(
            position,
            label,
            property.objectReferenceValue,
            typeof(AttackingNPCData), // Only allow AttackingNPCData to be assigned
            false
        );

        EditorGUI.EndProperty();
    }
}
