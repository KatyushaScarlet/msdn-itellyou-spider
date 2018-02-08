using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;

namespace cli
{
    class Program
    {
        static void Main(string[] args)
        {
            /*  以下uid均可在 msdn.itellyou.cn官网找到
             *  
             *  企业解决方案      aff8a80f-2dee-4bba-80ec-611ac56d3849
             *  MSDN 技术资源库   23958de6-bedb-4998-825c-aa3d1e00d097
             *  工具和资源        95c4acfd-d1a6-41fe-b14d-a6816973d2aa
             *  应用程序          051d75ee-ff53-43fe-80e9-bac5c10fc0fb
             *  开发人员工具      fcf12b78-0662-4dd4-9a82-72040db91c9e
             *  操作系统          7ab5f0cb-7607-4bbe-9e88-50716dc43de6
             *  服务器            36d3766e-0efb-491e-961b-d1a419e06c68
             *  设计人员工具      5d6967f0-b58d-4385-8769-b886bfc2b78c
             */

            itellyou.Start("aff8a80f-2dee-4bba-80ec-611ac56d3849", "企业解决方案");
            itellyou.Start("23958de6-bedb-4998-825c-aa3d1e00d097", "MSDN 技术资源库");
            itellyou.Start("95c4acfd-d1a6-41fe-b14d-a6816973d2aa", "工具和资源");
            itellyou.Start("051d75ee-ff53-43fe-80e9-bac5c10fc0fb", "应用程序");
            itellyou.Start("fcf12b78-0662-4dd4-9a82-72040db91c9e", "开发人员工具");
            itellyou.Start("7ab5f0cb-7607-4bbe-9e88-50716dc43de6", "操作系统");
            itellyou.Start("36d3766e-0efb-491e-961b-d1a419e06c68", "服务器");
            itellyou.Start("5d6967f0-b58d-4385-8769-b886bfc2b78c", "设计人员工具");

            Console.WriteLine("爬取完成！");
            Console.ReadKey();
        }
    }
}