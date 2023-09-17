using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupPauseMenu : UI_Popup
{
    enum Images
    {
    }

    enum Buttons
    {
        CancleButton,
        IngameExitButton,
        DebugButton
    }

    enum Texts
    {
    }

    enum GameObjects
    {
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        /** Button, Text, Image 오브젝트들을 가져와 _objects 딕셔너리에 바인딩 **/
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);
        GetButton((int)Buttons.IngameExitButton).gameObject.BindEvent(OnIngameExitButton);
        GetButton((int)Buttons.DebugButton).gameObject.BindEvent(OnDebugButton);
	}

    private void OnIngameExitButton(PointerEventData data)
    {
        GameManager.Scene.LoadScene(Define.Scene.StartScene);
    }

    private void OnDebugButton(PointerEventData data)
    {
        List<Evidence> evidences = GameManager.Ingame.CaseData.GetEvidences();
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[0]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[1]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[2]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[3]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[4]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[5]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[6]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[7]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[8]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[9]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[10]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[11]);
        GameManager.Ingame.PrograssData.FindNewEvidence(evidences[12]);
    }
}
