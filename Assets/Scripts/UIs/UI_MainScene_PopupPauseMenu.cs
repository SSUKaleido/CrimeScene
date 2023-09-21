using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_MainScene_PopupPauseMenu : UI_Popup
{
    enum Images
    {
    }

    enum Buttons
    {
        CancleButton,
        IngameExitButton
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

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(CloseThisPopupUI);
        GetButton((int)Buttons.IngameExitButton).gameObject.BindEvent(OnIngameExitButton);

        GetObject((int)GameObjects.PopupMenu).GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 0f), 0.5f, true);
	}

    private void OnIngameExitButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        GameManager.Scene.LoadScene(Define.Scene.StartScene);
    }
}
