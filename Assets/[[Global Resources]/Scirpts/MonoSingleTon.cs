using UnityEngine;

namespace FSF.Collection{
public class MonoSingleTon<T> : MonoBehaviour where T : MonoSingleTon<T>
{
    private static T instance;
    public static T Instance{
        get{
            return instance;
        }
    }

    private void Awake() {
        if(instance == null){
            instance = this as T;
        }
        else{
            DestroyImmediate(this.gameObject);
            return;
        }
        OnAwake();
    }

    protected virtual void OnAwake(){}
}}
