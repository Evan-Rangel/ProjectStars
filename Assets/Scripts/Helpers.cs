using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public static class Helpers
{
    //Obtener la camara
    private static Camera _camera;
    public static Camera Camera
    {
        get 
        {
            if (_camera == null)_camera=Camera.main;
            return _camera;
        }
    }

    //Optimizacion de WaitForSeconds
    private static readonly Dictionary <float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds> ();
    public static WaitForSeconds WaitForSeconds(float time)
    { 
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

        WaitDictionary[time]= new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    //Detecta si el raton esta sobre la ui para no realizar accion
    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _result;
    public static bool IsOverUI() {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _result= new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _result);
        return _result.Count > 0;
    }
    //Obtiene la posicion de elemento en ui (Se puede usar para localizar objectos 3d en la posicion de un RectTrans)
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }
    //Elimina todos los hijos de un objeto
    public static void DeleteChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }
}   
