using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/**
* StartScene_PopupSubmitCaseCodeMenu 캔버스에 붙을 컴포넌트
* 유니티 에디터에서 오브젝트들을 바인딩하지 않고 코드로 연결하려고 사용
*/
public class UI_StartScene_PopupSubmitCaseCodeMenu : UI_Popup
{
    enum Images
    {
        PopupMenu
    }

    enum Buttons
    {
        CancleButton,
        GameStartButton
    }

    enum Texts
    {
        TitleText,
        GuideText
    }

    enum GameObjects
    {
        CaseCodeInputField
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
        GetButton((int)Buttons.GameStartButton).gameObject.BindEvent(OnGameStartButton);
	}

    /** MainScene을 로드하는 메서드 */
    public void OnGameStartButton(PointerEventData data)
    {
        string inputedCaseCode = GetObject((int)GameObjects.CaseCodeInputField).GetComponent<TMP_InputField>().text;

        if (CaseCodeChecker.CheckRightCaseCode(inputedCaseCode))
        {
            GameManager.Scene.LoadScene(Define.Scene.MainScene);
        }
        else
        {
            GetText((int)Texts.GuideText).text = "올바른 사건번호를 입력해주세요.";
        }
        
    }
}
