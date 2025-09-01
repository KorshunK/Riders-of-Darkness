using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Car : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    // газ
    [HideInInspector]
    public float trottleInput = 0f;
    // тормоз
    [HideInInspector]
    public float breakInput = 0f;
    //// руль
    //[HideInInspector]
    //public float steerInput = 0f;

    // скорость в кмч
    [HideInInspector]
    public float kmh = 0f;

    // скорость в мс
    float ms = 0f;

    float max_Vel_ms = 180f / 3.6f;

    [SerializeField]
    AnimationCurve accel;

    //// макс угол пооворта
    //float max_rot_angle = 120f;
    //// падение угла поворота после опред-й скорости
    //float good_speed = 60f/3.6f;
    //float delta_rot = 60f;
    //// угол поворота
    //float rot_angle = 0f;
    // Мощность (Вт)
    float power_wt = 111900f;
    // Крутящий моментВ
    float hm = 250f;
    float mass = 1500f;
    float pered_num = 3.5f;
    float wheel_r = 0.3f;
    float efficiency_transmission = 0.8f;
    void MoveCar()
    {
        float traction_power = (hm * pered_num * efficiency_transmission) / wheel_r;
        float car_acceleration = (traction_power / mass);
        rb.linearVelocity += trottleInput * (Vector2)rb.transform.up * accel.Evaluate(ms+car_acceleration/3.6f/max_Vel_ms);
        //rb.AddForce(new Vector2(trottleInput * car_acceleration * Time.fixedDeltaTime, 0));
    }

    //void RotateCar()
    //{
    //    //if (ms < good_speed)
    //    //{
    //    //    rot_angle = max_rot_angle;
    //    //}
    //    //else
    //    //{
    //    //    rot_angle = max_rot_angle - Mathf.Lerp(0f, delta_rot, (ms - good_speed) / (max_Vel_ms - good_speed));
    //    //}

    //    //rb.transform.Rotate(0f, 0f, -100f * trottleInput * Time.deltaTime);
    //}

    void Brake()
    {
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime*breakInput);
    }
    private void FixedUpdate()
    {
        ms = rb.linearVelocity.magnitude;
        kmh = 3.6f * ms;
        if (trottleInput != 0f)
        {
            MoveCar();
        }
        
        //if(steerInput != 0f)
        //{
        //    RotateCar();
        //    Vector2 old_dir = rb.linearVelocity / ms;
        //    Vector2 new_dir = (Vector2)transform.up + old_dir;
        //    new_dir.Normalize();
        //    rb.linearVelocity = ms * new_dir;
        //}
        if(breakInput != 0f)
        {
            Brake();
        }
        
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(10, 10, 120, 20), kmh + "km/h");
    }
}
