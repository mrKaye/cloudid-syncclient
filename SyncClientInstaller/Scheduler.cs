using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncClientInstaller
{
    public class Scheduler
    {
        public static void ScheduleJob(string filePath)
        {
            if (string.IsNullOrEmpty(Scheduler.GetScheduledTaskPath("SyncClientApp")))
            {
                using (TaskService ts = new TaskService())
                {
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "EVRY - Sync Client Application";

                    TimeTrigger trigger = new TimeTrigger();
                    trigger.StartBoundary = DateTime.Now;
                    trigger.Repetition.Interval = TimeSpan.FromMinutes(15);//GlobalData.ScheduleTime);
                    td.Triggers.Add(trigger);

                    td.Actions.Add(new ExecAction(filePath, null, null));
                    ts.RootFolder.RegisterTaskDefinition("SyncClientApp", td);
                }
            }            
        }

        public static string GetScheduledTaskPath(string schedularName)
        {
            TaskService ts = new TaskService();
            Microsoft.Win32.TaskScheduler.Task scheduledTask = ts.GetTask(schedularName);
            if (scheduledTask != null)
                return scheduledTask.Definition.Actions[0].ToString();
            else
                return null;
        }

        public static void DeleteTask()
        {
            using (TaskService ts = new TaskService())
            {
                if (ts.GetTask("SyncClientApp") != null)
                {
                    ts.RootFolder.DeleteTask("SyncClientApp");
                }
            }
        }
    }
}
