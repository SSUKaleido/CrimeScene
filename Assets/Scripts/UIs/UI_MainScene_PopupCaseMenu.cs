using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using ScenarioType = Define.ScenarioType;

public class UI_MainScene_PopupCaseMenu : UI_MainSceneMenu
{
    GameObject caseLayout = null;
    GameObject victimLayout = null;
    RectTransform panel = null;

    Dictionary<ScenarioType, List<string>> ScenarioExplain = null;
    Dictionary<ScenarioType, List<string>> Testimonies = null;

    string victimCallName;

    enum Images
    {
    }

    enum Buttons
    {
        CaseInfoButton,
        VictimInfoButton
    }

    enum Texts
    {
        CaseCodeText,
        CaseNameText,
        CaseExplainText1,
        CaseExplainText2,
        CaseExplainText3,
        VictimNameText,
        VictimAgeText,
        VictimGenderText,
        VictimJopText,
        VictimCauseOfDeathText,
        TestimonyText1,
        TestimonyText2,
        TestimonyText3,
        TestimonyText4,
        TestimonyText5
    }

    enum GameObjects
    {
        CaseLayout,
        VictimLayout,
        Panel
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

        caseLayout = GetObject((int)GameObjects.CaseLayout);
        victimLayout = GetObject((int)GameObjects.VictimLayout);
        panel = GetObject((int)GameObjects.Panel).GetComponent<RectTransform>();

        GetButton((int)Buttons.CaseInfoButton).gameObject.BindEvent(OnCaseInfoButton);
        GetButton((int)Buttons.VictimInfoButton).gameObject.BindEvent(OnVictimInfoButton);

        victimLayout.SetActive(false);
        caseLayout.SetActive(true);

        InitFlavorTexts();
        InitTexts();
	}

