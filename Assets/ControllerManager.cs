using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Controller
{
    /// <summary>
    /// Possible Pressed Buttons up, down, left, and right are only useable on mac
    /// </summary>
    public enum PressedButton { A, B, X, Y, RB, LB, LS, RS, START, BACK, UP, DOWN, LEFT, RIGHT,UNDEFINED};
    /// <summary>
    /// All axis of input Dpad axis are unavailable on mac
    /// </summary>
    public enum InputAxis { LEFTX, LEFTY, RIGHTX, RIGHTY, DPADX, DPADY, RT, LT,UNDEFINED };
    public class Gamepad
    {
        public int gamePadId = 1;

        private Dictionary<PressedButton, KeyCode> pressedKeycode = new Dictionary<PressedButton, KeyCode>();
        private Dictionary<InputAxis, string> axisString = new Dictionary<InputAxis, string>();


#if UNITY_STANDALONE_OSX
    private KeyCode UP,DOWN,LEFT,RIGHT;
#endif
        /// <summary>
        /// Supply a InputAxis enum and it will return value identical to the Unity Input Manager
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public float GetAxis(InputAxis axis)
        {
            return Input.GetAxis(axisString[axis]);
        }

        /// <summary>
        /// Supply a PressedButton and it will return a valueIdentical to the Unity Input Manager
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool GetButtonDown(PressedButton button)
        {
            return Input.GetKeyDown(pressedKeycode[button]);
        }

        /// <summary>
        /// Supply a PressedButton and it will return a valueIdentical to the Unity Input Manager
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool GetButtonUp(PressedButton button)
        {
            return Input.GetKeyUp(pressedKeycode[button]);
        }

        /// <summary>
        /// Supply a PressedButton and it will return a valueIdentical to the Unity Input Manager
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool GetButton(PressedButton button)
        {
            return Input.GetKey(pressedKeycode[button]);
        }

        /// <summary>
        /// Checks if any button is pressed and returns the first found pressed
        /// </summary>
        /// <returns></returns>
        public PressedButton AnyButtonPressed()
        {
            PressedButton[] keys = pressedKeycode.Keys.ToArray();
            for(int i =0; i < keys.Length; i++)
            {
                if (Input.GetKey(pressedKeycode[keys[i]]))
                {
                    return keys[i];
                }
            }
            return PressedButton.UNDEFINED;
        }
       
        /// <summary>
        /// Checks if any button has been released and returns the first found released
        /// </summary>
        /// <returns></returns>
        public PressedButton AnyButtonReleased()
        {
            PressedButton[] keys = pressedKeycode.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKeyUp(pressedKeycode[keys[i]]))
                {
                    return keys[i];
                }
            }
            return PressedButton.UNDEFINED;
        }
       
        /// <summary>
        /// Checks if any button gets pressed and returns the first found getting pressed
        /// </summary>
        /// <returns></returns>
        public PressedButton AnyButtonDown()
        {
            PressedButton[] keys = pressedKeycode.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKeyDown(pressedKeycode[keys[i]]))
                {
                    return keys[i];
                }
            }
            return PressedButton.UNDEFINED;
        }

        /// <summary>
        /// Supply an input between 1 and 4 to assign this controller manager to that value
        /// </summary>
        /// <param name="controllerID"></param>
        public Gamepad(int controllerID)
        {
            if(controllerID > 4 || controllerID < 1)
            {
                Debug.LogError("Please use a number between 1 and 4");
                return;
            }
            gamePadId = controllerID;
            string joystickButton = "Joystick" + controllerID.ToString() + "Button";
            string gamePad = "Gamepad" + controllerID.ToString() + "A";
#if UNITY_STANDALONE_WIN
            pressedKeycode.Add(PressedButton.A, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "0"));
            pressedKeycode.Add(PressedButton.B, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "1"));
            pressedKeycode.Add(PressedButton.X, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "2"));
            pressedKeycode.Add(PressedButton.Y, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "3"));
            pressedKeycode.Add(PressedButton.LB, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "4"));
            pressedKeycode.Add(PressedButton.RB, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "5"));
            pressedKeycode.Add(PressedButton.BACK, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "6"));
            pressedKeycode.Add(PressedButton.START, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "7"));
            pressedKeycode.Add(PressedButton.LS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "8"));
            pressedKeycode.Add(PressedButton.RS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "9"));

            axisString.Add(InputAxis.LEFTX, gamePad + "X");
            axisString.Add(InputAxis.LEFTY, gamePad + "Y");
            axisString.Add(InputAxis.RIGHTX, gamePad + "4");
            axisString.Add(InputAxis.RIGHTY, gamePad + "5");
            axisString.Add(InputAxis.DPADX, gamePad + "6");
            axisString.Add(InputAxis.DPADY, gamePad + "7");
            axisString.Add(InputAxis.RT, gamePad + "9");
            axisString.Add(InputAxis.LT, gamePad + "10");

