using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSpeed : MonoBehaviour {

    public GameObject button;

	static public float speed { get { return _speed; } }

    static private float _speed = 1f;

    void Start()
    {
        _speed = 1f;
    }


    void Update()
    {
        if (WaveSpawner.canSpawn == true)
        {
            if(_speed != 1f)
                _speed = 1f;
            if (button.activeInHierarchy)
                button.SetActive(false);
        }
        else
            if (!button.activeInHierarchy)
            button.SetActive(true);
    }


    public void Speed()
    {
        if (_speed == 1f)
            _speed = 1.5f;
        else if (_speed == 1.5f)
            _speed = 2f;
        else
            _speed = 1f;
    }
}
