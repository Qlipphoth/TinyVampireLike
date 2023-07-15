using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    public Material LoadWeaponMaterial(string weaponName, int level) {
        return Resources.Load<Material>("Materials/Weapon/" + weaponName + "/" + "Lv" + level);
    }

}
