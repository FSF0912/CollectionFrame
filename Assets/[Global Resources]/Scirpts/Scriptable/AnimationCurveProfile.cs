using UnityEngine;

[CreateAssetMenu(fileName = "AnimationCurveProfile", menuName = "FSF_Custom/AnimationCurveProfile", order = 0)]
public class AnimationCurveProfile : ScriptableObject{
    [System.Serializable]
    public struct SingleCurve{
        public string name;
        public AnimationCurve curve;

        public SingleCurve(string name, AnimationCurve curve){
            this.name = name;
            this.curve = curve;
        }
        
    }
    [SerializeField]
    private SingleCurve[] curves;

    public AnimationCurve this[int index]{
        get{
            return curves[index].curve;
        }
    }

    public AnimationCurve this[string name]{
        get{
            foreach(var item in curves){
                if(item.name == name){
                    return item.curve;
                }
            }
            throw new System.NullReferenceException("curves of name not found.");
        }
    }
    
}