using UnityEngine;

public class Graph : MonoBehaviour {


    [SerializeField]
    Transform pointPrefab;

    Transform[] points;

    [SerializeField, Range(10, 1000)]
    int resolution = 10;

    

    void Awake () {

        

        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;

        points = new Transform[resolution];

        for (int i = 0; i < points.Length; i++) {
           
            Transform point = Instantiate(pointPrefab);
            points[i] = point;
            position.x = -5 + (i + 0.5f) * 10 * step - 1f;
            
            //will do this in the update method
            //position.y = Mathf.Sin(position.x * position.x);
            //position.y = position.x * position.x * position.x;
            point.localPosition = position;
            point.localScale = scale * 10f;
            
            point.SetParent(transform, false);

        }


    }

    void Update() {

        // optimized by keeping out of the loop
        float time = Time.time;

         for (int i = 0; i < points.Length; i++) {

             Transform point = points[i];
             Vector3 position = point.localPosition;

             // animate it with Time.time
             position.y = Mathf.Sin(Mathf.PI * (position.x + time));
             
             //NOTE that localposition is a property, not a public field. 
             //that's why have to do it this way
             point.localPosition = position;


         }

    }


}