using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.jiechan.main.core;

namespace org.jiechan.main.models {
    public class taskmodel {

        public taskmodel() {

            buy000001 = sinahq.GetInstance().marketindex("s_sh000001").price;
            buy399001 = sinahq.GetInstance().marketindex("s_sz399001").price;
            buy399006 = sinahq.GetInstance().marketindex("s_sz399006").price;
            buy000001bool = true;
            buy399001bool = false;
            buy399006bool = false;
            optpercent =1;

        }

        public int id { get; set; }

        /// <summary>
        /// 股票名称
        /// </summary>
        public stockmodel stock { get; set; }

        /// <summary>
        /// 仓位比例
        /// </summary>
        public float optpercent { get; set; }

        /// <summary>
        /// 股票数量
        /// </summary>
        public int optnum1 { get; set; }
        /// <summary>
        /// 股票数量
        /// </summary>
        public int optnum2 { get; set; }
        /// <summary>
        /// 股票数量
        /// </summary>
        public int optnum3 { get; set; }
        /// <summary>
        /// 股票数量
        /// </summary>
        public int optnum4 { get; set; }


        /// <summary>
        /// 买入价
        /// </summary>
        public float buyprice { get; set; }


        public float sellprice { get; set; }

        /// <summary>
        /// 买入的比较符号
        /// </summary>
        public char buycompare { get; set; }

        public float buy000001 { get; set; }
        public float buy399001 { get; set; }
        public float buy399006 { get; set; }
        public bool buy000001bool { get; set; }
        public bool buy399001bool { get; set; }
        public bool buy399006bool { get; set; }

        /// <summary>
        /// 策略，为什么这样执行。
        /// </summary>
        public string whydothis { get; set; }

        public taskstatus status { get; set; }

        public taskbuyorsell buyorsell { get; set; }

        public taskplan plan { get; set; }

        public tasksign sign { set; get; }


    }

    public enum tasksign {
        任意一条,
        全部满足,
    }

    public enum taskstatus {
        已停止,
        运行中,
    }

    public enum taskbuyorsell {
        买入,
        卖出,
    }

    public enum taskplan {
        单点,
        大盘,
        勾头,
        区间,
    }

}
