﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind_Waker_Event_Editor.src.Editor;
using Graph;
using Graph.Items;

namespace Wind_Waker_Event_Editor.src.Graph
{
    class ActionNode : Node
    {
        public NodeLabelItem LastNodeConnection;
        public NodeLabelItem CompletedConnection;
        public NodeLabelItem PropertiesConnection;
        private Editor.Action actionRef;

        /// <summary>
        /// A reference to the action that this node represents.
        /// </summary>
        public Editor.Action ActionRef
        {
            get { return actionRef; }
            set { actionRef = value; }
        }

        public ActionNode(string title) : base(title)
        {
            AddItem(new NodeTextBoxItem("name", false, false));
            AddItem(new NodeLabelItem("Completed", false, true));
            AddItem(new NodeLabelItem("Last Action", true, false));
            AddItem(new NodeLabelItem("Wait 1", true, false));
            AddItem(new NodeLabelItem("Wait 2", true, false));
            AddItem(new NodeLabelItem("Wait 3", true, false));
            AddItem(new NodeLabelItem("Properties", false, true));
        }

        public ActionNode(Editor.Action act) : base("Action")
        {
            AddItem(new NodeTextBoxItem(act.Name, false, false));
            LastNodeConnection = new NodeLabelItem("Last Action", true, false);
            AddItem(LastNodeConnection);
            AddItem(new NodeLabelItem("Wait 1", true, false));
            AddItem(new NodeLabelItem("Wait 2", true, false));
            AddItem(new NodeLabelItem("Wait 3", true, false));
            CompletedConnection = new NodeLabelItem("Completed", false, true);
            AddItem(CompletedConnection);
            PropertiesConnection = new NodeLabelItem("Properties", false, true);
            AddItem(PropertiesConnection);

            ActionRef = act;
        }
    }
}
