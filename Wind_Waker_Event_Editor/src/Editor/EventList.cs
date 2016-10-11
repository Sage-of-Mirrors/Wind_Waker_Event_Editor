﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GameFormatReader.Common;
using WArchiveTools.FileSystem;

namespace Wind_Waker_Event_Editor.src.Editor
{
    class EventList
    {
        public List<Event> Events;
        public List<Actor> Actors;
        public List<Action> Actions;
        public List<Property> Properties;

        //0x40/64 bytes long
        /*0x00*/
        private int EventOffset;
        /*0x04*/
        private int EventCount;
        /*0x08*/
        private int ActorOffset;
        /*0x0C*/
        private int ActorCount;
        /*0x10*/
        private int ActionOffset;
        /*0x14*/
        private int ActionCount;
        /*0x18*/
        private int PropertyOffset;
        /*0x1C*/
        private int PropertyCount;
        /*0x20*/
        private int FloatBankOffset;
        /*0x24*/
        private int FloatBankCount;
        /*0x28*/
        private int IntegerBankOffset;
        /*0x2C*/
        private int IntegerBankCount;
        /*0x30*/
        private int StringBankOffset;
        /*0x34*/
        private int StringBankLengthCount;

        public EventList(VirtualFilesystemFile event_list)
        {
            using (MemoryStream stream = new MemoryStream(event_list.File.GetData()))
            {
                EndianBinaryReader reader = new EndianBinaryReader(stream, Endian.Big);

                EventOffset = reader.ReadInt32();
                EventCount = reader.ReadInt32();
                ActorOffset = reader.ReadInt32();
                ActorCount = reader.ReadInt32();
                ActionOffset = reader.ReadInt32();
                ActionCount = reader.ReadInt32();
                PropertyOffset = reader.ReadInt32();
                PropertyCount = reader.ReadInt32();
                FloatBankOffset = reader.ReadInt32();
                FloatBankCount = reader.ReadInt32();
                IntegerBankOffset = reader.ReadInt32();
                IntegerBankCount = reader.ReadInt32();
                StringBankOffset = reader.ReadInt32();
                StringBankLengthCount = reader.ReadInt32();

                // Skip the 8 byte padding between the header and the event data
                reader.SkipInt64();
            }
        }
    }
}
