using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using Graph.Items;

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
    }
}
