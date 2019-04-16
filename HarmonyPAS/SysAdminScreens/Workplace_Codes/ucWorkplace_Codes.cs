using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.Global;
using Mineware.Systems.Global.sysMessages;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionGlobal;
using MWDataManager;

namespace Mineware.Systems.Production.SysAdminScreens.Workplace_Codes
{
	public partial class ucWorkplace_Codes : ucBaseUserControl
	{
		private readonly clsWorkplaceCodes _clsWorkplaceCodes = new clsWorkplaceCodes();
		DataTable GridData;
		public string TheAction;
		DataTable dt = new DataTable();
		public string update = "";
		DataTable dt1 = new DataTable();
		string theAction = "";
		string thewptype = "";
		ileGridEdit myEdit = new ileGridEdit();
		private readonly sysMessagesClass _sysMessagesClass = new sysMessagesClass();
        string aa = "";


        public ucWorkplace_Codes()
		{
			InitializeComponent();
            //aa = myEdit.result.ToString();
            //if (aa == "true")
            //{
            //    LoadGridData();
            //}
        }

		private void panelControl1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void ucWorkplace_Codes_Load(object sender, EventArgs e)
		{
			LoadScreenData();
		}

		public void LoadScreenData()
		{
			_clsWorkplaceCodes.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

			if (rdgrpWPCodes.SelectedIndex == 0)
			{
				gcDivision.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDivision.Visible = true;
				gcWPType.Visible = false;
				gcGrid.Visible = false;
				gcDetail.Visible = false;
				gcNumber.Visible = false;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				btnAdd.Visible = false;
				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
		}

		private void rdgrpWPCodes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (rdgrpWPCodes.SelectedIndex == 0)
			{
				gcDivision.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDivision.Visible = true;
				gcWPType.Visible = false;
				gcGrid.Visible = false;
				gcDetail.Visible = false;
				gcNumber.Visible = false;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				btnAdd.Visible = false;
				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 1)
			{
				gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDivision.Visible = false;
				gcWPType.Visible = true;
				gcGrid.Visible = false;
				gcDetail.Visible = false;
				gcNumber.Visible = false;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				//if (true)
				//{
				//	btnAdd.Visible = true;
				//}
				//else
				//{
					btnAdd.Visible = false;
				//}

				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 2)
			{
				loaddata();

				gcDivision.Visible = false;
				gcWPType.Visible = false;
				gcGrid.Visible = true;
				gcDetail.Visible = false;
				gcNumber.Visible = false;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				if (true)//TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).IsCentralizedDatabase == 1)
				{
					btnAdd.Visible = true;
				}
				else
				{
					btnAdd.Visible = false;
				}
				btnDelete.Visible = true;
				btnEdit.Visible = true;
				separatorControl1.Visible = true;
				// gcWPTSelection.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 3)
			{
				dt1 = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDetail.DataSource = dt1;

				_clsWorkplaceCodes.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				gcDivision.Visible = false;
				gcWPType.Visible = false;
				gcGrid.Visible = false;
				gcDetail.Visible = true;
				gcNumber.Visible = false;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				if (true)
				{
					btnAdd.Visible = true;
				}
				else
				{
					btnAdd.Visible = false;
				}
				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 4)
			{
				gcNumber.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDivision.Visible = false;
				gcWPType.Visible = false;
				gcGrid.Visible = false;
				gcDetail.Visible = false;
				gcNumber.Visible = true;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				if (true)
				{
					btnAdd.Visible = true;
				}
				else
				{
					btnAdd.Visible = false;
				}
				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 5)
			{
				gcDescription.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDivision.Visible = false;
				gcWPType.Visible = false;
				gcGrid.Visible = false;
				gcDetail.Visible = false;
				gcNumber.Visible = false;
				gcDescription.Visible = true;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				if (true)
				{
					btnAdd.Visible = true;
				}
				else
				{
					btnAdd.Visible = false;
				}
				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 6)
			{
				gcDescriptionNr.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDivision.Visible = false;
				gcWPType.Visible = false;
				gcGrid.Visible = false;
				gcDetail.Visible = false;
				gcNumber.Visible = false;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = true;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				if (true)
				{
					btnAdd.Visible = true;
				}
				else
				{
					btnAdd.Visible = false;
				}
				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 7)
			{
				gcInactiveReasons.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDivision.Visible = false;
				gcWPType.Visible = false;
				gcGrid.Visible = false;
				gcDetail.Visible = false;
				gcNumber.Visible = false;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = true;
				gcWPTActivity.Visible = false;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;
				if (true)
				{
					btnAdd.Visible = true;
				}
				else
				{
					btnAdd.Visible = false;
				}
				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 8)
			{
				gcWPTActivity.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
				gcDivision.Visible = false;
				gcWPType.Visible = false;
				gcGrid.Visible = false;
				gcDetail.Visible = false;
				gcNumber.Visible = false;
				gcDescription.Visible = false;
				gcDescriptionNr.Visible = false;
				gcInactiveReasons.Visible = false;
				gcWPTActivity.Visible = true;
				gcDetailSelected.Visible = false;
				gcNumberSelected.Visible = false;
				lblWPType.Visible = false;
				cmbWPTypeSelect.Visible = false;

				//if (true)
				//{
				//    btnAdd.Visible = true;
				//}
				//else
				//{
				btnAdd.Visible = false;
				// }
				btnDelete.Visible = false;
				btnEdit.Visible = false;
				separatorControl1.Visible = false;
			}
			else if (rdgrpWPCodes.SelectedIndex == 9)
			{
				var dtDiv = _clsWorkplaceCodes.Load_WPType();
				if (dtDiv.Rows.Count > 0)
				{
					cmbWPTypeSelect.Properties.DataSource = dtDiv;
					cmbWPTypeSelect.Properties.DisplayMember = "Description";
					cmbWPTypeSelect.Properties.ValueMember = "TypeCode";

				}
				cmbWPTypeSelect.Visible = true;
				cmbWPTypeSelect.Visible = true;
				cmbWPTypeSelect.ItemIndex = -1;

				wpActivity();
			}
		}

		public void wpActivity()
		{

			var sqlDetail = new DataTable();
			var sqlNumber = new DataTable();
			var sqlDirection = new DataTable();
			var sqlDescription = new DataTable();
			var sqlDescripNo = new DataTable();


			if (cmbWPTypeSelect.ItemIndex == -1)
			{
				thewptype = "";
			}

			else
			{
				thewptype = cmbWPTypeSelect.EditValue.ToString();
			}


			var _dbMan1 = new clsDataAccess();
			_dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			_dbMan1.SqlStatement = " SELECT d.DetailCode + ':' + d.Description Detail, \r\n " +
				   "        DetSelected = case when isnull(w.DetailCode,'') <> '' then cast(1 as bit) else cast(0 as bit) end \r\n " +
				   " FROM CODE_WPDetail d  \r\n " +
				   " left outer join CODE_WPTypeWPDetailLink w on  \r\n " +
				   "   w.DetailCode = d.DetailCode and \r\n " +
				   "   w.TypeCode = '" + thewptype + "' ";
			_dbMan1.queryExecutionType = ExecutionType.GeneralSQLStatement;
			_dbMan1.queryReturnType = ReturnType.DataTable;
			_dbMan1.ExecuteInstruction();
			sqlDetail = _dbMan1.ResultsDataTable;


			var _dbMan2 = new clsDataAccess();
			_dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			_dbMan2.SqlStatement = " SELECT n.NumberCode+':'+n.Description Number, \r\n " +
						"        NumSelected = case when isnull(w.NumberCode,'') <> '' then cast(1 as bit) else cast(0 as bit) end \r\n " +
						" FROM CODE_WPNumber n  \r\n " +
						" left outer join CODE_WPTypeWPNumberLink w on  \r\n " +
						"   w.NumberCode = n.NumberCode and \r\n " +
						"   w.TypeCode = '" + thewptype + "' ";
			_dbMan2.queryExecutionType = ExecutionType.GeneralSQLStatement;
			_dbMan2.queryReturnType = ReturnType.DataTable;
			_dbMan2.ExecuteInstruction();
			sqlNumber = _dbMan2.ResultsDataTable;



			var _dbMan3 = new clsDataAccess();
			_dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			_dbMan3.SqlStatement = " SELECT d.Direction+':'+d.Description Direction, \r\n " +
						"        DirSelected = case when isnull(w.Direction,'') <> '' then cast(1 as bit) else cast(0 as bit) end \r\n " +
						" FROM CODE_Direction d  \r\n " +
						" left outer join CODE_WPTypeWPDirectionLink w on  \r\n " +
						"   w.Direction = d.Direction and \r\n " +
						"   w.TypeCode = '" + thewptype + "' ";
			_dbMan3.queryExecutionType = ExecutionType.GeneralSQLStatement;
			_dbMan3.queryReturnType = ReturnType.DataTable;
			_dbMan3.ExecuteInstruction();
			sqlDirection = _dbMan3.ResultsDataTable;


			var _dbMan4 = new clsDataAccess();
			_dbMan4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			_dbMan4.SqlStatement = " SELECT d.DescrCode+':'+d.Description Description, \r\n " +
						"        DesSelected = case when isnull(w.DescrCode,'') <> '' then cast(1 as bit) else cast(0 as bit) end \r\n " +
						" FROM CODE_WPDescription d  \r\n " +
						" left outer join CODE_WPTypeWPDescLink w on  \r\n " +
						"   w.DescrCode = d.DescrCode and \r\n " +
						"   w.TypeCode = '" + thewptype + "' ";
			_dbMan4.queryExecutionType = ExecutionType.GeneralSQLStatement;
			_dbMan4.queryReturnType = ReturnType.DataTable;
			_dbMan4.ExecuteInstruction();
			sqlDescription = _dbMan4.ResultsDataTable;


			var _dbMan5 = new clsDataAccess();
			_dbMan5.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			_dbMan5.SqlStatement = " SELECT d.DescrNumberCode+':'+d.Description DescriptionNo, \r\n " +
						"        DesNoSelected = case when isnull(w.DescrNumberCode,'') <> '' then cast(1 as bit) else cast(0 as bit) end \r\n " +
						" FROM CODE_WPDESCRIPTIONNO d  \r\n " +
						" left outer join CODE_WPTypeWPDescNoLink w on  \r\n " +
						"   w.DescrNumberCode = d.DescrNumberCode and \r\n " +
						"   w.TypeCode = '" + thewptype + "' ";
			_dbMan5.queryExecutionType = ExecutionType.GeneralSQLStatement;
			_dbMan5.queryReturnType = ReturnType.DataTable;
			_dbMan5.ExecuteInstruction();
			sqlDescripNo = _dbMan5.ResultsDataTable;


			var MaxRowCount = 0;
			if (MaxRowCount < sqlDetail.Rows.Count)
				MaxRowCount = sqlDetail.Rows.Count;
			if (MaxRowCount < sqlNumber.Rows.Count)
				MaxRowCount = sqlNumber.Rows.Count;
			if (MaxRowCount < sqlDirection.Rows.Count)
				MaxRowCount = sqlDirection.Rows.Count;
			if (MaxRowCount < sqlDescription.Rows.Count)
				MaxRowCount = sqlDescription.Rows.Count;
			if (MaxRowCount < sqlDescripNo.Rows.Count)
				MaxRowCount = sqlDescripNo.Rows.Count;

			var dt = new DataTable();
			dt.Columns.Add("Number", typeof(string));
			dt.Columns.Add("NumSelected", typeof(bool));
			dt.Columns.Add("Direction", typeof(string));
			dt.Columns.Add("DirSelected", typeof(bool));
			dt.Columns.Add("DescriptionNo", typeof(string));
			dt.Columns.Add("DesNoSelected", typeof(bool));
			dt.Columns.Add("Description", typeof(string));
			dt.Columns.Add("DesSelected", typeof(bool));
			dt.Columns.Add("Detail", typeof(string));
			dt.Columns.Add("DetSelected", typeof(bool));
			gcNumberSelected.DataSource = dt;


			var i = 0;
			for (var j = i; j < MaxRowCount - 1; j++)
			{
				gvNumberSelected.AddNewRow();
				i = i + 1;
			}

			var m = 0;
			foreach (DataRow dr in sqlNumber.Rows)
			{
				gvNumberSelected.SetRowCellValue(m, gvNumberSelected.Columns["Number"], dr["Number"].ToString());
				if (Convert.ToBoolean(dr["NumSelected"]))
				{
					gvNumberSelected.SetRowCellValue(m, gvNumberSelected.Columns["NumSelected"], Convert.ToBoolean(dr["NumSelected"]));
				}
				else
				{
					gvNumberSelected.SetRowCellValue(m, gvNumberSelected.Columns["NumSelected"], Convert.ToBoolean(dr["NumSelected"]));
				}
				m = m + 1;
			}

			var n = 0;
			foreach (DataRow dr in sqlDirection.Rows)
			{
				gvNumberSelected.SetRowCellValue(n, gvNumberSelected.Columns["Direction"], dr["Direction"].ToString());
				if (Convert.ToBoolean(dr["DirSelected"]))
				{
					gvNumberSelected.SetRowCellValue(n, gvNumberSelected.Columns["DirSelected"], Convert.ToBoolean(dr["DirSelected"]));
				}
				else
				{
					gvNumberSelected.SetRowCellValue(n, gvNumberSelected.Columns["DirSelected"], Convert.ToBoolean(dr["DirSelected"]));
				}
				n = n + 1;
			}
			var p = 0;
			foreach (DataRow dr in sqlDescripNo.Rows)
			{
				gvNumberSelected.SetRowCellValue(p, gvNumberSelected.Columns["DescriptionNo"], dr["DescriptionNo"].ToString());
				if (Convert.ToBoolean(dr["DesNoSelected"]))
				{
					gvNumberSelected.SetRowCellValue(p, gvNumberSelected.Columns["DesNoSelected"], Convert.ToBoolean(dr["DesNoSelected"]));
				}
				else
				{
					gvNumberSelected.SetRowCellValue(p, gvNumberSelected.Columns["DesNoSelected"], Convert.ToBoolean(dr["DesNoSelected"]));
				}
				p = p + 1;
			}

			var q = 0;
			foreach (DataRow dr in sqlDescription.Rows)
			{
				gvNumberSelected.SetRowCellValue(q, gvNumberSelected.Columns["Description"], dr["Description"].ToString());
				if (Convert.ToBoolean(dr["DesSelected"]))
				{
					gvNumberSelected.SetRowCellValue(q, gvNumberSelected.Columns["DesSelected"], Convert.ToBoolean(dr["DesSelected"]));
				}
				else
				{
					gvNumberSelected.SetRowCellValue(q, gvNumberSelected.Columns["DesSelected"], Convert.ToBoolean(dr["DesSelected"]));
				}
				q = q + 1;
			}

			var r = 0;
			foreach (DataRow dr in sqlDetail.Rows)
			{
				gvNumberSelected.SetRowCellValue(r, gvNumberSelected.Columns["Detail"], dr["Detail"].ToString());
				if (Convert.ToBoolean(dr["DetSelected"]))
				{
					gvNumberSelected.SetRowCellValue(r, gvNumberSelected.Columns["DetSelected"], Convert.ToBoolean(dr["DetSelected"]));
				}
				else
				{
					gvNumberSelected.SetRowCellValue(r, gvNumberSelected.Columns["DetSelected"], Convert.ToBoolean(dr["DetSelected"]));
				}
				r = r + 1;
			}

			gvNumberSelected.UpdateCurrentRow();
			dt.AcceptChanges();
			gvNumberSelected.FocusedRowHandle = 0;
			gcDivision.Visible = false;
			gcWPType.Visible = false;
			gcGrid.Visible = false;
			gcDetail.Visible = false;
			gcNumber.Visible = false;
			gcDescription.Visible = false;
			gcDescriptionNr.Visible = false;
			gcInactiveReasons.Visible = false;
			gcWPTActivity.Visible = false;
			gcDetailSelected.Visible = false;
			gcNumberSelected.Visible = true;
			lblWPType.Visible = true;
			cmbWPTypeSelect.Visible = true;
			//if (true)
			//{
			//    btnAdd.Visible = true;
			//}
			//else
			//{
			btnAdd.Visible = false;
			//}
			btnDelete.Visible = false;
			btnEdit.Visible = false;
			separatorControl1.Visible = true;

		}

		private void gvGrid_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			TheAction = "Add";
			myEdit.lkpDivision.EditValue = "";
			myEdit.lkpDivision.Enabled = true;
			myEdit.txtGrid.Text = "";
			myEdit.txtDescription.Text = "";
			myEdit.txtGrid.Enabled = true;
			myEdit.txtCostarea.Text = "";
			myEdit.update = "Add";
			myEdit.gcGridEdit.DataSource = _clsWorkplaceCodes.loadGridEdit(TheAction, "", "");
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DialogResult result;
			result = MessageBox.Show("Are you sure you want to delete record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

			if (result == DialogResult.Yes)
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;

				var strDescription = "";
				var strCode = "";
				bool strSelected;
				var code = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Code"]).ToString();
				strCode = Convert.ToString(code);
				var Description = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Description"]).ToString();
				strDescription = Convert.ToString(Description);
				var sb = new StringBuilder();

				sb.AppendLine("select count(*) from WORKPLACE");
				sb.AppendLine("where GridCode = '" + strCode + "'");

				_dbMan.SqlStatement = sb.ToString();
				_dbMan.ExecuteInstruction();

				if (Convert.ToInt32(_dbMan.ResultsDataTable.Rows[0][0]) != 0)
				{
					MessageBox.Show("Workplaces are currently linked to this code. Please remove them before you can delete this code.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				sb.Clear();

				sb.AppendLine("delete CODE_Grid");
				sb.AppendLine("where Description = '" + strDescription + "'");
				sb.AppendLine("and Grid = '" + strCode + "'");

				_dbMan.SqlStatement = sb.ToString();
				_dbMan.ExecuteInstruction();
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("DELETED DATA", "All data was deleted successfully", System.Drawing.Color.Blue);
                loaddata();

                
                ///loadData(editProdmonth.Text);
            }
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (rdgrpWPCodes.SelectedIndex == 2)
			{
				TheAction = "Add";
				myEdit.lkpDivision.Enabled = true;
				myEdit.txtGrid.Enabled = true;
				gvGrid.AddNewRow();

				gvGrid.InitNewRow += gvGrid_InitNewRow;
				gvGrid.ShowEditForm();
			}
			else
			{
				switch (rdgrpWPCodes.SelectedIndex)
				{

					case 1:

						gvWPType.AddNewRow();
						gvWPType.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
						gvWPType.InitNewRow += gvWPType_InitNewRow;

						gvWPType.ShowEditForm();

						SetColumnEditability();
						break;
					case 3:
						gvDetail.AddNewRow();
						gvDetail.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
						gvDetail.InitNewRow += gvDetail_InitNewRow;
						gvDetail.ShowEditForm();
						break;
					case 4:
						gvNumber.AddNewRow();
						gvNumber.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
						gvNumber.InitNewRow += gvNumber_InitNewRow;
						gvNumber.ShowEditForm();
						break;
					case 5:
						gvDescription.AddNewRow();
						gvDescription.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
						gvDescription.InitNewRow += gvDescription_InitNewRow;
						gvDescription.ShowEditForm();
						break;
					case 6:
						gvDescriptionNr.AddNewRow();
						gvDescriptionNr.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
						gvDescriptionNr.InitNewRow += gvDescriptionNr_InitNewRow;
						gvDescriptionNr.ShowEditForm();
						break;
					case 7:
						gvInactiveReasons.AddNewRow();
						gvInactiveReasons.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
						gvInactiveReasons.InitNewRow += gvInactiveReasons_InitNewRow;
						gvInactiveReasons.ShowEditForm();
						break;

				}
			}
		}

		private bool? GetIsCodeInUse(string codeFilter)
		{
			if (string.IsNullOrEmpty(codeFilter))
			{
				throw new InvalidOperationException("CodeFilter required");
			}

			var sb = new StringBuilder();
			sb.AppendLine("Select Count(w.WorkplaceId) WorkplaceCount");
			sb.AppendLine("From Workplace w ");
			sb.AppendLine(codeFilter);

			var dbMan = new clsDataAccess
			{
				ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection),
				queryExecutionType = ExecutionType.GeneralSQLStatement,
				queryReturnType = ReturnType.DataTable,
				SqlStatement = sb.ToString()
			};
			var executionResult = dbMan.ExecuteInstruction();
			if (executionResult.success)
			{
				var table = dbMan.ResultsDataTable;
				return table.Rows[0][0].ToString() != "0";
			}
			
			_sysMessagesClass.viewMessage(Global.MessageType.Info, "ERROR LOADING DATA", this.theSystemTag, "ucWorkplace_Codes", "GetIsCodeInUse", executionResult.Message, Global.ButtonTypes.OK, Global.MessageDisplayType.Small);
			return null;
		}

		private void SetColumnEditability()
		{
			switch (rdgrpWPCodes.SelectedIndex)
			{
				case 1:
					var rowNumber = gvWPType.FocusedRowHandle;
					var row = gvWPType.GetRow(rowNumber);
					var code = gvWPType.GetRowCellValue(rowNumber, gvGrid.Columns["Code"]).ToString();
					var inUse = GetIsCodeInUse(string.Format("Where w.TypeCode = '{0}'", code));
					if (inUse != null && !inUse.Value)
					{
						rpSTPCode.ReadOnly = false;
						rpSTPCode.Enabled = true;
						colWPTCode.OptionsColumn.AllowEdit = true;
						colWPTCode.OptionsColumn.ReadOnly = false;
						colWPTDescription.OptionsColumn.AllowEdit = true;
						colWPTDescription.OptionsColumn.ReadOnly = false;
					}
					else
					{
						rpSTPCode.ReadOnly = true;
						rpSTPCode.Enabled = false;
						colWPTCode.OptionsColumn.AllowEdit = false;
						colWPTCode.OptionsColumn.ReadOnly = true;
						colWPTDescription.OptionsColumn.AllowEdit = false;
						colWPTDescription.OptionsColumn.ReadOnly = true;
					}
					break;
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			theAction = "";
			if (rdgrpWPCodes.SelectedIndex != 2)
			{
				switch (rdgrpWPCodes.SelectedIndex)
				{

					case 1:
						SetColumnEditability();
						// gvWPType.AddNewRow();
						gvWPType.ShowEditForm();
						break;
					case 3:

						colDETSelected.OptionsColumn.AllowEdit = false;
						colDETSelected.OptionsColumn.ReadOnly = true;
						colDETInactive.OptionsColumn.AllowEdit = false;
						colDETInactive.OptionsColumn.ReadOnly = true;
						gvDetail.ShowEditForm();
						break;
					case 4:

						break;
					case 5:

						break;
					case 6:

						break;
					case 7:

						break;

				}
			}
			else
			{
				TheAction = "Edit";

				myEdit.lkpDivision.Enabled = false;
				myEdit.txtGrid.Enabled = false;
				var Division = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Division"]).ToString();
				myEdit.lkpDivision.EditValue = Convert.ToString(Division);
				myEdit.lkpDivision.Enabled = false;
				var code = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Code"]).ToString();
				myEdit.txtGrid.Text = Convert.ToString(code);
				myEdit.txtGrid.Enabled = false;
				var Description = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Description"]).ToString();
				myEdit.txtDescription.Text = Convert.ToString(Description);
				var CostArea = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["CostArea"]).ToString();
				myEdit.txtCostarea.Text = Convert.ToString(CostArea);
				var gridedit = new DataTable();
				gridedit = _clsWorkplaceCodes.loadGridEdit(TheAction, Convert.ToString(Division), Convert.ToString(code));
				myEdit.update = "Edit";
				myEdit.gcGridEdit.DataSource = gridedit;
				gvGrid.ShowEditForm();
			}
		}

		private void gvGrid_DoubleClick(object sender, EventArgs e)
		{
			TheAction = "Edit";
			myEdit.lkpDivision.Enabled = false;
			myEdit.txtGrid.Enabled = false;

			var Division = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Division"]).ToString();
			myEdit.lkpDivision.EditValue = Convert.ToString(Division);
			var code = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Code"]).ToString();
			myEdit.txtGrid.Text = Convert.ToString(code);
			var Description = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["Description"]).ToString();
			myEdit.txtDescription.Text = Convert.ToString(Description);
			var CostArea = gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, gvGrid.Columns["CostArea"]).ToString();
			myEdit.txtCostarea.Text = Convert.ToString(CostArea);
			myEdit.update = "Edit";
			var dt = new DataTable();
			dt = _clsWorkplaceCodes.loadGridEdit(TheAction, Convert.ToString(Division), Convert.ToString(code));
			myEdit.gcGridEdit.DataSource = dt;
			gvGrid.ShowEditForm();

		}

