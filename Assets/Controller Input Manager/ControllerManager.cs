using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Text;
using System;

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

        private Dictionary<Enum, object> mappedController = new Dictionary<Enum, object>();


#if UNITY_STANDALONE_OSX
    private KeyCode UP,DOWN,LEFT,RIGHT;
#endif
        /// <summary>
        /// Supply a InputAxis enum and it will return value identical to the Unity Input Manager
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public float GetAxis(Enum axis)
        {
			if (Enum.IsDefined(typeof(KeyCode), mappedController[axis]))
			{
				return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), mappedController[axis].ToString())) == true ?1:0;
			}
			return Input.GetAxis(mappedController[axis].ToString());
        }

        /// <summary>
        /// Supply a PressedButton and it will return a valueIdentical to the Unity Input Manager
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool GetButtonDown(Enum button)
        {
			if (Enum.IsDefined(typeof(KeyCode), mappedController[button]))
			{
				return Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), mappedController[button].ToString()));
			}
			return false;
		}

        /// <summary>
        /// Supply a PressedButton and it will return a valueIdentical to the Unity Input Manager
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool GetButtonUp(Enum button)
        {
			if (Enum.IsDefined(typeof(KeyCode), mappedController[button]))
			{
				return Input.GetKeyUp((KeyCode)Enum.Parse(typeof(KeyCode), mappedController[button].ToString()));
			}
			return false;
		}

        /// <summary>
        /// Supply a PressedButton and it will return a valueIdentical to the Unity Input Manager
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool GetButton(Enum button)
        {
			if (Enum.IsDefined(typeof(KeyCode), mappedController[button]))
			{
				return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), mappedController[button].ToString()));
			}
			return Mathf.Abs(Input.GetAxis(mappedController[button].ToString())) > 0;
		}

        /// <summary>
        /// Checks if any button is pressed and returns the first found pressed
        /// </summary>
        /// <returns></returns>
        public PressedButton AnyButtonPressed()
        {
			Enum[] keys = mappedController.Keys.ToArray();
			for (int i = 0; i < keys.Length; i++)
			{
				if (Enum.IsDefined(typeof(PressedButton), keys[i]))
				{
					if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), mappedController[keys[i]].ToString())))
					{
						return (PressedButton)Enum.Parse(typeof(PressedButton), keys[i].ToString());
					}
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
			Enum[] keys = mappedController.Keys.ToArray();
			for (int i = 0; i < keys.Length; i++)
			{
				if (Enum.IsDefined(typeof(PressedButton), keys[i]))
				{
					if (Input.GetKeyUp((KeyCode)Enum.Parse(typeof(KeyCode), mappedController[keys[i]].ToString())))
					{
						return (PressedButton)Enum.Parse(typeof(PressedButton), keys[i].ToString());
					}
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
            Enum[] keys = mappedController.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
				if (Enum.IsDefined(typeof(PressedButton), keys[i]))
				{
					if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode),mappedController[keys[i]].ToString())))
					{
						return (PressedButton)Enum.Parse(typeof(PressedButton),keys[i].ToString());
					}
				}
            }
            return PressedButton.UNDEFINED;
        }

        /// <summary>
        /// Quick hand for checking if the right trigger has not been released
        /// </summary>
        /// <returns></returns>
        public bool RightTrigger()
        {
			if (Enum.IsDefined(typeof(KeyCode), mappedController[InputAxis.RT]))
			{
				return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), mappedController[InputAxis.RT].ToString()));
			}

			return Input.GetAxis(mappedController[InputAxis.RT].ToString()) > 0;
		}

        /// <summary>
        /// Quick hand for checking if the left trigger has not been released
        /// </summary>
        /// <returns></returns>
        public bool LeftTrigger()
        {
			if (Enum.IsDefined(typeof(KeyCode), mappedController[InputAxis.LT]))
			{
				return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), mappedController[InputAxis.LT].ToString()));
			}
            return Input.GetAxis(mappedController[InputAxis.LT].ToString()) > 0;
        }

        /// <summary>
        /// Checks if any axis is activated and returns the first found.
        /// </summary>
        /// <returns></returns>
        public InputAxis AnyAxis()
        {
            Enum[] keys = mappedController.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
				if (Enum.IsDefined(typeof(InputAxis), keys[i]))
				{
					if (Input.GetAxis(mappedController[keys[i]].ToString()) != 0 )
					{
						return (InputAxis) Enum.Parse(typeof(InputAxis),keys[i].ToString());
					}
				}
            }
            return InputAxis.UNDEFINED;
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
        }

		/// <summary>
		/// Sets all inputs to defaults for platform.
		/// </summary>
		public void ResetToDefault() {
			mappedController.Clear();
			string joystickButton = "Joystick" + gamePadId.ToString() + "Button";
			string gamePad = "Gamepad" + gamePadId.ToString() + "A";
#if UNITY_STANDALONE_WIN
			mappedController.Add(PressedButton.A, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "0"));
			mappedController.Add(PressedButton.B, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "1"));
			mappedController.Add(PressedButton.X, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "2"));
			mappedController.Add(PressedButton.Y, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "3"));
			mappedController.Add(PressedButton.LB, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "4"));
			mappedController.Add(PressedButton.RB, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "5"));
			mappedController.Add(PressedButton.BACK, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "6"));
			mappedController.Add(PressedButton.START, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "7"));
			mappedController.Add(PressedButton.LS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "8"));
			mappedController.Add(PressedButton.RS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "9"));

			mappedController.Add(InputAxis.LEFTX, gamePad + "X");
			mappedController.Add(InputAxis.LEFTY, gamePad + "Y");
			mappedController.Add(InputAxis.RIGHTX, gamePad + "4");
			mappedController.Add(InputAxis.RIGHTY, gamePad + "5");
			mappedController.Add(InputAxis.DPADX, gamePad + "6");
			mappedController.Add(InputAxis.DPADY, gamePad + "7");
			mappedController.Add(InputAxis.RT, gamePad + "9");
			mappedController.Add(InputAxis.LT, gamePad + "10");

