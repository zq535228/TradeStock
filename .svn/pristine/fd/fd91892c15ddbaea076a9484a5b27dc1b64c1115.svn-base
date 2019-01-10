using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.jiechan.service {
    public class ArrayTool<T> {


        /// <summary>查询值在数组中的下标</summary>
        /// <param name="array">数组</param>
        /// <param name="param">查询的参数</param>
        /// <returns>下标</returns>
        public static int ArrayQueryIndex(T[] array, T param) {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(param))
                    return i;
            return 0;
        }

        /// <summary>查询值在数组中的下标</summary>
        /// <param name="array">数组</param>
        /// <param name="param">查询的参数</param>
        /// <returns>下标</returns>
        public static int ArrayQueryIndex2(string[] array, string param) {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Contains(param))
                    return i;
            return 0;
        }

        /// <summary>根据下标拆分出新数组</summary>
        /// <param name="array">原数组</param>
        /// <param name="EndIndex">结束下标</param>
        /// <param name="StartIndex">开始下标</param>
        /// <returns>新数组</returns>
        public static T[] NewArrayByIndex(T[] array, int EndIndex, int StartIndex) {
            T[] result = new T[EndIndex - StartIndex + 1];
            for (int i = 0; i <= EndIndex - StartIndex; i++)
                result[i] = array[i + StartIndex];
            return result;
        }

    }
}
