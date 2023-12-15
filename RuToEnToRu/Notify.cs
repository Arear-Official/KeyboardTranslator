using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;
using FontStyle = System.Drawing.FontStyle;

namespace RuToEnToRu
{
    internal class Notify
    {
        private NotifyIcon _notifyIcon;

        private Window _mainwindow;

        public Notify() 
        { 
            _mainwindow = Application.Current.MainWindow;
            CreateNotifeyIcon();
        }

        public Notify(Window mainwindow)
        {
            _mainwindow = mainwindow;
            CreateNotifeyIcon();
        }

        private void CreateNotifeyIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = new System.Drawing.Icon("RuEn.ico");
            _notifyIcon.Text = "Translator";
            SetVisible(false);
            
            ContextMenuStrip contextMenu = new ContextMenuStrip();
          
            contextMenu.Items.Add("Развернуть");
            contextMenu.Items.Add("Выключить");
            contextMenu.Font = new Font(FontFamily.GenericMonospace,12,FontStyle.Bold);
            contextMenu.Items[0].Click += OpenMenuItem_Click;
            contextMenu.Items[1].Click += ExitMenuItem_Click;
            foreach (ToolStripMenuItem item in contextMenu.Items)
            {
                item.BackColor = Color.FromArgb(255,50,50,50);
                item.ForeColor = Color.WhiteSmoke;
            }

            _notifyIcon.ContextMenuStrip = contextMenu;

            _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            _mainwindow.Show();
            _mainwindow.WindowState = WindowState.Normal;
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            _mainwindow.Show();
            _mainwindow.WindowState = WindowState.Normal;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void SetVisible(bool Visible) 
        {
            _notifyIcon.Visible=Visible;
        }  
        
        public void Destroy()
        {
            _notifyIcon.Dispose();
        }
    }
}
