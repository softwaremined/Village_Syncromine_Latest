using Mineware.Systems.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.Global.sysNotification;

namespace Mineware.Systems.Planning.PlanningProtocolTemplates
{
    public class clsPlanProtTemplateData : clsBase, IDisposable
    {
        private ucPlanProtTemplateList _screenTemplateList;
        private DataTable _TemplateList;

        public ucPlanProtTemplateList screenTemplateList
        {
            get
            {
                if(_screenTemplateList == null)
                {
                    _screenTemplateList = new ucPlanProtTemplateList();
                }

                return _screenTemplateList;
            }
        }

        public DataTable TemplateList
        {
            get
            {
                if (_TemplateList == null)
                {
                    _TemplateList = getTemplateList();
                }
                return _TemplateList;
            }
        }

        public void Dispose()
        {
            if (_TemplateList != null)
            {
                _TemplateList.Dispose();
                _TemplateList = null;
            }

        }

        private DataTable getTemplateList()
        {
            try
            {

                sb.Clear();
                sb.AppendLine("select TemplateID, TemplateName, ");
                sb.AppendLine("Case when Activity = 0 then 'Stoping' else "); 
                sb.AppendLine("Case when Activity  = 1 then 'Development' ");
                sb.AppendLine("end end Activity " );
                sb.AppendLine("from PlanProt_Template ORDER BY Activity,TemplateName");

                theData.SqlStatement = sb.ToString();

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();


            }
            catch (Exception e)
            {
                Global.sysMessages.sysMessagesClass message = new Global.sysMessages.sysMessagesClass();
                message.viewMessage(MessageType.Error, "ERROR LOADING TEMPLATE LIST", "Mineware.Systems.Production", "clsPlanProtTemplateData", "getTemplateList", e.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);

            }

            return theData.ResultsDataTable;
        }
    }
}
