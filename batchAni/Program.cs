using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace batchAni
{
    class Program
    {
        static void Main(string[] args)
        {
            string originJson = args[0];
            string frameRate = args[1];
            //string originJson = @"C:\Users\develop\Desktop\文丑\跑\test\bb.json";
            string text = System.IO.File.ReadAllText(originJson);

            string resultJson = JsonInfoGetter.handleJson(text, frameRate);
            System.IO.File.WriteAllText(originJson, resultJson);
        }
    }
}
