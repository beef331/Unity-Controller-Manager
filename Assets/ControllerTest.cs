using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class ControllerTest : MonoBehaviour
{

    private Gamepad playerOne;

    [SerializeField]
    Transform modifiedObject;

    private MeshFilter mf;
    private Material mat;

    [SerializeField]
    private Mesh cube, sphere, cylinder, capsule;

    private Camera cam;

    // Use this for initialization
    void Start()
    {
        playerOne = new Gamepad(1);
        playerOne.LoadCustomScheme(Application.dataPath + "/PlayerOneControl");
        mf = modifiedObject.GetComponent<MeshFilter>();
        mat = modifiedObject.GetComponent<MeshRenderer>().material;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 leftStick = new Vector3(playerOne.GetAxis(InputAxis.LEFTX), -playerOne.GetAxis(InputAxis.LEFTY), (playerOne.LeftTrigger() ? 1 : 0) - (playerOne.RightTrigger() ? 1 : 0));
        Vector3 rightStick = new Vector3(-playerOne.GetAxis(InputAxis.RIGHTY), 0, -playerOne.GetAxis(InputAxis.RIGHTX));
        Vector2 dPad = new Vector2(playerOne.GetAxis(InputAxis.DPADX), playerOne.GetAxis(InputAxis.DPADY));

        modifiedObject.Translate(leftStick * 10 * Time.deltaTime, Space.World);
        modifiedObject.Rotate(rightStick, 100 * Time.deltaTime, Space.World);

        if (dPad.y > 0)
        {
            mf.mesh = cube;
        }
        if (dPad.y < 0)
        {
            mf.mesh = sphere;
        }
        if (dPad.x > 0)
        {
            mf.mesh = cylinder;
        }
        if (dPad.x < 0)
        {
            mf.mesh = capsule;
        }

        if (playerOne.GetButtonDown(PressedButton.START))
        {
            Application.Quit();
        }

        if (playerOne.GetButtonDown(PressedButton.BACK))
        {
            cam.gameObject.SetActive(!cam.gameObject.activeInHierarchy);
        }

        if (playerOne.GetButtonDown(PressedButton.A))
        {
            mat.color = Color.green;
        }

        if (playerOne.GetButtonDown(PressedButton.B))
        {
            mat.color = Color.red;
        }

        if (playerOne.GetButtonDown(PressedButton.X))
        {
            mat.color = Color.blue;
        }

        if (playerOne.GetButtonDown(PressedButton.Y))
        {
            mat.color = Color.yellow;
        }

        if (playerOne.GetButton(InputAxis.LT))
        {
            modifiedObject.localScale += 2 * Time.deltaTime * Vector3.one;
        }

        if (playerOne.GetButton(InputAxis.RT))
        {
            modifiedObject.localScale -= 2 * Time.deltaTime * Vector3.one;
        }

        if (playerOne.GetButtonDown(PressedButton.LS))
        {
            playerOne.SwitchInputs(InputAxis.RT, PressedButton.B);
            playerOne.SaveCustomScheme(Application.dataPath + "/PlayerOneControl");
        }

        if (playerOne.GetButtonDown(PressedButton.RS))
        {
            playerOne.SwitchInputs(InputAxis.LEFTX, InputAxis.RIGHTX);
            playerOne.SwitchInputs(InputAxis.LEFTY, InputAxis.RIGHTY);
            playerOne.SaveCustomScheme(Application.dataPath + "/PlayerOneControl");
        }
		if (playerOne.GetButtonDown(PressedButton.START))
		{
			playerOne.ResetToDefault();
		}

    }
}
