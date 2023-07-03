using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHelper : MonoBehaviour
{
    public void SaveInGame()
    {
        SaveManager.Instance.SaveItemsLifeAndCheckpoints();        
    }
}
