using UnityEngine;
using UnityEngine.UIElements;

public class levelScript : MonoBehaviour
{
    public TextElement timer;
    [SerializeField] float remainingTime;
    private bool isCarinside =  false;
    public GameObject nextlevelui;
   public GameObject car;
   public ParticleSystem particle;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isCarinside = true;
            CarController scriptComponent = car.GetComponent<CarController>();
            if(scriptComponent != null)
            {
                scriptComponent.Stop();
                scriptComponent.enabled = false;
            }
            particle.Stop();
            nextlevelui.SetActive(true);

        }
    }
    
 }

