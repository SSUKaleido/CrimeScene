using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* 씬 고정 UI 캔버스들에 붙을 스크립트
*/
public class UI_Scene : UI_Base
{
	public override void Init()
	{
		GameManager.UI.SetCanvas(gameObject, false);
	}
}