using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInfo : BaseSceneInfo
{
    private readonly Vector3 EvidenceScaleAtSlot = new Vector3(5f, 5f, 5f);

    /**
    * RenderTexture_Root를 생성하는 프로퍼티
    * RenderTexture_Root는 하이어라키 창의 오브젝트들을 정리할 용도
    */
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("RenderTexture_Root");
            if (root == null)
                root = new GameObject { name = "RenderTexture_Root" };
            return root;
		}
    }

    protected override void Init() // 상속 받은 Awake() 안에서 실행됨. "MainScene"씬 초기화
    {
        base.Init(); // BaseScene의 Init()

        SceneType = Define.Scene.MainScene;
        GameManager.UI.ShowSceneUI<UI_MainScene_SceneMenu>("MainScene_SceneMenu");

        GameManager.Input.AddInputAction(ActiveEscapeKey);

        SetRenderTexture();
	}

    public override void Clear()
    {
        GameManager.Input.RemoveInputAction(ActiveEscapeKey);
    }

    /**
    * 취소 키 입력받았을 때 반응
    * 아무런 팝업 화면이 없었다면 일시정지 메뉴, 일시정지 메뉴였다면 메인 화면으로
    * 그 외 다른 팝업 UI였다면 팝업 UI 닫음
    */ 
    public void ActiveEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.UI.CurrentTopUI() is UI_MainScene_PopupPauseMenu)
            {
                GameManager.Scene.LoadScene(Define.Scene.StartScene);
            }
            else
            {
                GameManager.UI.ShowPopupUI<UI_MainScene_PopupPauseMenu>("MainScene_PopupPauseMenu");
            }
        }
    }

    private void SetRenderTexture()
    {
        List<Evidence> evidences = GameManager.Ingame.CaseData.GetEvidences();
        Vector3 objPos = new Vector3(0f, 0f, 0f);

        for (int i = 0; i < 13; i++)
        {
            GameObject renderObject = GameManager.Ingame.CreateEvidenceModel(evidences[i]);
            GameObject renderCamera = GameManager.Resource.Instantiate("RenderTextureCamera");
            string path = $"RenderTexture/Evidence{i + 1}RenderTexture";          
            renderCamera.GetComponent<Camera>().targetTexture = GameManager.Resource.Load<RenderTexture>(path);
            
            renderObject.transform.SetParent(Root.transform);
            renderCamera.transform.SetParent(Root.transform);
            renderObject.transform.SetPositionAndRotation(objPos, Quaternion.Euler(15f, 30f, 0));
            renderObject.transform.localScale = EvidenceScaleAtSlot;
            renderCamera.transform.SetPositionAndRotation(objPos + new Vector3(0f, 0f, -5f), Quaternion.identity);

            renderObject.layer = LayerMask.NameToLayer("UICamera");
            Transform child = renderObject.transform.Find("Fingerprint");
            if (child != null)
            {
                child.gameObject.layer = LayerMask.NameToLayer("UICamera");
            }

            objPos += new Vector3(10f, 0f, 0f);
        }

    }
}