using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codeplex.Data;

namespace batchAni
{
    class origData {
        public double x;
        public double y;
        public double w;
        public double h;
        public double offX;
        public double offY;
        public double sourceW;
        public double sourceH;

        public string frameNm;
    }


    class JsonInfoGetter
    {
        private static List<origData> datas= new List<origData>();
        public static string[] FRAME_NAMES = {
            "FRAME1",
            "FRAME2",
            "FRAME3",
            "FRAME4",
            "FRAME5",
            "FRAME6",
            "FRAME7",
            "FRAME8",
            "FRAME9",
            "FRAME10",
            "FRAME11",
            "FRAME12",
            "FRAME13",
            "FRAME14",
            "FRAME15",
            "FRAME16",
            "FRAME17",
            "FRAME18",
            "FRAME19",
            "FRAME20",
            "FRAME21",
            "FRAME22",
            "FRAME23",
            "FRAME24",
            "FRAME25"
        };

        public static void app(ref string instr, string newstr) {
            instr += newstr;
            instr += "\n";
        }

        public static string handleJson(string jsonContent, string frameRate) {
            var json = DynamicJson.Parse(jsonContent);
            var frames = json["frames"];
            var jsonNm = json["file"];//aa.png
            string nm = (string)jsonNm;
            nm = nm.Split('.')[0];

            int i = 0;
            foreach (KeyValuePair<string, dynamic> item in frames)
            {
                dynamic frame = item.Value;
                origData data = new origData();
                data.frameNm = FRAME_NAMES[i++];
                data.x = frame["x"];
                data.y = frame["y"];
                data.w = frame["w"];
                data.h = frame["h"];
                data.offX = frame["offX"];
                data.offY = frame["offY"];
                data.sourceW = frame["sourceW"];
                data.sourceH = frame["sourceH"];

                datas.Add(data);
            }

            string ret = "";
            app(ref ret, "{'mc':{");
            app(ref ret, "	'" + nm + "':{");
            app(ref ret, "		'frameRate':"+ frameRate + ",");
            app(ref ret, "		'frames':[");
            //			{			'res':'x1',			'x':2,			'y':1			},
            int j = 0, all = datas.Count;
            foreach (origData tmp in datas) {
                j++;
                string t1 = "			{'res':'" + tmp.frameNm+"',	'x':"+ tmp.offX+ ",	'y':"+tmp.offY+"}";
                if (j != all) {
                    t1 += ",";
                }
                app(ref ret, t1);
            }
            //

            app(ref ret, "			]");
            app(ref ret, "	}},");
            app(ref ret, "	'res':{");
            //'x1':{'x':1,'y':156,'w':154,'h':140},
            j = 0;
            all = datas.Count;
            foreach (origData tmp in datas)
            {
                j++;
                string t1 = "		'" + tmp.frameNm+"':{'x':"+tmp.x+",'y':"+tmp.y+",'w':"+tmp.w+",'h':"+tmp.h+"}";
                if (j != all)
                {
                    t1 += ",";
                }
                app(ref ret, t1);
            }
            //

            app(ref ret, "	}}");
            ret = ret.Replace('\'','"');
            return ret;
        }

        //
        public static void getFirstFrameXY(string jsonContent, out int x, out int y)
        {
            var json = DynamicJson.Parse(jsonContent);
            var node = json["mc"];
            dynamic firstNode = null;
            foreach (KeyValuePair<string, dynamic> item in node)
            {
                //Console.WriteLine(item.Key + ":" + item.Value);
                firstNode = item.Value;
                break;
            }

            var frames = firstNode["frames"];
            var frame = frames[0];
            x = (int)frame["x"];
            y = (int)frame["y"];
            //foreach (dynamic item in frames) {
            //    return item["x"];
            //}
            //Logger.debug("failed when getFirstFrameX.");
            //return 0;
        }

        public static string modifyJson(int dltX, int dltY, string text)
        {
            var json = DynamicJson.Parse(text);
            var node = json["mc"];
            dynamic firstNode = null;
            foreach (KeyValuePair<string, dynamic> item in node)
            {
                //Console.WriteLine(item.Key + ":" + item.Value);
                firstNode = item.Value;
                break;
            }

            var frames = firstNode["frames"];
            foreach (dynamic frame in frames) {
                frame["x"] -= dltX;
                frame["y"] -= dltY;
            }

            var jsonstring = json.ToString();
            return jsonstring;
        }
    }
}
