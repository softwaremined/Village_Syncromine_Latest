using System;
using System.Data;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Global;
using Mineware.Systems.Planning.PlanningProtocolTemplates;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolTemplates
{
    public partial class ucPlanProtTemplateList : ucBaseUserControl 
    {
        public clsPlanProtTemplateData templateData = new clsPlanProtTemplateData();
        public ucPlanProtTemplateList()
        {
            InitializeComponent();
        }

        private void ucPlanProtTemplateList_Load(object sender, EventArgs e)
        {
            LoadTemplateList();

        }

        private void LoadTemplateList()
        {
            gridTemplate.DataSource = templateData.TemplateList;
        }

        public void NewTemplate()
        {
           // using (frmPlanProtTemplateSetup Template = new frmPlanProtTemplateSetup { formAction = currentAction.caAdd,theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo  })
            frmPlanProtTemplateSetup Template = new frmPlanProtTemplateSetup { formAction = currentAction.caAdd,theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo  };
           // {
                Template.UserCurrentInfo = this.UserCurrentInfo;
                
                OnSendControlToMainTab(new SendControlToMainTabHandlerArgs(Template,"New Template", UserCurrentInfo,false));
           // }
            
           // LoadTemplateList();
        }

        public void EditTemplate()
        {
            //using (frmPlanProtTemplateSetup Template = new frmPlanProtTemplateSetup { 
            //    formAction = currentAction.caEdit, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo  ,
            //    TempID = Convert.ToInt32(tableTemplate.Rows[viewTemlates.FocusedRowHandle]["TemplateID"].ToString()) })
            frmPlanProtTemplateSetup Template = new frmPlanProtTemplateSetup { 
                formAction = currentAction.caEdit, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo  , templateData = this.templateData,
                TempID = Convert.ToInt32(templateData.TemplateList.Rows[viewTemlates.FocusedRowHandle]["TemplateID"].ToString()) };
           // {
                Template.UserCurrentInfo = this.UserCurrentInfo;
                Template.tabCaption = "Edit Template " + templateData.TemplateList.Rows[viewTemlates.FocusedRowHandle]["TemplateName"].ToString();
                Template.CloseTab += Template_CloseTab;
                OnSendControlToMainTab(new SendControlToMainTabHandlerArgs(Template, "Edit Template " + templateData.TemplateList.Rows[viewTemlates.FocusedRowHandle]["TemplateName"].ToString(), UserCurrentInfo, false));
            //}

           // LoadTemplateList();
        }

        void Template_CloseTab(object sender, CloseTabArg e)
        {
            OnCloseTabRequest(new CloseTabArg(e.TabCaption));
        }
    
    }
}
