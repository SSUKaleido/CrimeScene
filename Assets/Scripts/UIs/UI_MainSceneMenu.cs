using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_MainSceneMenu : UI_Popup
{
    public abstract void SpawnAnimation(int previewIndex);

    public abstract int DespawnAnimation(int nextIndex);
}
