using UnityEngine;

public class GestionBilles : MonoBehaviour
{
    public GameObject bille1;
    public GameObject bille2;
    public GameObject bille3;
    public new Component camera;
    public GameObject objet_start;
    private bool action_4 = true;

    public void ActiverBilles(int Bille)
    {
        Rigidbody rg_bille1 = bille1.GetComponent<Rigidbody>();
        Rigidbody rg_bille2 = bille2.GetComponent<Rigidbody>();
        Rigidbody rg_bille3 = bille3.GetComponent<Rigidbody>();

        // 1pre Bille
        if (bille1.activeSelf && Bille == 1 && objet_start.activeSelf) {
            bille1.SetActive(false);
        } else if (Bille == 1) {
            bille1.SetActive(true);
        }

        // 2eme Bille
        if (bille2.activeSelf&& Bille == 2 && objet_start.activeSelf) {
            bille2.SetActive(false);
        } else if (Bille == 2) {
            bille2.SetActive(true);
        }

        // 3eme bille
        if (bille3.activeSelf && Bille == 3 && objet_start.activeSelf) {
            bille3.SetActive(false);
        } else if (Bille == 3) {
            bille3.SetActive(true);
        }

        // Start
        if ((bille1.activeSelf || bille2.activeSelf || bille3.activeSelf ) && Bille == 4 && action_4 && objet_start.activeSelf ) {
            objet_start.SetActive(false);
            action_4 =false;
        }

        // End
        if (Bille == 4 && !objet_start.activeSelf && action_4) {
            objet_start.SetActive(true);
            bille1.transform.position = new Vector3(0,0,0);
            bille2.transform.position = new Vector3((float)1.5, 0,0);
            bille3.transform.position = new Vector3((float)-1.5,0,0);

            Dont_Move(rg_bille1);
            Dont_Move(rg_bille2);
            Dont_Move(rg_bille3);

            action_4 =false;
        }
        action_4 = true;

        // Debug.Log(rg_bille2.linearVelocity);
    }


    public void Dont_Move(Rigidbody rg) { // Les rg ne bouge plus
        rg.linearVelocity = Vector3.zero;
        rg.angularVelocity = Vector3.zero;
    }
}
