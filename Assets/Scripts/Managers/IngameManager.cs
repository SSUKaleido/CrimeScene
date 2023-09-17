using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* 게임 내 각종 정보들과 게임 진행 메서드를 모아놓은 관리자
*/
public class IngameManager
{

    /** 사건 정보가 들어있는 CrimeCaseData **/
    public CrimeCaseData CaseData = null;
    /** 게임 진행 상황을 저장하는 InvestigationPrograssData **/
    public DeductionPrograssData PrograssData = null;

    /** 추적한 마커에 따라 생성할 오브젝트를 정해주는 딕셔너리. key: 마커 이름 value: 생성할 프리펩의 이름 **/
    private Dictionary<string, Evidence> _evidenceAccordingMarkers = new Dictionary<string, Evidence>();
    
    /**
    * 사건 코드에 따라 게임 정보를 초기화
    * 케이스데이터를 로드하고 새 
    * @parma caseCode 입력받은 사건 코드
    */
    public void LoadCrimeCaseData(string caseCode)
    {
        CaseData = new CrimeCaseData();
        CaseData.LoadCrimeCase(caseCode);

        PrograssData = new DeductionPrograssData();
        PrograssData.InitDeductotionData(CaseData);

        LinkMarkerToEvidence();
    }

    private void LinkMarkerToEvidence()
    {
        List<Evidence> evidences = CaseData.GetEvidences();
        for (int i = 1; i <= 13; i++)
        {
            string markerName = $"Marker_{i}";
            _evidenceAccordingMarkers.Add(markerName, evidences[i - 1]);
        }
    }

    public Evidence GetEvidencePerMarker(string markerName)
    {   
        return _evidenceAccordingMarkers[markerName];
    }

    public GameObject CreateEvidenceModel(Evidence evidence)
    {   
        /** 한 번 발견했던 프리펩은 쉽게 인스턴스?
        if (EvidencePrefabs.ContainsKey(evidence.GetName()))
        {
            GameObject existingEvidence = GameManager.Resource.Instantiate(EvidencePrefabs[evidence.GetName()]);
            return existingEvidence;
        } **/

        string evidenceFileName = evidence.GetFilename();
        GameObject newEvidence = new GameObject { name = evidence.GetName() };
        MeshFilter evidenceMeshFilter = newEvidence.AddComponent<MeshFilter>();
        MeshRenderer evidenceMeshRenderer = newEvidence.AddComponent<MeshRenderer>();

        /** 메쉬랑 매터리얼 로드해서 할당 **/
        Mesh evidenceMesh = GameManager.Resource.Load<Mesh>($"Meshes/{evidenceFileName}_Mesh");
        Material evidenceMaterial = GameManager.Resource.Load<Material>($"Materials/{evidenceFileName}_Material");
        evidenceMeshFilter.sharedMesh = evidenceMesh;
        evidenceMeshRenderer.material = evidenceMaterial;
    
        // 지문일 경우 이미지 추가해주는 함수 넣기.
        if (evidence is FingerprintMemory)
        {   
            newEvidence = CreateFingerprintImage(newEvidence, (evidence as FingerprintMemory).GetFingerprintCode());
        }

        return newEvidence;
    }

    /**
    * 생성한 단서가 지문 필름일 경우, 지문 스프타이트 이미지를 자식 메서드로 생성해주는 메서드
    * @param newEvidence 생성하고 있는 지문 필름 오브젝트
    * @param fingerprintCode 생성하고
    */
    public GameObject CreateFingerprintImage(GameObject newEvidence, int fingerprintCode)
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
}
