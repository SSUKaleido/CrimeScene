using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/**
* MainScene_ARCaptureScene_SceneMenu 캔버스에 붙을 컴포넌트
* 유니티 에디터에서 오브젝트들을 바인딩하지 않고 코드로 연결하려고 사용
*/
public class UI_ARCaptureScene_SceneMenu : UI_Scene
{
    enum Images
    {
    }

    enum Buttons
    {
        ResumeDeductionButton
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

        GetButton((int)Buttons.ResumeDeductionButton).gameObject.BindEvent(OnResumeDeductionButton);
	}

    public void OnResumeDeductionButton(PointerEventData data) {
        GameManager.UI.ShowPopupUI<UI_MainScene_PopupPauseMenu>("MainScene_PopupPauseMenu");
    }
}
