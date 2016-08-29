using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Money : MonoBehaviour {

    private int startMoney = 600;

    public Text moneyText;

    public int money
    {
        get { return _money; }
    }

    private int _money;

    void Start ()
    {
        Add(startMoney);
    }
	
    private void OnMoneyChange()
    {
        moneyText.text = _money + "$";
    }

    public void Add(int value)
    {
        _money += value;
        OnMoneyChange();
    }


}
