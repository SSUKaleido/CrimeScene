using System.Collections;
using System.Collections.Generic;

public class Define
{
    public enum Scene
    {
        Unknown, // 디폴트
        StartScene, // 시작 화면 씬
        MainScene, // 메인 게임 씬
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        Click,
    }

    public enum CameraMode
    {
        QuarterView,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,  // 아무것도 아님. 그냥 Sound enum의 개수 세기 위해 추가. (0, 1, '2' 이렇게 2개) 
    }
}