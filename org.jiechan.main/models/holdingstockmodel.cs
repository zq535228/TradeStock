using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.jiechan.main.models {
    public class holdingstockmodel {

        public int id { get; set; }
        /// <summary>
        /// 持有股票
        /// </summary>
        public stockmodel stock { get; set; }

        /// <summary>
        /// 持有数量
        /// </summary>
        public int num { get; set; }

        /// <summary>
        /// 可用余额
        /// </summary>
        public int usablenum { get; set; }

        /// <summary>
        /// 参考盈亏
        /// </summary>
        public float profit { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        public int coast { get; set; }

        /// <summary>
        /// 盈亏比率
        /// </summary>
        public float profitrate { get; set; }

        /// <summary>
        /// 市值
        /// </summary>
        public float marketvalue {
            get {
                return this.stock.price * this.num;
            }
        }



    }
}
