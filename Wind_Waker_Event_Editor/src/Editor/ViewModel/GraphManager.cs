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

                for (int i = 0; i < act.Actions.Count; i++)
                {
                    ActionNode actionNode = new ActionNode(act.Actions[i]);
                    actionNode.Location = actNodeLoc;
                    actNodeLoc.X += 150;
                    currentNodes.Add(actionNode);
                    Graph.AddNode(actionNode);
                    
                    if (i == 0)
                    {
                        Graph.Connect(actNode.Items.First(x => x.GetType() == typeof(NodeLabelItem)), actionNode.Items.First(x => x.GetType() == typeof(NodeLabelItem)));
                    }
                }

                actNodeLoc.X = 0;
                actNodeLoc.Y += 220;
            }
        }
    }
}
