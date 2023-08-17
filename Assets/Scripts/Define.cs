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
}