using Unity.VisualScripting;
using UnityEngine;

public class Trigger_end : MonoBehaviour
{
    [SerializeField]
    public GameObject bille1;
    public GameObject bille2;
    public GameObject bille3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name + " est arrivée !");
        if (other.CompareTag("Bille")) {
            other.gameObject.SetActive(false);
        }
    }
}
