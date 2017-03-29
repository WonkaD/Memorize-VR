using UnityEngine;

public class ConfirmationDialog : MonoBehaviour
{
    private bool confirmation;
    [SerializeField] private Canvas canvas;

    public void SetVisible(bool visible)
    {
        canvas.enabled = visible;
    }

    public bool GetVisible()
    {
        return canvas.enabled;
    }

    public bool GetConfirmation()
    {
        return confirmation;
    }

    public void SetConfirmation(bool confirmation)
    {
        this.confirmation = confirmation;
    }
}
