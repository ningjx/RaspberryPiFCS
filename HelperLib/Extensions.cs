using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLib
{
    public static class Extensions
    {
        /// <summary>
        /// 获取字符串数组的指定数量的子集
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string[] GetByCount(this string[] data,int offset, int count)
        {
            string[] result = new string[count- offset];
            for (int i = offset; i < count; i++)
            {
                result[i- offset] = data[i];
            }
            return result;
        }
    }
}
