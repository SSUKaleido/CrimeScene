using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNative.Toasts;

public class ToastMessageHelper : MonoBehaviour
{
    private static readonly IUnityNativeToasts unityNativeToasts;

    static ToastMessageHelper() {
        unityNativeToasts = UnityNativeToasts.Create();
    }

    public static void ShowToastMessage(string toastText) {
        unityNativeToasts.ShowShortToast(toastText);
    }
}
