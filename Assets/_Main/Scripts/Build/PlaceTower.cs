using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour
{
    GameObject parent;
    [SerializeField] GameObject tower;
    Button button;
    TMP_Text price;

    private void Start()
    {
        parent = GetComponentInParent<Build>().gameObject;

        button = gameObject.GetComponent<Button>();
        price = gameObject.GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        var enoughMoney = GameManager.Instance.EnoughMoneyForTurret(tower.tag);
        price.text = "$" + GameManager.Instance.PriceForTurret(tower.tag).ToString();

        if (enoughMoney)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void Place()
    {
        GameObject instance = Instantiate(tower, parent.transform.position, Quaternion.identity);
        GameManager.Instance.TurretBuilt(instance);

        parent.SetActive(false);
    }
}
