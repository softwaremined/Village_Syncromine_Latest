using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.PrePlanning
{
    class GridRadioGroupColumnHelper
    {
        public event EventHandler SelectedRowChanged;
        private GridView _GridView;

        private RepositoryItemCheckEdit _RepositoryItem = new RepositoryItemCheckEdit();
        public RepositoryItemCheckEdit RadioRepositoryItem
        {
            get { return _RepositoryItem; }
            set { _RepositoryItem = value; }
        }

        //private GridColumn _RadioGroupColumn = new GridColumn();
        //public GridColumn RadioGroupColumn
        //{
        //    get { return _RadioGroupColumn; }
        //    set { _RadioGroupColumn = value; }
        //}

        private GridColumn _Selected = new GridColumn();
        public GridColumn Selected
        {
            get { return _Selected; }
            set { _Selected = value; }
        }

        private int _SelectedDataSourceRowIndex;
        public int SelectedDataSourceRowIndex
        {
            get { return _SelectedDataSourceRowIndex; }
            set
            {
                if (_SelectedDataSourceRowIndex != value)
                {
                    int oldRowIndex = _SelectedDataSourceRowIndex;
                    _SelectedDataSourceRowIndex = value;
                    SetRowChecked(oldRowIndex, false);
                    OnSelectedRowChanged(oldRowIndex, value);
                }
            }
        }

        //private void SetRowChecked(int dataSourceRowIndex, bool value)
        //{
        //    _GridView.SetRowCellValue(_GridView.GetDataSourceRowIndex(dataSourceRowIndex), RadioGroupColumn, value);
        //}

        private void SetRowChecked(int dataSourceRowIndex, bool value)
        {
            _GridView.SetRowCellValue(_GridView.GetDataSourceRowIndex(dataSourceRowIndex), Selected , value);
        }
        public GridRadioGroupColumnHelper(GridView gridView)
        {
            _GridView = gridView;
            _GridView.BeginUpdate();
            InitGridView();
            InitColumn();
            InitRepositoryItem();
            _GridView.EndUpdate();
        }
        private void InitGridView()
        {
            _GridView.CustomUnboundColumnData += _GridView_CustomUnboundColumnData;
        }

        void _GridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            //if (e.Column == RadioGroupColumn)
                if (e.Column == Selected )
            {
                if (e.IsGetData)
                    e.Value = e.ListSourceRowIndex == SelectedDataSourceRowIndex;
                if (e.IsSetData)
                    if (e.Value.Equals(true))
                        SelectedDataSourceRowIndex = e.ListSourceRowIndex;
            }
        }
        private void InitColumn()
        {
            ////RadioGroupColumn.FieldName = "RadioGroupColumn";
            //RadioGroupColumn.FieldName = "Selected";
            //_GridView.Columns.Add(RadioGroupColumn);
            //RadioGroupColumn.Visible = true;
            //RadioGroupColumn.ColumnEdit = RadioRepositoryItem;
            //RadioGroupColumn.Caption = "Selection";
            ////RadioGroupColumn.Caption = "Radio";
            //RadioGroupColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            //RadioGroupColumn.MaxWidth = 50;
         
            //RadioGroupColumn.FieldName = "RadioGroupColumn";
            Selected.FieldName = "Selected";
            _GridView.Columns.Add(Selected);
            Selected.Visible = true;
            Selected.ColumnEdit = RadioRepositoryItem;
            Selected.Caption = "Selection";
            //RadioGroupColumn.Caption = "Radio";
            Selected.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            Selected.MaxWidth = 50;
            Selected.VisibleIndex = 0;
        }
        private void InitRepositoryItem()
        {
            RadioRepositoryItem.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            RadioRepositoryItem.EditValueChanged += new EventHandler(RadioRepositoryItem_EditValueChanged);
        }


        void RadioRepositoryItem_EditValueChanged(object sender, EventArgs e)
        {
            _GridView.PostEditor();
        }

        private void OnSelectedRowChanged(int oldValue, int newValue)
        {
            RaiseSelectedRowChanged();
        }
        protected virtual void RaiseSelectedRowChanged()
        {
            if (SelectedRowChanged != null)
                SelectedRowChanged(_GridView, EventArgs.Empty);
        }
    }
}
