using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : BasePanel
{
    public void DeleteAllData()
    {
        PlayerData.DeleteAll();
    }
}
