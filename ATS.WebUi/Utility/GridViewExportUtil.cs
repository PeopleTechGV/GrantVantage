using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
//using Excel = Microsoft.Office.Interop.Excel;

namespace ATS.WebUi.Utility
{
    public class GridViewExportUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="gv"></param>
        /// 

        public static void Export(string fileName, GridView gv, string title, List<string> gvHeader)
        {

            //StringBuilder builder = new StringBuilder();
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.Buffer = true;

            //HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            //HttpContext.Current.Response.ContentType = "application/ms-excel";

            //HttpContext.Current.Response.Charset = "";
            //HttpContext.Current.Response.Buffer = true;

            //using (StringWriter sw = new StringWriter())
            //{
            //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            //    {
            //        //NET CODE
            //        Table table = new Table();

            //        gv.GridLines = GridLines.None;
            //        table.GridLines = gv.GridLines;
            //        int keyColumnPosition = -1;

            //        if (gv.HeaderRow != null)
            //        {
            //            //GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
            //            //table.Rows.Add(gv.HeaderRow);
            //            TableRow mytr = new TableRow();
            //            TableRow mytr1 = new TableRow();
            //            int trCount = gv.HeaderRow.Cells.Count;
            //            //foreach (TableCell tc in gv.HeaderRow.Cells)

            //            TableCell mytc1 = new TableCell();
            //            mytc1.Text = title;
            //            mytc1.ColumnSpan = trCount;
            //            mytc1.HorizontalAlign = HorizontalAlign.Center;
            //            mytr1.Cells.Add(mytc1);
            //            table.Rows.Add(mytr1);
            //            string cap = string.Empty;
            //            for (int cnt = 0; cnt < trCount; cnt++)
            //            {
            //                if (gvHeader != null && trCount == gvHeader.Count)
            //                {
            //                    cap = gvHeader[cnt];//gv.HeaderRow.Cells[cnt].Text;
            //                }
            //                else
            //                { cap = gv.HeaderRow.Cells[cnt].Text; }
            //                TableCell mytc = new TableCell();
            //                mytc.Text = cap;
            //                mytc.HorizontalAlign = HorizontalAlign.Center;
            //                mytr.Cells.Add(mytc);
            //            }

            //            table.Rows.Add(mytr);

            //            //color the header
            //            gv.HeaderStyle.ForeColor = Color.DarkRed;
            //            table.Rows[0].BackColor = gv.HeaderStyle.BackColor;
            //            table.Rows[0].ForeColor = gv.HeaderStyle.ForeColor;
            //        }

            //        //  add each of the data rows to the table
            //        foreach (GridViewRow row in gv.Rows)
            //        {
            //            if (row.Visible == true)
            //            {
            //                //GridViewExportUtil.PrepareControlForExport(row);
            //                //table.Rows.Add(row);
            //                //TableRow tr = new TableRow();
            //                //foreach (TableCell tc in row.Cells)
            //                //{
            //                //    if (tc.Visible == true)
            //                //    {
            //                //        tr.Cells.Add(tc);
            //                //    }
            //                //}
            //                //table.Rows.Add(tr);

            //                TableRow mytr1 = new TableRow();
            //                int trCount1 = row.Cells.Count;
            //                //foreach (TableCell tc in gv.HeaderRow.Cells)
            //                for (int cnt1 = 0; cnt1 < trCount1; cnt1++)
            //                {
            //                    string cap1 = row.Cells[cnt1].Text;
            //                    TableCell mytc1 = new TableCell();
            //                    mytc1.Text = cap1;
            //                    if (cnt1 != keyColumnPosition)
            //                    {
            //                        mytr1.Cells.Add(mytc1);
            //                    }
            //                }
            //                table.Rows.Add(mytr1);
            //            }
            //        }

            //        //  color the rows
            //        bool altColor = false;
            //        for (int i = 1; i < table.Rows.Count; i++)
            //        {
            //            if (!altColor)
            //            {
            //                table.Rows[i].BackColor = gv.RowStyle.BackColor;
            //                altColor = true;
            //            }
            //            else
            //            {
            //                table.Rows[i].BackColor = gv.AlternatingRowStyle.BackColor;
            //                altColor = false;
            //            }
            //        }

            //        //  render the table into the htmlwriter
            //        table.RenderControl(htw);

            //        //  render the htmlwriter into the response
            //        HttpContext.Current.Response.Write(sw.ToString());
            //        HttpContext.Current.Response.Flush();
            //        HttpContext.Current.Response.End();
            //    }
            //}

            //#region unusedcode
            ////END NET CODE

            //// add the header row to the table
            ////        if (gv.HeaderRow != null)
            ////        {

            ////           GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);

            ////           table.Rows.Add(gv.HeaderRow);
            ////           //htw.write(gv.headerrow.cells.tostring());

            ////           HttpContext.Current.Response.Write("\t");
            ////           HttpContext.Current.Response.Write(sw.ToString());
            ////           builder = sw.GetStringBuilder();
            ////           builder.Clear();



            ////            //for (int var = 0; var < gv.Columns.Count; var++)
            ////            //{
            ////            //    GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
            ////            //    table.Rows.Add(gv.HeaderRow);
            ////            //    HttpContext.Current.Response.Write(gv.HeaderRow.Cells[var].Text);
            ////            //    HttpContext.Current.Response.Write("\t");
            ////            //}

            ////        }

            ////        //  add each of the data rows to the table
            ////        //HttpContext.Current.Response.Write("\n");


            ////        //for (int i = 0; i < gv.Rows.Count; i++)
            ////        //{
            ////        //    for (int j = 0; j <= gv.Columns.Count; j++)
            ////        //    //GridViewExportUtil.PrepareControlForExport(row);
            ////        //    {

            ////        //        htw.Write(gv.Rows[i].Cells[j].Text);
            ////        //        HttpContext.Current.Response.Write("\t");
            ////        //        HttpContext.Current.Response.Write(sw.ToString());
            ////        //        builder = sw.GetStringBuilder();
            ////        //        builder.Clear();

            ////        //    }
            ////        //    HttpContext.Current.Response.Write("\n");

            ////        //}

            ////        if (gv.FooterRow != null)
            ////        {
            ////            //GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
            ////            //table.Rows.Add(gv.FooterRow);
            ////        }


            ////    }
            ////}
            ////HttpContext.Current.Response.End();

            //#endregion
        }


