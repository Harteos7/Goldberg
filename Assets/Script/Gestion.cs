using UnityEngine;

public class GestionBilles : MonoBehaviour
{
    public GameObject bille1;
    public GameObject bille2;
    public GameObject bille3;
    public GameObject objet_start;
    public GameObject objet_end;
    private bool action_4 = true;
    private Rigidbody rg_bille1;
    private Rigidbody rg_bille2;
    private Rigidbody rg_bille3;

    void Start() {
        rg_bille1 = bille1.GetComponent<Rigidbody>();
        rg_bille2 = bille2.GetComponent<Rigidbody>();
        rg_bille3 = bille3.GetComponent<Rigidbody>();
        Bouge_Stp(bille1,rg_bille1);
        Bouge_Stp(bille2,rg_bille2);
        Bouge_Stp(bille2,rg_bille3);
    }
    
    void Update() {
        if (!objet_start.activeSelf) {
            Bouge_Stp(bille1,rg_bille1);
            Bouge_Stp(bille2,rg_bille2);
            Bouge_Stp(bille2,rg_bille3);
        }
    }

    public void ActiverBilles(int action)
    {
        // 1pre Bille
        if (bille1.activeSelf && action == 1 && objet_start.activeSelf) {
            bille1.SetActive(false);
        } else if (action == 1 && objet_start.activeSelf) {
            bille1.SetActive(true);
        }

        // 2eme Bille
        if (bille2.activeSelf&& action == 2 && objet_start.activeSelf) {
            bille2.SetActive(false);
        } else if (action == 2 && objet_start.activeSelf) {
            bille2.SetActive(true);
        }

        // 3eme bille
        if (bille3.activeSelf && action == 3 && objet_start.activeSelf) {
            bille3.SetActive(false);
        } else if (action == 3 && objet_start.activeSelf) {
            bille3.SetActive(true);
        }

        // Start
        if ((bille1.activeSelf || bille2.activeSelf || bille3.activeSelf ) && action == 4 && action_4 && objet_start.activeSelf ) {
            objet_start.SetActive(false);
            action_4 =false;
        }

        // End
        if (action == 4 && !objet_start.activeSelf && action_4) {
            End_parkour();
        }
        action_4 = true;

    }


    public void Dont_Move(Rigidbody rg) { // Les rg ne bouge plus
        rg.linearVelocity = Vector3.zero;
        rg.angularVelocity = Vector3.zero;
    }

    public void Bouge_Stp(GameObject gameObject, Rigidbody rigidbody) {
        if (rigidbody.linearVelocity.magnitude < 0.0001f && gameObject.activeSelf) {
            Dont_Move(rigidbody);

            // Générer une direction aléatoire
            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)
            ).normalized; // Normaliser pour avoir une direction unitaire

            Vector3 randomForce = randomDirection * 0.25f; 
            rigidbody.AddForce(randomForce, ForceMode.Impulse);
        }
    }

    public void End_parkour() {

        if (!bille1.activeSelf && !bille2.activeSelf && !bille3.activeSelf) {
            bille1.SetActive(true);
            bille2.SetActive(true);
            bille3.SetActive(true);
        }

        objet_start.SetActive(true);
        bille1.transform.position = new Vector3(0,0,0);
        bille2.transform.position = new Vector3((float)1.5, 0,0);
        bille3.transform.position = new Vector3((float)-1.5,0,0);

        Dont_Move(rg_bille1);
        Dont_Move(rg_bille2);
        Dont_Move(rg_bille3);

        action_4 =false;
    }

}
