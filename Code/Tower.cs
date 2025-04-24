using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int costPrice = 50;
    [SerializeField] float buildDelay;
    private void Start()
    {
        StartCoroutine(Build());
    }
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();


        if (bank == null) { return false; }

        if (bank.CurrentBalance >= costPrice)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(costPrice);
            return true;
        }

        return false;
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in transform)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);

            yield return new WaitForSeconds(buildDelay);

            foreach (Transform grandchild in transform)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
