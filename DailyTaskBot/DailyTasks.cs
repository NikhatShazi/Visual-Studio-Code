using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DailyTaskBot
{
    public partial class DailyTasks : Form
    {
        DailyTaskContext context;
        EmployeeDailyTask model;
        int step = 1;

        public DailyTasks(DbContextOptions<DailyTaskContext> options)
        {
            InitializeComponent();

            // Load connection string from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DailyTaskBotEntities")!;

            // Initialize EF6 context with the connection string
            context = new DailyTaskContext(options);
            model = new EmployeeDailyTask();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string description = txtTaskDescription.Text;

                switch (step)
                {
                    case 1:
                        {
                            if (string.IsNullOrEmpty(description))
                            {
                                lblMessage.Text = "Please enter yesterday's task.";
                                return;
                            }
                            string userName = Environment.UserName;
                            model.EmployeeName = userName;
                            model.YesterdaysTask = txtTaskDescription.Text;
                            step = 2;

                            lblTaskType.Text = "What are you doing today?";
                            break;
                        }
                    case 2:
                        {
                            if (string.IsNullOrEmpty(description))
                            {
                                lblMessage.Text = "Please enter today's task.";
                                return;
                            }
                            model.TodaysTask = txtTaskDescription.Text;
                            step = 3;

                            lblTaskType.Text = "Any obstacle?";

                            btnSubmit.Text = "Submit";
                            break;
                        }
                    case 3:
                        {
                            model.Obstacle = txtTaskDescription.Text;
                            model.CreatedDate = DateTime.Now;

                            context.EmployeeDailyTasks.Add(model);
                            context.SaveChanges();

                            step = 1;
                            lblTaskType.Text = "What you did yesterday?";
                            btnSubmit.Text = "Next";
                            Application.Exit();
                            break;
                        }
                }
                lblMessage.Text = string.Empty;
                txtTaskDescription.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
