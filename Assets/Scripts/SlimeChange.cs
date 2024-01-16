using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChange : MonoBehaviour
{

    [SerializeField]
    private Material changeMaterial;

    private Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = gameObject.GetComponent<MeshRenderer>().material;
        defaultMaterial.color = changeMaterial.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Slime slime = other.GetComponent<Slime>();
            slime.SlimeMesh.GetComponent<SkinnedMeshRenderer>().material = changeMaterial;
        }
    }
}
