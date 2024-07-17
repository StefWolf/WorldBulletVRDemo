using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public string name { get; set; }
    public int hp { get; set; }
    public int hunger { get; set; }
    public float moneys {  get; set; }

    public Player(string _name)
    {
        name = _name;
        hp = 100;
        hunger = 100;
        moneys = 10000;
    }

}


