using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace org.jiechan.service {
    public class ListViewHelper {
        /// <summary>
        /// 撑大LV的行高
        /// </summary>
        public static void listViewHeight(ListView lv, int height) {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);//分别是宽和高
            lv.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大
        }

    }
}
