using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;
using DevExpress.XtraPivotGrid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors.Repository;
using FastReport;
using DevExpress.Utils;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Menu;
using DevExpress.Data.Filtering;
using DevExpress.Utils.Win;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning.PlanningProtocolCapture;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolCapture
{


    public partial class ucPlanProtDataView : ucBaseUserControl 
    {
        ucHRPlanning ucHRPlanning;
     public    DataTable mainData = new DataTable();
        DataTable errorListData = new DataTable();

        PivotGridField theValue = new PivotGridField();
        PivotGridField FieldName = new PivotGridField();
        PivotGridField ApprovedField = new PivotGridField();
        PivotGridField WorkplaceIDField = new PivotGridField();
        PivotGridField WorkplaceField = new PivotGridField();
        PivotGridField OrgDayField = new PivotGridField();
        PivotGridField OrgNightField = new PivotGridField();
        PivotGridField MiningMethodField = new PivotGridField();
        PivotGridField FaceLengthField = new PivotGridField();
        PivotGridField StopingWidthField = new PivotGridField();
        PivotGridField SQMField = new PivotGridField();
        PivotGridField MetersField = new PivotGridField();
        PivotGridField CalcField = new PivotGridField();

        

        RepositoryItemComboBox cDropDownNew = new RepositoryItemComboBox(); // RepositoryItemLookUpEdit();
        RepositoryItemComboBox  cDropDown = new RepositoryItemComboBox();
        RepositoryItemMemoExEdit cText = new RepositoryItemMemoExEdit(); //RepositoryItemTextEdit(); 

        RepositoryItemTextEdit cNumber = new RepositoryItemTextEdit();
        RepositoryItemTextEdit cReal = new RepositoryItemTextEdit();
        RepositoryItemDateEdit cDate = new RepositoryItemDateEdit();
        
       
        int theActivity;
        int theProdMonth;
        string theSection;
        int theTemplateID;
        public string theFieldName;
        public string s;
        public int abc;
         
        

        public ucPlanProtDataView()
        {
            InitializeComponent();
            errorListData.Columns.Add("WPName", typeof(string));
            errorListData.Columns.Add("Error", typeof(string));
            DevExpress.Data.Mask.DateTimeMaskManager.DoNotClearValueOnInsertAfterSelectAll = true;
           
        }

       

        private void addGroup(string GroupFieldName, string GroupCaption)
        {
            DevExpress.XtraPivotGrid.PivotGridField GroupName = new PivotGridField();
            GroupName.FieldName = GroupFieldName;
            GroupName.Caption = GroupCaption;
            GroupName.Area = PivotArea.ColumnArea;
        
            gridPlanProtData.Fields.Add(GroupName);
        }

        public void printReport()
        {
            Report theReport = new Report();
            

            DataSet repMODDataSet = new DataSet();
            repMODDataSet.Tables.Clear();
            mainData.TableName = "PPData";

            theReport.Load(TGlobalItems.ReportsFolder + "\\PlanProtDataReport.frx");
            repMODDataSet.Tables.Add(mainData);
            theReport.RegisterData(repMODDataSet);

            theReport.Show();
           // cDate 
           // theReport.Design();
        }

        public void saveData()
        {
            try
            {

                MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
                _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_SaveData.SqlStatement = "spPlanProt_SaveData";
                _SaveData.SqlStatement = "sp_PlanProt_SaveData";
                _SaveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                Mineware.Systems.ProductionGlobal.frmProgress theProgress = new Mineware.Systems.ProductionGlobal.frmProgress();
                //Mineware .Systems .ProductionGlobal .frmProgress 
                theProgress.SetCaption("Save Data");
                theProgress.SetProgresMax(mainData.Rows.Count);
                theProgress.Show();
                int thePos = 0;
                foreach (DataRow r in mainData.Rows)
                {


                    if (r["ValueChanged"].ToString() == "1")
                    {
                        string theNewValue;
                        DateTime theNewDate;

                        if (r["fieldDescription"].ToString() == "Date")
                        {
                            try
                            {
                                theNewDate = Convert.ToDateTime(r["TheValue"].ToString());
                                theNewValue = String.Format("{0:yyy/MM/dd}", theNewDate);
                            }
                            catch
                            {
                                theNewValue = "";
                            }

                        }
                        else theNewValue = r["TheValue"].ToString();
                        SqlParameter[] _paramCollection = 
                    {
                        _SaveData.CreateParameter("@Prodmonth", SqlDbType.Int, 7,Convert.ToInt32(r["PRODMONTH"].ToString())),
                        _SaveData.CreateParameter("@SectionID ", SqlDbType.VarChar, 50,r["SECTIONID"].ToString()),
                        _SaveData.CreateParameter("@WorkplaceID", SqlDbType.VarChar , 50,r["WORKPLACEID"]),
                        _SaveData.CreateParameter("@FieldID", SqlDbType.Int, 0,r["FieldID"]),
                        _SaveData.CreateParameter("@TheValue", SqlDbType.VarChar, 0,theNewValue),
                        _SaveData.CreateParameter("@ActivityType", SqlDbType.Int, 0,theActivity),
                        _SaveData.CreateParameter("@UserID", SqlDbType.VarChar, 60,TUserInfo.UserID),
                    };

                        _SaveData.ParamCollection = _paramCollection;
                        _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        _SaveData.ExecuteInstruction();
                    }
                    thePos++;
                    theProgress.SetProgressPosition(thePos);
                }

                theProgress.Close();
                if (ucHRPlanning != null)
                {
                    ucHRPlanning.saveHRPlannData();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    
        public void memoEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
      // cText .KeyPress  += cText_KeyPress;
           
            cDate.MaxValue = DateTime.Now;
          
            string expression;
            expression = "FieldName = '" + theFieldName + "'";
            DataRow[] s1;
            s1 = mainData.Select(expression );
         

            for (int i = 0; i < s1.Length; i++)
            {
              
                    string theFieldType = s1[i]["fieldDescription"].ToString();
               
                    if (theFieldType == "Alpha")
                    {
                     
                         s = e.NewValue as string;
                        if (s == "")
                        {
                            break;
                        }
                        // s = s.Substring(0, 300);
                        int abcd = 0;
                        if (s1[i]["NoLines"] is DBNull || s1 [i]["NoLines"].ToString ()=="" || s1[i]["NoLines"].ToString() == "NULL")
                        {
                           // s1[i]["NoLines"] = 0;
                            s = e.NewValue as string;
                        }
                        else
                        {
                             abcd = Convert.ToInt32(s1[i]["NoLines"]);
                        }
                       // char abc1 = "\r\n";
                        bool valid = true;
                        int k = 0;
                        int m = 0;
                        
            int count=0;
            while (valid)
            {
              //  k = s.Substring(m, s.Length - m).IndexOf("\r\n");
                k = s.Substring(m, s.Length - m ).IndexOf("\r\n");
                abc = Convert.ToInt32(s1[i]["NoCharacters"]);
                //ToolTip abc = new ToolTip();
                //RepositoryItemComboBox ab = (RepositoryItemComboBox)sender;
                //abc.SetToolTip(cText , "hello");
               // GetRemainingChars(sender );
               // k = s.IndexOf("\r\n");
                if (abc != 0)
                {
                    if (k != -1)
                    {
                        //string abc1 = s.Substring(0, k);
                        count++;

                        m = m + k + 1;

                        // int con = count;
                        // abc = Convert.ToInt32(s1[i]["NoCharacters"]);

                        //if (cText.MaxLength > abc)
                        //{
                        //    MessageBox .Show ("Cannot enter more than "+abc +" lines","",MessageBoxButtons .OK );
                        //}


                    }
                    // }

                    else
                    {
                        while (valid)
                        {
                            k = s.Substring(m, s.Length - m).IndexOf("\r\n");
                            //ToolTip abc = new ToolTip();
                            //RepositoryItemComboBox ab = (RepositoryItemComboBox)sender;
                            //abc.SetToolTip(cText , "hello");
                            // GetRemainingChars(sender );
                            // k = s.IndexOf("\r\n");
                            if (k != -1)
                            {
                                //string abc1 = s.Substring(0, k);
                                count++;

                                m = m + k + 1;
                            }
                            else
                            {
                                // s = e.NewValue as string;
                                // abc = Convert .ToInt32 ( e.NewValue);
                                valid = false;
                                // valid = true;
                            }
                        }
                    }


                }
                else
                {
                    valid = false;
                }
            }
            //if (s.Length != s.Length)
            //{
            //    gridPlanProtData.ActiveEditor.ToolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(ToolTipController_GetActiveObjectInfo);
            //    // gridPlanProtData.ActiveEditor.ToolTipController.AutoPopDelay = 5000;
            //}
           
            int con = count;
            if (abcd == 0)
            {
                break;
            }
            else
            {
                if (con >= abcd)
                {
                    MessageBox.Show("Cannot enter more than " + abcd + " lines", "", MessageBoxButtons.OK);
                    e.Cancel = true;
                }
            }
            
            break;
                   
               }
                   
           }
        }

     
        private void cText_Keydown(object sender, KeyEventArgs e)
        {
           // MemoExEdit edit1 = sender as MemoExEdit;
           // MemoExPopupForm form = (edit1 as IPopupControl).PopupWindow as MemoExPopupForm;
          // gridPlanProtData .ActiveEditor .Text =gridPlanProtData 
            if (e.KeyData == Keys.Tab)
            {
                if (gridPlanProtData.ActiveEditor.Text == "")
                {
                   // MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
                   // break;

                }
                gridPlanProtData.CloseEditor();
                gridPlanProtData.Cells.FocusedCell = new System.Drawing.Point(gridPlanProtData.Cells.FocusedCell.X + 1, gridPlanProtData.Cells.FocusedCell.Y);
                //e.Handled = true;
            }
            
          




            //if (gridPlanProtData.ActiveEditor.Text == "")
            //{
            //    MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
            //}

            //if (e.KeyCode == Keys.ShiftKey)
            //{
            //    form.OkButton.Focus();
            //}
            // gridPlanProtData.ShowEditor();
            // ComboBoxEdit edit = gridPlanProtData.ActiveEditor as ComboBoxEdit;
            ComboBoxEdit edit = sender as ComboBoxEdit;
            //  if (e.KeyData == Keys.Down)
            if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
            {
                // if (gridPlanProtData.ActiveEditor == null)
                // {
                // gridPlanProtData.ShowEditor();

                ((PopupBaseEdit)gridPlanProtData.ActiveEditor).ShowPopup();
               
                Keys abc = new Keys();
                abc =(Keys .Enter |Keys .Alt );
                Message msg = new Message();
                ProcessCmdKey(ref  msg ,abc );
                e.Handled = true;
               // ComboBoxEdit edit = sender as ComboBoxEdit;
                //  if (e.KeyData == Keys.Down)
                //if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
                //{
                //   // ((PopupBaseEdit)gridPlanProtData.ActiveEditor).ShowPopup();
                //    (gridPlanProtData.ActiveEditor as PopupBaseEdit).ShowPopup();
                //    gridPlanProtData.ShowEditor();
                   
                //    e.Handled = true;
                //    // }
                //}
               // gridPlanProtData.ShowEditor();
            }
        }


        private void cDate_KeyDown(object sender, KeyEventArgs e)
        {
            // cDate.Editable = true;
            DevExpress.Data.Mask.DateTimeMaskManager.DoNotClearValueOnInsertAfterSelectAll = true;
            //PopupDateEditForm popupform = ((sender as IPopupControl).PopupWindow as PopupDateEditForm);
            if (e.KeyData == Keys.Tab)
            {
                gridPlanProtData.CloseEditor();
                gridPlanProtData.Cells.FocusedCell = new System.Drawing.Point(gridPlanProtData.Cells.FocusedCell.X + 1, gridPlanProtData.Cells.FocusedCell.Y);
            }
            //((PopupBaseEdit)gridPlanProtData.ActiveEditor).ClosePopup();

            //e.Handled = true;

            cDate.MaxValue = DateTime.Now;
            
            if (e.KeyCode == Keys.Left)
            {
                //(sender as DateEdit).DateTime = (sender as DateEdit).DateTime.AddDays(-1);
                e.Handled = true;

            }
            if (e.KeyCode == Keys.Right)
            {
              //  (sender as DateEdit).DateTime = (sender as DateEdit).DateTime.AddDays(1);
               //  TryCast(gridView1.ActiveEditor, DateEdit).MaskBox.MaskBoxSpin(e.KeyCode = Keys.Up)
                
                e.Handled = true ;

               
                e.Handled = true;

            }

            ComboBoxEdit edit = sender as ComboBoxEdit;
            //  if (e.KeyData == Keys.Down)
            if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
            {
              
                ((PopupBaseEdit)gridPlanProtData.ActiveEditor).ShowPopup();
                e.Handled = true;
                
            }
            if (e.KeyData == Keys.Enter)
            {
               // DevExpress.Data.Mask.DateTimeMaskManager.DoNotClearValueOnInsertAfterSelectAll;
                cDate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                cDate.Mask.EditMask = "yyyy-MM-dd";
                DevExpress.Data.Mask.DateTimeMaskManager.DoNotClearValueOnInsertAfterSelectAll = true;
                gridPlanProtData.CloseEditor();
            }
            if (e.KeyData == Keys.Right )
            {
                e.Handled = true;
               // cDate.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
                //cDate.MaxValue = DateTime.Today.AddDays(1);
              // popupform.Focus() = cDate.MaxValue;
               // cDate .ParseEditValue +=cDate_ParseEditValue;
            }
        }

        void cDate_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            //e.Value = DateTime.MaxValue.AddDays(1);
        }
     
        private void cNumber_KeyDown(object sender, KeyEventArgs e)
        {
            int row = gridPlanProtData.Cells.FocusedCell.Y;
            if (e.KeyData == Keys.Tab)
            {
                gridPlanProtData.CloseEditor();
                //gridPlanProtData.EditValue = null;
                gridPlanProtData.Cells.FocusedCell = new System.Drawing.Point(gridPlanProtData.Cells.FocusedCell.X + 1, gridPlanProtData.Cells.FocusedCell.Y);
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;                
                if (e.KeyCode == Keys.Up)
                    row = Math.Max(0, row - 1);
                else
                    row = Math.Min(gridPlanProtData.Cells.RowCount - 1, row + 1);
                gridPlanProtData.CloseEditor();
                gridPlanProtData.Cells.FocusedCell = new Point(gridPlanProtData.Cells.FocusedCell.X, row);

            }

            //if (e.KeyData == Keys.Up)
            //{

            //    row = Math.Min(gridPlanProtData.Cells.RowCount - 1, row - 1);
        
                    
            //    gridPlanProtData.Cells.FocusedCell = new System.Drawing.Point(gridPlanProtData.Cells.FocusedCell.X, gridPlanProtData.Cells.FocusedCell.Y - 1);

            //}

            //if (e.KeyData == Keys.Down)
            //{
            //    row = Math.Min(gridPlanProtData.Cells.RowCount - 1, row + 1);
            //    gridPlanProtData.Cells.FocusedCell = new System.Drawing.Point(gridPlanProtData.Cells.FocusedCell.X , gridPlanProtData.Cells.FocusedCell.Y + 1);
                
            //}
        }

        private void cReal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                gridPlanProtData.CloseEditor();
                gridPlanProtData.Cells.FocusedCell = new System.Drawing.Point(gridPlanProtData.Cells.FocusedCell.X + 1, gridPlanProtData.Cells.FocusedCell.Y);
            }
            ComboBoxEdit edit = sender as ComboBoxEdit;
            //  if (e.KeyData == Keys.Down)
            if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
            {
                (gridPlanProtData.ActiveEditor as PopupBaseEdit).ShowPopup();
                e.Handled = true;
                // }
            }
           
        }

        private void cDropDownNew_KeyDown(object sender, KeyEventArgs e)
                {
           
            
            ComboBoxEdit edit = sender as ComboBoxEdit;
            //  if (e.KeyData == Keys.Down)
            int i1 = 0;
            string ab;
            if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
            {
                // if (gridPlanProtData.ActiveEditor == null)
                // {
                // gridPlanProtData.ShowEditor();

                ((PopupBaseEdit)gridPlanProtData.ActiveEditor).ShowPopup();
                
                if (e.KeyData == Keys.Up)
                {
                    
                 //   ((PopupBaseEdit)gridPlanProtData.ActiveEditor).ButtonClick +=
                    gridPlanProtData.ActiveEditor.EditValue  = cDropDownNew.Items[i1].ToString();
                    
                    ab = gridPlanProtData.ActiveEditor.Text;
                    e.Handled = true;

                    if (e.KeyData == Keys.Enter)
                    {
                        string ac = ab;
                        gridPlanProtData.ActiveEditor.EditValue  = ac;
                        // e.KeyValue = cDropDownNew.Items[i1].ToString();
                        ((PopupBaseEdit)gridPlanProtData.ActiveEditor).ClosePopup();
                       
                        e.Handled = true;
                        
                    }

                   // ComboBoxEdit edit1 = sender as ComboBoxEdit;
                    //  if (e.KeyData == Keys.Down)
                    if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
                    {
                        (gridPlanProtData.ActiveEditor as PopupBaseEdit).ShowPopup();
                        e.Handled = true;
                        // }
                    }
                }
            }

            
            if (e.KeyData == Keys.Tab)
            {
                gridPlanProtData.CloseEditor();
              
                 gridPlanProtData.Cells.FocusedCell = new System.Drawing.Point(gridPlanProtData.Cells.FocusedCell.X + 1, gridPlanProtData.Cells.FocusedCell.Y);
            }
            if (e.KeyData == Keys.Right)
            {
                // cDate.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
                cDate.MaxValue = DateTime.Now.AddDays(1);

            }
        }

     

        private void cDropDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                gridPlanProtData.CloseEditor();
                gridPlanProtData.Cells.FocusedCell = new System.Drawing.Point(gridPlanProtData.Cells.FocusedCell.X + 1, gridPlanProtData.Cells.FocusedCell.Y);
            }
            ComboBoxEdit edit = sender as ComboBoxEdit;
            //  if (e.KeyData == Keys.Down)
            if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
            {
                // if (gridPlanProtData.ActiveEditor == null)
                // {
                // gridPlanProtData.ShowEditor();

                (gridPlanProtData.ActiveEditor as PopupBaseEdit).ShowPopup();
                e.Handled = true;
                // }
            }
            if (e.KeyData == Keys.Right)
            {
                // cDate.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
                cDate.MaxValue = DateTime.Now.AddDays(1);

            }
        }

        private void cText_KeyPress(object sender, KeyPressEventArgs e)
        {
            ToolTipController abc = null;
            abc = new ToolTipController();
            abc.ShowHint(gridPlanProtData.ActiveEditor.ToString(), PointToScreen(gridPlanProtData.ActiveEditor.Location));
        }

      
        private void abc123_TextChanged(object sender, EventArgs e)
        {
            ToolTipController abc = null;
            abc = new ToolTipController();
            abc.ShowHint(gridPlanProtData.ActiveEditor.Text, PointToScreen(gridPlanProtData.ActiveEditor.Location));
        }

        private void cText_MouseWheel(object sender, MouseEventArgs e)
        {
            ToolTipController abc = null;
            abc = new ToolTipController();
            abc.ShowHint(gridPlanProtData.ActiveEditor.Text, PointToScreen(gridPlanProtData.ActiveEditor.Location));
        }

        private void cText_Validating(object sender, CancelEventArgs  e)
        {
            // cDate.MaxValue = DateTime.Now;
          
            //string expression;
            //expression = "FieldName = '" + theFieldName + "'";
            //DataRow[] s1;
            //s1 = mainData.Select(expression );

            
            //for (int i = 0; i < s1.Length; i++)
            //{

            //    string theFieldType = s1[i]["fieldDescription"].ToString();

            //    if (theFieldType == "Alpha")
            //    {

            //        s = gridPlanProtData .ActiveEditor .Text   as string;
            //        if (s == "")
            //        {
            //           // MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
            //            break;
            //        }
            //        // s = s.Substring(0, 300);
            //        int abcd = Convert.ToInt32(s1[i]["NoLines"]);
            //        // char abc1 = "\r\n";
            //        bool valid = true;
            //        int k = 0;
            //        int m = 0;

            //        int count = 0;
            //        while (valid)
            //        {
            //            //  k = s.Substring(m, s.Length - m).IndexOf("\r\n");
            //            k = s.Substring(m, s.Length - m).IndexOf("\r\n");
            //            //ToolTip abc = new ToolTip();
            //            //RepositoryItemComboBox ab = (RepositoryItemComboBox)sender;
            //            //abc.SetToolTip(cText , "hello");
            //            // GetRemainingChars(sender );
            //            // k = s.IndexOf("\r\n");
            //            if (k != -1)
            //            {
            //                //string abc1 = s.Substring(0, k);
            //                count++;

            //                m = m + k + 1;

            //                // int con = count;
            //                abc = Convert.ToInt32(s1[i]["NoCharacters"]);
            //                //if (gridPlanProtData.ActiveEditor.Text == "")
            //                //{
            //                //    MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
            //                //}

            //                if (cText.MaxLength > abc)
            //                {
            //                //    MessageBox .Show ("Cannot enter more than "+abc +" lines","",MessageBoxButtons .OK );
            //                    ToolTipController abc1 = null;
            //                    abc1 = new ToolTipController();
            //                    abc1.ShowHint(gridPlanProtData.ActiveEditor.Text, PointToScreen(gridPlanProtData.ActiveEditor.Location));

            //                }


            //            }
            //        }
            //    }
            //}
        
        }

        private void cText_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.OK;
        }

        private void cText_Popup(object sender, EventArgs e)
            {
           // MemoExEdit lit = ((sender as IPopupControl).PopupWindow as MemoExEdit);
          //  CheckedListBoxControl listbox = ((sender as IPopupControl).PopupWindow.Controls[2].Controls [0] as CheckedListBoxControl);
           // RepositoryItemComboBox listbox = ((sender as IPopupControl).PopupWindow as RepositoryItemComboBox);
          //  listbox.KeyDown += new KeyEventHandler(cText_Keydown);
           // cText .KeyDown +=new KeyEventHandler(cText_Keydown);
           // MemoExEdit edit = sender as MemoExEdit;
           // MemoExPopupForm form = (edit as IPopupControl).PopupWindow as MemoExPopupForm;
            MemoEdit memoEdit= ((sender as IPopupControl ).PopupWindow .Controls [2] as MemoEdit) ;
            memoEdit .KeyDown += new KeyEventHandler (listBox_KeyDown);
           
           
        }

        private void cDate_Popup(object sender, EventArgs e)
        {
          // MemoEdit memoEdit=((sender  as IPopupControl ).PopupWindow.Controls [2]  as MemoEdit) ;
           // DateEdit dateedit = ((sender as IPopupControl).PopupWindow as DateEdit );
            //cDate.WeekNumberRule;
          
           PopupDateEditForm popupform=((sender as IPopupControl).PopupWindow as PopupDateEditForm);
           popupform .KeyDown  += new KeyEventHandler(cDate_KeyDown);
        }

     

        void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Alt)
            {
                (gridPlanProtData.ActiveEditor as IPopupControl).PopupWindow.Controls[0].Focus();
            }
        }

        private void cText_Leave(object sender, EventArgs e)
        {

        //    string expression1;
        //    expression1 = "FieldName = '" + theFieldName + "'";
        //    DataRow[] s1;
        //    s1 = mainData.Select(expression1);


        //    for (int i = 0; i < s1.Length; i++)
        //    {

        //        string theFieldType = s1[i]["fieldDescription"].ToString();
              

        //    //string theFieldType = s1["fieldDescription"].ToString();
        //        if (theFieldType == "Alpha")
        //        {

        //            if (s1[i]["FieldRequired"] is DBNull)
        //            {
        //               // s2[i]["FieldRequired"] = 0;
        //                break;
        //            }
        //            else  if (Convert.ToInt32(s1[i]["FieldRequired"]) == 1)
        //            {

        //                if (gridPlanProtData.ActiveEditor.Text == "")
        //                {
        //                    MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
        //                    break;
                            
        //                }
        //                else
        //                {
        //                    //break;
        //                    gridPlanProtData.ActiveEditor.Text = gridPlanProtData.ActiveEditor.Text;
        //                }
        //            }
                   
        //        }
        //    }

          //  MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);

            //if (gridPlanProtData.ActiveEditor.Text == "")
            //{
            //    MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
            //}

        }


        private void cNumber_Leave(object sender, EventArgs e)
        {
            string expression2;
            expression2 = "FieldName = '" + theFieldName + "'";
            DataRow[] s2;
            s2 = mainData.Select(expression2);


            for (int i = 0; i < s2.Length; i++)
            {

                string theFieldType = s2[i]["fieldDescription"].ToString();

                //string theFieldType = s1["fieldDescription"].ToString();
                if (theFieldType == "Number")
                {

                    if (s2[i]["FieldRequired"] is DBNull)
                    {
                       // s2[i]["FieldRequired"] = 0;
                        break;
                    }
                    else if (Convert.ToInt32(s2[i]["FieldRequired"]) == 1)
                    {

                        if (gridPlanProtData.ActiveEditor.Text == "")
                        {
                           // MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
                            break;
                        }
                    }
                }
            }

        }


        private void cReal_Leave(object sender, EventArgs e)
        {
            string expression2;
            expression2 = "FieldName = '" + theFieldName + "'";
            DataRow[] s2;
            s2 = mainData.Select(expression2);


            for (int i = 0; i < s2.Length; i++)
            {

                string theFieldType = s2[i]["fieldDescription"].ToString();

                //string theFieldType = s1["fieldDescription"].ToString();
                if (theFieldType == "Real")
                {
                   
                    if (s2[i]["FieldRequired"] is DBNull)
                    {
                       // s2[i]["FieldRequired"] = 0;
                        break;
                    }


                    else if (Convert.ToInt32(s2[i]["FieldRequired"]) == 1)
                    {

                        if (gridPlanProtData.ActiveEditor.Text == "")
                        {
                            //MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
                            break;
                        }
                    }
                }
            }


        }

        private void cDate_CloseUp(object sender, CloseUpEventArgs e)
        {
            try
            {
                // cDate.MaxValue = DateTime.Now;
                int currentYear = DateTime.Today.Year;
                //int actualdate = ((DateTime)e.Value).Year;
                DateTime abc = Convert.ToDateTime(e.Value);
                int c = abc.Year;

                cDate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                cDate.Mask.EditMask = "yyyy-MM-dd";
                //repositoryItemDateEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                //repositoryItemDateEdit.Mask.EditMask = "yyyy-MM-dd";

                if ((e.Value).ToString() == "")
                {
                    MessageBox.Show("Please enter a date", "", MessageBoxButtons.OK);
                    //  DateTime selectedDate = (DateTime)e.Value;
                }
                //   DateTime selectedDate = (DateTime)e.Value;

                //if (cDate.MaxValue < DateTime.Now.AddYears(-1))

               // else if (((DateTime)e.Value).Year < currentYear)
                else if (c  < currentYear)
                {// e.Value = new DateTime(currentYear, 1, 1);

                    MessageBox.Show("Date cannot be less than the current year", "", MessageBoxButtons.OK);

                }
            }
            catch (Exception x)
            {
                MessageBox.Show("please enter a date", "", MessageBoxButtons.OK);
            }
        }

        //public void loadTemplateData(int ProdMonth, string section, int TemplateID, int WorkPlaceID, int ActivityType, string captureOption, bool Readonly)
        //{
        public void loadTemplateData(int ProdMonth, string section, int TemplateID, string WorkPlaceID, int ActivityType, string captureOption, bool Readonly)
        {
            mainData.Clear();
            DevExpress.Data.Mask.DateTimeMaskManager.DoNotClearValueOnInsertAfterSelectAll = true;
            int GroupCount = 0;
            Boolean isHR = false;
            GlobalVar.ActivityType = ActivityType;
            GlobalVar.captureOption = captureOption;
            GlobalVar.ProdMonth = ProdMonth;
            GlobalVar.Readonly = Readonly;
            GlobalVar.section = section;
            GlobalVar.TemplateID = TemplateID;
          //  GlobalVar.WorkPlaceID = WorkPlaceID;
           cText .Validating +=cText_Validating;
            ComboBoxEdit abc123 = new ComboBoxEdit();
            abc123.TextChanged += abc123_TextChanged;
           cText .MouseWheel +=cText_MouseWheel;
            cText.KeyPress += cText_KeyPress;
           cText.EditValueChanging += memoEdit1_EditValueChanging;
         
            cText.KeyDown += cText_Keydown;
            cDate .KeyDown += cDate_KeyDown;
            cNumber .KeyDown +=cNumber_KeyDown;
            cReal.KeyDown +=cReal_KeyDown;
            cDropDownNew.KeyDown +=cDropDownNew_KeyDown;
            cDropDown .KeyDown +=cDropDown_KeyDown;
            cText .ButtonClick +=cText_ButtonClick;
            cText .Popup +=cText_Popup;
            cDate .Popup +=cDate_Popup;
            cText .Leave +=cText_Leave;
            cNumber .Leave +=cNumber_Leave;
            cReal .Leave +=cReal_Leave;
            cDate .CloseUp +=cDate_CloseUp;
            

            MWDataManager.clsDataAccess _HRScreen = new MWDataManager.clsDataAccess();
            _HRScreen.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _HRScreen.SqlStatement = "SELECT * FROM PlanProt_Template WHERE TemplateID = " + TemplateID.ToString();
            _HRScreen.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _HRScreen.queryReturnType = MWDataManager.ReturnType.DataTable;
            _HRScreen.ExecuteInstruction();

            foreach (DataRow r in _HRScreen.ResultsDataTable.Rows)
            {
                if (Convert.ToBoolean(r["HUMAN_RESOURCES"].ToString()) == true)
                {
                    isHR = true;
                    lcAditionalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    if (ucHRPlanning == null)
                    {
                        ucHRPlanning = new PlanningProtocolCapture.ucHRPlanning { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                     ucHRPlanning.loadHRPlanningData(ProdMonth, section, ActivityType);
                     
                    }
                    ucHRPlanning.Parent = pcParent;
                    ucHRPlanning.Dock = DockStyle.Fill;
                   // ucHRPlanning.countcheck();
                }
                else { isHR = false;  lcAditionalData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; }
            }


            theActivity = ActivityType;
            theProdMonth = ProdMonth;
            theSection = section;
            theTemplateID = TemplateID;
            MWDataManager.clsDataAccess _ReportData = new MWDataManager.clsDataAccess();
            _ReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _ReportData.SqlStatement = "sp_PlanProt_LoadData";
            //_ReportData.SqlStatement = "sp_PlanProt_LoadData";
            _ReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

            if (captureOption == "2")
            {
                SqlParameter[] _paramCollection = 
             {
              _ReportData.CreateParameter("@ProdMonth", SqlDbType.Int, 7,ProdMonth),
              _ReportData.CreateParameter("@SectionID ", SqlDbType.VarChar, 50,section),
              _ReportData.CreateParameter("@TemplateID", SqlDbType.Int, 0,TemplateID),
             // _ReportData.CreateParameter("@WorkplaceID", SqlDbType.Int, 0,WorkPlaceID),
               _ReportData.CreateParameter("@WorkplaceID", SqlDbType.VarChar , 50,WorkPlaceID),
              _ReportData.CreateParameter("@ActivityType", SqlDbType.Int, 0,ActivityType),
             };

                _ReportData.ParamCollection = _paramCollection;
                _ReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _ReportData.ExecuteInstruction();
						
                DataTableReader reader = new DataTableReader(_ReportData.ResultsDataTable);
                mainData.Load(reader);
            }
            else
            {

                MWDataManager.clsDataAccess _PrePlanDataData = new MWDataManager.clsDataAccess();
                _PrePlanDataData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _PrePlanDataData.SqlStatement = "SELECT * FROM dbo.PLANMONTH a inner join Section_Complete b on \n" +
                                                "a.Prodmonth = b.Prodmonth and \n" +
                                                "a.Sectionid = b.Sectionid \n" +
                                                "WHERE Sectionid_2 = '" + Convert.ToString(section)  + "'  AND \n" +
                                                "a.Prodmonth = " + Convert.ToString(ProdMonth) + "\n" +
                                                " AND  PlanCode = 'MP'\n" +
                                                " AND  Activity = " + Convert.ToString(ActivityType);
                _PrePlanDataData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _PrePlanDataData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _PrePlanDataData.ExecuteInstruction();

                foreach (DataRow r in _PrePlanDataData.ResultsDataTable.Rows)
                {
                    if (r["WorkplaceID"].ToString() != "-1")
                    {
                        SqlParameter[] _paramCollection = 
                     {
                      _ReportData.CreateParameter("@ProdMonth", SqlDbType.Int, 7,ProdMonth),
                      _ReportData.CreateParameter("@SectionID ", SqlDbType.VarChar, 50,section),
                      _ReportData.CreateParameter("@TemplateID", SqlDbType.Int, 0,TemplateID),
                      _ReportData.CreateParameter("@WorkplaceID", SqlDbType.VarChar , 0,r["WorkplaceID"]),
                      _ReportData.CreateParameter("@ActivityType", SqlDbType.Int, 0,ActivityType),
                     };

                        _ReportData.ParamCollection = _paramCollection;
                        _ReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _ReportData.ExecuteInstruction();

                        if (mainData.Columns.Count != 0)
                        {

                           // mainData = _ReportData.ResultsDataTable.Copy();
                            foreach (DataRow n in _ReportData.ResultsDataTable.Rows)
                            {
                                mainData.LoadDataRow(n.ItemArray, false);
                            }
                        }
                        else
                        {
                            DataTableReader reader = new DataTableReader(_ReportData.ResultsDataTable);
                            mainData.Load(reader);
                        }
                    }
                    else 
                    {
                        if (lcErrorList.Visibility != DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                        {
                            lcErrorList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            gcError.DataSource = errorListData;
                        }
                        errorListData.Rows.Add(r["WorkplaceDesc"], "The workplace is currently not available. The workplace needs to be created in IRRIS before you can capture information against it.");

                      
                    }

                } 

            }


            MWDataManager.clsDataAccess _CountGroup = new MWDataManager.clsDataAccess();
            _CountGroup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _CountGroup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _CountGroup.queryReturnType = MWDataManager.ReturnType.DataTable;

            _CountGroup.SqlStatement =  "SELECT Count(Group1.GroupName1)G1,Count(Group2.GroupName2)G2,COUNT(Group3.GroupName3)G3,COUNT(Group4.GroupName4)G4 FROM ( \n\n " +
                                        "SELECT PPT.TemplateName, PPF.FieldID,PPF.FieldName,PPF.FieldType,PPF.ParentID,PPT.TemplateID FROM dbo.PlanProt_Template PPT \n " +
                                        "INNER JOIN dbo.PlanProt_Fields PPF ON \n " +
                                        "PPT.TemplateID = PPF.TemplateID \n " +
                                        //"LEFT JOIN dbo.PlanProt_Data PPG ON \n " +
                                        //"PPF.FieldID = PPG.FieldID  \n " +
                                        "WHERE PPT.TemplateID = \n " + Convert.ToString(TemplateID) + " AND \n " +
                                        "      PPF.FieldType <> 1) theFields \n " +
                                        "LEFT JOIN  \n " +
                                        "( SELECT FieldName GroupName1,PPF.ParentID,PPF.FieldID FROM dbo.PlanProt_Fields PPF  \n " +
                                        //"  LEFT JOIN dbo.PlanProt_Data PPG ON \n " +
                                        //"  PPF.FieldID = PPG.FieldID \n " +
                                        "  WHERE PPF.FieldType = 1  ) Group1 ON \n " +
                                        "theFields.ParentID = Group1.FieldID \n " +
                                        "LEFT JOIN  \n " +
                                        "( SELECT FieldName GroupName2,PPF.ParentID,PPF.FieldID FROM dbo.PlanProt_Fields PPF  \n " +
                                        //"  LEFT JOIN dbo.PlanProt_Data PPG ON \n " +
                                        //"  PPF.FieldID = PPG.FieldID \n " +
                                        "  WHERE PPF.FieldType = 1  ) Group2 ON \n " +
                                        "Group1.ParentID = Group2.FieldID \n " +
                                        "LEFT JOIN  \n " +
                                        "( SELECT FieldName GroupName3,PPF.ParentID,PPF.FieldID FROM dbo.PlanProt_Fields PPF  \n " +
                                        //"  LEFT JOIN dbo.PlanProt_Data PPG ON \n " +
                                        //"  PPF.FieldID = PPG.FieldID \n " +
                                        "  WHERE PPF.FieldType = 1  ) Group3 ON \n " +
                                        "Group2.ParentID = Group3.FieldID \n " +
                                        "LEFT JOIN  \n " +
                                        "( SELECT FieldName GroupName4,PPF.ParentID,PPF.FieldID FROM dbo.PlanProt_Fields PPF  \n " +
                                        //"  LEFT JOIN dbo.PlanProt_Data PPG ON \n " +
                                        //"  PPF.FieldID = PPG.FieldID \n " +
                                        "  WHERE PPF.FieldType = 1  ) Group4 ON \n " +
                                        "Group3.ParentID = Group4.FieldID";
            _CountGroup.ExecuteInstruction();
            foreach (DataRow s in _CountGroup.ResultsDataTable.Rows)
            {
                if (Convert.ToInt32(s["G1"].ToString()) > 0)
                    GroupCount = GroupCount + 1;
                if (Convert.ToInt32(s["G2"].ToString()) > 0)
                    GroupCount = GroupCount + 1;
                if (Convert.ToInt32(s["G3"].ToString()) > 0)
                    GroupCount = GroupCount + 1;
                if (Convert.ToInt32(s["G4"].ToString()) > 0)
                    GroupCount = GroupCount + 1;
            }

            switch (GroupCount)
            {
                case 0:
               //     addGroup("GroupName0", "Group 0");

                    break;
                case 1:
                    addGroup("GroupName1","Group 1");
                    
                    break;

                case 2:
                    addGroup("GroupName1", "Group 1");
                    addGroup("GroupName2", "Group 2");

                    break;

                case 3:
                    addGroup("GroupName1", "Group 1");
                    addGroup("GroupName2", "Group 2");
                    addGroup("GroupName3", "Group 3");

                    break;

                case 4:
                    addGroup("GroupName1", "Group 1");
                    addGroup("GroupName2", "Group 2");
                    addGroup("GroupName3", "Group 3");
                    addGroup("GroupName4", "Group 4");

                    break;
            }

            _CountGroup.SqlStatement = "SELECT TemplateName,(SELECT DISTINCT NAME_2 FROM dbo.SECTION_COMPLETE WHERE PRODMONTH = " + Convert.ToString(ProdMonth) + " AND SECTIONID_2 = '" + Convert.ToString(section) + "') SECTION, " +
                                       "CASE WHEN " + Convert.ToString(ActivityType) + " = 1 THEN 'Development'  " +
                                       "WHEN " + Convert.ToString(ActivityType) + " = 0 THEN 'STOPING' END Activity  " +
                                       "FROM PlanProt_Template WHERE TemplateID = " + Convert.ToString(TemplateID);
            _CountGroup.ExecuteInstruction();

            foreach (DataRow s in _CountGroup.ResultsDataTable.Rows)
            {
                labHeading.Text = s["TemplateName"].ToString();
                editActivity.Text = s["Activity"].ToString();
                editProdMonth.Text = Convert.ToString(ProdMonth);
               editSection.Text = s["SECTION"].ToString(); 
            }

            FieldName.FieldName = "FieldName";
            FieldName.Caption = "Field Name";
            FieldName.Area = PivotArea.ColumnArea;
            FieldName.SortMode = PivotSortMode.None;

            gridPlanProtData.Fields.Add(FieldName);

            if (gridPlanProtData.Fields.Count > 1)
            {
              gridPlanProtData.Fields[1].Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
              gridPlanProtData.Fields[1].BestFit();
            }

            WorkplaceIDField.Area = PivotArea.RowArea;
            WorkplaceIDField.Caption = "Workplace ID";
            WorkplaceIDField.FieldName = "WORKPLACEID";
            WorkplaceIDField.Width = 80;

            gridPlanProtData.Fields.Add(WorkplaceIDField);

            WorkplaceField.Area = PivotArea.RowArea;
            WorkplaceField.Caption = "Workplace";
            WorkplaceField.FieldName = "WPDESCRIPTION";
            WorkplaceField.Width = 120;

            gridPlanProtData.Fields.Add(WorkplaceField);

            if (isHR == true)
            {
                MiningMethodField.Area = PivotArea.RowArea;
                MiningMethodField.Caption = "Mining Method";
                MiningMethodField.FieldName = "MiningType";
                MiningMethodField.Width = 110;

                gridPlanProtData.Fields.Add(MiningMethodField);


                OrgDayField.Area = PivotArea.RowArea;
                OrgDayField.Caption = "Orgunit Day";
                OrgDayField.FieldName = "OrgUnitDay";
                OrgDayField.Width = 90;
                OrgDayField.SortMode = PivotSortMode.Custom;

                gridPlanProtData.Fields.Add(OrgDayField);

                OrgNightField.Area = PivotArea.RowArea;
                OrgNightField.Caption = "Orgunit Night";
                OrgNightField.FieldName = "OrgUnitNight";
                OrgNightField.Width = 90;

                gridPlanProtData.Fields.Add(OrgNightField);
                
            }

            if (ActivityType == 0)
            {
                FaceLengthField.Area = PivotArea.RowArea;
                FaceLengthField.Caption = "Face Length";
                FaceLengthField.FieldName = "FL";
                FaceLengthField.Width = 90;
                gridPlanProtData.Fields.Add(FaceLengthField);

                StopingWidthField.Area = PivotArea.RowArea;
                StopingWidthField.Caption = "Stoping Width";
                StopingWidthField.FieldName = "SW";
                StopingWidthField.Width = 95;
                gridPlanProtData.Fields.Add(StopingWidthField);

                SQMField.Area = PivotArea.RowArea;
                SQMField.Caption = "Total m²";
                SQMField.FieldName = "TotalSQM";
                SQMField.ValueFormat.FormatType = FormatType.Custom;
                SQMField.ValueFormat.FormatString = "0";
                //SQMField.CellFormat.FormatType = FormatType.Custom;
                //SQMField.CellFormat.FormatString = "0.0";
                SQMField.Width = 80;
                gridPlanProtData.Fields.Add(SQMField);

            }
            else
            {
                MetersField.Area = PivotArea.RowArea;
                MetersField.Caption = "Total m";
                MetersField.FieldName = "TotalMeters";
                MetersField.Width = 50;

                MetersField.ValueFormat.FormatType = FormatType.Custom;
                MetersField.ValueFormat.FormatString = "0.0";
               
                gridPlanProtData.Fields.Add(MetersField);

            }

            

            ApprovedField.Area = PivotArea.RowArea;
            ApprovedField.FieldName = "Approved";
            ApprovedField.Width = 75;
            ApprovedField.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridPlanProtData.Fields.Add(ApprovedField);

            

            FieldName.ColumnValueLineCount = 10;


            theValue.FieldName = "TheValue";
            theValue.Caption = "The Value";
            theValue.Area = PivotArea.DataArea;
            if (Readonly == true)
               theValue.Options.AllowEdit = false;
            else theValue.Options.AllowEdit = true;
            theValue.SortMode = PivotSortMode.None;
            theValue.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;        

            gridPlanProtData.Fields.Add(theValue);

            //CalcField.Area = PivotArea.DataArea;
            //CalcField.FieldEditName = "CalcField";
            //CalcField.Visible = false;
            //gridPlanProtData.Fields.Add(CalcField);



            gridPlanProtData.DataSource = mainData;
            gridPlanProtData.Refresh();
            //if (mainData.Rows.Count == 0)
            //{
            //    frmPlanProtCpature abd = new frmPlanProtCpature();
            //    abd.Show();
            //}
            foreach (DataRow r in _HRScreen.ResultsDataTable.Rows)
            {
                if (Convert.ToBoolean(r["HUMAN_RESOURCES"].ToString()) == true)
                {
                    ucHRPlanning.countcheck();
                }
            }

        }

        //public void canceldata()
        //{
        //    ucHRPlanning.countcheck();
        //}

        public void DoApproveData(string approveType)
        {
            //saveData();
            DataTable data1 = new DataTable();
            MWDataManager.clsDataAccess _ReportData1 = new MWDataManager.clsDataAccess();
            _ReportData1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _ReportData1.SqlStatement = "sp_PlanProtData_ApproveWorkplaceLIST";
            _ReportData1.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

            SqlParameter[] _paramCollection = 
             {
                _ReportData1.CreateParameter("@ProdMonth", SqlDbType.Int, 7,GlobalVar .ProdMonth  ),
                _ReportData1.CreateParameter("@SectionID_2", SqlDbType.VarChar, 50,GlobalVar .section  ),
                _ReportData1.CreateParameter("@TemplateID", SqlDbType.Int, 0,GlobalVar .TemplateID  ),
                _ReportData1.CreateParameter("@ActivityType", SqlDbType.Int, 0,GlobalVar .ActivityType ),
             };


                _ReportData1.ParamCollection = _paramCollection;
                _ReportData1.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult theresult = _ReportData1.ExecuteInstruction();

                DataTableReader reader1 = new DataTableReader(_ReportData1.ResultsDataTable);

                data1.Load(reader1);
                frmLockUnlockData frmLockUnlockData = new frmLockUnlockData { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                data1  = frmLockUnlockData.setLockUnlockData(data1 ,mainData , approveType);
                gridPlanProtData.RefreshData();
                gridPlanProtData.Update();
                 //loadTemplateData(GlobalVar.ProdMonth, GlobalVar.section, GlobalVar.TemplateID, GlobalVar.WorkPlaceID, GlobalVar.ActivityType, GlobalVar.captureOption, GlobalVar.Readonly);
           // }
        }
        private void gridPlanProtData_CustomDrawFieldValue(object sender, PivotCustomDrawFieldValueEventArgs e)
        {
            //if (e.Area == DevExpress.XtraPivotGrid.PivotArea.RowArea && e.Field == OrgDayField)
            //{

            //    e.Painter.DrawObject(e.Info);
            //    e.Painter.DrawIndicator(e.Info);
            //    e.Graphics.FillRectangle(e.GraphicsCache.GetSolidBrush(Color.FromArgb(50, 0, 0, 200)), e.Bounds);
            //    e.Handled = true;
            //}

            if (e.Field == FieldName)
            {
                DevExpress.Utils.Drawing.HeaderObjectPainter newPainter = e.Painter;


                string c = e.Info.Caption;

                e.Info.Caption = "";
                newPainter.DrawObject(e.Info);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;



                if (IsColumnHeaderHorizontal(e.Field))
                {
                    //New Font(CType(sender, XRTableCell).Font, FontStyle.Bold) 
                    Font newFont = new Font(e.Appearance.Font, FontStyle.Bold);  //Font(e.Appearance.Font.FontFamily,  10);
                    e.Appearance.Font = newFont;

                    StringFormat fmt = new StringFormat();
                    fmt.Alignment = StringAlignment.Far;
                    fmt.Trimming = StringTrimming.EllipsisCharacter;
                    fmt.FormatFlags |= StringFormatFlags.NoWrap;

                    e.GraphicsCache.DrawString(c, e.Appearance.Font,
                        e.Appearance.GetForeBrush(e.GraphicsCache),
                        e.Info.CaptionRect, fmt);
                }
                else
                {
                    Rectangle newRect = new Rectangle();
                    newRect = e.Bounds;
                    //  newRect.X += newRect.Width;
                    //    newRect.Y += 30;// newRect.Height;

                    //   newRect.Width *= 7;
                    //    newRect.Width /= 5;

                    //    newRect.Height *= 7;
                    //    newRect.Height /= 5;

                    //    newRect.Y -= 8;
                    //   newRect.Height -= 8;
                    newRect.Height = 130;
                    //    newRect.Width = 10;
                    StringFormat fmt = new StringFormat();
                    fmt.Alignment = StringAlignment.Near;
                    fmt.Trimming = System.Drawing.StringTrimming.Word;
                    //fmt.Trimming = StringTrimming.EllipsisCharacter;
                    fmt.LineAlignment = System.Drawing.StringAlignment.Center;
                    fmt.FormatFlags = StringFormatFlags.LineLimit;



                    e.GraphicsCache.DrawVString(c, e.Appearance.Font,
                        e.Appearance.GetForeBrush(e.GraphicsCache),
                        newRect, fmt,270);
                }

                e.Info.InnerElements.DrawObjects(e.Info, e.Info.Cache, Point.Empty);
                e.Handled = true;
            }
        }

        private bool IsColumnHeaderHorizontal(PivotGridField field)
        {
            if (field == null)
            {
                return true;
            }
            if (field.Area == PivotArea.RowArea)
            {
                return true;
            }
            return false;
        }

        private void gridPlanProtData_CustomCellEdit(object sender, PivotCustomCellEditEventArgs e)
        {

            //string theFieldName = e.Item.ColumnFieldValueItem.DisplayText.ToString();

            //string expression;
            //expression = "FieldName = '" + theFieldName + "'";
            //DataRow[] s;
            //s = mainData.Select(expression);

            

            //for (int i = 0; i < s.Length; i++)
            //{
            //    //    if (e.Value != null)
            //    //        {                   
                


            //    if (s[i]["FieldName"].ToString() == theFieldName)
            //    {
            //        string theFieldType = s[i]["fieldDescription"].ToString();
            //        if (theFieldType == "Alpha")
            //        {
            //            e.RepositoryItem = cText;
            //        }
            //        if (theFieldType == "Selection")
            //        {
            //            MWDataManager.clsDataAccess _DropDownData = new MWDataManager.clsDataAccess();
            //            _DropDownData.ConnectionString = TUserInfo.ConnectionString;
            //            _DropDownData.SqlStatement = "SELECT MinValue FROM PlanProt_FieldValues WHERE FieldID = " + s[i]["FieldID"].ToString();
            //            _DropDownData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //            _DropDownData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //            _DropDownData.ExecuteInstruction();

            //            cDropDown.Items.Clear();
            //            foreach (DataRow d in _DropDownData.ResultsDataTable.Rows)
            //            {
            //                cDropDown.Items.Add(d["MinValue"].ToString());
            //            }

            //            e.RepositoryItem = cDropDown;
            //            e.ColumnField.Options.AllowEdit = true;
            //        }
            //        if (theFieldType == "Number")
            //        {
            //            e.RepositoryItem = cNumber;
            //        }
            //        if (theFieldType == "Real")
            //        {
            //            cReal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            cReal.DisplayFormat.FormatString = "0.00";

            //            cReal.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            cReal.EditFormat.FormatString = "0.00";

            //            e.RepositoryItem = cReal;
            //        }
            //        if (theFieldType == "Date")
            //        {
            //            e.RepositoryItem = cDate;
            //        }

            //        break;
            //    }

            //    //  }
            //}

        }

        private void gridPlanProtData_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
                               if (e.DataField == theValue)
            {
                ChangeCellValue(e, Convert.ToString(e.Editor.EditValue));
            }

         //   gridPlanProtData.RefreshData();
        }

        public void ChangeCellValue(EditValueChangedEventArgs e, string value)
        {
            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            for (int i = 0; i < ds.RowCount; i++)
            {
                ds[i][theValue] = value;
                ds[i]["ValueChanged"] = 1;
              //  MessageBox.Show(ds[i]["fieldDescription"].ToString());
          
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void gridPlanProtData_CustomCellEditForEditing(object sender, PivotCustomCellEditEventArgs e)
        {
        
            theFieldName  = e.Item.ColumnFieldValueItem.DisplayText.ToString();
           // cText.EditValueChanging += memoEdit1_EditValueChanging;

           

            //cDate.DisplayFormat.FormatString = "YYYY/MM/DD";
            //cDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //cDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //cDate.EditFormat.FormatString = "YYYY/MM/DD";

            cDate.MaxValue = DateTime.Now;
          //  cDate.MinValue = DateTime.Now - 120;
            
            string expression;
            expression = "FieldName = '" + theFieldName + "'" ;
            DataRow[] s;
            s = mainData.Select(expression);

            string exp1;
          exp1=  "FieldName = '" + theFieldName + "' and FieldRequired = 'true'";
          DataRow[] s1;
          s1 = mainData.Select(exp1);
          //for (int a = 0; a < s1.Length; a++)
          //{
          //    if (s1[1]["FieldName"].ToString() == theFieldName)
          //    {
          //       // gridPlanProtData.Appearance.Cell.BackColor = Color.Red;
          //      //  gridPlanProtData.Appearance.FocusedCell.BackColor = Color.Red;
          //       // gridPlanProtData.Appearance.Cell.BackColor = Color.Red;
          //        //gridPlanProtData.Appearance.TotalCell.BackColor = Color.Red;
              
          //    }
          //}

            for (int i = 0; i < s.Length; i++)
            {
                //    if (e.Value != null)
                //        {                   



                if (s[i]["FieldName"].ToString() == theFieldName)
                {
                    string theFieldType = s[i]["fieldDescription"].ToString();
                    if (theFieldType == "Alpha")
                    {
                        
                       // Environment .NewLine 
                        cText.AutoHeight=true ;
                       // int abc = 0;
                        if (s[i]["NoCharacters"] is DBNull || s[i]["NoCharacters"].ToString ()=="" || s[i]["NoCharacters"].ToString() == "NULL")
                        {
                            s[i]["NoCharacters"] = 0;
                        }
                        else
                        {
                            int abc = Convert.ToInt32(s[i]["NoCharacters"]);

                            cText.MaxLength = abc;
                            e.RepositoryItem = cText;
                        //    gridPlanProtData.ActiveEditor.ToolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(ToolTipController_GetActiveObjectInfo);
                            
                        }

                        
                    }
                    if (theFieldType == "Selection")
                    {
                        MWDataManager.clsDataAccess _DropDownData = new MWDataManager.clsDataAccess();
                        _DropDownData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _DropDownData.SqlStatement = "SELECT MinValue FROM PlanProt_FieldValue WHERE FieldID = " + s[i]["FieldID"].ToString();
                        _DropDownData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _DropDownData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _DropDownData.ExecuteInstruction();

                        //cDropDownNew.DataSource = _DropDownData.ResultsDataTable;
                        //cDropDownNew.ValueMember = "MinValue";
                        //cDropDownNew.DisplayMember = "MinValue";
                        cDropDownNew.Items.Clear();
                        foreach (DataRow d in _DropDownData.ResultsDataTable.Rows)
                        {
                            cDropDownNew.Items.Add(d["MinValue"].ToString());

                        }

                        e.RepositoryItem = cDropDownNew;
                        e.ColumnField.Options.AllowEdit = true;
                    }
                    if (theFieldType == "Number")
                    {


                        cNumber.Mask.MaskType = MaskType.Numeric;
                        cNumber.Mask.EditMask = "n0";
                        cNumber.Appearance.TextOptions.HAlignment = HorzAlignment.Far;


                        e.RepositoryItem = cNumber;
                    }
                    if (theFieldType == "Real")
                    {
                        cReal.Mask.MaskType = MaskType.Numeric;
                        cReal.Mask.EditMask = "n2";
                        cReal.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

                        e.RepositoryItem = cReal;
                    }
                    if (theFieldType == "Date")
                    {
                        e.RepositoryItem = cDate;
                    }

                    break;
                }

                //  }
            }

        }


        //public void abc(Keys key)
            
        //{
        //    if(key == Keys .Tab )
        //    {
        //        gridPlanProtData .Cells .FocusedCell = new Point (gridPlanProtData .Cells .FocusedCell .X , gridPlanProtData .Cells .FocusedCell .Y +1 );
        //   // return base.ProcessCmdKey(ref msg, key);
        //    }
        //}
        


        private void gridPlanProtData_FieldValueImageIndex(object sender, PivotFieldImageIndexEventArgs e)
        {
            if (e.Field == ApprovedField && e.ValueType == PivotGridValueType.Value)
            {
                if (e.Value.ToString() == "NO")
                    e.ImageIndex = 0;
                else e.ImageIndex = 1; 
            }

            
        }



        private void gridPlanProtData_ShowingEditor(object sender, CancelPivotCellEditEventArgs e)
        {
            PivotGridControl theview;
            theview = sender as PivotGridControl;


            //object theValue = theview.GetFieldValue(CalcField, e.RowIndex);

            //if (theValue.ToString() == "TRUE")
            //{
            //  e.Cancel = true;
            //}

            object cellValue = theview.GetFieldValue(ApprovedField, e.RowIndex);

            if (cellValue.ToString() == "YES")
            {
                //object cellWPValue = theview.GetFieldValue(WorkplaceField, e.RowIndex);
                //MessageBox.Show("The current value is locked for production month: " + editProdMonth.Text + "  . You can only capture data for a non-approved production month. If you need to change the value please contact the system administrator to unlock workplace: " + cellWPValue.ToString() + " for production month: " + editProdMonth.Text);
                e.Cancel = true;
            }

        }

        private void gridPlanProtData_Click(object sender, EventArgs e)
        {
          
        }

        private void gridPlanProtData_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {
            if (e.ColumnField == theValue)
            {
            e.Appearance.BackColor = Color.Blue;
            }
            if (mainData.Rows .Count  == 0)
            {
                //MessageBox.Show("No Data available to show for selected settings", "", MessageBoxButtons.OK);
                ////break;
                //frmPlanProtCpature abc = new frmPlanProtCpature();
                //abc.Show();
                ucPlanProtDataView ucpp = new ucPlanProtDataView();
                ucpp.Visible = false;
            }
            else
            {

                string exp2 = "FieldRequired=1";
                DataRow[] s3;
                s3 = mainData.Select(exp2);
                string theFieldType = "";

                for (int i = 0; i < s3.Length; i++)
                {  //if (Convert .ToInt32 ( s3[1]["FieldRequired"]) == 1 && s3 [1]["FieldName"]==theFieldName )
                    //{
                    //    e.Appearance.ForeColor = Color.Blue;
                    //   // e.ColumnField.Appearance.Cell.BackColor = Color.Blue;

                    //}
                    // if (s3[i]["FieldName"].ToString() == theFieldName)
                    // {
                    theFieldType = s3[i]["fieldDescription"].ToString();
                    if (theFieldType == "Alpha" || theFieldType == "Real" || theFieldType == "Number")
                    {
                        if (s3[i]["FieldRequired"] is DBNull)
                        {
                            s3[i]["FieldRequired"] = 0;

                        }
                        if (Convert.ToInt32(s3[i]["FieldRequired"]) == 1 && e.GetFieldValue(FieldName) == s3[i]["FieldName"])
                        {
            
                            e.Appearance.BackColor = pcComulsory.BackColor;
                        }
                    }
                }
            }
       }


        private void gridPlanProtData_FocusedCellChanged(object sender, EventArgs e)
        {
           // gridPlanProtData.ShowEditor();

         // object activeEdi =   gridPlanProtData.ActiveEditor;
          if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
          {
              (gridPlanProtData.ActiveEditor as PopupBaseEdit).ShowPopup ();
              gridPlanProtData.ShowEditor();
          }
          gridPlanProtData.ShowEditor();
        }

        private void gridPlanProtData_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraPivotGrid.PivotGridControl view = sender as DevExpress.XtraPivotGrid.PivotGridControl;
            PivotGridControl view1= new PivotGridControl ();
            //gridPlanProtData.ShowEditor();
            object activedi = gridPlanProtData.ActiveEditor;
            if (activedi == "MemoExEdit" && e.KeyData == Keys.Down)
            {
                ComboBoxEdit edit = activedi as ComboBoxEdit;
                edit.ShowPopup();
                e.Handled = true;
            }

           //if(view1 .ActiveEditor as RepositoryItemComboBox   && e.KeyData == Keys .Down )
           //{
           //}

            
           //DevExpress .XtraPivotGrid .ViewInfo .PivotGridViewInfo viewinfo =((IPivotGridViewInfoDataOwner )gridPlanProtData ).DataViewInfo .ViewInfo ;
           // DevExpress .XtraPivotGrid .PivotCellEditEventArgs cellinfo=gridPlanProtData .Cells .GetCellInfo 
           //if (viewinfo .edit  is RepositoryItemComboBox   && e.KeyData == Keys .Down )
              // if(gridPlanProtData .editor
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void gridPlanProtData_ShownEditor(object sender, PivotCellEditEventArgs e)
        {
            e.Edit.SelectAll();
            //if (gridPlanProtData.ToolTipController != null)
            //{
            //    ToolTipController abc = null;
            //    abc = new ToolTipController();
            //    abc.ShowHint(gridPlanProtData.ActiveEditor.Text, PointToScreen(gridPlanProtData.ActiveEditor.Location));
            //  //  gridPlanProtData.ActiveEditor.ToolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(ToolTipController_GetActiveObjectInfo);
            //   // gridPlanProtData.ActiveEditor.ToolTipController.AutoPopDelay = 5000;

            //}
            
           // cText .KeyPress += cText_KeyPress;
           
        }
        
    

        private void ToolTipController_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            
            ToolTipControlInfo info = null;
           // ToolTipController abc = null;
            //if (gridPlanProtData.ActiveEditor is PopupBaseEdit)
            //{
            //    object o = gridPlanProtData.ActiveEditor.Text ;

            //    string text = "Test";

            //    info = new ToolTipControlInfo(o, text);
               
            //    e.Info = info;
            //}
           // cText .KeyPress +=cText_KeyPress;
           
        }

        private void gridPlanProtData_TextChanged(object sender, EventArgs e)
        {
           // gridPlanProtData.ActiveEditor.ToolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(ToolTipController_GetActiveObjectInfo);
        }

        private void gridPlanProtData_Leave(object sender, EventArgs e)
        {
           
            //     if (gridPlanProtData.ActiveEditor.Text == "")
            //{
            //    MessageBox.Show("Please enter some text", "", MessageBoxButtons.OK);
            //}
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridPlanProtData_CustomUnboundFieldData(object sender, CustomFieldDataEventArgs e)
        {
            //     if (e.Field.Caption == "TotalMeters")
            //{
            //    e.Field.ValueFormat.FormatString = "{0:0,0.0}";
            //    e.Field.CellFormat.FormatString = "{0:0,0.0}";
            //}
        }

      


    
//>>>>>>> .r1489
   

    }
}
