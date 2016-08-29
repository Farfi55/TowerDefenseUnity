using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour
{

    public Turret selectedTurret { get { return turret; } }
    private Turret turret;
    public int selectedTurretCost { get { return turretCost; } }
    private int turretCost;

    [SerializeField]
    private GameObject[] turretsArray;

    private Turret[] turrets;
    private Money money;


    void Awake()
    {
        money = GetComponent<Money>();

        if (turretsArray != null)
        {
            List<Turret> _turrets = new List<Turret>();
            foreach (GameObject turret in turretsArray)
            {
                Turret TurretScript = turret.GetComponent<Turret>();
                if (TurretScript != null)
                {
                    _turrets.Add(TurretScript);
                }
                else
                {
                    Debug.Log("Turret is Null");
                }
            }
            turrets = _turrets.ToArray();
        }

    }

    public void Build(Vector3 Position, Quaternion Rotation)
    {

        if (money.money >= selectedTurretCost && turret != null)
        {
            money.Add(-selectedTurretCost);
            GameObject obj = (GameObject)Instantiate( selectedTurret.gameObject, Position, Rotation);
            turret = null;
        }
    }

    public void Build(Vector3 Position, Quaternion Rotation, out Turret _turret)
    {

        if (money.money >= selectedTurretCost && turret != null)
        {
            money.Add(-selectedTurretCost);
            GameObject obj = (GameObject)Instantiate(selectedTurret.gameObject, Position, Rotation);
            _turret = obj.GetComponent<Turret>();
            turret = null;
            return;
        }
        _turret = null;
    }

    public void SelectTurret(int index)
    {
        if (index < 0 || index >= turrets.Length || turrets == null || turrets[index] == null)
            return;

        turret = turrets[index];
        turretCost = turret.Cost;
    }

}
