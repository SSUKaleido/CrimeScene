using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupDetectiveNoteMenu : UI_Popup
{
    enum Images
    {
    }

    enum Buttons
    {
        CancleButton
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

        //AR 세션 비활성화 시키는 코드 추가

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);
	}

    new public void CloseThisPopupUI(PointerEventData data) {
        //AR 세션 재활성화 시키는 코드 추가
        GameManager.UI.ClosePopupUI(this);
    }
}
