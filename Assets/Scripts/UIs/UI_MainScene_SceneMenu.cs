using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/**
* MainScene_SceneMenu 캔버스에 붙을 컴포넌트
* 유니티 에디터에서 오브젝트들을 바인딩하지 않고 코드로 연결하려고 사용
*/
public class UI_MainScene_SceneMenu : UI_Scene
{
    private MainSceneInfo _mainSceneInfo;

    enum Images
    {
    }

    enum Buttons
    {
        PauseMenuButton,
        DetectiveNoteMenuButton
    }

    enum Texts
    {
        CaseNameText,
        CaseCodeText
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
        //Bind<Image>(typeof(Images));
        //Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.PauseMenuButton).gameObject.BindEvent(OnPauseMenuButton);
        GetButton((int)Buttons.DetectiveNoteMenuButton).gameObject.BindEvent(OnDetectiveNoteMenuButton);

        _mainSceneInfo = GameObject.FindWithTag("SceneInfo").GetComponent<MainSceneInfo>();
	}

    public void OnPauseMenuButton(PointerEventData data) {
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupPauseMenu>("MainScene_PopupPauseMenu");
    }
    
    public void OnDetectiveNoteMenuButton(PointerEventData data) {
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupDetectiveNoteMenu>("MainScene_PopupDetectiveNoteMenu");
        _mainSceneInfo.SetCameraOff();
    }
}
