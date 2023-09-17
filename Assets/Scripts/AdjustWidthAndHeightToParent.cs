using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustWidthAndHeightToParent : MonoBehaviour
{
    private void Start()
    {
        // 현재 게임 오브젝트의 부모 RectTransform을 가져옵니다.
        RectTransform parentRectTransform = GetComponent<RectTransform>().parent as RectTransform;

        if (parentRectTransform != null)
        {
            // 부모 RectTransform의 높이와 너비를 가져옵니다.
            float parentWidth = parentRectTransform.rect.width;
            float parentHeight = parentRectTransform.rect.height;

            float shorterLength = (parentHeight < parentWidth) ? parentHeight : parentWidth;
            shorterLength -= 100f;

            if (shorterLength > 256f)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(shorterLength, shorterLength);
            }
        }
        else
        {
            Debug.LogWarning("부모 RectTransform을 찾을 수 없습니다.");
        }
    }

}
