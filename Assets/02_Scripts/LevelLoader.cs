using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public void LoadLevel(int index)
    {
        Application.LoadLevel(index);
    }
}
