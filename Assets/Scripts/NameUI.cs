using UnityEngine;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    Transform cam;
    Transform ui;
    Text nameUI;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                nameUI = ui.GetChild(0).GetComponent<Text>();
                nameUI.text = PlayerManager.instance.player.GetComponent<PlayerStats>().CharacterName;
                break;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
        }

    }
}
