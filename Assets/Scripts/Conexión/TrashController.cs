using UnityEngine;

public class TrashController : MonoBehaviour
{
    public bool GreenTrashActivated { get; private set; }

    // Método para activar la basura verde
    public void ActivateGreenTrash()
    {
        GreenTrashActivated = true;
        Debug.Log("Activando basura verde...");
        // Aquí puedes añadir la lógica de tu juego para activar las acciones
    }

    // Método para desactivar la basura verde
    public void DeactivateGreenTrash()
    {
        GreenTrashActivated = false;
        Debug.Log("Desactivando basura verde...");
        // Aquí puedes añadir la lógica de tu juego para desactivar las acciones
    }

    // Añadir métodos similares para otras basuras (blanco, negro, etc.)
}
