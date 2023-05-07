using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualFrameworkLibrary
{
    internal class BorderConfig
    {
        internal int borderXHeight;
        internal int borderYHeight;
        internal string borderUpLeft;
        internal string borderUpRight;
        internal string borderDownLeft;
        internal string borderDownRight;
        internal string borderUpper;
        internal string borderLower;
        internal string borderRight;
        internal string borderLeft;

        internal BorderConfig(string inputData)
        {
            string[] split1 = inputData.Split('\n');
            for (int i = 0; i < split1.Length; i++)
            {
                //Console.WriteLine("i = " + i + ": " + split1[i].Trim());
                split1[i] = split1[i].Trim();
                if (split1[i].Contains("borderXHeight"))
                {
                    borderXHeight = Convert.ToInt32(split1[i].Split(':')[1].Trim());
                }
                if (split1[i].Contains("borderYHeight"))
                {
                    borderYHeight = Convert.ToInt32(split1[i].Split(':')[1].Trim());
                }
                if (split1[i].Contains("borderUpLeft"))
                {
                    borderUpLeft = split1[i].Split(':')[1].Trim();
                }
                if (split1[i].Contains("borderUpRight"))
                {
                    borderUpRight = split1[i].Split(':')[1].Trim();
                }
                if (split1[i].Contains("borderDownLeft"))
                {
                    borderDownLeft = split1[i].Split(':')[1].Trim();
                }
                if (split1[i].Contains("borderDownRight"))
                {
                    borderDownRight = split1[i].Split(':')[1].Trim();
                }
                if (split1[i].Contains("borderUpper"))
                {
                    borderUpper = split1[i].Split(':')[1].Trim();
                }
                if (split1[i].Contains("borderLower"))
                {
                    borderLower = split1[i].Split(':')[1].Trim();
                }
                if (split1[i].Contains("borderRight"))
                {
                    borderRight = split1[i].Split(':')[1].Trim();
                }
                if (split1[i].Contains("borderLeft"))
                {
                    borderLeft = split1[i].Split(':')[1].Trim();
                }
            }
            /*Console.WriteLine(borderXHeight);
            Console.WriteLine(borderYHeight);
            Console.WriteLine(borderUpLeft);
            Console.WriteLine(borderUpRight);
            Console.WriteLine(borderDownLeft);
            Console.WriteLine(borderDownRight);
            Console.WriteLine(borderUpper);
            Console.WriteLine(borderLower);
            Console.WriteLine(borderRight);
            Console.WriteLine(borderLeft);*/
        }
    }
}
