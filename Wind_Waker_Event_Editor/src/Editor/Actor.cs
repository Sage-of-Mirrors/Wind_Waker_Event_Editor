using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind_Waker_Event_Editor.src.Editor
{
    /// <summary>
    /// Defines an event object that typically represents an entity in the scene.
    /// It uses Actions to do things within the event.
    /// </summary>
    class Actor : IFlaggable
    {
        #region string Name

        private string name;

        /// <summary>
        /// The name of the actor.
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
        /// The flag that this actor sets when it has finished all of its actions.
        /// </summary>
        public int FlagID
        {
            get { return flagID; }
            set { flagID = value; }
        }

        #endregion

        #region int StaffIdentifier

        private int staffIdentifier;

        /// <summary>
        /// An identifier for the staff?
        /// Not much is known about this.
        /// </summary>
        public int StaffIdentifier
        {
            get { return staffIdentifier; }
            set { staffIdentifier = value; }
        }

        #endregion

        #region int StaffType

        private int staffType;

        /// <summary>
        /// A type for the staff?
        /// Not much is known about this.
        /// </summary>
        public int StaffType
        {
            get { return staffType; }
            set { staffType = value; }
        }


        #endregion

        #region List<Action> Actions

        private List<Action> actions;

        /// <summary>
        /// A list of Actions that this actor performs.
        /// </summary>
        public List<Action> Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        #endregion
    }
}
