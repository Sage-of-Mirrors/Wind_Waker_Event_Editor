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
    class EventNode : Node
    {
        /// <summary>
        /// This is the event that this node represents.
        /// </summary>
        private Event AttatchedEvent;

        public EventNode(string title) : base(title)
        {
        }

        public EventNode(Event ev) : base("Event")
        {
            AttatchedEvent = ev;

            NodeTextBoxItem nameBox = new NodeTextBoxItem(AttatchedEvent.Name);
            nameBox.TextChanged += NameBox_TextChanged;
            AddItem(nameBox);
            NodeNumericSliderItem priorityBox = new NodeNumericSliderItem("Priority: ", 100, 0, 0, 100, AttatchedEvent.Priority, false, false);
            priorityBox.ValueChanged += PriorityBox_ValueChanged;
            AddItem(priorityBox);
            NodeCheckboxItem jingleBox = new NodeCheckboxItem("Play Jingle", false, false);
            jingleBox.Checked = AttatchedEvent.PlayJingle;
            AddItem(jingleBox);
        }

        private void PriorityBox_ValueChanged(object sender, NodeItemEventArgs e)
        {
            if (sender.GetType() == typeof(NodeNumericSliderItem))
            {
                NodeNumericSliderItem num = sender as NodeNumericSliderItem;
                num.Value = (int)num.Value;
                AttatchedEvent.Priority = (int)num.Value;
            }
        }

        private void NameBox_TextChanged(object sender, AcceptNodeTextChangedEventArgs e)
        {
            AttatchedEvent.Name = e.Text;
        }
    }
}
