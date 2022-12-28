using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    // Player Specification
    public float stamina = 1000f;
    private float maxStamina;

    // References
    public Text stText;
    public RectTransform stBar;
    private CharacterController characterController;

    // Use this for initialization
    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        maxStamina = stamina;
        stText.text = ((int)(stamina / maxStamina * 100f)).ToString() + "%";
        stBar.localScale = Vector3.one;
    }

    private void FixedUpdate()
    {
        if (characterController.velocity.sqrMagnitude > 50 && Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina--;
            UpdateST();
        }
        else if (stamina < maxStamina)
        {
            stamina += .5f;
            UpdateST();
        }
    }

    private void UpdateST()
    {
        stText.text = ((int)(stamina / maxStamina * 100f)).ToString() + "%";
        stBar.localScale = new Vector3(stamina / maxStamina, 1, 1);
    }
}
