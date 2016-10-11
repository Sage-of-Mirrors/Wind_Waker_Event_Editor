using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace Wind_Waker_Event_Editor.src.Editor
{
    /// <summary>
    /// Defines a script within the event_list.dat file.
    /// Actors are parented to it in order to actually run the event.
    /// </summary>
    class Event
    {
        #region string Name
        private string name;

        /// <summary>
        /// The name of the event. This is used to reference the event
        /// from the EVNT chunk of the map's DZS file.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region int Unknown1
        private int unknown1;

        /// <summary>
        /// An unknown integer that seems to be either 0 or 1.
        /// </summary>
        public int Unknown1
        {
            get { return unknown1; }
            set { unknown1 = value; }
        }
        #endregion

        #region Priority
        private int priority;

        /// <summary>
        /// The importance of the event. If two events try to play at the same time,
        /// the game will play the event with higher priority.
        /// </summary>
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        #endregion

        #region List<Actor> Actors
        private List<Actor> actors;

        /// <summary>
        /// The list of actors that belong to this event.
        /// There is a maximum of 20 actors.
        /// </summary>
        public List<Actor> Actors
        {
            get { return actors; }
            set { actors = value; }
        }
        #endregion

        #region bool PlayJingle
        private bool playJingle;

        /// <summary>
        /// If this is true, the famous "Secret found!" jingle will play when the event
        /// is finished playing.
        /// </summary>
        public bool PlayJingle
        {
            get { return playJingle; }
            set { playJingle = value; }
        }
        #endregion

        // This stores the indexes of the actors directly ripped from the file.
        // The indexes stored here will be used to fill the Actors list above
        private List<int> actorIndexes;
        // This stores the flag values that the event has.
        // I'm keeping it as a list since I don't know what exactly these do
        private List<int> flagInts;

        public Event(EndianBinaryReader reader)
        {
            long startOffset = reader.BaseStream.Position;

            // The name gets 32/0x20 bytes of space. We'll read the name until we get to
            // a null terminator, then skip ahead to the Unknown1, skipping the event's index
            // property, using startingOffset + 0x24. 0x20 + 4.
            Name = reader.ReadStringUntil('\0');
            reader.BaseStream.Position = startOffset + 0x24;

            Unknown1 = reader.ReadInt32();
            Priority = reader.ReadInt32();

            actorIndexes = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                actorIndexes.Add(reader.ReadInt32());
            }

            // Since we're given the number of valid actors in the event,
            // we'll remove the invalid ones (stored as -1) that the format
            // uses to pad the actor index array.
            int actorCount = reader.ReadInt32();
            actorIndexes.RemoveRange(actorCount, actorIndexes.Count - actorCount);

            flagInts = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                flagInts.Add(reader.ReadInt32());
            }

            PlayJingle = reader.ReadBoolean();
            
            // Skip ahead to the next event.
            // The data in the 27/0x1B bytes we're skipping is just zero-initialized
            // storage space for the event during gameplay.
            reader.BaseStream.Position += 0x1B;
        }

        /// <summary>
        /// Fills the Actor list by pulling actors from the provided bank
        /// using the indexes that were loaded from the file when the event
        /// was read.
        /// </summary>
        /// <param name="bank">List of Actors to get actors from</param>
        public void FillActorList(List<Actor> bank)
        {
            Actors = new List<Actor>();
            foreach (int i in actorIndexes)
                Actors.Add(bank[i]);
        }
    }
}
