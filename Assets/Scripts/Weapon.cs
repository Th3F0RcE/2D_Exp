using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;

    public Sprite artwork;

    public int range;
    public int damage;
    public int attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
