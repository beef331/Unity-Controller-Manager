using System;
using System.IO;
using System.Net;
using System.Text;
namespace NetCoreInputAdder {
    class Program {
        static string axesURL = "https://raw.githubusercontent.com/beef331/Unity-Controller-Manager/master/InputManager.asset";
        static void Main (string[] args) {
            Stream stream = HttpWebRequest.Create (axesURL).GetResponse ().GetResponseStream ();
            Start: ;
            Console.Write ("Type the directory to the Unity project folder.\nFollowed by -R to replace old axes or -K to keep old axes.");
            Console.WriteLine ("\nA backup will be saved.");
            string input = Console.ReadLine ();
            if (input.Contains (" ")) {
                string option = input.Split (" ") [1];
                input = input.Split (" ") [0];
                input = input[input.Length-1] == '/' ? input.Substring(0,input.Length-1): input;
                input = String.Format ("{0}/ProjectSettings", input);
                ColouredMessage(input,ConsoleColor.Green);
                if (Directory.Exists (input)) {
                    if (option.Equals ("-R") || option.Equals("-r") || option.Equals("--replace")) {
                         string file;
                        using (StreamReader fileSR = new StreamReader(File.OpenRead (input + "/InputManager.asset"))) {
                            file = fileSR.ReadToEnd();
                        }
                        using (StreamWriter sw = new StreamWriter( File.OpenWrite (input + "/OldInputManager.asset"))) {
                                sw.Write(file);
                            }
                        using (FileStream fs = File.OpenWrite (input + "/InputManager.asset")) {
                            
                            stream.CopyTo (fs);
                        }
                    }if(option.Equals("-K") || option.Equals("-k") || option.Equals("--keep")){
                        StreamReader sr = new StreamReader(stream);
                        for(int i =0;i < 7;i++){
                            sr.ReadLine();
                        }
                        string inputAdder = sr.ReadToEnd();
                        string file;
                        using (StreamReader fileSR = new StreamReader(File.OpenRead (input + "/InputManager.asset"))) {
                            file = fileSR.ReadToEnd();
                        }
                        using(StreamWriter fileSW = new StreamWriter(File.OpenWrite(input + "/OldInputManager.asset"))){
                            fileSW.Write(file);
                        }
                        file += inputAdder;
                        using(StreamWriter sw = new StreamWriter(File.OpenWrite(input +"/InputManager.asset"))){
                            sw.Write(file);
                        }
                    }
                }else{
                    ColouredMessage("Path does not exist,please supply unity directory",ConsoleColor.Red);
                    goto Start;
                }
            } else {
                ColouredMessage("Path and command required.",ConsoleColor.Red);
                goto Start;
            }
        }

        static void ColouredMessage (string message,ConsoleColor fontCol) {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = fontCol;
            Console.WriteLine (message);
            Console.ForegroundColor = temp;
        }
    }
}