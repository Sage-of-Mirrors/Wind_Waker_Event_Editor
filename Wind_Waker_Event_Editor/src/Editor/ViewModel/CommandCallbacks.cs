using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Wind_Waker_Event_Editor.src.Editor.ViewModel
{
    partial class ViewModel
    {
        #region Command Callbacks
        public ICommand OnRequestOpenMap
        {
            get { return new RelayCommand(x => Open()); }
        }

        public ICommand OnRequestSave
        {
            get { return new RelayCommand(x => ReportBug()); }
        }

        public ICommand OnRequestSaveAs
        {
            get { return new RelayCommand(x => ReportBug()); }
        }

        public ICommand OnRequestCloseMap
        {
            get { return new RelayCommand(x => ReportBug()); }
        }

        public ICommand OnRequestExitApp
        {
            get { return new RelayCommand(x => ExitApplication()); }
        }
        /// <summary> The user has clicked Report a Bug... from the Help menu. </summary>
        public ICommand OnRequestReportBug
        {
            get { return new RelayCommand(x => ReportBug()); }
        }

        /// <summary> The user has clicked Wiki from the Help menu. </summary>
        public ICommand OnRequestOpenWiki
        {
            get { return new RelayCommand(x => OpenWiki()); }
        }

        public ICommand OnRequestDisplayAbout
        {
            get { return new RelayCommand(x => OpenWiki()); }
        }
        #endregion

        #region Callback Methods
        private void ExitApplication()
        {
            Application.Current.MainWindow.Close();
        }
        /// <summary>
        /// Opens the user's default browser to Wind_Waker_Event_Editor's Issues page.
        /// </summary>
        private void ReportBug()
        {
            System.Diagnostics.Process.Start("https://github.com/Sage-of-Mirrors/Wind_Waker_Event_Editor/issues");
        }

        /// <summary>
        /// Opens the user's default browser to Wind_Waker_Event_Editor's Wiki page.
        /// </summary>
        private void OpenWiki()
        {
            System.Diagnostics.Process.Start("https://github.com/Sage-of-Mirrors/Wind_Waker_Event_Editor/wiki");
        }
        #endregion
    }
}
