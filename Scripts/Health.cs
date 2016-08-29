using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public Text livesText;

    private int startLives = 100;
    

    public int lives
    {
        get { return _lives; }
    }

    private int _lives;

    void Start()
    {
        _lives = startLives;
        OnLivesChange();
    }

    private void OnLivesChange()
    {
        livesText.text = _lives + "♥";
    }

    public void Damage(int value)
    {
        if (value > 0)
        {
            _lives -= value;
        }
        OnLivesChange();
    }
}
