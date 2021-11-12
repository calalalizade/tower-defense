using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void UpdateHealth(float health)
    {
        slider.value = health;
    }
}