#endif
#if UNITY_STANDALONE_LINUX
            pressedKeycode.Add(PressedButton.A, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "0"));
            pressedKeycode.Add(PressedButton.B, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "1"));
            pressedKeycode.Add(PressedButton.X, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "2"));
            pressedKeycode.Add(PressedButton.Y, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "3"));
            pressedKeycode.Add(PressedButton.LB, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "4"));
            pressedKeycode.Add(PressedButton.RB, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "5"));
            pressedKeycode.Add(PressedButton.BACK, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "6"));
            pressedKeycode.Add(PressedButton.START, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "7"));
            pressedKeycode.Add(PressedButton.LS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "9"));
            pressedKeycode.Add(PressedButton.RS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "10"));

            axisString.Add(InputAxis.LEFTX, gamePad + "X");
            axisString.Add(InputAxis.LEFTY, gamePad + "Y");
            axisString.Add(InputAxis.RIGHTX, gamePad + "4");
            axisString.Add(InputAxis.RIGHTY, gamePad + "5");
            axisString.Add(InputAxis.DPADX, gamePad + "7");
            axisString.Add(InputAxis.DPADY, gamePad + "8");
            axisString.Add(InputAxis.RT, gamePad + "3");
            axisString.Add(InputAxis.LT, gamePad + "6");
#endif
#if UNITY_STANDALONE_MAC
            pressedKeycode.Add(PressedButton.A, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "16"));
            pressedKeycode.Add(PressedButton.B, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "17"));
            pressedKeycode.Add(PressedButton.X, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "18"));
            pressedKeycode.Add(PressedButton.Y, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "19"));
            pressedKeycode.Add(PressedButton.LB, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "13"));
            pressedKeycode.Add(PressedButton.RB, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "14"));
            pressedKeycode.Add(PressedButton.BACK, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "10"));
            pressedKeycode.Add(PressedButton.START, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "9"));
            pressedKeycode.Add(PressedButton.LS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "11"));
            pressedKeycode.Add(PressedButton.RS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "12"));
            pressedKeycode.Add(PressedButton.LS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "5"));
            pressedKeycode.Add(PressedButton.RS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "6"));
            pressedKeycode.Add(PressedButton.LS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "7"));
            pressedKeycode.Add(PressedButton.RS, (KeyCode)System.Enum.Parse(typeof(KeyCode), joystickButton + "8"));

            axisString.Add(InputAxis.LEFTX, gamePad + "X");
            axisString.Add(InputAxis.LEFTY, gamePad + "Y");
            axisString.Add(InputAxis.RIGHTX, gamePad + "3");
            axisString.Add(InputAxis.RIGHTY, gamePad + "4");
            axisString.Add(InputAxis.RT, gamePad + "5");
            axisString.Add(InputAxis.LT, gamePad + "6");
#endif
        }
    }
}
