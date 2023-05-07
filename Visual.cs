using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using VisualFrameworkLibrary.Properties;

namespace VisualFrameworkLibrary
{
    public class Visual
    {
        private VisualConfiguration configuration;
        private DebugExtended debug;
        private BorderConfig borderConfig;

        internal string[] letters;
        internal List<ServiceSymbol> symbols;
        

        public Visual(string FNT_NAME) //mb add a way to txt
        {
            symbols = new List<ServiceSymbol>();
            debug = new DebugExtended("VisualFramework");
            if (!Directory.Exists("fonts"))
            {
                Directory.CreateDirectory("fonts");
                debug.WriteLine("\"fonts\" directory not found. Creating new..");
            }

            string lettersInput = Resources.DEFAULT_LETTERS;
            string symbolsInput = Resources.DEFAULT_SERVICE_LETTERS;

            if (Directory.Exists("fonts/" + FNT_NAME))
            {
                if (File.Exists("fonts/" + FNT_NAME + "/config.xml") && File.Exists("fonts/" + FNT_NAME + "/letters.fnt") && File.Exists("fonts/" + FNT_NAME + "/service_symbols.fnt"))
                {
                    lettersInput = File.ReadAllText("fonts/" + FNT_NAME + "/letters.fnt");
                    symbolsInput = File.ReadAllText("fonts/" + FNT_NAME + "/service_symbols.fnt");
                }
                else
                {
                    debug.WriteLine("Font " + FNT_NAME + " is damaged. The default font will be selected");
                }
            }
            else
            {
                debug.WriteLine("Font " + FNT_NAME + " is not exist. The default font will be selected");
            }

            try
            {
                #region Letters input
                letters = lettersInput.Split(",");
                for (int i = 0; i < letters.Length; i++)
                {
                    letters[i] = letters[i].Replace(']', ' ');
                    letters[i] = letters[i].Replace('[', ' ');
                    letters[i] = letters[i].TrimStart();
                    letters[i] = letters[i].TrimEnd();
                }
                #endregion
            }
            catch
            {
                debug.WriteLine("Letters' file damaged. Default letters will be used.");
                #region Letters input(damaged scenario)
                lettersInput = Resources.DEFAULT_LETTERS;
                letters = lettersInput.Split(",");
                for (int i = 0; i < letters.Length; i++)
                {
                    letters[i] = letters[i].Replace(']', ' ');
                    letters[i] = letters[i].Replace('[', ' ');
                    letters[i] = letters[i].TrimStart();
                    letters[i] = letters[i].TrimEnd();
                }
                #endregion
            } //trynna parce letters

            try
            {
                #region Symbols input
                string[] preSymbols = symbolsInput.Split('}')[symbolsInput.Split('}').Length - 1].Split(',');
                for (int i = 0; i < preSymbols.Length; i++)
                {
                    //Console.Write(spl1line[i].Trim());
                    string[] spl2line = preSymbols[i].Split(':');
                    symbols.Add(new ServiceSymbol(spl2line[0].Replace('\'', ' ').Trim().Trim() == "" ? ' ' : spl2line[0].Replace('\'', ' ').Trim().Trim()[0], spl2line[1].Replace('[', ' ').Replace(']', ' ').Trim()));
                }
                #endregion
            }
            catch
            {
                debug.WriteLine("Symbols' file damaged. Default symbols will be used.");
                #region Symbols input(damaged scenario)
                symbolsInput = Resources.DEFAULT_SERVICE_LETTERS;
                string[] preSymbols = symbolsInput.Split('}')[symbolsInput.Split('}').Length - 1].Split(',');
                for (int i = 0; i < preSymbols.Length; i++)
                {
                    //Console.Write(spl1line[i].Trim());
                    string[] spl2line = preSymbols[i].Split(':');
                    symbols.Add(new ServiceSymbol(spl2line[0].Replace('\'', ' ').Trim().Trim() == "" ? ' ' : spl2line[0].Replace('\'', ' ').Trim().Trim()[0], spl2line[1].Replace('[', ' ').Replace(']', ' ').Trim()));
                }
                #endregion
            } //trynna parce symbols

            string borderData = symbolsInput.Split('\'')[0].Trim();
            borderConfig = new BorderConfig(borderData);
        }

