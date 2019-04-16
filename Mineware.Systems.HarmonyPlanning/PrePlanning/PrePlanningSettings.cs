using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Planning.PrePlanning
{
    /// <summary>
    /// Used to save current settings for selected planning
    /// </summary>
    public class PrePlanningSettings
    {

        #region Private Properties
        private int _activityID;
        private string _activityString;
        private string _MOSectionID;
        private string _sectionName;
        private string _prodMonth;
        private string _currentProdMonth;
        private bool _isRevised;
        private double _DefaultAdvance;
 
        #endregion

        #region Public Properties 

        public int ActivityID
        {
            get
            {
                return _activityID;
            }

            set
            {
                _activityID = value;
            }
        }

        public string ActivityString
        {
            get
            {
                return _activityString;
            }

            set
            {
                _activityString = value;
            }
        }

        public string MOSectionID
        {
            get
            {
                return _MOSectionID;
            }

            set
            {
                _MOSectionID = value;
            }
        }

        public string SectionName
        {
            get
            {
                return _sectionName;
            }

            set
            {
                _sectionName = value;
            }
        }

        public string ProdMonth
        {
            get
            {
                return _prodMonth;
            }

            set
            {
                _prodMonth = value;
            }
        }

        public string CurrentProdMonth
        {
            get
            {
                return _currentProdMonth;
            }

            set
            {
                _currentProdMonth = value;
            }
        }

        public bool IsRevised
        {
            get
            {
                return _isRevised;
            }

            set
            {
                _isRevised = value;
            }
        }

        public double DefaultAdvance
        {
            get
            {
                return _DefaultAdvance;
            }

            set
            {
                _DefaultAdvance = value;
            }
        }

        #endregion
    }
}