#endif
#if UNITY_STANDALONE_LINUX
            mappedController.Add(PressedButton.A, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "0"));
            mappedController.Add(PressedButton.B, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "1"));
            mappedController.Add(PressedButton.X, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "2"));
            mappedController.Add(PressedButton.Y, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "3"));
            mappedController.Add(PressedButton.LB, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "4"));
            mappedController.Add(PressedButton.RB, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "5"));
            mappedController.Add(PressedButton.BACK, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "6"));
            mappedController.Add(PressedButton.START, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "7"));
            mappedController.Add(PressedButton.LS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "9"));
            mappedController.Add(PressedButton.RS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "10"));

            mappedController.Add(InputAxis.LEFTX, gamePad + "X");
            mappedController.Add(InputAxis.LEFTY, gamePad + "Y");
            mappedController.Add(InputAxis.RIGHTX, gamePad + "4");
            mappedController.Add(InputAxis.RIGHTY, gamePad + "5");
            mappedController.Add(InputAxis.DPADX, gamePad + "7");
            mappedController.Add(InputAxis.DPADY, gamePad + "8");
            mappedController.Add(InputAxis.RT, gamePad + "3");
            mappedController.Add(InputAxis.LT, gamePad + "6");
#endif
#if UNITY_STANDALONE_MAC
            mappedController.Add(PressedButton.A, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "16"));
            mappedController.Add(PressedButton.B, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "17"));
            mappedController.Add(PressedButton.X, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "18"));
            mappedController.Add(PressedButton.Y, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "19"));
            mappedController.Add(PressedButton.LB, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "13"));
            mappedController.Add(PressedButton.RB, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "14"));
            mappedController.Add(PressedButton.BACK, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "10"));
            mappedController.Add(PressedButton.START, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "9"));
            mappedController.Add(PressedButton.LS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "11"));
            mappedController.Add(PressedButton.RS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "12"));
            mappedController.Add(PressedButton.LS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "5"));
            mappedController.Add(PressedButton.RS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "6"));
            mappedController.Add(PressedButton.LS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "7"));
            mappedController.Add(PressedButton.RS, (KeyCode)Enum.Parse(typeof(KeyCode), joystickButton + "8"));

            mappedController.Add(InputAxis.LEFTX, gamePad + "X");
            mappedController.Add(InputAxis.LEFTY, gamePad + "Y");
            mappedController.Add(InputAxis.RIGHTX, gamePad + "3");
            mappedController.Add(InputAxis.RIGHTY, gamePad + "4");
            mappedController.Add(InputAxis.RT, gamePad + "5");
            mappedController.Add(InputAxis.LT, gamePad + "6");
#endif





		}

		/// <summary>
		/// Switches two item inputs either an axis or a button. Note axes only work for GetButton and GetAxis. Buttons only return 1 or 0 in GetAxis
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public void SwitchInputs(Enum a, Enum b)
        {
            object temp = mappedController[a];
            mappedController[a] = mappedController[b];
            mappedController[b] = temp;
        }
        
        /// <summary>
        /// Saves to the path with the name provided
        /// </summary>
        /// <param name="path"></param>
        public void SaveCustomScheme(string path)
        {
            string outGoing = "";
            Enum[] buttons = mappedController.Keys.ToArray();
            for(int i =0; i < buttons.Length; i++)
            {
                outGoing += buttons[i].ToString() + "=" + mappedController[buttons[i]];
                outGoing += System.Environment.NewLine;
            }
            using (FileStream file = File.Create(path))
            {
                file.Write(Encoding.UTF8.GetBytes(outGoing), 0, outGoing.Length);

            }
        }

        /// <summary>
        /// Loads from the path with name provided
        /// </summary>
        /// <param name="path"></param>
        public void LoadCustomScheme(string path)
        {
            StreamReader reader = File.OpenText(path);
            string line;
            PressedButton parsedButton;
            InputAxis parsedAxis;
            while((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split('=');
                if (Enum.IsDefined(typeof(PressedButton), items[0]))
                {
                    parsedButton = (PressedButton)Enum.Parse(typeof(PressedButton), items[0]);
                    if (!mappedController.ContainsKey(parsedButton))
                    {
                        mappedController.Add(parsedButton, (KeyCode)Enum.Parse(typeof(KeyCode), items[1]));
                    }
                    else
                    {
                        mappedController[parsedButton] = (KeyCode)Enum.Parse(typeof(KeyCode), items[1]);
                    }
                }else if (Enum.IsDefined(typeof(InputAxis), items[0]))
                {
                    parsedAxis = (InputAxis)Enum.Parse(typeof(InputAxis), items[0]);
                    if (!mappedController.ContainsKey(parsedAxis))
                    {
                        mappedController.Add(parsedAxis, items[1]);
                    }
                    else
                    {
                        mappedController[parsedAxis] = items[1];
                    }
                }
            }
        }



    }
}
