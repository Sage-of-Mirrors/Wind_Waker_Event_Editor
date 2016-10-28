using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using Graph.Items;
using Wind_Waker_Event_Editor.src.Editor;

namespace Wind_Waker_Event_Editor.src.Graph
{
    class ActorNode : Node
    {
        public NodeLabelItem ActionConnectorLabel;
        private Actor AttatchedActor;

        public ActorNode(string title) : base(title)
        {
        }

        public ActorNode(Actor act) : base("Actor")
        {
            AttatchedActor = act;

            NodeTextBoxItem nameBox = new NodeTextBoxItem(AttatchedActor.Name);
            nameBox.TextChanged += NameBox_TextChanged;
            AddItem(nameBox);
            NodeNumericSliderItem staffIDBox = new NodeNumericSliderItem("Staff ID:", 100, 0, 0, 100, AttatchedActor.StaffIdentifier, false, false);
            staffIDBox.ValueChanged += StaffIDBox_ValueChanged;
            AddItem(staffIDBox);
            NodeNumericSliderItem StaffTypeBox = new NodeNumericSliderItem("Staff Type:", 100, 0, 0, 100, AttatchedActor.StaffType, false, false);
            StaffTypeBox.ValueChanged += StaffTypeBox_ValueChanged;
            AddItem(StaffTypeBox);
            ActionConnectorLabel = new NodeLabelItem("Actions", false, true);
            AddItem(ActionConnectorLabel);
        }

        private void StaffTypeBox_ValueChanged(object sender, NodeItemEventArgs e)
        {
            if (sender.GetType() == typeof(NodeNumericSliderItem))
            {
                NodeNumericSliderItem num = sender as NodeNumericSliderItem;
                num.Value = (int)num.Value;
                AttatchedActor.StaffType = (int)num.Value;
            }
        }

        private void StaffIDBox_ValueChanged(object sender, NodeItemEventArgs e)
        {
            if (sender.GetType() == typeof(NodeNumericSliderItem))
            {
                NodeNumericSliderItem num = sender as NodeNumericSliderItem;
                num.Value = (int)num.Value;
                AttatchedActor.StaffIdentifier = (int)num.Value;
            }
        }

        private void NameBox_TextChanged(object sender, AcceptNodeTextChangedEventArgs e)
        {
            AttatchedActor.Name = e.Text;
        }
    }
}
