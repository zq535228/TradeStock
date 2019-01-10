using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace org.renzhe.plat.service {
    public class MstscHelper {

        public static void Connection(string accountDomain, string ipAddress, string accountUserName, string accountPassword) {
            var rdcProcess = new Process {
                StartInfo = {
                    FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\cmdkey.exe"),
                    Arguments = String.Format(@"/generic:TERMSRV/{0} /user:{1} /pass:{2}",
                                ipAddress, accountUserName, accountPassword),
                    WindowStyle = ProcessWindowStyle.Hidden


                }
            };
            rdcProcess.Start();
            rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
            rdcProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            rdcProcess.StartInfo.Arguments = String.Format("/f /v {0}", ipAddress); // ip or name of computer to connect
            rdcProcess.Start();


        }


        public static void Connection2(string accountDomain, string ipAddress, string accountUserName, string accountPassword) {
            Process rdcProcess = new Process();
            rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\cmdkey.exe");
            rdcProcess.StartInfo.Arguments = "/generic:TERMSRV/" + ipAddress + " /user:" + accountUserName + " /pass:" + accountPassword;
            rdcProcess.Start();

            rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
            rdcProcess.StartInfo.Arguments = "/v " + ipAddress; // ip or name of computer to connect
            rdcProcess.Start();

        }
    }
}
