using UnityEngine;
using UnityEngine.UI;

public class AdjustHeightToParent : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("This script requires a RectTransform component.");
            return;
        }

        // 부모 오브젝트의 RectTransform 가져오기
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            parentRectTransform = parentTransform.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("No parent found for this UI object.");
        }

        // 자신의 높이를 부모의 높이로 설정
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        if (rectTransform == null || parentRectTransform == null)
        {
            return;
        }

        // 자신의 높이가 부모의 높이보다 작으면 부모의 높이로 설정
        if (rectTransform.rect.height < parentRectTransform.rect.height)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentRectTransform.rect.height);
        }
    }
}