		public void loaddata()
		{
            gcGrid.DataSource = null;
            _clsWorkplaceCodes.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			dt = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
			gcGrid.DataSource = dt;

			myEdit.UserCurrentInfo = UserCurrentInfo;
			myEdit.theSystemDBTag = theSystemDBTag;
			gvGrid.OptionsEditForm.CustomEditFormLayout = myEdit;
            //myEdit.Show();
		}

		private void gvDivison_RowUpdated(object sender, RowObjectEventArgs e)
		{
			var sel = gvDivison.GetRowCellValue(gvDivison.FocusedRowHandle, gvDivison.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var wpcode = gvDivison.GetRowCellValue(gvDivison.FocusedRowHandle, gvDivison.Columns["Code"]);
			var WPCode = Convert.ToString(wpcode);

			var selected = "";
			if (SEL)
			{
				selected = "Y";
			}
			else
			{
				selected = "N";
			}
			var _dbMan = new clsDataAccess();
			_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
			_dbMan.SqlStatement = "UPDATE CODE_WPDivision SET Selected = '" + selected + "' WHERE DivisionCode = '" + WPCode + "'";
			_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
			_dbMan.queryReturnType = ReturnType.DataTable;
			_dbMan.ExecuteInstruction();
		}

		private void gvWPType_RowUpdated(object sender, RowObjectEventArgs e)
		{
			bool SEL;
			var WPCode = "";
			var Description = "";
			bool INACT;
			bool CAL;

			if (gvWPType.IsNewItemRow(e.RowHandle))
			{
				object wpcode = e.Row as DataRowView;
				WPCode = Convert.ToString(((DataRowView)wpcode)["Code"]);
				if (WPCode == "")
				{

					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";

					return;
				}
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "select * from CODE_WPTYPE where typecode='" + WPCode + "'";// (TypeCode,Description,Selected,Inactive,Classification)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "','" + Classification + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				if (_dbMan.ResultsDataTable.Rows.Count > 0)
				{
					MessageBox.Show("Code already exists", "", MessageBoxButtons.OK);
					return;
				}
			}
			else
			{
				var wpcode = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Code"]);
				WPCode = Convert.ToString(wpcode);

			}
			if (gvWPType.IsNewItemRow(e.RowHandle))
			{
				object desc = e.Row as DataRowView;
				Description = Convert.ToString(((DataRowView)desc)["Description"]);
				if (Description == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var desc = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Description"]);
				Description = Convert.ToString(desc);
			}

			if (gvWPType.IsNewItemRow(e.RowHandle))
			{
				object sel = e.Row as DataRowView;
				SEL = Convert.ToBoolean(((DataRowView)sel)["Selected"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var sel = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Selected"]);
				SEL = Convert.ToBoolean(sel);
			}

			if (gvWPType.IsNewItemRow(e.RowHandle))
			{
				object inact = e.Row as DataRowView;
				INACT = Convert.ToBoolean(((DataRowView)inact)["Inactive"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}

			}
			else
			{
				var inact = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Inactive"]);
				INACT = Convert.ToBoolean(inact);
			}

			if (gvWPType.IsNewItemRow(e.RowHandle))
			{
				object cal = e.Row as DataRowView;
				CAL = Convert.ToBoolean(((DataRowView)cal)["Classification"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}

			}
			else
			{
				var cal = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Classification"]);
				CAL = Convert.ToBoolean(cal);
			}

			var selected = "";
			var Inactive = "";
			var Classification = "";
			if (SEL)
			{
				selected = "Y";
			}
			else
			{
				selected = "N";
			}

			if (INACT)
			{
				Inactive = "Y";
			}
			else
			{
				Inactive = "N";
			}
			if (CAL)
			{
				Classification = "Y";
			}
			else
			{
				Classification = "N";
			}

			if (gvWPType.FocusedRowHandle < 0)
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "insert into CODE_WPTYPE (TypeCode,Description,Selected,Inactive,Classification)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "','" + Classification + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				theAction = "";
			}
			else
			{


				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "UPDATE CODE_WPTYPE SET Selected = '" + selected + "',Inactive='" + Inactive + "',Classification='" + Classification + "'  WHERE TypeCode = '" + WPCode + "'";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
			}

		}

		private void gvDetail_RowUpdated(object sender, RowObjectEventArgs e)
		{
			dt1.AcceptChanges();
			bool SEL;
			var WPCode = "";
			var Description = "";
			bool INACT;

			if (gvDetail.IsNewItemRow(e.RowHandle))
			{
				object wpcode = e.Row as DataRowView;
				WPCode = Convert.ToString(((DataRowView)wpcode)["Code"]);
				if (WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcDetail.DataSource = "";
					gcDetail.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "select * from CODE_WPDETAIL where typecode='" + WPCode + "'";// (TypeCode,Description,Selected,Inactive,Classification)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "','" + Classification + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				if (_dbMan.ResultsDataTable.Rows.Count > 0)
				{
					MessageBox.Show("Code already exists", "", MessageBoxButtons.OK);
					return;
				}
			}
			else
			{
				var wpcode = gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Code"]);
				WPCode = Convert.ToString(wpcode);
			}
			if (gvDetail.IsNewItemRow(e.RowHandle))
			{
				object desc = e.Row as DataRowView;
				Description = Convert.ToString(((DataRowView)desc)["Description"]);
				if (Description == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description", ButtonTypes.OK, MessageDisplayType.Small);
					gcDetail.DataSource = "";
					gcDetail.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var desc = gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Description"]);
				Description = Convert.ToString(desc);
			}

			if (gvDetail.IsNewItemRow(e.RowHandle))
			{
				object sel = e.Row as DataRowView;
				SEL = Convert.ToBoolean(((DataRowView)sel)["Selected"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var sel = gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Selected"]);
				SEL = Convert.ToBoolean(sel);
			}

			if (gvDetail.IsNewItemRow(e.RowHandle))
			{
				object inact = e.Row as DataRowView;
				INACT = Convert.ToBoolean(((DataRowView)inact)["Inactive"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var inact = gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Inactive"]);
				INACT = Convert.ToBoolean(inact);
			}

			var selected = "";
			var Inactive = "";
			if (SEL)
			{
				selected = "Y";
			}
			else
			{
				selected = "N";
			}

			if (INACT)
			{
				Inactive = "Y";
			}
			else
			{
				Inactive = "N";
			}
			if (gvDetail.FocusedRowHandle < 0)
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "insert into CODE_WPDETAIL (DetailCode,Description,Selected,Inactive)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				theAction = "";
			}
			else
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "UPDATE CODE_WPDETAIL SET Selected = '" + selected + "',Inactive='" + Inactive + "' WHERE DetailCode = '" + WPCode + "'";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
			}

		}

		private void gvNumber_RowUpdated(object sender, RowObjectEventArgs e)
		{
			bool SEL;
			var WPCode = "";
			var Description = "";
			bool INACT;

			if (gvNumber.IsNewItemRow(e.RowHandle))
			{
				object wpcode = e.Row as DataRowView;
				WPCode = Convert.ToString(((DataRowView)wpcode)["Code"]);
				if (WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcNumber.DataSource = "";
					gcNumber.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "select * from CODE_WPNUMBER where NumberCode='" + WPCode + "'";// (TypeCode,Description,Selected,Inactive,Classification)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "','" + Classification + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				if (_dbMan.ResultsDataTable.Rows.Count > 0)
				{
					MessageBox.Show("Code already exists", "", MessageBoxButtons.OK);
					return;
				}

			}
			else
			{
				var wpcode = gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Code"]);
				WPCode = Convert.ToString(wpcode);
			}
			if (gvNumber.IsNewItemRow(e.RowHandle))
			{
				object desc = e.Row as DataRowView;
				Description = Convert.ToString(((DataRowView)desc)["Description"]);
				if (Description == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description", ButtonTypes.OK, MessageDisplayType.Small);
					gcNumber.DataSource = "";
					gcNumber.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var desc = gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Description"]);
				Description = Convert.ToString(desc);
			}

			if (gvNumber.IsNewItemRow(e.RowHandle))
			{
				object sel = e.Row as DataRowView;
				SEL = Convert.ToBoolean(((DataRowView)sel)["Selected"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var sel = gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Selected"]);
				SEL = Convert.ToBoolean(sel);
			}


			if (gvNumber.IsNewItemRow(e.RowHandle))
			{
				object inact = e.Row as DataRowView;
				INACT = Convert.ToBoolean(((DataRowView)inact)["Inactive"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var inact = gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Inactive"]);
				INACT = Convert.ToBoolean(inact);
			}

			var selected = "";
			var Inactive = "";
			if (SEL)
			{
				selected = "Y";
			}
			else
			{
				selected = "N";
			}

			if (INACT)
			{
				Inactive = "Y";
			}
			else
			{
				Inactive = "N";
			}
			if (gvNumber.FocusedRowHandle < 0)
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "insert into CODE_WPNUMBER (NumberCode,Description,Selected,Inactive)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				theAction = "";
			}
			else
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "UPDATE CODE_WPNUMBER SET Selected = '" + selected + "',Inactive='" + Inactive + "' WHERE NumberCode = '" + WPCode + "'";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
			}
		}

		private void gvDescription_RowUpdated(object sender, RowObjectEventArgs e)
		{
			bool SEL;
			var WPCode = "";
			var Description = "";
			bool INACT;

			if (gvDescription.IsNewItemRow(e.RowHandle))
			{
				object wpcode = e.Row as DataRowView;
				WPCode = Convert.ToString(((DataRowView)wpcode)["Code"]);
				if (WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcDescription.DataSource = "";
					gcDescription.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "select * from CODE_WPDESCRIPTION where DescrCode='" + WPCode + "'";// (TypeCode,Description,Selected,Inactive,Classification)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "','" + Classification + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				if (_dbMan.ResultsDataTable.Rows.Count > 0)
				{
					MessageBox.Show("Code already exists", "", MessageBoxButtons.OK);
					return;
				}
			}
			else
			{
				var wpcode = gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Code"]);
				WPCode = Convert.ToString(wpcode);
			}
			if (gvDescription.IsNewItemRow(e.RowHandle))
			{
				object desc = e.Row as DataRowView;
				Description = Convert.ToString(((DataRowView)desc)["Description"]);
				if (Description == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description", ButtonTypes.OK, MessageDisplayType.Small);
					gcDescription.DataSource = "";
					gcDescription.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var desc = gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Description"]);
				Description = Convert.ToString(desc);
			}

			if (gvDescription.IsNewItemRow(e.RowHandle))
			{
				object sel = e.Row as DataRowView;
				SEL = Convert.ToBoolean(((DataRowView)sel)["Selected"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var sel = gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Selected"]);
				SEL = Convert.ToBoolean(sel);
			}


			if (gvDescription.IsNewItemRow(e.RowHandle))
			{
				object inact = e.Row as DataRowView;
				INACT = Convert.ToBoolean(((DataRowView)inact)["Inactive"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var inact = gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Inactive"]);
				INACT = Convert.ToBoolean(inact);
			}

			var selected = "";
			var Inactive = "";
			if (SEL)
			{
				selected = "Y";
			}
			else
			{
				selected = "N";
			}

			if (INACT)
			{
				Inactive = "Y";
			}
			else
			{
				Inactive = "N";
			}
			if (gvDescription.FocusedRowHandle < 0)
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "insert into CODE_WPDESCRIPTION (DescrCode,Description,Selected,Inactive)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				theAction = "";
			}
			else
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "UPDATE CODE_WPDESCRIPTION SET Selected = '" + selected + "',Inactive='" + Inactive + "' WHERE DescrCode = '" + WPCode + "'";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
			}
		}

		private void gvDescriptionNr_RowUpdated(object sender, RowObjectEventArgs e)
		{
			bool SEL;
			var WPCode = "";
			var Description = "";
			bool INACT;

			if (gvDescriptionNr.IsNewItemRow(e.RowHandle))
			{
				object wpcode = e.Row as DataRowView;
				WPCode = Convert.ToString(((DataRowView)wpcode)["Code"]);
				if (WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcDescriptionNr.DataSource = "";
					gcDescriptionNr.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "select * from CODE_WPDESCRIPTIONNO where DescrNumberCode='" + WPCode + "'";// (TypeCode,Description,Selected,Inactive,Classification)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "','" + Classification + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				if (_dbMan.ResultsDataTable.Rows.Count > 0)
				{
					MessageBox.Show("Code already exists", "", MessageBoxButtons.OK);
					return;
				}
			}
			else
			{
				var wpcode = gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Code"]);
				WPCode = Convert.ToString(wpcode);
			}
			if (gvDescriptionNr.IsNewItemRow(e.RowHandle))
			{
				object desc = e.Row as DataRowView;
				Description = Convert.ToString(((DataRowView)desc)["Description"]);
				if (Description == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description", ButtonTypes.OK, MessageDisplayType.Small);
					gcDescriptionNr.DataSource = "";
					gcDescriptionNr.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var desc = gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Description"]);
				Description = Convert.ToString(desc);
			}

			if (gvDescriptionNr.IsNewItemRow(e.RowHandle))
			{
				object sel = e.Row as DataRowView;
				SEL = Convert.ToBoolean(((DataRowView)sel)["Selected"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var sel = gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Selected"]);
				SEL = Convert.ToBoolean(sel);
			}

			if (gvDescriptionNr.IsNewItemRow(e.RowHandle))
			{
				object inact = e.Row as DataRowView;
				INACT = Convert.ToBoolean(((DataRowView)inact)["Inactive"]);
				if (Description == "" || WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var inact = gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Inactive"]);
				INACT = Convert.ToBoolean(inact);
			}

			var selected = "";
			var Inactive = "";
			if (SEL)
			{
				selected = "Y";
			}
			else
			{
				selected = "N";
			}

			if (INACT)
			{
				Inactive = "Y";
			}
			else
			{
				Inactive = "N";
			}
			if (gvDescriptionNr.FocusedRowHandle < 0)
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "insert into CODE_WPDESCRIPTIONNO (DescrNumberCode,Description,Selected,Inactive)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				theAction = "";
			}
			else
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "UPDATE CODE_WPDESCRIPTIONNO SET Selected = '" + selected + "',Inactive='" + Inactive + "' WHERE DescrNumberCode = '" + WPCode + "'";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
			}
		}

		private void gvInactiveReasons_RowUpdated(object sender, RowObjectEventArgs e)
		{
			bool SEL;
			var WPCode = "";
			var Description = "";
			var REASON = "";
			bool INACT;

			//if (gvInactiveReasons.IsNewItemRow(e.RowHandle))
			//{
			//    object reason = e.Row as DataRowView;
			//    REASON = Convert.ToString(((DataRowView)reason)["Reason"]);
			//    if (REASON == "")
			//    {
			//        _sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Code", ButtonTypes.OK, MessageDisplayType.Small);
			//        gcInactiveReasons.DataSource = "";
			//        gcInactiveReasons.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
			//        theAction = "";
			//        return;
			//    }
			//}
			//else
			//{
			//    object reason = gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Reason"]);
			//    REASON = Convert.ToString(reason);
			//}

			if (gvInactiveReasons.IsNewItemRow(e.RowHandle))
			{
				object wpcode = e.Row as DataRowView;
				WPCode = Convert.ToString(((DataRowView)wpcode)["Code"]);
				if (WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcInactiveReasons.DataSource = "";
					gcInactiveReasons.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "select * from WorkPlace_Inactivation_Reason where Reason='" + WPCode + "'";// (TypeCode,Description,Selected,Inactive,Classification)values('" + WPCode + "','" + Description + "','" + selected + "','" + Inactive + "','" + Classification + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				if (_dbMan.ResultsDataTable.Rows.Count > 0)
				{
					MessageBox.Show("Code already exists", "", MessageBoxButtons.OK);
					return;
				}
			}
			else
			{
				var wpcode = gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Code"]);
				WPCode = Convert.ToString(wpcode);
			}

			if (gvInactiveReasons.IsNewItemRow(e.RowHandle))
			{
				object sel = e.Row as DataRowView;
				SEL = Convert.ToBoolean(((DataRowView)sel)["Selected"]);
				if (WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var sel = gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Selected"]);
				SEL = Convert.ToBoolean(sel);
			}

			if (gvInactiveReasons.IsNewItemRow(e.RowHandle))
			{
				object inact = e.Row as DataRowView;
				INACT = Convert.ToBoolean(((DataRowView)inact)["Inactive"]);
				if (WPCode == "")
				{
					_sysMessagesClass.viewMessage(MessageType.Info, "Info", "Please enter the Description/Code", ButtonTypes.OK, MessageDisplayType.Small);
					gcWPType.DataSource = "";
					gcWPType.DataSource = _clsWorkplaceCodes.getData(rdgrpWPCodes.SelectedIndex);
					theAction = "";
					return;
				}
			}
			else
			{
				var inact = gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Inactive"]);
				INACT = Convert.ToBoolean(inact);
			}

			var selected = "";
			var Inactive = "";
			if (SEL)
			{
				selected = "Y";
			}
			else
			{
				selected = "N";
			}

			if (INACT)
			{
				Inactive = "Y";
			}
			else
			{
				Inactive = "N";
			}
			if (gvInactiveReasons.FocusedRowHandle < 0)
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "insert into WorkPlace_Inactivation_Reason (Reason,Selected,Inactive)values('" + WPCode + "','" + selected + "','" + Inactive + "')";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
				theAction = "";
			}
			else
			{
				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.SqlStatement = "UPDATE WorkPlace_Inactivation_Reason SET Selected = '" + selected + "',Inactive='" + Inactive + "' WHERE Reason = '" + WPCode + "'";
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.ExecuteInstruction();
			}
		}

		private void gvWPType_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			var rowhand = gvWPType.FocusedRowHandle;
			// if (rowhand >= 0)
			if (theAction == "")
			{
				colSTPSelected.OptionsColumn.AllowEdit = true;
				colSTPSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colSTPInactive.OptionsColumn.AllowEdit = true;
					colSTPInactive.OptionsColumn.ReadOnly = false;
					colSTPClassification.OptionsColumn.AllowEdit = true;
					colSTPClassification.OptionsColumn.ReadOnly = false;
				}
				else
				{
					colSTPInactive.OptionsColumn.AllowEdit = false;
					colSTPInactive.OptionsColumn.ReadOnly = true;
					colSTPClassification.OptionsColumn.AllowEdit = false;
					colSTPClassification.OptionsColumn.ReadOnly = true;
				}

				var rowNumber = gvWPType.FocusedRowHandle;
				var code = gvWPType.GetRowCellValue(rowNumber, gvGrid.Columns["Code"]).ToString();
				var inUse = GetIsCodeInUse(string.Format("Where w.TypeCode = '{0}'", code));
				if (inUse != null && !inUse.Value)
				{
					colWPTCode.OptionsColumn.AllowEdit = true;
					colWPTCode.OptionsColumn.ReadOnly = false;
					colWPTDescription.OptionsColumn.AllowEdit = true;
					colWPTDescription.OptionsColumn.ReadOnly = false;
					return;
				}

				colWPTCode.OptionsColumn.AllowEdit = false;
				colWPTCode.OptionsColumn.ReadOnly = true;
				colWPTDescription.OptionsColumn.AllowEdit = false;
				colWPTDescription.OptionsColumn.ReadOnly = true;
			}
			if (theAction == "Add")
			//  if (rowhand < 0)
			{
				colSTPSelected.OptionsColumn.AllowEdit = true;
				colSTPSelected.OptionsColumn.ReadOnly = false;
				colSTPInactive.OptionsColumn.AllowEdit = true;
				colSTPInactive.OptionsColumn.ReadOnly = false;
				colSTPClassification.OptionsColumn.AllowEdit = true;
				colSTPClassification.OptionsColumn.ReadOnly = false;
				colWPTCode.OptionsColumn.AllowEdit = true;
				colWPTCode.OptionsColumn.ReadOnly = false;
				colWPTDescription.OptionsColumn.AllowEdit = true;
				colWPTDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvWPType_RowCellClick(object sender, RowCellClickEventArgs e)
		{

		}

		private void gvWPType_RowClick(object sender, RowClickEventArgs e)
		{
			var rowhand = gvWPType.FocusedRowHandle;
			//if (rowhand >= 0)
			if (theAction == "")
			{
				theAction = "";
				colSTPSelected.OptionsColumn.AllowEdit = true;
				colSTPSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colSTPInactive.OptionsColumn.AllowEdit = true;
					colSTPInactive.OptionsColumn.ReadOnly = false;
					colSTPClassification.OptionsColumn.AllowEdit = true;
					colSTPClassification.OptionsColumn.ReadOnly = false;
				}
				else
				{
					colSTPInactive.OptionsColumn.AllowEdit = false;
					colSTPInactive.OptionsColumn.ReadOnly = true;
					colSTPClassification.OptionsColumn.AllowEdit = false;
					colSTPClassification.OptionsColumn.ReadOnly = true;
				}
				//colSTPInactive.OptionsColumn.AllowEdit = true;
				//colSTPInactive.OptionsColumn.ReadOnly = false;
				//colSTPClassification.OptionsColumn.AllowEdit = true;
				//colSTPClassification.OptionsColumn.ReadOnly = false;
				colWPTCode.OptionsColumn.AllowEdit = false;
				colWPTCode.OptionsColumn.ReadOnly = true;
				colWPTDescription.OptionsColumn.AllowEdit = false;
				colWPTDescription.OptionsColumn.ReadOnly = true;
			}
			if (theAction == "Add")
			//if (rowhand < 0)
			{
				theAction = "Add";
				colSTPSelected.OptionsColumn.AllowEdit = true;
				colSTPSelected.OptionsColumn.ReadOnly = false;
				colSTPInactive.OptionsColumn.AllowEdit = true;
				colSTPInactive.OptionsColumn.ReadOnly = false;
				colSTPClassification.OptionsColumn.AllowEdit = true;
				colSTPClassification.OptionsColumn.ReadOnly = false;
				colWPTCode.OptionsColumn.AllowEdit = true;
				colWPTCode.OptionsColumn.ReadOnly = false;
				colWPTDescription.OptionsColumn.AllowEdit = true;
				colWPTDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void rpSTPInactive_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Inactive"]);
				gvWPType.SetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Selected"]);
				gvWPType.SetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Selected"], false);
			}
		}

		private void rpSTPSelected_CheckedChanged(object sender, EventArgs e)
		{

			var sel = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Inactive"]);
				gvWPType.SetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvWPType.GetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Selected"]);
				gvWPType.SetRowCellValue(gvWPType.FocusedRowHandle, gvWPType.Columns["Selected"], false);
			}
		}

		private void rpDETSelected_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Inactive"]);
				gvDetail.SetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Selected"]);
				gvDetail.SetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Selected"], false);
			}
		}

		private void rpDETInactive_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Inactive"]);
				gvDetail.SetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvDetail.GetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Selected"]);
				gvDetail.SetRowCellValue(gvDetail.FocusedRowHandle, gvDetail.Columns["Selected"], false);
			}
		}

		private void rpNUMSelected_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Inactive"]);
				gvNumber.SetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Selected"]);
				gvNumber.SetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Selected"], false);
			}
		}

		private void rpNUMInactive_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Inactive"]);
				gvNumber.SetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvNumber.GetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Selected"]);
				gvNumber.SetRowCellValue(gvNumber.FocusedRowHandle, gvNumber.Columns["Selected"], false);
			}
		}

		private void rpDESSelected_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Inactive"]);
				gvDescription.SetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Selected"]);
				gvDescription.SetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Selected"], false);
			}
		}

		private void rpDESInactive_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Inactive"]);
				gvDescription.SetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvDescription.GetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Selected"]);
				gvDescription.SetRowCellValue(gvDescription.FocusedRowHandle, gvDescription.Columns["Selected"], false);
			}
		}

		private void rpINRSelected_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Inactive"]);
				gvInactiveReasons.SetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Selected"]);
				gvInactiveReasons.SetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Selected"], false);
			}
		}

		private void rpINRInactive_CheckedChanged(object sender, EventArgs e)
		{

			var sel = gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Inactive"]);
				gvInactiveReasons.SetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvInactiveReasons.GetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Selected"]);
				gvInactiveReasons.SetRowCellValue(gvInactiveReasons.FocusedRowHandle, gvInactiveReasons.Columns["Selected"], false);
			}
		}

		private void rpDESNRSelected_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Inactive"]);
				gvDescriptionNr.SetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Selected"]);
				gvDescriptionNr.SetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Selected"], false);
			}
		}

		private void rpDESNRInactive_CheckedChanged(object sender, EventArgs e)
		{
			var sel = gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Selected"]);
			var SEL = Convert.ToBoolean(sel);

			var inact = gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Inactive"]);
			var INACT = Convert.ToBoolean(inact);

			if (SEL)
			{
				gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Inactive"]);
				gvDescriptionNr.SetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Inactive"], false);
			}

			if (INACT)
			{
				gvDescriptionNr.GetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Selected"]);
				gvDescriptionNr.SetRowCellValue(gvDescriptionNr.FocusedRowHandle, gvDescriptionNr.Columns["Selected"], false);
			}
		}

		private void gvNumberSelected_RowUpdated(object sender, RowObjectEventArgs e)
		{
			if (true)
			{
				var num = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["Number"]);

				var Number = Convert.ToString(num);
				bool NumberSel;
				bool DIRSEL;
				bool DESCSEL;
				bool DESCNOSEL;
				bool DETSEL;
				var numSel = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["NumSelected"]);
				if (numSel is DBNull || numSel == null)
				{
					NumberSel = false;
				}
				else
				{
					NumberSel = Convert.ToBoolean(numSel);
				}

				var dir = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["Direction"]);
				var DIR = Convert.ToString(dir);

				var dirsel = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["DirNumber"]);
				//  bool DIRSEL = Convert.ToBoolean(dirsel);
				if (dirsel is DBNull || dirsel == null)
				{
					DIRSEL = false;
				}
				else
				{
					DIRSEL = Convert.ToBoolean(dirsel);
				}

