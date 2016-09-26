using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind_Waker_Event_Editor.src.Editor
{
    /// <summary>
    /// Defines a function of an actor.
    /// It uses Properties to contain the data needed to complete that function.
    /// </summary>
    class Action : IFlaggable
    {
        #region string Name

        private string name;

        /// <summary>
        /// The name of the action.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region int FlagID

        private int flagID;

        /// <summary>
        /// The flag that this action sets when it is completed.
        /// </summary>
        public int FlagID
        {
            get { return flagID; }
            set { flagID = value; }
        }

        #endregion

        #region IFlaggable WaitFlag1

        private IFlaggable waitFlag1;

        /// <summary>
        /// The first flag that must be set before this action can run.
        /// If this is null, the action runs regardless.
        /// </summary>
        public IFlaggable WaitFlag1
        {
            get { return WaitFlag1; }
            set { WaitFlag1 = value; }
        }

        #endregion

        #region IFlaggable WaitFlag2

        private IFlaggable waitFlag2;

        /// <summary>
        /// The second flag that must be set before this action can run.
        /// If WaitFlag1 is null, then this should be, too.
        /// </summary>
        public IFlaggable WaitFlag2
        {
            get { return waitFlag2; }
            set { waitFlag2 = value; }
        }


        #endregion

        #region IFlaggable WaitFlag3

        private IFlaggable waitFlag3;

        /// <summary>
        /// The third flag that must be set before this action can run.
        /// If WaitFlag1 is null, then this should be, too.
        /// </summary>
        public IFlaggable WaitFlag3
        {
            get { return waitFlag3; }
            set { waitFlag3 = value; }
        }


        #endregion

        #region Action NextAction

        private Action nextAction;

        /// <summary>
        /// The action that runs after this action has been completed.
        /// </summary>
        public Action NextAction
        {
            get { return nextAction; }
            set { nextAction = value; }
        }

        #endregion

        #region List<Property> Properties

        private List<Property> properties;

        /// <summary>
        /// The list of properties that this action uses.
        /// </summary>
        public List<Property> Properties
        {
            get { return properties; }
            set { properties = value; }
        }

        #endregion
    }
}
