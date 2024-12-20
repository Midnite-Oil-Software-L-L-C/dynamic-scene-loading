using UnityEngine;
using UnityEngine.Serialization;

public class Spinner : MonoBehaviour
{
    [FormerlySerializedAs("speed")] [SerializeField] float _speed = -256f;

    void Update()
    {
        transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
    }
}
