using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Hurricane
{
    /// <summary>
    /// Container for game offsets, setting offsets from file
    /// </summary>
    public static class Offsets
    {

        private static Dictionary<string, int> offset = new Dictionary<string, int>();
        private static List<string> expectedOffset = new List<string>();

        public static int m_fAccuracyPenalty { get; private set; }
        public static int m_iWeaponID { get; private set; }
        public static int m_zoomLevel { get; private set; }
        public static int m_iTeamNum { get; private set; }
        public static int m_flFlashMaxAlpha { get; private set; }
        public static int m_flFlashDuration { get; private set; }
        public static int m_iGlowIndex { get; private set; }
        public static int m_iHealth { get; private set; }
        public static int m_hActiveWeapon { get; private set; }
        public static int m_iCrossHairID { get; private set; }
        public static int m_bDormant { get; private set; }
        public static int m_dwLocalPlayer { get; private set; }
        public static int m_dwEntityList { get; private set; }
        public static int m_dwGlowObject { get; private set; }
        public static int m_iClip { get; private set; }
        public static int m_fFlags { get; private set; }
        public const int m_iItemDefinitionIndex = 0x2F88;

        private static string dir = "Misc";
        private static string path = Path.Combine(Environment.CurrentDirectory, dir, "Offsets.txt");


        ///<summary>
        /// Loading offsets from txt file, setting them to local variables
        ///</summary>
        public static void LoadOffsetsFromFile()
        {            


            offset.Clear();
            SetExpectedOffsets();

            if (File.Exists(path))
            {
                try
                {
                    StreamReader sr = new StreamReader(path);
                    string file = sr.ReadToEnd();
                    sr.Close();

                    string[] singleLine = file.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (string line in singleLine)
                    {
                        foreach (string expected in expectedOffset)
                        {
                            if (line.Contains(expected))
                            {
                                int value = Int32.Parse(getBetween(line, "\"", "\""), System.Globalization.NumberStyles.HexNumber);
                                offset.Add(expected, value);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Your offsets.txt are invalid. Get new offsets!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                try
                {
                    m_fAccuracyPenalty = offset["m_fAccuracyPenalty"];
                    m_iWeaponID = offset["m_iWeaponID"];
                    m_zoomLevel = offset["m_zoomLevel"];
                    m_iTeamNum = offset["m_iTeamNum"];
                    m_flFlashMaxAlpha = offset["m_flFlashMaxAlpha"];
                    m_flFlashDuration = offset["m_flFlashDuration"];
                    m_iGlowIndex = offset["m_iGlowIndex"];
                    m_iHealth = offset["m_iHealth"];
                    m_hActiveWeapon = offset["m_hActiveWeapon"];
                    m_iCrossHairID = offset["m_iCrossHairID"];
                    m_bDormant = offset["m_bDormant"];
                    m_dwLocalPlayer = offset["m_dwLocalPlayer"];
                    m_dwEntityList = offset["m_dwEntityList"];
                    m_dwGlowObject = offset["m_dwGlowObject"];
                    m_iClip = offset["m_iClip"];
                    m_fFlags = offset["m_fFlags"];
                }

                catch (Exception)
                {
                    MessageBox.Show("Your offsets.txt doesn't have all expected offsets", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                finally
                {
                    offset.Clear();
                }
            }
            else
                MessageBox.Show("Offsets.txt doesn't exist!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);           
            
            
        }





        ///<summary>
        /// Setting list of offsets what we expect inside file
        ///</summary>
        private static void SetExpectedOffsets()
        {
             
            expectedOffset.Clear();
            expectedOffset.Add("m_fAccuracyPenalty");
            expectedOffset.Add("m_iWeaponID");
            expectedOffset.Add("m_zoomLevel");
            expectedOffset.Add("m_iTeamNum");
            expectedOffset.Add("m_flFlashMaxAlpha");
            expectedOffset.Add("m_flFlashDuration");
            expectedOffset.Add("m_iGlowIndex");
            expectedOffset.Add("m_iHealth");
            expectedOffset.Add("m_hActiveWeapon");
            expectedOffset.Add("m_iCrossHairID");
            expectedOffset.Add("m_bDormant");
            expectedOffset.Add("m_dwLocalPlayer");
            expectedOffset.Add("m_dwEntityList");
            expectedOffset.Add("m_dwGlowObject");
            expectedOffset.Add("m_iClip");
            expectedOffset.Add("m_fFlags");            
        }

        /// <summary>
        /// Simple getting part of string by extracting what we need between two char arrays
        /// </summary>
        /// <param name="strSource"> Input string</param>
        /// <param name="strStart"> First char </param>
        /// <param name="strEnd"> Last char </param>
        /// <returns> value between last two params as string </returns>
        private static string getBetween(string strSource, string strStart, string strEnd) 
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

    }
}
