using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

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

        private int initialActionIndex;

        public Actor(EndianBinaryReader reader)
        {
            // The name gets 32/0x20 bytes of space. We'll read the field until we hit
            // null terminator and then skip ahead using the startOffset we store + 0x20.
            long startOffset = reader.BaseStream.Position;
            Name = reader.ReadStringUntil('\0');
            reader.BaseStream.Position = startOffset + 0x20;

            StaffIdentifier = reader.ReadInt32();

            // Skipping the Action's index. We don't need it.
            reader.SkipInt32();

            FlagID = reader.ReadInt32();

            StaffType = reader.ReadInt32();

            initialActionIndex = reader.ReadInt32();

            // Skip to the next Action. The 28/0x1C bytes we're skipping
            // are zero-initialized storage space for data during gameplay.
            reader.BaseStream.Position += 0x1C;
        }

        /// <summary>
        /// Fills the Actions list using the provided bank of Actions
        /// and initialActionIndex as the starting point.
        /// </summary>
        /// <param name="bank">List of Actions to get the Actions from</param>
        public void FillActionList(List<Action> bank)
        {
            Actions = new List<Action>();
            Action current = bank[initialActionIndex];

            while (current != null)
            {
                Actions.Add(current);
                current = current.NextAction;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} Actions", name, Actions == null ? 0 : Actions.Count);
        }
    }
}
