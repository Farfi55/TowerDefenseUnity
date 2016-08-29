using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
    private BuildManager buildManager;
       
    private Turret turret;
    public static bool IsCanvasOpen;

    public Color hoverColor;
    private Color defaultColor;
    private Material nodeMaterial;

    void Awake()
    {
        nodeMaterial = GetComponent<Renderer>().material;
        defaultColor = nodeMaterial.color;        
        buildManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuildManager>();
    }
    
    void OnMouseEnter()
    {
        if(buildManager.selectedTurret != null)
        nodeMaterial.color = hoverColor;
    }

    void OnMouseExit()
    {
        nodeMaterial.color = defaultColor;
    }

    void OnMouseDown()
    {
        if (turret == null)
        {
            float offset = .5f;
            Vector3 position = transform.position;
            position.y += offset;

            buildManager.Build(position, new Quaternion(), out turret);
        }
    }

    

    //public void Sell()
    //{       
    //    money.Add((int)(buildManager.selectedTurretCost * 0.8));
    //    Destroy(turret.gameObject);
    //}
}
