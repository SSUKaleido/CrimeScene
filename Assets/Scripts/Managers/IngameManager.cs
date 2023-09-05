using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* 게임 내 각종 정보들과 게임 진행 메서드를 모아놓은 관리자
*/
public class IngameManager
{
    /** 추적한 마커에 따라 생성할 오브젝트를 정해주는 딕셔너리. key: 마커 이름 value: 생성할 프리펩의 이름 */
    private Dictionary<string, string> _evidenceAccordingMarkersDic = new Dictionary<string, string>();
    /** 추적한 마커에 따른 지문 이미지를 정해주는 딕셔너리. key: 마커 이름 value: 할당할 지문 번호 */
    private Dictionary<string, int> _fingerprintIndexCodeDic = new Dictionary<string, int>();
    /** 이번 게임에서 사용하는 12개 3d 오브젝트를 관리하는 딕셔너리 **/
    private Dictionary<string, GameObject> _evidenceDic = new Dictionary<string, GameObject>();

    /**
    * 단서 오브젝트를 AREvidenceHolder에 반환.
    * 빈 오브젝트를 생성해서 MeshFilter, MeshRenderer 등을 붙여 단서로 만듬
    * @param markerName 인식한 이미지 마커의 이름
    */
    public GameObject CreateEvidenceModel(string markerName)
    {
        // 이 if절 나중에 삭제
        if (!_evidenceAccordingMarkersDic.ContainsKey(markerName))
            _evidenceAccordingMarkersDic.Add(markerName, "FingerprintFilm");
        
        string evidenceName = _evidenceAccordingMarkersDic[markerName];
        GameObject newEvidence = new GameObject { name = evidenceName };
        MeshFilter evidenceMeshFilter = newEvidence.AddComponent<MeshFilter>();
        MeshRenderer evidenceMeshRenderer = newEvidence.AddComponent<MeshRenderer>();

        /** 메쉬랑 매터리얼 로드해서 할당 **/
        Mesh evidenceMesh = GameManager.Resource.Load<Mesh>($"Meshes/{evidenceName}_Mesh");
        Material evidenceMaterial = GameManager.Resource.Load<Material>($"Materials/{evidenceName}_Material");
        evidenceMeshFilter.sharedMesh = evidenceMesh;
        evidenceMeshRenderer.material = evidenceMaterial;

        /** AR 오브젝트 리스트에 넣기. **/
        _evidenceDic.Add(evidenceName, newEvidence);
    
        // 지문일 경우 이미지 추가해주는 함수 넣기.
        if (evidenceName == "FingerprintFilm")
        {
            // 이 if절 나중에 삭제
            if (!_fingerprintIndexCodeDic.ContainsKey(markerName))
                _fingerprintIndexCodeDic.Add(markerName, 5);
            
            newEvidence = CreateFingerprintImage(newEvidence, _fingerprintIndexCodeDic[markerName]);
        }

        return newEvidence;
    }

    /**
    * 생성한 단서가 지문 필름일 경우, 지문 스프타이트 이미지를 자식 메서드로 생성해주는 메서드
    * @param newEvidence 생성하고 있는 지문 필름 오브젝트
    * @param fingerprintCode 생성하고
    */
    private GameObject CreateFingerprintImage(GameObject newEvidence, int fingerprintCode)
    {
        /** 자식 오브젝트 지문 이미지를 생성하고 크기와 위치를 설정 **/
        GameObject fingerPrint = new GameObject("Fingerprint");
        fingerPrint.transform.SetParent(newEvidence.transform);
        fingerPrint.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        fingerPrint.transform.SetLocalPositionAndRotation(Camera.main.transform.forward * -0.011f, Quaternion.identity);

        /** 스프라이트 렌더러를 추가하고 스프라이트를 할당함 **/
        SpriteRenderer fingerprintSpriteRenderer = fingerPrint.AddComponent<SpriteRenderer>();
        Sprite[] fingerPrintSprites = GameManager.Resource.LoadAll<Sprite>("Sprites/Fingerprint_Sprite");
        fingerprintSpriteRenderer.sprite = fingerPrintSprites[fingerprintCode];
        fingerprintSpriteRenderer.drawMode = SpriteDrawMode.Simple;

        return newEvidence;
    }

    /*
    // 임시로 칼레이도 아이콘 모델, 지문만 나오도록 함.
        _evidenceAccordingMarkersDic.Add("KaleidoIcon", "KaleidoIconModel");
        _evidenceAccordingMarkersDic.Add("FingerprintFilm", "Evidences/FingerprintFilm");
    */
}
