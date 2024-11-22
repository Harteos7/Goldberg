using UnityEngine;

public class CameraBilles : MonoBehaviour
{
    public GameObject bille1;
    public GameObject bille2;
    public GameObject bille3;
    public GameObject Objet_start;
    public float distance = 10f; 
    private Vector3 offset = new Vector3(0, 2, -10);
    public Vector3 start_position = new Vector3(0, 10,-15);
    private bool camera_libre = false;
    private GameObject cible; 

    void Start()
    {
        // Choix de la bille qui centre la camera
        if (bille1.activeSelf) {
            cible = bille1;
        } else if (bille2.activeSelf) {
            cible= bille2;
        } else if (bille3.activeSelf) {
            cible= bille3;
        }
    }

    void Update()
    {
        if (!Objet_start.activeSelf)
        {
            if (cible != null && !camera_libre) {
                SuivreBille(cible);
            } else { // si on à pas de cible ou camera_mode, on ce déplace normalement
                Deplacement_camera();
            }

        } else {
            // Un restart : on ce replace au début
            transform.position = start_position;
            transform.LookAt(cible.transform);
        }
    }

    void SuivreBille(GameObject cible)
    {
        if (cible != null)
        {
            Vector3 ciblePosition = cible.transform.position + offset;

            transform.position = Vector3.Lerp(transform.position, ciblePosition, Time.deltaTime * 5);

            transform.LookAt(cible.transform);
        }
    }

    public void RegarderBille(GameObject nouvelleCible)
    {
        // On choisi la cible
        if (nouvelleCible != null && !Objet_start.activeSelf && cible != nouvelleCible)
        {
            cible = nouvelleCible;
            camera_libre = false;
        } 
        // si la cible est deja celle qu'on suis on passe en camera libre
        else if (cible == nouvelleCible && !camera_libre) {
            camera_libre = true;
        }
        // si on rechoisi cette cible c'est qu'on veux la resuivre
        else if (cible == nouvelleCible && camera_libre) {
            camera_libre = false;
        }
    }

    void Deplacement_camera()
    {

    }
}
