using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupPointRealCriminalMenu : UI_Popup
{
    enum Images
    {
        Suspect1Cursor,
        Suspect2Cursor,
        Suspect3Cursor
    }

    enum Buttons
    {
        CancleButton,
        ConfirmPointButton
    }

    enum Texts
    {
        Suspect1NameText,
        Suspect2NameText,
        Suspect3NameText,
        Suspect1JopText,
        Suspect2JopText,
        Suspect3JopText,
        Suspect1RelationText,
        Suspect2RelationText,
        Suspect3RelationText,
        Suspect1SuitablityText,
        Suspect2SuitablityText,
        Suspect3SuitablityText,
        Suspect1DetectiveFingerprintText,
        Suspect2DetectiveFingerprintText,
        Suspect3DetectiveFingerprintText,
        Suspect1MotivationText,
        Suspect2MotivationText,
        Suspect3MotivationText,
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
        GetButton((int)Buttons.ConfirmPointButton).gameObject.BindEvent(OnConfirmPointButton);
    }

    private void OnConfirmPointButton(PointerEventData data)
    {

    }
}
