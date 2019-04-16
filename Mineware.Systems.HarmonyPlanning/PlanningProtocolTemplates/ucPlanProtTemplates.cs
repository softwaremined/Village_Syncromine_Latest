using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraBars;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Global;
using Mineware.Systems.Planning.PlanningProtocolTemplates;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PlanningProtocolTemplates
{
    public partial class ucPlanProtTemplates : ucBaseUserControl 
    {
        public clsPlanProtTemplateData templateData = new clsPlanProtTemplateData();
     
        public ucPlanProtTemplates()
        {
            InitializeComponent();
            CanClose = true;
           
        }

        private void sendToMainScreen(object sender, SendControlToMainTabHandlerArgs  e)
        {
            OnSendControlToMainTab(new SendControlToMainTabHandlerArgs(e.TheControl,e.TheName,e.TheCurrentUserInfo,e.IsReport));
        }

        private void btnNewTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            templateData.screenTemplateList.SendControlToMainTabRequest += new SendControlToMainTabHandler(sendToMainScreen);
            templateData.screenTemplateList.NewTemplate();         

        }

     

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            templateData.screenTemplateList.SendControlToMainTabRequest += new SendControlToMainTabHandler(sendToMainScreen);
            templateData.screenTemplateList.CloseTab += screenTemplateList_CloseTab;
            templateData.screenTemplateList.EditTemplate();
        }

        void screenTemplateList_CloseTab(object sender, CloseTabArg e)
        {
            OnCloseTabRequest(new CloseTabArg(e.TabCaption));
        }

        public override void setSecurity()
        {
            LoadSecurity();

        }
     

        private void frmPlanProtTemplates_Load(object sender, EventArgs e)
        {

            templateData.UserCurrentInfo = UserCurrentInfo;
            templateData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            templateData.screenTemplateList.theSystemDBTag = this.theSystemDBTag;
            templateData.screenTemplateList.theSystemTag = this.theSystemTag;
            templateData.screenTemplateList.UserCurrentInfo = this.UserCurrentInfo;
            templateData.screenTemplateList.templateData = this.templateData;
            templateData.screenTemplateList.Dock = DockStyle.Fill;
            templateData.screenTemplateList.Parent = clientPanel;


            LoadSecurity();
           // th.Abort();
        }

        private void LoadSecurity()
        {
            switch (UserCurrentInfo.theSecurityLevel(TProductionGlobal.WPASMenuStructure.miEditPlanningProtocolTemplate_HPASPlanProtEdit_MinewareSystemsHarmonyPAS.ItemID))
            // switch (TUserInfo.theSecurityLevel("RPROS"))
            {
                case 0:
                    btnEdit.Enabled = false;
                    break;
                case 1:
                    btnEdit.Enabled = true;
                    break;
                case 2:
                    btnEdit.Enabled = true;
                    break;
            }

            switch (UserCurrentInfo.theSecurityLevel(TProductionGlobal.WPASMenuStructure.miNewPlanningProtocolTemplate_HPASPlanProtNew_MinewareSystemsHarmonyPAS.ItemID))
            // switch (TUserInfo.theSecurityLevel("RPROS"))
            {
                case 0:
                    btnNewTemplate.Enabled = false;
                    break;
                case 1:
                    btnNewTemplate.Enabled = false;
                    break;
                case 2:
                    btnNewTemplate.Enabled = true;
                    break;
            }

            switch (UserCurrentInfo.theSecurityLevel(TProductionGlobal.WPASMenuStructure.miDeletePlanningProtocolTemplate_HPASPlanProtDelete_MinewareSystemsHarmonyPAS.ItemID))
            // switch (TUserInfo.theSecurityLevel("RPROS"))
            {
                case 0:
                    btnDelete.Enabled = false;
                    break;
                case 1:
                    btnDelete.Enabled = false;
                    break;
                case 2:
                    btnDelete.Enabled = true;
                    break;
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CanClose)
            {
                CanClose = true;         
                OnCloseTabRequest(new CloseTabArg(tabCaption)); ;
            }
            else
            {
                MessageResult myresult = MessageItem.viewMessage(MessageType.Question, "Cancel Action", "If you cancel you will lose all changes", ButtonTypes.YesNo, MessageDisplayType.FullScreen);
                if (myresult == MessageResult.Yes)
                {
                    CanClose = true;
                    OnCloseTabRequest(new CloseTabArg(tabCaption)); ;
                }
            }
        }
    }
}