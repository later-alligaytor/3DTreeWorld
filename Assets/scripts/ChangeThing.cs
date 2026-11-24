using UnityEngine;

public class ChangeThing : MonoBehaviour
{
    //[SerializeField]
    //private GameObject player;

    //private CharacterController controler;

    [SerializeField]
    private GameObject gunLight;

    private Light light;
    private float defaultIntensity;
    [SerializeField]
    private float changeSpeed;

    private bool lightOn, lightOut;

    // private Color32 red, green, blue, white;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = gunLight.GetComponent<Light>();
        defaultIntensity = 0.005f;
        // red = new Color32( 255 , 0 , 0 , 255 );
        // green = new Color32( 0 , 255 , 0 , 255 );
        // blue = new Color32( 0 , 0 , 255 , 255 );
        // white = new Color32( 255 , 255 , 255 , 255 );


    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P)) {
        //     if (light.intensity >= 6){
        //         light.intensity = 3f;
        //     }
        //     else{
        //         light.intensity += 1f;
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.O)) {
        //     if (light.color == white){
        //         light.color = red;
        //     }
        //     else if (light.color == red){
        //         light.color = blue;
        //     }
        //     else if (light.color == blue){
        //         light.color = green;
        //     }
        //     else{
        //         light.color = white;
        //     }
        // }
        if (lightOut)
        {
            light.intensity = Mathf.MoveTowards(light.intensity, 0f, changeSpeed * Time.deltaTime); //start intensity, end intensity, unitys per second.
            if (light.intensity == 0f){
                lightOut = false;
            }
        }

        else if (lightOn)
        {
            light.intensity = Mathf.MoveTowards(light.intensity, defaultIntensity, changeSpeed * Time.deltaTime);
            if (light.intensity == defaultIntensity){
                lightOn = false;
            }
        }

    }

    // Steady linear change toward target intensity
    public void LightOut()
    {
        lightOn = false;
        lightOut = true;
    }
    public void LightOn()
    {
        lightOut = false;
        lightOn = true;
    }
}
