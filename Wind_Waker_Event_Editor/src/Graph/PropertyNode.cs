using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind_Waker_Event_Editor.src.Editor;
using Graph;
using Graph.Items;

namespace Wind_Waker_Event_Editor.src.Graph
{
    class PropertyNode : Node
    {
        public NodeLabelItem LastNodeConnection;
        public NodeLabelItem NextNodeConnection;
        private Property AttatchedProperty;

        public PropertyNode(string title) : base(title)
        {
        }

        public PropertyNode(Property prop) : base("Property")
        {
            AttatchedProperty = prop;
            NodeLabelItem nameLabel = new NodeLabelItem(prop.Name);
            AddItem(nameLabel);
            LastNodeConnection = new NodeLabelItem("Last Property", true, false);
            AddItem(LastNodeConnection);

            switch(prop.DataType)
            {
                case PropertyType.Single:
                    NodeNumericSliderItem singleSlider = new NodeNumericSliderItem("Value", 100, 0, 0, 100000, (float)prop.Data, false, false);
                    AddItem(singleSlider);
                    break;
                case PropertyType.Vec3:
                    NodeLabelItem temp = new NodeLabelItem("temp");
                    AddItem(temp);
                    break;
                case PropertyType.Integer:
                    NodeNumericSliderItem intSlider = new NodeNumericSliderItem("Value", 100, 0, 0, 100000, (int)prop.Data, false, false);
                    AddItem(intSlider);
                    break;
                case PropertyType.String:
                    NodeTextBoxItem stringBox = new NodeTextBoxItem((string)prop.Data);
                    stringBox.TextChanged += StringBox_TextChanged;
                    AddItem(stringBox);
                    break;
                default:
                    break;
            }

            NextNodeConnection = new NodeLabelItem("Next Property", false, true);
            AddItem(NextNodeConnection);
        }

        private void StringBox_TextChanged(object sender, AcceptNodeTextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
