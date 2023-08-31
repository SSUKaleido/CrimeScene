using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_MainScene_PopupDetectiveNoteMenu : UI_Popup
{
    private MainSceneInfo _mainSceneInfo;
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

        //AR 카메라 비활성화 시키는 용도로
        _mainSceneInfo = GameObject.FindWithTag("SceneInfo").GetComponent<MainSceneInfo>();

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);
	}

    new public void CloseThisPopupUI(PointerEventData data) {
        _mainSceneInfo.SetCameraOn();
        GameManager.UI.ClosePopupUI(this);
    }
}
