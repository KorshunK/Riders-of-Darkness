using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    [SerializeField]
    Car car;

    float pressedBreak = 0f;
    void Update()
    {
        car.trottleInput = Input.GetAxis("Vertical");
        //car.steerInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space))
        {
            pressedBreak = Mathf.Min(pressedBreak + 0.02f, 1f);

        }
        else
        {
            pressedBreak = 0f;
        }
        car.breakInput = pressedBreak;

    }
}
