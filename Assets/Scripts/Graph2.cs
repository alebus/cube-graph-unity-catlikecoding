using UnityEngine;

public class Graph2 : MonoBehaviour {


    [SerializeField]
    Transform pointPrefab;

    Transform[] points;

    [SerializeField, Range(10, 1000)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function;

    void Awake () {

        

        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;

        points = new Transform[resolution * resolution];

        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
           
            if(x == resolution) {
                x = 0;
                z += 1;
            }

            Transform point = Instantiate(pointPrefab);
            points[i] = point;

            //position.x = -5 + (i + 0.5f) * 10 * step - 1f;
            position.x = (x + 0.5f) * step - 1f;
            position.z = (z + 0.5f) * step - 1f;
            
            //will do this in the update method
            //position.y = Mathf.Sin(position.x * position.x);
            //position.y = position.x * position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
            
            point.SetParent(transform, false);

        }


    }

    void Update() {

        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);

        // optimized by keeping out of the loop
        float time = Time.time;

         for (int i = 0; i < points.Length; i++) {

             Transform point = points[i];
             Vector3 position = point.localPosition;

            position.y = f(position.x, position.z, time);
             
             //NOTE that localposition is a property, not a public field. 
             //that's why have to do it this way
             point.localPosition = position;


         }

    }


}