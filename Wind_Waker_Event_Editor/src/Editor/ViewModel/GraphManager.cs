using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using Graph.Items;
using Wind_Waker_Event_Editor.src.Graph;

namespace Wind_Waker_Event_Editor.src.Editor.ViewModel
{
    partial class ViewModel : INotifyPropertyChanged
    {
        #region GraphControl Graph
        private GraphControl graph;

        public GraphControl Graph
        {
            get { return graph; }
            set
            {
                if (graph != value)
                {
                    graph = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Event CurrentEvent
        private Event curEvent;

        public Event CurrentEvent
        {
            get { return curEvent; }
            set
            {
                if (curEvent != value)
                {
                    curEvent = value;
                    NotifyPropertyChanged();
                    BuildGraph(curEvent);
                }
                curEvent = value;
            }
        }

        #endregion

        // This holds the nodes we're currently displaying in the graph.
        // It's because the graph lacks a way to remove all of the nodes
        // easily, so since I'm lazy I'll just use the RemoveNodes() method
        // with this
        private List<Node> currentNodes = new List<Node>();

        private void BuildGraph(Event ev)
        {
            Graph.RemoveNodes(currentNodes);
            currentNodes.Clear();

            EventNode evt = new EventNode(ev);
            currentNodes.Add(evt);
            Graph.AddNode(evt);

            PointF actNodeLoc = new PointF(0, 150);

            foreach (Actor act in ev.Actors)
            {
                ActorNode actNode = new ActorNode(act);
                actNode.Location = actNodeLoc;
                currentNodes.Add(actNode);
                Graph.AddNode(actNode);

                actNodeLoc.X = 250;

                List<ActionNode> currentActionNodes = new List<ActionNode>();
                for (int i = 0; i < act.Actions.Count; i++)
                {
                    ActionNode actionNode = new ActionNode(act.Actions[i]);
                    actionNode.Location = actNodeLoc;
                    actNodeLoc.X += 150;
                    currentActionNodes.Add(actionNode);
                    Graph.AddNode(actionNode);

                    List<PropertyNode> currentPropNodes = new List<PropertyNode>();
                    float curY = actNodeLoc.Y;
                    actNodeLoc.Y += 150;
                    for (int j = 0; j < act.Actions[i].Properties.Count; j++)
                    {
                        PropertyNode propNode = new PropertyNode(act.Actions[i].Properties[j]);
                        propNode.Location = actNodeLoc;
                        actNodeLoc.X += 150;
                        currentPropNodes.Add(propNode);
                        Graph.AddNode(propNode);

                        if (j == 0)
                            Graph.Connect(actionNode.PropertiesConnection, propNode.LastNodeConnection);
                        else if (j >= 1)
                        {
                            Graph.Connect(currentPropNodes[j - 1].NextNodeConnection, currentPropNodes[j].LastNodeConnection);
                        }

                    }
                    actNodeLoc.Y = curY;

                    currentNodes.AddRange(currentPropNodes);

                    if (i == 0)
                    {
                        Graph.Connect(actNode.ActionConnectorLabel, actionNode.LastNodeConnection);
                    }
                    else if (i >= 1)
                    {
                        Graph.Connect(currentActionNodes[i - 1].CompletedConnection, currentActionNodes[i].LastNodeConnection);
                    }
                }

                currentNodes.AddRange(currentActionNodes);
                actNodeLoc.X = 0;
                actNodeLoc.Y += 280;
            }
        }
    }
}