        private static void PrepareControlForExport(Control control)
        {
            //for (int i = 0; i < control.Controls.Count; i++)
            //{
            //    Control current = control.Controls[i];
            //    if (current is LinkButton)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            //    }
            //    else if (current is ImageButton)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            //    }
            //    else if (current is HyperLink)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            //    }
            //    else if (current is DropDownList)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            //    }
            //    else if (current is CheckBox)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            //    }

            //    if (current.HasControls())
            //    {
            //        GridViewExportUtil.PrepareControlForExport(current);
            //    }
            //}
        }



        public static void AddBoundFields(GridView gvCustomerList, string DataField)
        {
            //BoundField BF = new BoundField();
            //BF.DataField = DataField;
            //// BF.HeaderText = HeaderText;
            //gvCustomerList.Columns.Add(BF);


        }
        public static void Export1(GridView gv)
        {

            //Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

            //Microsoft.Office.Interop.Excel._Worksheet worksheet = null;



            //// see the excel sheet behind the program

            //app.Visible = true;



            //// get the reference of first sheet. By default its name is Sheet1.

            //// store its reference to worksheet

            //worksheet = workbook.Sheets["Sheet1"];

            //worksheet = workbook.ActiveSheet;
            //for (int i = 1; i < gv.Rows[0].Cells.Count; i++)
            //{

            //    //worksheet.Cells[1, i] = gv.Rows[0].Cells[i - 1].Text;
            //    worksheet.Cells[1, i] = gv.HeaderRow.Cells[i].Text;

            //}







            //// storing Each row and column value to excel sheet

            //for (int i = 0; i < gv.Rows.Count; i++)
            //{

            //    for (int j = 0; j < gv.Rows[0].Cells.Count; j++)
            //    {

            //        worksheet.Cells[i + 2, j + 1] = gv.Rows[i].Cells[j].Text;

            //    }

            //}

            //// save the application

            //workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);



            //// Exit from the application

            //app.Quit();





            ////HttpContext.Current.Response.Clear();
            ////HttpContext.Current.Response.ClearContent();
            ////HttpContext.Current.Response.Buffer = true;

            ////HttpContext.Current.Response.AddHeader(
            ////    "content-disposition", string.Format("attachment; filename={0}", fileName));
            ////HttpContext.Current.Response.ContentType = "application/ms-excel";

            ////HttpContext.Current.Response.Charset = "";
            ////HttpContext.Current.Response.Buffer = true;

            ////using (StringWriter sw = new StringWriter())
            ////{
            ////    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            ////    {
            ////        //  Create a form to contain the grid
            ////        Table table = new Table();

            ////        //  add the header row to the table
            ////        if (gv.HeaderRow != null)
            ////        {
            ////            GridViewExportUtil.PrepareControlForExport1(gv.HeaderRow);
            ////            table.Rows.Add(gv.HeaderRow);
            ////        }

            ////        //  add each of the data rows to the table
            ////        foreach (GridViewRow row in gv.Rows)
            ////        {
            ////            GridViewExportUtil.PrepareControlForExport1(row);
            ////            table.Rows.Add(row);
            ////        }

            ////        //  add the footer row to the table
            ////        if (gv.FooterRow != null)
            ////        {
            ////            GridViewExportUtil.PrepareControlForExport1(gv.FooterRow);
            ////            table.Rows.Add(gv.FooterRow);
            ////        }

            ////        //  render the table into the htmlwriter
            ////        table.RenderControl(htw);

            ////        //  render the htmlwriter into the response
            ////        HttpContext.Current.Response.Write(sw.ToString());
            ////        HttpContext.Current.Response.End();
            ////    }
            ////}
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport1(Control control)
        {
            //for (int i = 0; i < control.Controls.Count; i++)
            //{
            //    Control current = control.Controls[i];
            //    if (current is LinkButton)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            //    }
            //    else if (current is ImageButton)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            //    }
            //    else if (current is HyperLink)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            //    }
            //    else if (current is DropDownList)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            //    }
            //    else if (current is CheckBox)
            //    {
            //        control.Controls.Remove(current);
            //        control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            //    }

            //    if (current.HasControls())
            //    {
            //        GridViewExportUtil.PrepareControlForExport(current);
            //    }
            //}
        }



        public static void ExportForSQL(string FileName, DataSet gv)
        {

            //FileStream ft = new FileStream("D:\\" + FileName, FileMode.Create, FileAccess.Write);
            //StreamWriter sr = new StreamWriter(ft);
            //foreach (DataRow dr in gv.Tables[0].Rows)
            //{
            //    sr.WriteLine(dr.ItemArray[0]);
            //}
            //sr.Close();
            //ft.Close();

            //DirectoryInfo di = new DirectoryInfo("D:\\");
            //FileInfo[] File = di.GetFiles();

            //FileStream fs = null;
            //fs = System.IO.File.Open("D:\\" + FileName, System.IO.FileMode.Open);
            //byte[] btFile = new byte[fs.Length];
            //fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            //fs.Close();
            //HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" + FileName);
            //HttpContext.Current.Response.ContentType = "application/octet-stream";
            //HttpContext.Current.Response.BinaryWrite(btFile);
            //HttpContext.Current.Response.End();
            ////fi.Delete();
            //System.IO.File.Delete("D:\\" + FileName);

        }
    }
}