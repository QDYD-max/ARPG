using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class RoleMgr : Singleton<RoleMgr>
{

    public GameObject LoadRole(RoleType roleType)
    {
        string path = string.Empty;
        
        switch(roleType)
        {
            case RoleType.Monster:
                path = "Monster";
                break;
            case RoleType.Player:
                path = "MainPlayer";
                break;
        }

        return ResourceLoader.Load<GameObject>(ResourceType.Role, path);
    }
}
