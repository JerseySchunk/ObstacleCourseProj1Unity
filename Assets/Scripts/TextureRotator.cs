// 7/7/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class TextureRotator : MonoBehaviour
{
    public Renderer playerRenderer;
    public float rotation = 0f;

    void Start()
    {
        if (playerRenderer != null)
        {
            Material material = playerRenderer.material;

            if (material.HasProperty("_Rotation"))
            {
                Debug.Log("Setting texture rotation to: " + rotation);
                material.SetFloat("_Rotation", rotation);
            }
            else
            {
                Debug.LogError("Material does not have a '_Rotation' property. Ensure the correct shader is assigned.");
            }
        }
        else
        {
            Debug.LogError("Player Renderer is not assigned in the TextureRotator script.");
        }
    }
}