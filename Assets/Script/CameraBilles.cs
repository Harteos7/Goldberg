using UnityEngine;

public class CameraBilles : MonoBehaviour
{
    [SerializeField] private GameObject bille1;
    [SerializeField] private GameObject bille2;
    [SerializeField] private GameObject bille3;
    [SerializeField] private GameObject Objet_start; 
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -25);
    [SerializeField] private Vector3 start_position = new Vector3(0, 10, -15);
    private bool camera_libre = false;
    private GameObject cible; 
    [SerializeField] private float smoothSpeed = 20000f; // Vitesse de déplacement de la caméra
    public float rotationSmoothSpeed = 10f;
    public float positionThreshold = 0.01f; 
    private float vitesseDeplacement = 5f;
    private float vitesseScroll = 20f; 
    private float vitesseMin = 5f; 
    private float vitesseMax = 50f;
    public float sensibiliteSouris = 5f;
    public float limiteVerticale = 80f;
    private float rotationX = 0f;
    
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

            SmoothLookAt(cible.transform.position);
        }
    }

    void SmoothLookAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);
    }

    public void RegarderBille(GameObject nouvelleCible)
    {
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
        // Déplacement de base
        float moveSpeed = vitesseDeplacement * Time.deltaTime;

        // Entrées clavier (ZQSD / WASD)
        float horizontal = Input.GetAxis("Horizontal"); // A / D ou Q / D
        float vertical = Input.GetAxis("Vertical"); // W / S ou Z / S
        float elevation = 0f;

        // Monter/descendre (Espace/Ctrl)
        if (Input.GetKey(KeyCode.Space))
        {
            elevation += 1f;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            elevation -= 1f;
        }

        Vector3 moveDirection = new Vector3(horizontal, elevation, vertical);
        transform.Translate(moveDirection * moveSpeed, Space.Self);

        // Changer la vitesse de déplacement avec le scroll de la souris
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            vitesseDeplacement = Mathf.Clamp(vitesseDeplacement + scroll * vitesseScroll, vitesseMin, vitesseMax);
        }

        if (Input.GetMouseButton(1)) // Bouton droit de la souris maintenu
        {
            float mouseX = Input.GetAxis("Mouse X") * sensibiliteSouris; // Rotation gauche-droite
            float mouseY = Input.GetAxis("Mouse Y") * sensibiliteSouris; // Rotation haut-bas

            rotationX -= mouseY; // On inverse le Y pour un comportement naturel
            rotationX = Mathf.Clamp(rotationX, -limiteVerticale, limiteVerticale);

            transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y + mouseX, 0);
        }
    }


}
