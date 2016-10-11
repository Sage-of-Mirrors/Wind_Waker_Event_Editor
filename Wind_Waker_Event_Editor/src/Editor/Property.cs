using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind_Waker_Event_Editor.src.Editor
{
    /// <summary>
    /// Defines a primitive data type for use by an Action.
    /// </summary>
    class Property
    {
        #region Property NextProperty
        private Property nextProperty;

        public Property NextProperty
        {
            get { return nextProperty; }
            set { nextProperty = value; }
        }
        #endregion
    }
}
