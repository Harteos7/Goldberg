using UnityEngine;

public class CameraBilles : MonoBehaviour
{
    [SerializeField] private GameObject bille1;
    [SerializeField] private GameObject bille2;
    [SerializeField] private GameObject bille3;
    [SerializeField] private GameObject Objet_start;
    public float distance = 10f; 
    private Vector3 offset = new Vector3(0, 2, -10);
    [SerializeField] private Vector3 start_position = new Vector3(0, 10, -15);
    private bool camera_libre = false;
    private GameObject cible; 
    [SerializeField] private float smoothSpeed = 20000f; // Vitesse de déplacement de la caméra
    public float rotationSmoothSpeed = 5f; // Vitesse de rotation pour stabiliser LookAt
    public float positionThreshold = 0.01f; // Seuil pour éviter les tremblements
    void Start()
    {
        // Choix de la bille qui centre la caméra
        if (bille1.activeSelf) {
            cible = bille1;
        } else if (bille2.activeSelf) {
            cible = bille2;
        } else if (bille3.activeSelf) {
            cible = bille3;
        }
    }

    void Update()
    {
        if (!Objet_start.activeSelf)
        {
            if (cible != null && !camera_libre) {
                SuivreBille(cible);
            } else { 
                Deplacement_camera();
            }
        } 
        else 
        {
            // Un restart : on se replace au début
            transform.position = start_position;
            if (cible != null)
                SmoothLookAt(cible.transform.position);
        }
    }

    void SuivreBille(GameObject cible)
    {
        if (cible != null)
        {
            Vector3 ciblePosition = cible.transform.position + offset;

            // Ne bouge que si la distance est significative
            if (Vector3.Distance(transform.position, ciblePosition) > positionThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, ciblePosition, Time.deltaTime * smoothSpeed);
            }

            // Orientation lissée de la caméra vers la cible
            SmoothLookAt(cible.transform.position);
        }
    }

    void SmoothLookAt(Vector3 targetPosition)
    {
        // Calcul de la direction cible
        Vector3 direction = targetPosition - transform.position;

        // Rotation actuelle
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Rotation lissée
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);
    }

    public void RegarderBille(GameObject nouvelleCible)
    {
        // On choisit la cible
        if (nouvelleCible != null && !Objet_start.activeSelf && cible != nouvelleCible)
        {
            cible = nouvelleCible;
            camera_libre = false;
        } 
        // Si la cible est déjà celle qu'on suit, on passe en caméra libre
        else if (cible == nouvelleCible && !camera_libre) {
            camera_libre = true;
        }
        // Si on rechoisit cette cible, on veut la resuivre
        else if (cible == nouvelleCible && camera_libre) {
            camera_libre = false;
        }
    }

    void Deplacement_camera()
    {
        // Logique pour le déplacement manuel
    }
}
