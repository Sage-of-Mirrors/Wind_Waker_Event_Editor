using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using WArchiveTools;
using WArchiveTools.FileSystem;
using GameFormatReader.Common;
using Ookii.Dialogs.Wpf;

namespace Wind_Waker_Event_Editor.src.Editor.ViewModel
{
    partial class ViewModel : INotifyPropertyChanged
    {
        #region EventList MasterList
        private EventList masterList;

        public EventList MasterList
        {
            get { return masterList; }
            set
            {
                if (masterList != value)
                {
                    masterList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region string WindowTitle
        private string windowTitle;

        public string WindowTitle
        {
            get { return windowTitle + " - Wind Waker Event Editor"; }
            set
            {
                if (windowTitle != value)
                {
                    windowTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        private void Open()
        {
            VistaFolderBrowserDialog dia = new VistaFolderBrowserDialog();
            dia.Description = "Open map folder";
            dia.UseDescriptionForTitle = true;

            if ((bool)dia.ShowDialog())
            {
                OpenFromPath(dia.SelectedPath);
            }
        }

        private void OpenFromPath(string path)
        {
            string[] dirs = Directory.GetFiles(path);

            try
            {
                foreach (string dir in dirs)
                {
                    // This will load the stage archive
                    if (dir.ToLower().EndsWith("stage.arc"))
                    {
                        OpenStageArc(dir);
                    }
                    // This will load a room archive
                    else if (dir.ToLower().Contains("room"))
                    {
                        OpenRoomArc(dir);
                    }
                    else
                        continue;
                }
            }
            catch
            {

            }

            WindowTitle = path;
        }

        private void OpenStageArc(string arc)
        {
            VirtualFilesystemDirectory loadedArc = ArchiveUtilities.LoadArchive(arc);

            // Search for a directory called "dat'
            foreach (VirtualFilesystemNode node in loadedArc.Children)
            {
                if (node.Name != "dat")
                    continue;

                VirtualFilesystemDirectory datDir = node as VirtualFilesystemDirectory;

                // Search for a file called "event_list"
                foreach (VirtualFilesystemNode fil in datDir.Children)
                {
                    if (fil.Name == "event_list" && fil.GetType() == typeof(VirtualFilesystemFile))
                    {
                        MasterList = new EventList(fil as VirtualFilesystemFile);
                    }
                }
            }
        }

        private void OpenRoomArc(string arc)
        {

        }
    }
}
