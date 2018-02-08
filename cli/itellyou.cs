using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cli
{
    class itellyou
    {
        public static void Start(string id,string name)
        {
            {
                var arry = GetIndex(id)["arry"];//读取总列表
                DirectoryInfo dir = new DirectoryInfo(name);
                if (!dir.Exists)//如果目录不存在，则创建
                {
                    dir.Create();
                }

                for (int a = 0; a < arry.Count(); a++)
                {
                    //Console.WriteLine(arry[a].ToString());
                    string indexID = arry[a]["id"].ToString();//总列表项目id
                    string indexName = CheckString(arry[a]["name"].ToString());//总列表项目
                    dir = new DirectoryInfo(name + "/" + indexName);//根据项目名创建目录
                    if (!dir.Exists)//如果目录不存在，则创建
                    {
                        dir.Create();
                    }

                    var langArry = GetLang(indexID)["result"];//读取项目语言列表

                    for (int b = 0; b < langArry.Count(); b++)
                    {
                        //Console.WriteLine(langArry[b].ToString());
                        string langID = langArry[b]["id"].ToString();//语言id
                        string langName = CheckString(langArry[b]["lang"].ToString());//语言名称

                        dir = new DirectoryInfo(name + "/" + indexName + "/" + langName);//根据语言名创建目录
                        if (!dir.Exists)//如果目录不存在，则创建
                        {
                            dir.Create();
                        }

                        var listArry = GetList(indexID, langID)["result"];//根据项目与语言获取产品列表

                        for (int c = 0; c < listArry.Count(); c++)
                        {
                            //Console.WriteLine(listArry[c].ToString());
                            string listID = listArry[c]["id"].ToString();//产品id
                            string listName = CheckString(listArry[c]["name"].ToString());//产品名称


                            //foreach (var item in Path.GetInvalidFileNameChars())
                            //{
                            //    listName = listName.Replace(item, ' ');//剔除非法字符
                            //}

                            //foreach (var item in Path.GetInvalidPathChars())
                            //{
                            //    listName = listName.Replace(item, ' ');//剔除非法字符
                            //}

                            var productArry = GetProduct(listID)["result"];

                            string fileContent = "";//要写入的内容
                            fileContent += listName + "\n\n";
                            fileContent += "文件名称 ： " + productArry["FileName"].ToString() + "\n";
                            fileContent += "下载地址 ： " + productArry["DownLoad"].ToString() + "\n";
                            fileContent += "提交时间 ： " + productArry["PostDateString"].ToString() + "\n";
                            fileContent += "SHA1校验 ： " + productArry["SHA1"].ToString() + "\n";
                            fileContent += "文件大小 ： " + productArry["size"].ToString();

                            Console.WriteLine(fileContent + "\n");

                            FileStream file = new FileStream(name + "/" + indexName + "/" + langName + "/" + productArry["FileName"].ToString() + ".txt", FileMode.Create);//输出至txt文件
                            StreamWriter writer = new StreamWriter(file);
                            writer.Write(fileContent);
                            writer.Flush();
                            writer.Close();
                            file.Close();
                        }
                    }
                }
            }
        }

        public static string CheckString(string str)//过滤非法字符
        {
            foreach (var item in Path.GetInvalidFileNameChars())
            {
                str = str.Replace(item, ' ');//文件名非法字符
            }

            foreach (var item in Path.GetInvalidPathChars())
            {
                str = str.Replace(item, ' ');//路径非法字符
            }

            return str;
        }

        public static JObject GetIndex(string id)
        {
            //根据项目ID读取列表,返回项目名称与语言ID
            string url = "https://msdn.itellyou.cn/Category/Index";
            string output = "{\"arry\":"+HttpPost(url, "id=" + id)+"}";//默认返回一个数组而不是键值对,有点迷，于是手动加上
            return JsonConvert.DeserializeObject(output) as JObject;
        }

        public static JObject GetLang(string id)
        {
            //根据语言ID读取产品的语言列表
            string url = "https://msdn.itellyou.cn/Category/GetLang";
            string output = HttpPost(url, "id=" + id);
            return JsonConvert.DeserializeObject(output) as JObject;
        }

        public static JObject GetList(string productId,string langId)
        {
            //根据产品ID和语言ID读取列表
            string url = "https://msdn.itellyou.cn/Category/GetList";
            string output = HttpPost(url, "id=" + productId + "&lang=" + langId + "&filter=true");
            return JsonConvert.DeserializeObject(output) as JObject;
        }

        public static JObject GetProduct(string id)
        {
            //根据产品ID读取详细信息
            string url = "https://msdn.itellyou.cn/Category/GetProduct";
            string output = HttpPost(url, "id=" + id );
            return JsonConvert.DeserializeObject(output) as JObject;
        }

        public static JObject Search(string keyword)
        {
            //搜索
            string url = "https://msdn.itellyou.cn/Category/Search";
            string output = HttpPost(url, "keyword=" + keyword.Replace(" ","+") + "&filter=true");//空格替换成"+"
            return JsonConvert.DeserializeObject(output) as JObject;
        }

        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            request.Referer = "https://msdn.itellyou.cn/";//反反爬虫
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }
    }
}
