using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/**
* StartScene_PopupMenuSettingMEnu 캔버스에 붙을 컴포넌트
* 유니티 에디터에서 오브젝트들을 바인딩하지 않고 코드로 연결하려고 사용
*/
public class UI_StartScene_PopupSettingMenu : UI_Popup
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
        PopupMenu
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

        // (확장 메서드) CancleButton에 UI_EvenetHandler를 붙이고 OnCalcleButton 메서드를 등록한다.
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);

        GetObject((int)GameObjects.PopupMenu).GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 0f), 0.5f, true);
	}
}