    private void OnCaseInfoButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        victimLayout.SetActive(false);
        caseLayout.SetActive(true);
    }

    private void OnVictimInfoButton(PointerEventData data)
    {
        GameManager.Sound.Play("Sounds/UIButtonEffect1");
        caseLayout.SetActive(false);
        victimLayout.SetActive(true);
    }

    private void InitFlavorTexts()
    {
        VictimInfo _victim = GameManager.Ingame.CaseData.GetVictim();
        string victimCallName = _victim.GetName() + ((_victim.GetGender() == "Male") ? " 군" : " 양");
        DateTime today = DateTime.Today;

         ScenarioExplain = new Dictionary<ScenarioType, List<string>> {
            { ScenarioType.ConsertMurderCrime , new List<string> { 
                $"{today.Year}년 {today.Month}월 {today.Day}일 오후 2시 경, 팬들이 서서히 모여들기 시작한 콘서트장에서 살인 신고가 접수되었습니다.",
                $"피해자는 오늘 공연이 예정되어 있던 뮤지션 {victimCallName}으로, 컨디션 관리를 명목으로 홀로 대기실에 남아 있다가 시체로 발견되었습니다.",
                $"경찰 부검 결과 사인은 {_victim.GetCauseOfDeath()}으로 밝혀졌습니다. 경찰은 공연 취소로 인해 아비규환이 된 콘서트에서 수사를 이어나가고 있습니다."
            } },
            { ScenarioType.HospitalMurderCrime , new List<string> { 
                $"{today.Year}년 {today.Month}월 {today.Day}일 오전 11시 경, 화경종합병원에서 시체 발견 신고가 접수되었습니다. 해당 시간대에는 레지던트 인체 해부실습이 예정되어 있었습니다.",
                $"피해자는 병원 외과 레지던트 {victimCallName}으로, 본래 기증 받은 해부용 시신이 배치되어 있었어야 할 실습대 위에서 시체로 발견되었습니다.",
                $"경찰 부검 결과 사인은 {_victim.GetCauseOfDeath()}으로 밝혀졌습니다. 경찰은 피해자의 예상 동선을 산출하여 용의선상을 구성하고 수사를 이어나가고 있습니다."
            } },
            { ScenarioType.HighSchoolMurderCrime , new List<string> { 
                $"{today.Year}년 {today.Month}월 {today.Day}일 오전 4시 경, 명문 사립 고등학교 화경고등학교 인근에서 살인 신고가 접수되었습니다. 이날은 화경고등학교 동창회가 열린 날이었습니다.",
                $"피해자는 학교 졸업생 {victimCallName}으로, 동창회가 거의 끝나갈 때 잠시 바람을 쐬겠다고 주변에 말한 후 외출하였다가 시체로 발견되었습니다.",
                $"경찰 부검 결과 사인은 {_victim.GetCauseOfDeath()}으로 밝혀졌습니다. 주변인들은 피해자의 평소 행실이 좋지 않았으며, 범행 동기를 품은 사람이 적지 않을 것이라고 진술하였습니다."
            } },
            { ScenarioType.WeddingMurderCrime , new List<string> { 
                $"{today.Year}년 {today.Month}월 {today.Day}일 오후 1시 경, 결혼을 축하하기 위해 모인 하객들이 북적이는 결혼식장에서 살인 신고가 접수되었습니다. 식을 올리기까지 불과 십여 분을 남겨둔 때였습니다.",
                $"피해자는 오늘 결혼식의 신부 {victimCallName}으로, 드레스 착용과 메이크업을 모두 마친 후 신부 대기실에서 대기하던 중 시체로 발견되었습니다.",
                $"경찰 부검 결과 사인은 {_victim.GetCauseOfDeath()}으로 밝혀졌습니다. 경찰은 당일 신부와 접촉했던 이들을 용의선상에 올리고 수사를 이어나가고 있습니다."
            } }
        };

        Testimonies = new Dictionary<ScenarioType, List<string>> {
            { ScenarioType.ConsertMurderCrime, new List<string> {
                "끔찍한 기분이에요. 좀 쉬시겠냐고요? 아뇨. 말하게 해주세요.",
                "그 사람의 노래를 못 들어본 사람은 있어도 한 번만 들은 사람을 없을 거예요. 그만큼 자신의 색깔이 뚜렷한 예술을 하는 아티스트였고, 노래만큼이나 고운 심성으로도 유명했죠.",
                "그 흔한 구설수 하나 만든 적이 없었고 팬들한테도 항상 친절했어요. 지난 사인회 때에는 제 이름도 기억하고 불러줬는데, 대체 누가 이런 짓을.",
                "…… 이 불쌍한 사람이 무슨 잘못이 있어서 이런 일을 당한 거죠? 범인은 분명 착하고 좋은 사람을 보면 심사가 뒤틀리는 미친 사이코패스일 거에요!",
                "—콘서트홀을 찾은 피해자의 팬 A씨"
            } },
            { ScenarioType.HospitalMurderCrime, new List<string> {
                "인기가 많은 학생이었죠. 성격도 유들유들했고…. 누군가의 원한을 사는 상상을 하기는 어렵군요.",
                "뭘 가르쳐도 금방 배워서 저도 많이 아꼈죠. 보기보다 융통성이 모자라 자기 생각에 아니다 싶으면 인상을 찌푸리긴 했지만, 큰 문제는 아니었습니다.",
                "떠오른 불만을 바로 말할 정도로 사회성 떨어지는 성격도 아니었거니와, 우리 병원 사람들이 그런 사소한 의견 충돌로 문제를 만드는 사람들은 아니거든요.",
                "…… 범인이 우리 병원 사람인 것 같다고요? 그럴 리가 없습니다. 다시 생각해보세요.",
                "—피해자의 지도 교수 B씨"
            } },
            { ScenarioType.HighSchoolMurderCrime, new List<string> {
                "걔가 죽었다고요? 하! 언제 누구한테 맞아 죽어도 이상하지 않겠다고 생각하긴 했어요. 그 때가 우리 동창회가 될 줄은 몰랐지만.",
                "제 감상을 이상하게 여기실 것 없어요. 그런 양아치를 좋아하는 사람은 아무도 없을 테니까요. 아뇨. 개인적인 감정 때문에 이렇게 말하는 게 아니에요.",
                "학교 다닐 때 저들끼리 어울려다니며 부리는 행패는 어린 치기 탓이라도 하지, 성인이 되고서도 똑같이 하고 다녔으면 확실히 문제가 있는 거 아니겠어요?",
                "범인이 우리 동창 중에 있다죠? 찾기 어려우실 수도 있을 거에요. 동창 중에 걔한테 원한 가진 애가 한둘이 아닐거라.",
                "—동창회에 미참석한 화경고 졸업생 C씨"
            } },
            { ScenarioType.WeddingMurderCrime, new List<string> {
                "이 결혼을 더 말렸어야 했던 걸까요? 아니. 이제는 의미 없는 생각이네요. 그 애에 대해 물으셨죠? 엄청 열심히 살던 애라고 말하겠어요.",
                "어릴 때 부모님을 여의고 악착같이 살았다 하더라고요. 저랑은 대학교 새내기로 처음 만났는데, 하루 종일 알바하면서도 장학금 받아가는 걸 보고 혀를 내둘렀던 기억이 있어요.",
                "결혼 준비가 순탄하지 않았죠. 상대방 집안에서 부모가 없는 애를 며느리로 맞을 수는 없다고 나왔거든요. 저는 헤어지라 했었는데, 이제야 사랑하는 사람을 찾았다고, 이젠 행복하고 싶다고 하길래….",
                "… 후회라는 게 참는다고 참아지는 게 아니네요. 역시, 이 결혼 조금 더 말릴 걸 그랬어요.",
                "—피해자의 절친 D씨"
            } }
        };
    }

    private void InitTexts()
    {
        ScenarioType _scenario = GameManager.Ingame.CaseData.GetScenarioType();
        VictimInfo _victim = GameManager.Ingame.CaseData.GetVictim();

        GetText((int)Texts.CaseCodeText).text = $"사건 코드: {GameManager.Ingame.CaseData.GetCaseCode()}";
        GetText((int)Texts.CaseNameText).text = $"사건명: {GameManager.Ingame.CaseData.GetCaseName()}";
        GetText((int)Texts.CaseExplainText1).text = ScenarioExplain[_scenario][0];
        GetText((int)Texts.CaseExplainText2).text = ScenarioExplain[_scenario][1];
        GetText((int)Texts.CaseExplainText3).text = ScenarioExplain[_scenario][2];

        string victimGender = _victim.GetGender() == "Male" ? "남성" : "여성";
        GetText((int)Texts.VictimNameText).text = $"이름: {_victim.GetName()}";
        GetText((int)Texts.VictimAgeText).text = $"나이: {_victim.GetAge()}";
        GetText((int)Texts.VictimGenderText).text = $"성별: {victimGender}";
        GetText((int)Texts.VictimJopText).text = $"직업: {_victim.GetJop()}";
        GetText((int)Texts.VictimCauseOfDeathText).text = $"사인: {_victim.GetCauseOfDeath()}";
        GetText((int)Texts.TestimonyText1).text = Testimonies[_scenario][0];
        GetText((int)Texts.TestimonyText2).text = Testimonies[_scenario][1];
        GetText((int)Texts.TestimonyText3).text = Testimonies[_scenario][2];
        GetText((int)Texts.TestimonyText4).text = Testimonies[_scenario][3];
        GetText((int)Texts.TestimonyText5).text = Testimonies[_scenario][4];
    }

    public override void SpawnAnimation(int previewIndex)
    {
    }

    public override int DespawnAnimation(int nextIndex)
    {
        return 0;
    }
}
