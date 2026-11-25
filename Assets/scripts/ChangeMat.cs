using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    // enum ModeSwitching { Start, Acceleration, Slow };
    // ModeSwitching mode;
    Material[] playerMat;

    Color offGreeb, offYellow, offOrange, offRed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMat = gameObject.GetComponent<Renderer>().materials;
        offGreeb = new Color32(188, 244, 14, 255); // rgb(188, 244, 14)
        offYellow = new Color32(242, 224, 17, 255); // rgb(242, 214, 17)
        offOrange = new Color32(235, 146, 17, 255); // rgb(235, 146, 17)
        offRed = new Color32(191, 52, 29, 255); // rgb(191, 52, 29)
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeColor(int HP)
    {
        if (HP <= 2){
            playerMat[1].color = offRed;
        }
        else if (2 < HP && HP <= 4){
            playerMat[1].color = offOrange;
        }
        else if (4 < HP && HP <= 6){
            playerMat[1].color = offYellow;
        }
        else if (6 < HP && HP<=8 ){
            playerMat[1].color = offGreeb;
        }
        else if (8< HP){
        }
    }
}