				var desc = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["Description"]);
				var DESC = Convert.ToString(desc);

				var descsel = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["DesNumber"]);
				// bool DESCSEL = Convert.ToBoolean(descsel);
				if (descsel is DBNull || descsel == null)
				{
					DESCSEL = false;
				}
				else
				{
					DESCSEL = Convert.ToBoolean(descsel);
				}

				var descno = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["DescriptionNo"]);
				var DESCNO = Convert.ToString(descno);

				var descnosel = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["DesNoSelected"]);
				// bool DESCNOSEL = Convert.ToBoolean(descnosel);
				if (descnosel is DBNull || descnosel == null)
				{
					DESCNOSEL = false;
				}
				else
				{
					DESCNOSEL = Convert.ToBoolean(descnosel);
				}

				var det = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["Detail"]);
				var DET = Convert.ToString(det);

				var detsel = gvNumberSelected.GetRowCellValue(gvNumberSelected.FocusedRowHandle, gvNumberSelected.Columns["DetSelected"]);
				// bool DETSEL = Convert.ToBoolean(detsel);
				if (detsel is DBNull || detsel == null)
				{
					DETSEL = false;
				}
				else
				{
					DETSEL = Convert.ToBoolean(detsel);
				}



				var detail = "";
				var number = "";
				var direction = "";
				var description = "";
				var descpno = "";

				if (DETSEL)
				{
					detail = "Y";
				}
				else
				{
					detail = "N";
				}

				if (NumberSel)
				{
					number = "Y";
				}
				else
				{
					number = "N";
				}

				if (DIRSEL)
				{
					direction = "Y";
				}
				else
				{
					direction = "N";
				}

				if (DESCSEL)
				{
					description = "Y";
				}
				else
				{
					description = "N";
				}

				if (DESCNOSEL)
				{
					descpno = "Y";
				}
				else
				{
					descpno = "N";
				}


				var _dbMan = new clsDataAccess();
				_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan.queryReturnType = ReturnType.DataTable;
				_dbMan.SqlStatement = " delete from Code_WPTypeWPDetailLink \r\n " +
												  " where TypeCode = '" + thewptype + "' and \r\n " +
												  "       DetailCode = '" + ExtractBeforeColon(DET) + "' \r\n ";



				if (detail == "Y")
				{
					_dbMan.SqlStatement = _dbMan.SqlStatement + " insert into Code_WPTypeWPDetailLink (TypeCode, DetailCode) \r\n " +
						" values ('" + thewptype + "', '" + ExtractBeforeColon(DET) + "') ";
				}

				_dbMan.ExecuteInstruction();


				var _dbMan1 = new clsDataAccess();
				_dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan1.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan1.queryReturnType = ReturnType.DataTable;
				_dbMan1.SqlStatement = " delete from Code_WPTypeWPNumberLink \r\n " +
												  " where TypeCode = '" + thewptype + "' and \r\n " +
												  "       NumberCode = '" + ExtractBeforeColon(Number) + "' \r\n ";
				if (number == "Y")
				{
					_dbMan1.SqlStatement = _dbMan1.SqlStatement + " insert into Code_WPTypeWPNumberLink (TypeCode, NumberCode) \r\n " +
						" values ('" + thewptype + "', '" + ExtractBeforeColon(Number) + "') ";
				}
				_dbMan1.ExecuteInstruction();

				var _dbMan2 = new clsDataAccess();
				_dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan2.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan2.queryReturnType = ReturnType.DataTable;
				_dbMan2.SqlStatement = " delete from Code_WPTypeWPDirectionLink \r\n " +
												   " where TypeCode = '" + thewptype + "' and \r\n " +
												   "       Direction = '" + ExtractBeforeColon(DIR) + "' \r\n ";





				if (direction == "Y")
				{
					_dbMan2.SqlStatement = _dbMan2.SqlStatement + " insert into Code_WPTypeWPDirectionLink (TypeCode, Direction) \r\n " +
						" values ('" + thewptype + "', '" + ExtractBeforeColon(DIR) + "') ";
				}
				_dbMan2.ExecuteInstruction();


				var _dbMan3 = new clsDataAccess();
				_dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan3.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan3.queryReturnType = ReturnType.DataTable;
				_dbMan3.SqlStatement = " delete from CODE_WPTypeWPDescNoLink \r\n " +
												  " where TypeCode = '" + thewptype + "' and \r\n " +
												  "       DescrNumberCode = '" + ExtractBeforeColon(DESCNO) + "' \r\n ";
				if (descpno == "Y")
				{
					_dbMan3.SqlStatement = _dbMan3.SqlStatement + " insert into CODE_WPTypeWPDescNoLink (TypeCode, DescrNumberCode) \r\n " +
						" values ('" + thewptype + "', '" + ExtractBeforeColon(DESCNO) + "') ";
				}
				_dbMan3.ExecuteInstruction();



				var _dbMan4 = new clsDataAccess();
				_dbMan4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbMan4.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbMan4.queryReturnType = ReturnType.DataTable;
				_dbMan4.SqlStatement = " delete from Code_WPTypeWPDescLink \r\n " +
												 " where TypeCode = '" + thewptype + "' and \r\n " +
												 "       DescrCode = '" + ExtractBeforeColon(DESC) + "' \r\n ";
				if (description == "Y")
				{
					_dbMan4.SqlStatement = _dbMan.SqlStatement + " insert into Code_WPTypeWPDescLink (TypeCode, DescrCode) \r\n " +
						" values ('" + thewptype + "', '" + ExtractBeforeColon(DESC) + "') ";
				}
				_dbMan4.ExecuteInstruction();
			}
		}

		private void gvWPTActitvity_RowUpdated(object sender, RowObjectEventArgs e)
		{
			if (true)
			{
				var stop = gvWPTActitvity.GetRowCellValue(gvWPTActitvity.FocusedRowHandle, gvWPTActitvity.Columns["Stoping"]);
				var STOP = Convert.ToBoolean(stop);

				var wpcode = gvWPTActitvity.GetRowCellValue(gvWPTActitvity.FocusedRowHandle, gvWPTActitvity.Columns["Code"]);
				var WPCode = Convert.ToString(wpcode);


				var _dbManOrgUnit = new clsDataAccess();
				_dbManOrgUnit.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbManOrgUnit.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbManOrgUnit.queryReturnType = ReturnType.DataTable;

				var sqlStatement = " delete wptype_setup where TypeCode = '" + WPCode + "' and SetupCode = 'S' \r\n ";
				if (STOP)
					sqlStatement = sqlStatement + " insert into wptype_setup(TypeCode, SetupCode) values('" + WPCode + "','S') ";

				_dbManOrgUnit.SqlStatement = sqlStatement;
				_dbManOrgUnit.ExecuteInstruction();

				var dev = gvWPTActitvity.GetRowCellValue(gvWPTActitvity.FocusedRowHandle, gvWPTActitvity.Columns["Development"]);
				var DEV = Convert.ToBoolean(dev);



				var _dbManOrgUnit1 = new clsDataAccess();
				_dbManOrgUnit1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbManOrgUnit1.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbManOrgUnit1.queryReturnType = ReturnType.DataTable;

				var sqlStatement1 = " delete wptype_setup where TypeCode = '" + WPCode + "' and SetupCode = 'D' \r\n ";
				if (DEV)
					sqlStatement1 = sqlStatement1 + " insert into wptype_setup(TypeCode, SetupCode) values('" + WPCode + "','D') ";

				_dbManOrgUnit1.SqlStatement = sqlStatement1;
				_dbManOrgUnit1.ExecuteInstruction();


				var oug = gvWPTActitvity.GetRowCellValue(gvWPTActitvity.FocusedRowHandle, gvWPTActitvity.Columns["OtherUnderground"]);
				var OUG = Convert.ToBoolean(oug);


				var _dbManOrgUnit2 = new clsDataAccess();
				_dbManOrgUnit2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbManOrgUnit2.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbManOrgUnit2.queryReturnType = ReturnType.DataTable;

				var sqlStatement2 = " delete wptype_setup where TypeCode = '" + WPCode + "' and SetupCode = 'OUG' \r\n ";
				if (OUG)
					sqlStatement2 = sqlStatement2 + " insert into wptype_setup(TypeCode, SetupCode) values('" + WPCode + "','OUG') ";

				_dbManOrgUnit2.SqlStatement = sqlStatement2;
				_dbManOrgUnit2.ExecuteInstruction();


				var sur = gvWPTActitvity.GetRowCellValue(gvWPTActitvity.FocusedRowHandle, gvWPTActitvity.Columns["Surface"]);
				var SUR = Convert.ToBoolean(sur);


				var _dbManOrgUnit3 = new clsDataAccess();
				_dbManOrgUnit3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
				_dbManOrgUnit3.queryExecutionType = ExecutionType.GeneralSQLStatement;
				_dbManOrgUnit3.queryReturnType = ReturnType.DataTable;

				var sqlStatement3 = " delete wptype_setup where TypeCode = '" + WPCode + "' and SetupCode = 'SU' \r\n ";
				if (SUR)
					sqlStatement3 = sqlStatement3 + " insert into wptype_setup(TypeCode, SetupCode) values('" + WPCode + "','SU') ";

				_dbManOrgUnit3.SqlStatement = sqlStatement3;
				_dbManOrgUnit3.ExecuteInstruction();
			}
		}

		private void gvDetail_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			theAction = "Add";
			GridView view;
			view = sender as GridView;

			var row = view.GetRow(e.RowHandle);

			(row as DataRowView)["Selected"] = false;
			(row as DataRowView)["Inactive"] = false;
			colDETSelected.OptionsColumn.AllowEdit = true;
			colDETSelected.OptionsColumn.ReadOnly = false;
			colDETInactive.OptionsColumn.AllowEdit = true;
			colDETInactive.OptionsColumn.ReadOnly = false;
			colDETCode.OptionsColumn.AllowEdit = true;
			colDETCode.OptionsColumn.ReadOnly = false;
			colDETDescription.OptionsColumn.AllowEdit = true;
			colDETDescription.OptionsColumn.ReadOnly = false;
		}

		private void gvDetail_RowClick(object sender, RowClickEventArgs e)
		{
			var rowhand = gvDetail.FocusedRowHandle;
			if (rowhand >= 0)
			//if (theAction == "")
			{
				theAction = "";
				colDETSelected.OptionsColumn.AllowEdit = true;
				colDETSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colDETInactive.OptionsColumn.AllowEdit = true;
					colDETInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colDETInactive.OptionsColumn.AllowEdit = false;
					colDETInactive.OptionsColumn.ReadOnly = true;

				}
				//    colDETInactive.OptionsColumn.AllowEdit = true;
				//colDETInactive.OptionsColumn.ReadOnly = false;
				colDETCode.OptionsColumn.AllowEdit = false;
				colDETCode.OptionsColumn.ReadOnly = true;
				colDETDescription.OptionsColumn.AllowEdit = false;
				colDETDescription.OptionsColumn.ReadOnly = true;
			}
			// if (theAction == "Add")
			if (rowhand < 0)
			{
				theAction = "Add";
				colDETSelected.OptionsColumn.AllowEdit = true;
				colDETSelected.OptionsColumn.ReadOnly = false;
				colDETInactive.OptionsColumn.AllowEdit = true;
				colDETInactive.OptionsColumn.ReadOnly = false;
				colDETCode.OptionsColumn.AllowEdit = true;
				colDETCode.OptionsColumn.ReadOnly = false;
				colDETDescription.OptionsColumn.AllowEdit = true;
				colDETDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvDetail_RowCellClick(object sender, RowCellClickEventArgs e)
		{

		}

		private void gvDetail_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{

			var rowhand = gvDetail.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{
				colDETSelected.OptionsColumn.AllowEdit = true;
				colDETSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colDETInactive.OptionsColumn.AllowEdit = true;
					colDETInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colDETInactive.OptionsColumn.AllowEdit = false;
					colDETInactive.OptionsColumn.ReadOnly = true;

				}
				//colDETInactive.OptionsColumn.AllowEdit = true;
				//colDETInactive.OptionsColumn.ReadOnly = false;
				colDETCode.OptionsColumn.AllowEdit = false;
				colDETCode.OptionsColumn.ReadOnly = true;
				colDETDescription.OptionsColumn.AllowEdit = false;
				colDETDescription.OptionsColumn.ReadOnly = true;
			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{
				colDETSelected.OptionsColumn.AllowEdit = true;
				colDETSelected.OptionsColumn.ReadOnly = false;
				colDETInactive.OptionsColumn.AllowEdit = true;
				colDETInactive.OptionsColumn.ReadOnly = false;
				colDETCode.OptionsColumn.AllowEdit = true;
				colDETCode.OptionsColumn.ReadOnly = false;
				colDETDescription.OptionsColumn.AllowEdit = true;
				colDETDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvNumber_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			theAction = "Add";
			GridView view;
			view = sender as GridView;

			var row = view.GetRow(e.RowHandle);

			(row as DataRowView)["Selected"] = false;
			(row as DataRowView)["Inactive"] = false;
			colNUMSelected.OptionsColumn.AllowEdit = true;
			colNUMSelected.OptionsColumn.ReadOnly = false;
			colNUMInactive.OptionsColumn.AllowEdit = true;
			colNUMInactive.OptionsColumn.ReadOnly = false;
			colNUMCode.OptionsColumn.AllowEdit = true;
			colNUMCode.OptionsColumn.ReadOnly = false;
			colNUMDescription.OptionsColumn.AllowEdit = true;
			colNUMDescription.OptionsColumn.ReadOnly = false;
		}

		private void gvDescription_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			theAction = "Add";
			GridView view;
			view = sender as GridView;

			var row = view.GetRow(e.RowHandle);

			(row as DataRowView)["Selected"] = false;
			(row as DataRowView)["Inactive"] = false;
			colDESSelected.OptionsColumn.AllowEdit = true;
			colDESSelected.OptionsColumn.ReadOnly = false;
			colDESInactive.OptionsColumn.AllowEdit = true;
			colDESInactive.OptionsColumn.ReadOnly = false;
			colDESCode.OptionsColumn.AllowEdit = true;
			colDESCode.OptionsColumn.ReadOnly = false;
			colDESDescription.OptionsColumn.AllowEdit = true;
			colDESDescription.OptionsColumn.ReadOnly = false;
		}

		private void gvInactiveReasons_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			theAction = "Add";
			GridView view;
			view = sender as GridView;

			var row = view.GetRow(e.RowHandle);

			(row as DataRowView)["Selected"] = false;
			(row as DataRowView)["Inactive"] = false;
			colINRSelected.OptionsColumn.AllowEdit = true;
			colINRSelected.OptionsColumn.ReadOnly = false;
			colINRInactive.OptionsColumn.AllowEdit = true;
			colINRInactive.OptionsColumn.ReadOnly = false;
			//colDETCode.OptionsColumn.AllowEdit = true;
			//colDETCode.OptionsColumn.ReadOnly = false;
			colINRReason.OptionsColumn.AllowEdit = true;
			colINRReason.OptionsColumn.ReadOnly = false;
		}

		private void gvDescriptionNr_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			theAction = "Add";
			GridView view;
			view = sender as GridView;

			var row = view.GetRow(e.RowHandle);

			(row as DataRowView)["Selected"] = false;
			(row as DataRowView)["Inactive"] = false;
			colDESNRSelected.OptionsColumn.AllowEdit = true;
			colDESNRSelected.OptionsColumn.ReadOnly = false;
			colDESNRInactive.OptionsColumn.AllowEdit = true;
			colDESNRInactive.OptionsColumn.ReadOnly = false;
			colDESNRCode.OptionsColumn.AllowEdit = true;
			colDESNRCode.OptionsColumn.ReadOnly = false;
			colDESNRDescription.OptionsColumn.AllowEdit = true;
			colDESNRDescription.OptionsColumn.ReadOnly = false;
		}

		private void gvWPType_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			theAction = "Add";
			GridView view;
			view = sender as GridView;

			var row = view.GetRow(e.RowHandle);

			(row as DataRowView)["Selected"] = false;
			(row as DataRowView)["Inactive"] = false;
			(row as DataRowView)["Classification"] = false;
			colSTPSelected.OptionsColumn.AllowEdit = true;
			colSTPSelected.OptionsColumn.ReadOnly = true;
			colSTPInactive.OptionsColumn.AllowEdit = true;
			colSTPInactive.OptionsColumn.ReadOnly = true;
			colSTPClassification.OptionsColumn.AllowEdit = true;
			colSTPClassification.OptionsColumn.ReadOnly = false;
			colWPTCode.OptionsColumn.AllowEdit = true;
			colWPTCode.OptionsColumn.ReadOnly = false;
			colWPTDescription.OptionsColumn.AllowEdit = true;
			colWPTDescription.OptionsColumn.ReadOnly = false;
		}

		private void gvNumber_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			var rowhand = gvNumber.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{

				colNUMSelected.OptionsColumn.AllowEdit = true;
				colNUMSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colNUMInactive.OptionsColumn.AllowEdit = true;
					colNUMInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colNUMInactive.OptionsColumn.AllowEdit = false;
					colNUMInactive.OptionsColumn.ReadOnly = true;

				}
				//colNUMInactive.OptionsColumn.AllowEdit = true;
				//colNUMInactive.OptionsColumn.ReadOnly = false;
				colNUMCode.OptionsColumn.AllowEdit = false;
				colNUMCode.OptionsColumn.ReadOnly = true;
				colNUMDescription.OptionsColumn.AllowEdit = false;
				colNUMDescription.OptionsColumn.ReadOnly = true;

			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{
				colNUMSelected.OptionsColumn.AllowEdit = true;
				colNUMSelected.OptionsColumn.ReadOnly = false;
				colNUMInactive.OptionsColumn.AllowEdit = true;
				colNUMInactive.OptionsColumn.ReadOnly = false;
				colNUMCode.OptionsColumn.AllowEdit = true;
				colNUMCode.OptionsColumn.ReadOnly = false;
				colNUMDescription.OptionsColumn.AllowEdit = true;
				colNUMDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvDescription_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			var rowhand = gvDescription.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{
				colDESSelected.OptionsColumn.AllowEdit = true;
				colDESSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colDESInactive.OptionsColumn.AllowEdit = true;
					colDESInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colDESInactive.OptionsColumn.AllowEdit = false;
					colDESInactive.OptionsColumn.ReadOnly = true;

				}
				//colDESInactive.OptionsColumn.AllowEdit = true;
				//colDESInactive.OptionsColumn.ReadOnly = false;
				colDESCode.OptionsColumn.AllowEdit = false;
				colDESCode.OptionsColumn.ReadOnly = true;
				colDESDescription.OptionsColumn.AllowEdit = false;
				colDESDescription.OptionsColumn.ReadOnly = true;
			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{

				colDESSelected.OptionsColumn.AllowEdit = true;
				colDESSelected.OptionsColumn.ReadOnly = false;
				colDESInactive.OptionsColumn.AllowEdit = true;
				colDESInactive.OptionsColumn.ReadOnly = false;
				colDESCode.OptionsColumn.AllowEdit = true;
				colDESCode.OptionsColumn.ReadOnly = false;
				colDESDescription.OptionsColumn.AllowEdit = true;
				colDESDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvDescriptionNr_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			var rowhand = gvDescriptionNr.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{
				colDESNRSelected.OptionsColumn.AllowEdit = true;
				colDESNRSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colDESNRInactive.OptionsColumn.AllowEdit = true;
					colDESNRInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colDESNRInactive.OptionsColumn.AllowEdit = false;
					colDESNRInactive.OptionsColumn.ReadOnly = true;

				}
				//colDESNRInactive.OptionsColumn.AllowEdit = true;
				//colDESNRInactive.OptionsColumn.ReadOnly = false;
				colDESNRCode.OptionsColumn.AllowEdit = false;
				colDESNRCode.OptionsColumn.ReadOnly = true;
				colDESNRDescription.OptionsColumn.AllowEdit = false;
				colDESNRDescription.OptionsColumn.ReadOnly = true;
			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{
				colDESNRSelected.OptionsColumn.AllowEdit = true;
				colDESNRSelected.OptionsColumn.ReadOnly = false;
				colDESNRInactive.OptionsColumn.AllowEdit = true;
				colDESNRInactive.OptionsColumn.ReadOnly = false;
				colDESNRCode.OptionsColumn.AllowEdit = true;
				colDESNRCode.OptionsColumn.ReadOnly = false;
				colDESNRDescription.OptionsColumn.AllowEdit = true;
				colDESNRDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvInactiveReasons_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			var rowhand = gvInactiveReasons.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{
				colINRSelected.OptionsColumn.AllowEdit = true;
				colINRSelected.OptionsColumn.ReadOnly = false;
				if (TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).IsCentralizedDatabase == 1)
				{
					colINRInactive.OptionsColumn.AllowEdit = true;
					colINRInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colINRInactive.OptionsColumn.AllowEdit = false;
					colINRInactive.OptionsColumn.ReadOnly = true;

				}
				//colINRInactive.OptionsColumn.AllowEdit = true;
				//colINRInactive.OptionsColumn.ReadOnly = false;
				//colDETCode.OptionsColumn.AllowEdit = false;
				//colDETCode.OptionsColumn.ReadOnly = true;
				colINRReason.OptionsColumn.AllowEdit = false;
				colINRReason.OptionsColumn.ReadOnly = true;
			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{
				colINRSelected.OptionsColumn.AllowEdit = true;
				colINRSelected.OptionsColumn.ReadOnly = false;
				colINRInactive.OptionsColumn.AllowEdit = true;
				colINRInactive.OptionsColumn.ReadOnly = false;
				//colDETCode.OptionsColumn.AllowEdit = true;
				//colDETCode.OptionsColumn.ReadOnly = false;
				colINRReason.OptionsColumn.AllowEdit = true;
				colINRReason.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvNumber_RowClick(object sender, RowClickEventArgs e)
		{
			var rowhand = gvNumber.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{
				theAction = "";
				colNUMSelected.OptionsColumn.AllowEdit = true;
				colNUMSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colNUMInactive.OptionsColumn.AllowEdit = true;
					colNUMInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colNUMInactive.OptionsColumn.AllowEdit = false;
					colNUMInactive.OptionsColumn.ReadOnly = true;

				}
				//colNUMInactive.OptionsColumn.AllowEdit = true;
				//colNUMInactive.OptionsColumn.ReadOnly = false;
				colNUMCode.OptionsColumn.AllowEdit = false;
				colNUMCode.OptionsColumn.ReadOnly = true;
				colNUMDescription.OptionsColumn.AllowEdit = false;
				colNUMDescription.OptionsColumn.ReadOnly = true;

			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{
				theAction = "Add";
				colNUMSelected.OptionsColumn.AllowEdit = true;
				colNUMSelected.OptionsColumn.ReadOnly = false;
				colNUMInactive.OptionsColumn.AllowEdit = true;
				colNUMInactive.OptionsColumn.ReadOnly = false;
				colNUMCode.OptionsColumn.AllowEdit = true;
				colNUMCode.OptionsColumn.ReadOnly = false;
				colNUMDescription.OptionsColumn.AllowEdit = true;
				colNUMDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvDescription_RowClick(object sender, RowClickEventArgs e)
		{
			var rowhand = gvDescription.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{
				theAction = "";
				colDESSelected.OptionsColumn.AllowEdit = true;
				colDESSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colDESInactive.OptionsColumn.AllowEdit = true;
					colDESInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colDESInactive.OptionsColumn.AllowEdit = false;
					colDESInactive.OptionsColumn.ReadOnly = true;

				}

				colDESCode.OptionsColumn.AllowEdit = false;
				colDESCode.OptionsColumn.ReadOnly = true;
				colDESDescription.OptionsColumn.AllowEdit = false;
				colDESDescription.OptionsColumn.ReadOnly = true;
			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{
				theAction = "Add";
				colDESSelected.OptionsColumn.AllowEdit = true;
				colDESSelected.OptionsColumn.ReadOnly = false;
				colDESInactive.OptionsColumn.AllowEdit = true;
				colDESInactive.OptionsColumn.ReadOnly = false;
				colDESCode.OptionsColumn.AllowEdit = true;
				colDESCode.OptionsColumn.ReadOnly = false;
				colDESDescription.OptionsColumn.AllowEdit = true;
				colDESDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvDescriptionNr_RowClick(object sender, RowClickEventArgs e)
		{
			var rowhand = gvDescriptionNr.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{
				theAction = "";
				colDESNRSelected.OptionsColumn.AllowEdit = true;
				colDESNRSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colDESNRInactive.OptionsColumn.AllowEdit = true;
					colDESNRInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colDESNRInactive.OptionsColumn.AllowEdit = false;
					colDESNRInactive.OptionsColumn.ReadOnly = true;

				}

				colDESNRCode.OptionsColumn.AllowEdit = false;
				colDESNRCode.OptionsColumn.ReadOnly = true;
				colDESNRDescription.OptionsColumn.AllowEdit = false;
				colDESNRDescription.OptionsColumn.ReadOnly = true;
			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{
				theAction = "Add";
				colDESNRSelected.OptionsColumn.AllowEdit = true;
				colDESNRSelected.OptionsColumn.ReadOnly = false;
				colDESNRInactive.OptionsColumn.AllowEdit = true;
				colDESNRInactive.OptionsColumn.ReadOnly = false;
				colDESNRCode.OptionsColumn.AllowEdit = true;
				colDESNRCode.OptionsColumn.ReadOnly = false;
				colDESNRDescription.OptionsColumn.AllowEdit = true;
				colDESNRDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void gvInactiveReasons_RowClick(object sender, RowClickEventArgs e)
		{
			var rowhand = gvDescriptionNr.FocusedRowHandle;
			if (rowhand >= 0)
			// if (theAction == "")
			{
				theAction = "";
				colDESNRSelected.OptionsColumn.AllowEdit = true;
				colDESNRSelected.OptionsColumn.ReadOnly = false;
				if (true)
				{
					colDESNRInactive.OptionsColumn.AllowEdit = true;
					colDESNRInactive.OptionsColumn.ReadOnly = false;

				}
				else
				{
					colDESNRInactive.OptionsColumn.AllowEdit = false;
					colDESNRInactive.OptionsColumn.ReadOnly = true;

				}

				colDESNRCode.OptionsColumn.AllowEdit = false;
				colDESNRCode.OptionsColumn.ReadOnly = true;
				colDESNRDescription.OptionsColumn.AllowEdit = false;
				colDESNRDescription.OptionsColumn.ReadOnly = true;
			}
			// if(theAction =="Add")
			if (rowhand < 0)
			{
				theAction = "Add";
				colDESNRSelected.OptionsColumn.AllowEdit = true;
				colDESNRSelected.OptionsColumn.ReadOnly = false;
				colDESNRInactive.OptionsColumn.AllowEdit = true;
				colDESNRInactive.OptionsColumn.ReadOnly = false;
				colDESNRCode.OptionsColumn.AllowEdit = true;
				colDESNRCode.OptionsColumn.ReadOnly = false;
				colDESNRDescription.OptionsColumn.AllowEdit = true;
				colDESNRDescription.OptionsColumn.ReadOnly = false;
			}
		}

		private void cmbWPTypeSelect_EditValueChanged(object sender, EventArgs e)
		{
			if (cmbWPTypeSelect.ItemIndex == -1)
			{

			}
			else
			{
				wpActivity();
			}
		}

		public string ExtractAfterColon(string TheString)
		{
			string AfterColon;

			var index = TheString.IndexOf(":"); // Kry die postion van die :

			AfterColon = TheString.Substring(index + 1); // kry alles na :

			return AfterColon;
		}

		public string ExtractBeforeColon(string TheString)
		{
			if (TheString != "")
			{
				string BeforeColon;

				var index = TheString.IndexOf(":");

				BeforeColon = TheString.Substring(0, index);

				return BeforeColon;
			}
			return "";
		}

		private void gvGrid_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
		{

			
			

            


        }

		void b_Click(object sender, EventArgs e)
		{
			// System.Diagnostics.Debugger.Break();
			//  myEdit = new ileGridEdit();
			myEdit.btnUpdate_Click(sender, e);
			loaddata();
		}

		private void gvNumberSelected_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			if (TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).IsCentralizedDatabase == 1)
			{

				colNUMSSelected.OptionsColumn.AllowEdit = true;
				colNUMSSelected.OptionsColumn.ReadOnly = false;
				colDETSEL.OptionsColumn.AllowEdit = true;
				colDETSEL.OptionsColumn.ReadOnly = false;
				colDIRSelected.OptionsColumn.AllowEdit = true;
				colDIRSelected.OptionsColumn.ReadOnly = false;
				colDESCNrSelected.OptionsColumn.AllowEdit = true;
				colDESCNrSelected.OptionsColumn.ReadOnly = false;
				colDESSEL.OptionsColumn.AllowEdit = true;
				colDESSEL.OptionsColumn.ReadOnly = false;
			}
			else
			{
				colNUMSSelected.OptionsColumn.AllowEdit = false;
				colNUMSSelected.OptionsColumn.ReadOnly = true;
				colDETSEL.OptionsColumn.AllowEdit = false;
				colDETSEL.OptionsColumn.ReadOnly = true;
				colDIRSelected.OptionsColumn.AllowEdit = false;
				colDIRSelected.OptionsColumn.ReadOnly = true;
				colDESCNrSelected.OptionsColumn.AllowEdit = false;
				colDESCNrSelected.OptionsColumn.ReadOnly = true;
				colDESSEL.OptionsColumn.AllowEdit = false;
				colDESSEL.OptionsColumn.ReadOnly = true;

			}
		}

        private void gcNumberSelected_Click(object sender, EventArgs e)
        {

        }

        private void gvGrid_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {

        }

        private void gvGrid_RowUpdated(object sender, RowObjectEventArgs e)
        {
            loaddata();
        }

        private void gvGrid_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            aa = myEdit.result.ToString();
            if (rdgrpWPCodes.SelectedIndex == 2 && aa == "true")
            {

                loaddata();
                myEdit.result = "false";
                aa = "false";
            }
        }

        void LoadGridData()
        {
            if (rdgrpWPCodes.SelectedIndex == 2 && aa == "true")
            {

                 loaddata();
            }
        }
    }
}
