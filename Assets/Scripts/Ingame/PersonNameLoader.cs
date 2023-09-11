using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

/**
* 상황에 맞는 사람 이름을 로드하는 클래스
*/
public class PersonNameLoader
{
    /** 성씨를 담은 리스트 */
    List<string> _secondNameList;
    /** 연령대, 성별 별로 어울리는 이름들을 담아놓은 클래스 */
    List<string> _youthMaleNameList;
    List<string> _youthFemaleNameList;
    List<string> _midlifeMaleNameList;
    List<string> _midlifeFemaleNameList;
    List<string> _agedMaleNameList;
    List<string> _agedFemaleNameList;


    public PersonNameLoader()
    {
        _secondNameList = new List<string>() {
        "김", "이", "박", "최", "정", "조", "강", "윤", "장", "임"
        };

        _youthMaleNameList = new List<string>() {
        "건우", "도경", "도윤", "도일", "도준", "동훈", "민준", "민호", "서준", "선우",
        "성진", "승현", "시온", "우빈", "우주", "유찬", "연우", "영민", "은우", "은호",
        "이현", "준영", "재현", "지용", "지훈", "찬우", "하늘", "하준", "한결", "현우"
        };

        _youthFemaleNameList = new List<string>() {
        "가빈", "다은", "리아", "민서", "서아", "서우", "서윤", "서호", "수빈", "소라",
        "소율", "세아", "아름", "연우", "유주", "유진", "예린", "은별", "이슬", "지안",
        "지영", "지윤", "지율", "지현", "지혜", "초하", "채아", "채원", "하빈", "하은"
        };

        _midlifeMaleNameList = new List<string>() {
        "강산", "경열", "경태", "경표", "덕준", "동환", "대건", "만식", "명준", "병만",
        "원준", "일영", "정길", "정배", "진호", "창균", "평식", "현오", "형균", "호민"
        };

        _midlifeFemaleNameList = new List<string>() {
        "남주", "명선", "미란", "미애", "미주", "소정", "수연", "수정", "순옥", "시호",
        "유라", "유선", "영숙", "주혜", "지수", "진경", "진서", "진주", "현경", "희애"
        };

        _agedMaleNameList = new List<string>() {
        "경목", "길석", "덕춘", "대길", "봉수", "상국", "용구", "일용", "춘재", "혁구"
        };

        _agedFemaleNameList = new List<string>() {
        "금남", "끝순", "막례", "말순", "미자", "순덕", "순자", "옥분", "점례", "종덕"
        };
    }

    /**
    * 랜덤으로 중복되지 않는 사람 이름을 로드함
    * @param gender 새로 얻을 사람의 성별
    * @param targetAge 새로 얻을 사람의 나이(정수)
    * @param alreadyAssignedNames 이미 생성된 이름 리스트, 이 안에 있는 이름은 생성되어선 안 됨
    */
    public string LoadName(string gender, int targetage)
    {
        /** 성별과 연령대에 맞는 올바른 이름 리스트를 할당 */
        List<string> targetNameList = _youthMaleNameList;
        if (gender == "Female" && targetage < 40)
            targetNameList = _youthFemaleNameList;
        else if (gender == "Male" && targetage < 60)
            targetNameList = _midlifeMaleNameList;
        else if (gender == "Female" && targetage < 60)
            targetNameList = _midlifeFemaleNameList;
        else if (gender == "Male" && targetage < 100)
            targetNameList = _agedMaleNameList;
        else if (gender == "Female" && targetage < 100)
            targetNameList = _agedFemaleNameList;

        /**
        * 얻어낸 이름 리스트에서 랜덤으로 이름 추출
        * 추출한 이름은 리스트에서 제거 - 똑같은 이름 두번 뽑히면 안되니까 
        */
        string newName = targetNameList[Random.Range(0, targetNameList.Count)];
        targetNameList.Remove(newName);

        return _secondNameList[Random.Range(1,10)] + newName;
    }

    /**
    * 랜덤으로 성별을 로드함
    */
    public string CreateGender(List<string> genderArray)
    {
        return genderArray[Random.Range(0,genderArray.Count)];
    }

    /**
    * 최소값과 최대값 사이의 나이를 로드함
    * @param AgeGap 나이의 최소값
    */
    public int CreateAge((int, int) AgeGap)
    {
        return Random.Range(AgeGap.Item1, ++AgeGap.Item2);
    }
}
