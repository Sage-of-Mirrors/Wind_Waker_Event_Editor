using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using GameFormatReader.Common;

namespace Wind_Waker_Event_Editor.src.Editor
{
    /// <summary>
    /// Defines a primitive data type for use by an Action.
    /// </summary>
    class Property
    {
        #region string Name
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region PropertyType DataType
        private PropertyType dataType;

        public PropertyType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
        #endregion

        #region Object Data
        private Object data;

        public Object Data
        {
            get
            {
                switch (DataType)
                {
                    case PropertyType.Single:
                        return (float)data;
                    case PropertyType.Vec3:
                    return (Vector3)data;
                    case PropertyType.Integer:
                        return (int)data;
                    case PropertyType.String:
                        return (string)data;
                    default:
                        return null;
                }
            }
            set
            {
                switch (DataType)
                {
                    case PropertyType.Single:
                        if (value.GetType() == typeof(float))
                            data = (float)value;
                        break;
                    case PropertyType.Vec3:
                        if (value.GetType() == typeof(Vector3))
                            data = (Vector3)value;
                        break;
                    default:
                        data = value;
                        break;
                }
            }
        }
        #endregion

        #region Property NextProperty
        private Property nextProperty;

        public Property NextProperty
        {
            get { return nextProperty; }
            set { nextProperty = value; }
        }
        #endregion

        private int dataOffset;
        private int dataLength;
        private int nextPropertyIndex;

        public Property(EndianBinaryReader reader)
        {
            // The name gets 32/0x20 bytes of space. We'll read the field until we hit
            // null terminator and then skip ahead using the startOffset we store + 0x20.
            long startOffset = reader.BaseStream.Position;
            Name = reader.ReadStringUntil('\0');
            reader.BaseStream.Position = startOffset + 0x20;

            reader.SkipInt32();

            DataType = (PropertyType)reader.ReadInt32();

            dataOffset = reader.ReadInt32();
            dataLength = reader.ReadInt32();

            nextPropertyIndex = reader.ReadInt32();

            reader.BaseStream.Position += 0xC;
        }

        public void GetNextProperty(List<Property> bank)
        {
            if (nextPropertyIndex == -1)
                NextProperty = null;
            else
                NextProperty = bank[nextPropertyIndex];
        }

        public void GetPropData(List<float> floatBank, List<int> intBank, char[] stringBank)
        {
            switch (DataType)
            {
                case PropertyType.Single:
                    Data = floatBank[dataOffset];
                    break;
                case PropertyType.Vec3:
                    Data = new Vector3(floatBank[dataOffset], floatBank[dataOffset + 1], floatBank[dataOffset + 2]);
                    break;
                case PropertyType.Integer:
                    Data = intBank[dataOffset];
                    break;
                case PropertyType.String:
                    char[] newString = new char[dataLength];
                    for (int i = 0; i < dataLength; i++)
                    {
                        newString[i] = stringBank[dataOffset + i];
                    }
                    Data = new string(newString);
                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, type {1}, data {2}", Name, DataType, Data);
        }
    }
}
