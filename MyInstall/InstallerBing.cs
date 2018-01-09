using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstall
{
	[RunInstaller(true)]
	public partial class InstallerBing : System.Configuration.Install.Installer
	{
		public InstallerBing()
		{
			InitializeComponent();
			//this.AfterInstall += new InstallEventHandler(InstallerBing_AfterInstall);
		}
		protected override void OnAfterInstall(IDictionary savedState)
		{
			LogWrite("OnAfterInstall！");
			string path = this.Context.Parameters["targetdir"];//获取用户设定的安装目标路径, 注意，需要在Setup项目里面自定义操作的属性栏里面的CustomActionData添加上/targetdir="[TARGETDIR]\"
			LogWrite("安装路径："+path);                                                //开机启动
			RegistryKey hklm = Registry.LocalMachine;
			RegistryKey run = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

			try
			{//64位系统在计算机\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
				LogWrite("设置注册表！");
				LogWrite("bingpi路径："+path.Substring(1, path.Length - 4) + @"bin\\BingPic.exe");
				run.CreateSubKey("BingPic应用", true);
				//run.CreateSubKey("BingPic应用");
				LogWrite("添加注册表项路径：" + run.ToString());
				run.SetValue("BingPic应用",path.Substring(1, path.Length - 4) + @"bin\\BingPic.exe");
				hklm.Close();
				LogWrite("设置结束！");

			}
			catch (Exception my)
			{
				my.ToString();
				LogWrite(my.ToString());
			}
			base.OnAfterInstall(savedState);
		}

		public override void Install(IDictionary stateSaver)
		{
			LogWrite("Install！");
			base.Install(stateSaver);
		}

		protected override void OnBeforeInstall(IDictionary savedState)
		{
			LogWrite("OnBeforeInstall!");
			base.OnBeforeInstall(savedState);
		}

		protected override void OnBeforeUninstall(IDictionary savedState)
		{
			LogWrite("OnBeforeUninstall");
			//开机启动
			RegistryKey hklm = Registry.LocalMachine;
			RegistryKey run = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

			try
			{//64位系统在计算机\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run
				LogWrite("删除注册表！");
				run.DeleteSubKey("BingPic应用", true);
				LogWrite("删除注册表项路径：" + run.ToString());
				hklm.Close();
				LogWrite("删除结束！");

			}
			catch (Exception my)
			{
				my.ToString();
				LogWrite(my.ToString());
			}
			base.OnBeforeUninstall(savedState);
		}

		public override void Uninstall(IDictionary savedState)
		{
			LogWrite("Uninstall!");
			base.Uninstall(savedState);
		}

		protected override void OnAfterUninstall(IDictionary savedState)
		{
			//string path = this.Context.Parameters["targetdir"];//获取用户设定的安装目标路径, 注意，需要在Setup项目里面自定义操作的属性栏里面的CustomActionData添加上/targetdir="[TARGETDIR]\"
			//Directory.Delete(path, true);
			LogWrite("OnAfterUninstall");
			base.OnAfterUninstall(savedState);
		}

		public override void Rollback(IDictionary savedState)
		{
			LogWrite("Rollback");
			base.Rollback(savedState);
		}

		public void LogWrite(string str)
		{
			string LogPath = @"c:\log\";
			if (!Directory.Exists(LogPath))
				Directory.CreateDirectory(LogPath);
			using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogPath + @"SetUpLog.txt", true))
			{
				sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + str + "\n");

			}
		}





	}
}
