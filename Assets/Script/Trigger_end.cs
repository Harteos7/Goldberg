using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerHandler : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Canvas text
    public float messageDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bille"))
        {
            // Désactive la bille
            other.gameObject.SetActive(false);

            Debug.Log(other.gameObject);

            // Affiche un message sur le Canvas
            ShowMessage(other.name + " est arrivée !");
        }
    }

    void ShowMessage(string message)
    {
        // Change le texte
        if (messageText != null)
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true); // Affiche le texte
            Invoke(nameof(HideMessage), messageDuration); // Cache le texte après un délai
        }
    }

    void HideMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false); // Cache le texte
        }
    }
}
