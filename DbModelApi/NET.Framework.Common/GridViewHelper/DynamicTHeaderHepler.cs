using System.Linq;
using System.Web.UI.WebControls;

namespace NET.Framework.Common.GridViewHelper
{
    //
    //***********************************************************************
    //  Created: 2007-10-29    Author:  ruijc
    //  File: DynamicTHeaderHepler.cs
    //  Description: 动态生成复合表头帮助类
    //  相邻父列头之间用'#'分隔,父列头与子列头用空格(' ')分隔,相邻子列头用逗号分隔(',').
    //***********************************************************************
    public class DynamicTHeaderHepler
    {
        /**/

        /// <summary>
        ///     重写表头
        /// </summary>
        /// <param name="targetHeader">目标表头</param>
        /// <param name="newHeaderNames">新表头</param>
        /// <remarks>
        ///     等级#级别#上期结存 件数,重量,比例#本期调入 收购调入 件数,重量,比例#本期发出 车间投料 件数,重量,
        ///     比例#本期发出 产品外销百分比 件数,重量,比例#平均值
        /// </remarks>
        public void SplitTableHeader(GridViewRow targetHeader, string newHeaderNames)
        {
            TableCellCollection tcl = targetHeader.Cells; //获得表头元素的实例
            tcl.Clear(); //清除元素
            int row = GetRowCount(newHeaderNames);
            int col = GetColCount(newHeaderNames);
            string[,] nameList = ConvertList(newHeaderNames, row, col);
            for (int k = 0; k < row; k++)
            {
                string lastFName = "";
                for (int i = 0; i < col; i++)
                {
                    if (lastFName == nameList[i, k] && k != row - 1)
                    {
                        lastFName = nameList[i, k];
                        continue;
                    }
                    lastFName = nameList[i, k];
                    int bFlag = IsVisible(nameList, k, i, lastFName);
                    switch (bFlag)
                    {
                        case 0:
                            break;
                        case 1:
                            int rowSpan = GetSpanRowCount(nameList, row, k, i);
                            int colSpan = GetSpanColCount(nameList, row, col, k, i);
                            tcl.Add(new TableHeaderCell()); //添加表头控件
                            tcl[tcl.Count - 1].RowSpan = rowSpan;
                            tcl[tcl.Count - 1].ColumnSpan = colSpan;
                            tcl[tcl.Count - 1].HorizontalAlign = HorizontalAlign.Center;
                            tcl[tcl.Count - 1].Text = lastFName;
                            break;
                        case -1:
                            string[] endColName = lastFName.Split(new[] {','});
                            foreach (string eName in endColName)
                            {
                                tcl.Add(new TableHeaderCell()); //添加表头控件
                                tcl[tcl.Count - 1].HorizontalAlign = HorizontalAlign.Center;
                                tcl[tcl.Count - 1].Text = eName;
                            }
                            break;
                    }
                }
                if (k != row - 1)
                {
                    //不是起始行,加入新行标签
                    tcl[tcl.Count - 1].Text = tcl[tcl.Count - 1].Text + @"</th></tr><tr>";
                }
            }
        }

        /**/

        /// <summary>
        ///     如果上一行已经输出和当前内容相同的列头，则不显示
        /// </summary>
        /// <param name="columnList">表头集合</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="currName"></param>
        /// <returns>1:显示,-1:含','分隔符,0:不显示</returns>
        private int IsVisible(string[,] columnList, int rowIndex, int colIndex, string currName)
        {
            if (rowIndex != 0)
            {
                if (columnList[colIndex, rowIndex - 1] == currName)
                {
                    return 0;
                }
                if (columnList[colIndex, rowIndex].Contains(","))
                {
                    return -1;
                }
                return 1;
            }
            return 1;
        }

        /**/

        /// <summary>
        ///     取得和当前索引行及列对应的下级的内容所跨的行数
        /// </summary>
        /// <param name="columnList">表头集合</param>
        /// <param name="row">行数</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="colIndex">列索引</param>
        /// <returns>行数</returns>
        private int GetSpanRowCount(string[,] columnList, int row, int rowIndex, int colIndex)
        {
            string lastName = "";
            int rowSpan = 1;
            for (int k = rowIndex; k < row; k++)
            {
                if (columnList[colIndex, k] == lastName)
                {
                    rowSpan++;
                }
                else
                {
                    lastName = columnList[colIndex, k];
                }
            }
            return rowSpan;
        }

        /**/

        /// <summary>
        ///     取得和当前索引行及列对应的下级的内容所跨的列数
        /// </summary>
        /// <param name="columnList">表头集合</param>
        /// <param name="row">行数</param>
        /// <param name="col">列数</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="colIndex">列索引</param>
        /// <returns>列数</returns>
        private int GetSpanColCount(string[,] columnList, int row, int col, int rowIndex, int colIndex)
        {
            string lastName = columnList[colIndex, rowIndex];
            int colSpan = columnList[colIndex, row - 1].Split(new[] {','}).Length;
            colSpan = colSpan == 1 ? 0 : colSpan;
            for (int i = colIndex + 1; i < col; i++)
            {
                if (columnList[i, rowIndex] == lastName)
                {
                    colSpan += columnList[i, row - 1].Split(new[] {','}).Length;
                }
                else
                {
                    lastName = columnList[i, rowIndex];
                    break;
                }
            }
            return colSpan;
        }

        /**/

        /// <summary>
        ///     将已定义的表头保存到数组
        /// </summary>
        /// <param name="newHeaders">新表头</param>
        /// <param name="row">行数</param>
        /// <param name="col">列数</param>
        /// <returns>表头数组</returns>
        private string[,] ConvertList(string newHeaders, int row, int col)
        {
            string[] columnNames = newHeaders.Split(new[] {'#'});
            var news = new string[col, row];
            string name = "";
            for (int i = 0; i < col; i++)
            {
                string[] currColNames = columnNames[i].Split(new[] {' '});
                for (int k = 0; k < row; k++)
                {
                    if (currColNames.Length - 1 >= k)
                    {
                        if (currColNames[k].Contains(","))
                        {
                            if (currColNames.Length != row)
                            {
                                if (name == "")
                                {
                                    news[i, k] = news[i, k - 1];
                                    name = currColNames[k];
                                }
                                else
                                {
                                    news[i, k + 1] = name;
                                    name = "";
                                }
                            }
                            else
                            {
                                news[i, k] = currColNames[k];
                            }
                        }
                        else
                        {
                            news[i, k] = currColNames[k];
                        }
                    }
                    else
                    {
                        if (name == "")
                        {
                            news[i, k] = news[i, k - 1];
                        }
                        else
                        {
                            news[i, k] = name;
                            name = "";
                        }
                    }
                }
            }
            return news;
        }

        /**/

        /// <summary>
        ///     取得复合表头的行数
        /// </summary>
        /// <param name="newHeaders">新表头</param>
        /// <returns>行数</returns>
        private int GetRowCount(string newHeaders)
        {
            string[] columnNames = newHeaders.Split(new[] {'#'});
            return columnNames.Select(name => name.Split(new[] {' '}).Length).Concat(new[] {0}).Max();
        }

        /**/

        /// <summary>
        ///     取得复合表头的列数
        /// </summary>
        /// <param name="newHeaders">新表头</param>
        /// <returns>列数</returns>
        private int GetColCount(string newHeaders)
        {
            return newHeaders.Split(new[] {'#'}).Length;
        }
    }
}