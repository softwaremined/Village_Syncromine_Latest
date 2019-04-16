using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mineware.Systems.Planning.PrePlanning
{
    public partial class ucPrePlanningHeader : UserControl
    {
        private PrePlanningSettings planningSettings = new PrePlanningSettings();
        public ucPrePlanningHeader()
        {
            InitializeComponent();
           

        }

        /// <summary>
        /// Set the planningSettings and links controls to class 
        /// </summary>
        /// <param name="settings">PrePlanningSettings</param>
        public void SetPlanningSettings(PrePlanningSettings settings)
        {
            planningSettings = settings;
            editProdmonth.DataBindings
               .Add((new System.Windows.Forms.Binding("Text", this.planningSettings, "ProdMonth", true)));
            editMoSection.DataBindings
               .Add((new System.Windows.Forms.Binding("Text", this.planningSettings, "SectionName", true)));
            editMoSectionID.DataBindings
              .Add((new System.Windows.Forms.Binding("Text", this.planningSettings, "MOSectionID", true)));
            editActivity.DataBindings
             .Add((new System.Windows.Forms.Binding("Text", this.planningSettings, "ActivityString", true)));
        }
    }
}
