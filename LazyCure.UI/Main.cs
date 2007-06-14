using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LifeIdea.LazyCure.Interfaces;
using System.Diagnostics;

namespace LifeIdea.LazyCure.UI
{
    public partial class Main : Form
    {
        private ILazyCureDriver lazyCure;
        private string nextActivity = "(specify what you are doing)";

        public Main(ILazyCureDriver driver)
        {
            InitializeComponent();
            this.lazyCure = driver;
            Dialogs.LazyCureDriver = driver;
            timer.Start();
            SetCaption();
            UpdateCurrentActivity();
            UpdateActivityStartTime();
        }
        private void SetCaption()
        {
            string[] versionNumbers = Application.ProductVersion.Split('.');
            this.Text = String.Format("{0} - {1} {2}.{3}",lazyCure.TimeLogDate,Application.ProductName,
                versionNumbers[0],versionNumbers[1]);
        }
        private void UpdateActivityStartTime()
        {
            this.activityStartTime.Text = Format.Time(lazyCure.CurrentActivity.StartTime);
        }

        private void showTimeLog_Click(object sender, EventArgs e)
        {
            Dialogs.TimeLog.Show();
        }

        private void switchButton_Click(object sender, EventArgs e)
        {
            lazyCure.FinishActivity(this.currentActivity.Text,nextActivity);
            UpdateCurrentActivity();
            UpdateActivityStartTime();
            currentActivity.SelectAll();
        }

        private void UpdateCurrentActivity()
        {
            currentActivity.Text = nextActivity;
            currentActivity.Items.Clear();
            currentActivity.Items.AddRange(lazyCure.LatestActivities);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.activityDuration.Text = Format.Duration(lazyCure.CurrentActivity.Duration);
            this.currentTime.Text = Format.Time(DateTime.Now);
        }

        private void activityStartTime_TextChanged(object sender, EventArgs e)
        {
            lazyCure.CurrentActivity.StartTime = DateTime.Parse(this.activityStartTime.Text);
        }

        private void miShowSummary_Click(object sender, EventArgs e)
        {
            Dialogs.Summary.Show();
        }

        private void miActivityDetails_Click(object sender, EventArgs e)
        {
            statusBar.Visible = miActivityDetails.Checked;
            if (statusBar.Visible)
                this.Size = new Size(300, 128);
            else
                this.Size = new Size(300, 128-statusBar.Height);
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            this.currentActivity.Focus();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            Dialogs.About.Show();
        }

        private void miOnline_Click(object sender, EventArgs e)
        {
            Process.Start("http://lifeidea.org/lazycure/");
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!lazyCure.SaveTimeLog())
            {
                DialogResult result = MessageBox.Show("Time Log could not be saved. Exit from LazyCure anyway?", "Could not save time log", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                    return;
                else
                    e.Cancel = true;
            }
        }
        private void miSave_Click(object sender, EventArgs e)
        {
            if (!lazyCure.SaveTimeLog())
                MessageBox.Show("Time log has not been saved!", "Saving  error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = Dialogs.Open;
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                lazyCure.LoadTimeLog(openDialog.FileName);
                SetCaption();
            }
        }
        
        private void miSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = Dialogs.Save;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                lazyCure.SaveTimeLog(saveDialog.FileName);
            }
        }
    }
}