        public string ReturnHeaderLable(string str)
        {
            str = str.ToLower();
            string output = "";
            int downsize = letters[0].Split('\n').Length;
            for (int i = 0; i < downsize; i++)
            {
                for (int y = 0; y < str.Length; y++)
                {
                    if (str[y] >= 'a' && str[y] <= 'z')
                    {
                        output += letters[Convert.ToInt32(str[y]) - 97].Split('\n')[i].Trim();
                    }
                    else output += symbols[SERV_GetSymbolIndex(str[y])].symbol.Split('\n')[i].Trim();
                }
                output += '\n';
            }
            return output;
        }

        public void WriteLineHeaderLable(string str)
        {
            Console.WriteLine(ReturnHeaderLable(str));
        }

        public string ReturnBorderedHeaderLable(string str)
        {
            str = str.ToLower();
            string output = "";
            int charLenght = 0;
            string[] upLeftBorders = borderConfig.borderUpLeft.Split(',');
            for (int i = 0; i < borderConfig.borderXHeight; i++)
            {
                upLeftBorders[i] = upLeftBorders[i].Replace('{', ' ').Replace('}', ' ').Trim();
            }
            string[] upRightBorders = borderConfig.borderUpRight.Split(',');
            for (int i = 0; i < borderConfig.borderXHeight; i++)
            {
                upRightBorders[i] = upRightBorders[i].Replace('{', ' ').Replace('}', ' ').Trim();
            }
            string[] downLeftBorders = borderConfig.borderDownLeft.Split(',');
            for (int i = 0; i < borderConfig.borderXHeight; i++)
            {
                downLeftBorders[i] = downLeftBorders[i].Replace('{', ' ').Replace('}', ' ').Trim();
            }
            string[] downRightBorders = borderConfig.borderDownRight.Split(',');
            for (int i = 0; i < borderConfig.borderXHeight; i++)
            {
                downRightBorders[i] = downRightBorders[i].Replace('{', ' ').Replace('}', ' ').Trim();
            }
            string[] upperBorders = borderConfig.borderUpper.Split(',');
            for (int i = 0; i < borderConfig.borderXHeight; i++)
            {
                upperBorders[i] = upperBorders[i].Replace('{', ' ').Replace('}', ' ').Trim();
            }
            string[] lowerBorders = borderConfig.borderLower.Split(',');
            for (int i = 0; i < borderConfig.borderXHeight; i++)
            {
                lowerBorders[i] = lowerBorders[i].Replace('{', ' ').Replace('}', ' ').Trim();
            }
            
            int downsize = letters[0].Split('\n').Length;
            for (int i = 0; i < downsize; i++)
            {
                charLenght = 0;
                output += borderConfig.borderRight.Replace('{', ' ').Replace('}', ' ').Trim();
                for (int y = 0; y < str.Length; y++)
                {
                    if (str[y] >= 'a' && str[y] <= 'z')
                    {
                        output += letters[Convert.ToInt32(str[y]) - 97].Split('\n')[i].Trim();
                        charLenght += letters[Convert.ToInt32(str[y]) - 97].Split('\n')[i].Trim().Length;
                    }
                    else
                    {
                        output += symbols[SERV_GetSymbolIndex(str[y])].symbol.Split('\n')[i].Trim();
                        charLenght += symbols[SERV_GetSymbolIndex(str[y])].symbol.Split('\n')[i].Trim().Length;
                    }
                }
                output += borderConfig.borderLeft.Replace('{', ' ').Replace('}', ' ').Trim();
                output += '\n';
                
            }
            string upperborder = "";

            for (int i = 0; i < borderConfig.borderXHeight; i++)
            {
                upperborder += upLeftBorders[i] + SERV_ReturnLine(upperBorders[i].Trim()[0], charLenght) + upRightBorders[i] + "\n";
            }
            output = upperborder + output;

            string lowerborder = "";
            for (int i = 0; i < borderConfig.borderXHeight; i++)
            {
                lowerborder += downLeftBorders[i] + SERV_ReturnLine(lowerBorders[i].Trim()[0], charLenght) + downRightBorders[i] + "\n";
            }
            output += lowerborder;
            return output;
        }

        public void WriteLineBorderedHeaderLable(string str)
        {
            Console.WriteLine(ReturnBorderedHeaderLable(str));
        }

        private string SERV_ReturnLine(char ch, int lenght)
        {
            string output = "";
            for (int i = 0; i < lenght; i++)
            {
                output+=ch.ToString();
            }
            return output;
        }

        private int SERV_GetSymbolIndex(char symbol)
        {
            for (int i = 0; i < symbols.Count; i++)
            {
                if (symbols[i].ch == symbol)
                {
                    return i;
                }
            }
            return 0; 
        }
    }
}