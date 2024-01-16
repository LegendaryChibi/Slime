using System.Collections;
using UnityEngine;

public class Destruct : MonoBehaviour
{
    [SerializeField]
    private float destructTimer = 1f;

    void OnEnable()
    {
        StartCoroutine("SelfDestruct");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(destructTimer);
        gameObject.SetActive(false);
    }
}
