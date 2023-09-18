using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilenameLoader
{
    Dictionary<string, string> fileNameDictionary;
    Dictionary<string, string> flavorTextDictionary;

    public FilenameLoader()
    {
        fileNameDictionary = new Dictionary<string, string> {
            { "지문#1", "FingerprintFilm" },
            { "지문#2", "FingerprintFilm" },
            { "지문#3", "FingerprintFilm" },
            { "지문#4", "FingerprintFilm" },
            { "마이크 스탠드", "ExampleEvidence" },
            { "가건물 골조", "ExampleEvidence" },
            { "기타 줄", "ExampleEvidence" },
            { "전선", "ExampleEvidence" },
            { "스포트라이트", "ExampleEvidence" },
            { "단속봉", "ExampleEvidence" },
            { "메스", "Mess" },
            { "송곳", "Awl" },
            { "톱", "Saw" },
            { "양날칼", "Mess" },
            { "주사기", "Syringe" },
            { "포르말린", "MediBottle" },
            { "낫", "ExampleEvidence" },
            { "와이어", "ExampleEvidence" },
            { "목장갑", "ExampleEvidence" },
            { "술병", "ExampleEvidence" },
            { "야구 배트", "ExampleEvidence" },
            { "와인 오프너", "ExampleEvidence" },
            { "촛대", "ExampleEvidence" },
            { "꽃병", "ExampleEvidence" },
            { "와인병", "ExampleEvidence" },
            { "안약", "ExampleEvidence" },
            { "다이어트 약", "ExampleEvidence" },
            { "부서진 음반", "ExampleEvidence" },
            { "구겨진 악보", "ExampleEvidence" },
            { "반쯤 빈 향수병", "ExampleEvidence" },
            { "다시 포장한 선물상자", "ExampleEvidence" },
            { "콧소리가 녹음된 MP3", "ExampleEvidence" },
            { "선물 받은 안경집", "GlassesCase" },
            { "미발표 논문", "Letter" },
            { "간호일지 수첩", "NurseNote" },
            { "연구비로 결제한 영수증", "Letter" },
            { "낡은 고무장갑", "RubberGlove" },
            { "눌러 쓴 손편지", "Letter" },
            { "경구 투여 항불안제", "ExampleEvidence" },
            { "수상한 커플 반지", "ExampleEvidence" },
            { "파산한 법인 통장", "ExampleEvidence" },
            { "공증 없는 차용증", "ExampleEvidence" },
            { "자산 관리 위탁 계약서", "ExampleEvidence" },
            { "채무조회 서류", "ExampleEvidence" },
            { "오래된 일기장", "ExampleEvidence" },
            { "저주 부적", "ExampleEvidence" },
            { "오래된 커플 반지", "ExampleEvidence" },
            { "축가용 마이크", "ExampleEvidence" },
            { "무대 마이크", "ExampleEvidence" },
            { "메이크업 파우치", "ExampleEvidence" },
            { "혈액팩", "ExampleEvidence" },
            { "수술용 마스크", "ExampleEvidence" },
            { "칠판 지우개", "ExampleEvidence" },
            { "외제차 차키", "ExampleEvidence" },
            { "꽃다발", "ExampleEvidence" },
            { "드레스 장갑", "ExampleEvidence" }
        };

        flavorTextDictionary = new Dictionary<string, string> {
            { "지문#1", "사이코메트리로 최대한 정밀하게 검출해낸 지문 기억입니다. 용의자 [Name]의 지문으로, 이 기억을 통해서 흉기에서 발견한 다른 지문 기억과 비교할 수 있습니다." },
            { "지문#2", "사이코메트리로 최대한 정밀하게 검출해낸 지문 기억입니다. 용의자 [Name]의 지문으로, 이 기억을 통해서 흉기에서 발견한 다른 지문 기억과 비교할 수 있습니다." },
            { "지문#3", "사이코메트리로 최대한 정밀하게 검출해낸 지문 기억입니다. 용의자 [Name]의 지문으로, 이 기억을 통해서 흉기에서 발견한 다른 지문 기억과 비교할 수 있습니다." },
            { "지문#4", "사이코메트리로 최대한 정밀하게 검출해낸 지문 기억입니다. 피해자 [Name]의 지문으로, 현장에서 피해자의 지문이 검출되는 것은 이상한 일이 아닙니다." },
            { "마이크 스탠드", "무대 위에서 마이크를 걸어놓는 스탠드입니다. 얼핏 보면 평범해보이지만, 부품을 분해한 결과 유독 뾰족한 말단부를 발견할 수 있었습니다." },
            { "가건물 골조", "공연 환경을 조성할 때 사용하던 뾰족한 골조입니다. 콘서트 당일까지 미처 치워지지 못하고 방치되었습니다." },
            { "기타 줄", "피해자가 사용하는 악기를 정비할 소품입니다. 이 물건을 갖고 피해자에게 접근했어도 아무도 의심하지 않았을 것입니다." },
            { "전선", "공연에 필요한 각종 전자 제품에 전원을 공급할 전선입니다. 기회를 보아 여유분을 빼돌리는 것이 어렵지 않았을 것입니다." },
            { "스포트라이트", "세심하고 무거운 조명 장비이지만, 고장났습니다. 콘서트 홀에서 보여도 이상할 것 없는 물건이니 흉기로 채택되었을 가능성이 있습니다." },
            { "단속봉", "진행요원들이 몰려드는 인파를 통제하기 위해 지급 받은 단속봉입니다. 일반적으로 지급되는 것보다 크고 무거워 흉기로 사용할 수 있을 것 같습니다." },
            { "메스", "용의자들이 어렵지 않게 입수할 수 있는 날붙이입니다. 살아 있는 사람을 이 물건으로 해부하는 상상은 무섭지만, 가능성을 배제할 수는 없습니다." },
            { "송곳", "다양한 목적을 위해 배치된 평범한 송곳입니다. 흔한 물건이라고 살상력이 떨어지진 않습니다." },
            { "톱", "주로 석고 깁스를 자르는 용도로 사용하는 전기톱입니다. 대부분의 경우 매우 안전하지만, 악의를 지닌 사람의 손에 들렸을 때에는 그렇지 않습니다." },
            { "양날칼", "외과 수술에서 주로 뼈와 주변 조직을 자를 때 쓰는 도구입니다. 본래 보관 장소가 아닌 바깥에 배치되어 있던 사실을 확인했습니다." },
            { "주사기", "간호 작업 중에 유실된 주사기입니다. 이런 주사기가 있다면 병원 내에 흔히 존재하는 각종 독극물을 잠든 누군가에게 주사할 수 있을 것입니다." },
            { "포르말린", "해부 실습실에서 사용하는 방부제 겸 소독제입니다. 매우 독한 냄새를 풍기며, 인체에 매우 치명적입니다." },
            { "낫", "학교 화단을 정리하기 위해 창고에 보관되던 농기구입니다. 오래 보관하던 기자재 답지 않게 날카롭게 날이 세워져 있습니다." },
            { "와이어", "학교의 시설이 고장났을 경우 수리하기 위해 여분으로 보관하던 강철 와이어입니다. 와이어를 보관하던 창고의 손잡이는 고장 나 잠기지 않습니다." },
            { "목장갑", "다양한 작업을 위해 창고에서 보관하던 작업용 장갑입니다. 다른 목적에는 부적합하지만, 지문을 남기지 않고 목을 조르는 데에는 사용할 수 있습니다." },
            { "술병", "동창들끼리 근처 편의점에서 구매한 술병입니다. 우발적 범행의 단골 흉기이기도 합니다." },
            { "야구 배트", "학교 체육창고에 있던 야구배트입니다. 옛 기억을 살려 야구 놀이를 하기 위해 가져온 듯 싶지만, 다른 목적으로 사용되었을 가능성을 배제할 수 없습니다." },
            { "와인 오프너", "결혼을 축하하기 위한 와인을 열기 위한 물건입니다. 유난히 긴 나선형 스크루에 묻은 액체가 와인이 맞는 지 확신할 수 없습니다." },
            { "촛대", "분위기를 고취시키는 용도의 물건입니다. 기회를 보아 여유분을 빼돌리는 것이 어렵지 않았을 것입니다." },
            { "꽃병", "결혼을 축하하기 위해 누군가 선물한 유리병입니다. 피해자를 발견했을 당시 깨져 있던 이유가 피해자가 쓰러지면서 건드렸기 때문이라고 확신할 수 없습니다." },
            { "와인병", "결혼을 축하하기 위해 마련된 와인입니다. 피해자를 발견했을 당시 어떤 이유로 깨져서 산산조각 났는지 알 수 없습니다." },
            { "안약", "피해자가 평소 지병 때문에 투약하던 안약입니다. 누군가 이 안약에 독성 물질을 첨가하였을 가능성을 배제할 수 없습니다." },
            { "다이어트 약", "이날만을 위해 피해자가 복용하던 다이어트 약입니다. 피해자에게 발급 받은 기록이 없다는 것은 주위 누군가 선물했다는 의미입니다." },
            { "부서진 음반", "억지로 힘을 주어 망가뜨린 피해자의 신규 음반입니다. 용의자 [Name]의 피해자를 향한 집요한 분노와 시기심, 질투심이 엿보입니다." },
            { "구겨진 악보", "발표되지 않은 용의자 [Name]의 신곡 악보입니다. 피해자의 신곡과 유사한 코드가 발견됩니다. 피해자의 신곡이 용의자가 작곡하던 곡을 몰래 표절했다고 생각할 수도 있어 보입니다." },
            { "반쯤 빈 향수병", "구매한 지 얼마 되지 않은 향수병입니다. 용의자 [Name]의 개인 물품이지만 피해자가 자신의 것처럼 사용해 벌써 반이 비어있습니다." },
            { "다시 포장한 선물상자", "팬심에 사비를 들여 용의자 [Name]이(가) 피해자에게 선물한 물건입니다. 피해자가 다른 누군가에게 주기 위해 포장을 다시 한 흔적이 보입니다." },
            { "콧소리가 녹음된 MP3", "용의자 [Name]의 콧소리가 녹음된 피해자의 MP3입니다. 추후 이 소리를 이용해 음악을 작곡하겠다는 장난기 서린 대화도 녹음되어 있습니다. 피해자와 용의자가 얼마나 친밀한 관계였는지 짐작하게 합니다." },
            { "선물 받은 안경집", "병원 펠로우가 피해자를 아끼는 의미로 선물한 물건으로, 평소 용의자 [Name]이(가) 존경하던 교수의 신임을 피해자가 가져갔다는 증거입니다." },
            { "미발표한 논문", "피해자와 용의자 [Name]이(가) 공동 발표할 예정이었던 논문입니다. 피해자의 사망 소식이 들린 지 얼마 되지 않았는데 기다렸다는 듯이 피해자의 이름에 취소선이 그어져 있습니다." },
            { "간호일지 수첩", "용의자 [Name]가 들고 다니던 수첩입니다. 피해자가 부당하게 자신을 무시한다고 있다는 메모와 비방이 몇 페이지에 걸쳐 나타납니다." },
            { "연구비로 결제한 영수증", "용의자 [Name]이(가) 연구실 공금으로 사생활 용품을 결제했다는 증거입니다. 피해자가 이 자료를 모으고 있던 것으로 보아 용의자의 공금 횡령을 폭로할 작정이었던 것으로 보입니다." },
            { "낡은 고무장갑", "용의자 [Name]이(가) 시체 처리를 할 때 썼던 고무 장갑입니다. 규정대로 사체를 처리한다면 사용하지 말아야 할 물건입니다. 피해자가 잘못된 시체 처리에 많은 컴플레인을 넣었다는 증언이 있었습니다." },
            { "눌러 쓴 손편지", "평소 고마웠던 주변인을 위해, 피해자가 직접 꾹꾹 눌러 쓴 손편지입니다. 피해자와 용의자 [Name]이(가) 얼마나 친밀한 관계였는지 짐작하게 합니다." },
            { "경구 투여 항불안제", "용의자 [Name]이(가) 고등학생 때부터 지속적으로 복용하던 약물입니다. 이와 관련하여 피해자가 고등학교 때 용의자를 심하게 괴롭혔다는 증언이 있었습니다." },
            { "수상한 커플 반지", "용의자 [Name]와(과) 용의자의 남편이 아닌 다른 이름이 새겨진 커플 반지입니다. 용의자의 불륜 사실을 암시합니다. 피해자가 이를 알았다면 분명 이를 빌미로 피해자를 협박했을 것입니다." },
            { "파산한 법인 통장", "피해자와 용의자 [Name]이(가) 함께 경영하던 사업체 명의의 통장입니다. 피해자가 거금을 인출한 기록과 이로 인해 사업체가 파산한 정황을 확인할 수 있습니다." },
            { "공증 없는 차용증", "법적 효력이 없는 차용증입니다. 용의자 [Name]이(가) 피해자에게 돈을 빌려주었으나 피해자가 갚지 않았다는 정황을 암시합니다." },
            { "자산 관리 위탁 계약서", "용의자 [Name]이(가) 최근 \'자산관리사\'로 취직한 피해자에게 자산 관리 업무를 위탁한다는 내용의 서류입니다. 이런 계약을 체결할 정도면 상호 간의 신뢰가 두터웠던 것으로 보입니다." },
            { "채무조회 서류", "용의자 [Name]이 신부인 피해자의 채무를 조회, 인쇄한 서류입니다. 홀로는 감당하기 힘드나 둘이라면 감당할 수 있는 빚이 있습니다." },
            { "오래된 일기장", "용의자 [Name]이(가) 오래 전부터 적어오던 일기장입니다. 임자 있는 남자를 좋아하게 되었다는 한탄과 남자의 연인을 향한 비방을 꽤 오래 전 기록에서도 확인할 수 있습니다." },
            { "저주 부적", "부부 간의 사랑을 없애주는 저주가 서린 부적입니다. 평소 미신을 신봉하던 용의자 [Name]이(가) 이 결혼을 탐탁치 않게 여기고 있었음을 방증합니다." },
            { "오래된 커플 반지", "용의자 [Name]이(가) 피해자의 남편과 교제할 당시 마련했던 커플 반지입니다. 이걸 지금까지 간직하고 있었다는 것은 용의자의 미련이 많이 남아있음을 암시합니다." },
            { "축가용 마이크", "결혼식을 축복하기 위해 축가를 부르기로 한 용의자 [Name]이(가) 받은 마이크입니다. 시간을 내 축가를 준비할 정도였다면 분명 피해자와의 관계가 친밀했을 것입니다." },
            { "무대 마이크", "무대에서 사용될 예정이었던 마이크입니다. 특이한 점은 없어보입니다." },
            { "메이크업 파우치", "무대에 오를 아티스트들을 위한 화장 도구입니다. 특이한 점은 없어보입니다." },
            { "혈액팩", "새지 않도록 단단히 포장되어있는 수혈용 혈액팩입니다. 특이한 점은 없어 보입니다." },
            { "수술용 마스크", "위생 유지를 위해 착용하는 마스크입니다. 특이한 점은 없어보입니다." },
            { "칠판 지우개", "요즘은 사용하지 않는 곳이 더 많은 분필 칠판 지우개입니다. 특이한 점은 없어보입니다." },
            { "외제차 차키", "자랑하려고 준비했다는 의도가 다분한 고급 외제차 차키입니다. 특이한 점은 없어보입니다." },
            { "꽃다발", "신부가 결혼식 중 던지는 꽃다발입니다. 꽃다발을 받은 사람이 다음에 결혼한다는 미신이 있습니다. 특이한 점은 없어보입니다." },
            { "드레스 장갑", "신부가 결혼식에서 착용하는 장갑입니다. 새하얗고 아름다워 결혼식의 주인공에게 어울립니다. 특이한 점은 없어보입니다." }
        };
    }

    public string GetFilename(string evidenceName)
    {
        return fileNameDictionary[evidenceName];
    }

    public string GetFlavorText(Evidence evidence, List<SuspectInfo> explainingSuspectCandidate)
    {
        string flavorText = flavorTextDictionary[evidence.GetName()];

        if (evidence is MotivationExplainProp)
        {
            MotivationExplainProp motivationExplainProp = evidence as MotivationExplainProp;
            Define.SuspectCode explainingSuspectCode = motivationExplainProp.GetExplainingSuspect();

            foreach (SuspectInfo eachSuspect in explainingSuspectCandidate)
            {
                if (eachSuspect.GetSuspectCode() == explainingSuspectCode)
                {
                    flavorText = flavorText.Replace("[Name]", eachSuspect.GetName());
                    break;
                }
            }
        }
        if (evidence is FingerprintMemory)
        {
            FingerprintMemory fingerprintFilm = evidence as FingerprintMemory;
            Define.SuspectCode masterSuspectCode = fingerprintFilm.GetSuspectCode();

            if (masterSuspectCode == Define.SuspectCode.DoNotSuspect)
            {
                flavorText = flavorText.Replace("[Name]", GameManager.Ingame.CaseData.GetVictim().GetName());
            }
            else
            {
                foreach (SuspectInfo eachSuspect in explainingSuspectCandidate)
                {
                    if (eachSuspect.GetSuspectCode() == masterSuspectCode)
                    {
                        flavorText = flavorText.Replace("[Name]", eachSuspect.GetName());
                        break;
                    }
                }
            }
        }

        return flavorText;
    }
}
