using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Threading;
using Microsoft.VisualBasic;
namespace BingPic
{
	public partial class Form1 : Form
	{
		static string url0 = "http://cn.bing.com/";
		static string[] url = { "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=-1&n=8", "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=7&n=8" };//前8天 ConfigurationSettings.AppSettings.GetValues("url").ToString();
																																							  //static string url2 =;//后八天
		static string ImageSavePath = @"C:\Users\" + System.Environment.UserName + @"\Pictures\BingWallpaper"; //保存墙纸路径 
		[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
		public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
		Thread threadOfSetDeskBackGround = new Thread(SetDeskBackGround);
		Thread threadOfDownloadWallpaperToDisk = new Thread(DownloadWallpaperToDisk);
		public Form1()
		{
			InitializeComponent();
			this.label_Version_Value.Text = "V" + ConfigurationManager.AppSettings["sysVersion"].ToString();
			Start();
		}

		/**开始切换保存壁纸*/
		public void Start()
		{
			//string n = System.Environment.MachineName;

			//	string userName=System.Environment.UserName;
			if (!threadOfSetDeskBackGround.IsAlive)
			{
				threadOfSetDeskBackGround.Start();

			}
			if (!threadOfDownloadWallpaperToDisk.IsAlive)
			{
				threadOfDownloadWallpaperToDisk.Start();

			}
			this.Hide();
		}
		/**停止*/
		public void Stop()
		{


			if (threadOfSetDeskBackGround.IsAlive)
			{
				threadOfSetDeskBackGround.Abort();

			}
			if (threadOfDownloadWallpaperToDisk.IsAlive)
			{
				threadOfDownloadWallpaperToDisk.Abort();

			}

		}

		/**获取近16的图片url**/
		public static List<ImageInfo> GetUrls()
		{
			List<ImageInfo> list = new List<ImageInfo> { };
			try
			{
				foreach (string ur in url)
				{
					string postContent = GetPostInfo(ur);
					JObject jo = (JObject)JsonConvert.DeserializeObject(postContent);
					//获取第一个json数组
					JArray jArray = JArray.Parse(jo["images"].ToString());
					string[] urls = new string[jArray.Count];
					foreach (var item in jArray)
					{
						ImageInfo imageInfo = new ImageInfo();
						imageInfo.Url = url0 + item["url"].ToString();
						imageInfo.Startdate = item["startdate"].ToString();
						imageInfo.Copyright = item["copyright"].ToString();
						list.Add(imageInfo);
						//urls[count++] = url0 + item["url"].ToString();
					}
				}
			}
			catch (Exception e)
			{
				LogWrite("GetUrls ：" + e.ToString());
			}
			return list;
		}

		/**获取近16天的图片url**/
		public static List<ImageInfo> GetUrls(string thisistest)
		{
			List<ImageInfo> list = new List<ImageInfo> { };
			try
			{
				StringBuilder sburl = new StringBuilder();
				for (int count = -1; count < 29; count++)
				{//循环30次
					sburl.Append("http://cn.bing.com/HPImageArchive.aspx?format=js&idx=");
					sburl.Append(count + "&n=1");
					string postContent = GetPostInfo(sburl.ToString());

					JObject jo = (JObject)JsonConvert.DeserializeObject(postContent);
					//获取第一个json数组
					JArray jArray = JArray.Parse(jo["images"].ToString());
					string[] urls = new string[jArray.Count];
					foreach (var item in jArray)
					{
						ImageInfo imageInfo = new ImageInfo();
						imageInfo.Url = url0 + item["url"].ToString();
						imageInfo.Startdate = item["startdate"].ToString();
						imageInfo.Copyright = item["copyright"].ToString();
						list.Add(imageInfo);
						//urls[count++] = url0 + item["url"].ToString();
					}
					sburl.Remove(0, sburl.Length);
				}
			}
			catch (Exception e)
			{
				LogWrite("GetUrls ：" + e.ToString());
			}
			return list;
		}
		public static string GetPostInfo(string url)
		{
			string strBuff = "";
			try
			{
				//访问http方法  

				Uri httpURL = new Uri(url);
				///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法建立，并进行强制的类型转换     
				HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
				///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换     
				HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
				///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容     
				///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理     
				Stream respStream = httpResp.GetResponseStream();
				///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以     
				//StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）     
				StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
				strBuff = respStreamReader.ReadToEnd();
			}
			catch (Exception e)
			{
				LogWrite("GetPostInfo ：" + e.ToString());
			}
			return strBuff;
		}

		public static void SaveWallpaperToDisk()
		{

			//要保存的路径，路径不存在可以使用下面的Directory.CreateDirectory(path)生成文件夹 
			//string ImageSavePath = @"C:\Users\Long\Pictures\BingWallpaper"; //保存墙纸路径 
			Bitmap bmpWallpaper;
			List<ImageInfo> list = GetUrls();
			if (list.Count != 0)
			{
				try
				{
					foreach (var item in list)
					{
						string fileName = ImageSavePath + "\\Bing" + item.Startdate.ToString() + ".jpg";
						if (FileExists(fileName))//文件存在跳过
						{
							continue;
						}
						WebRequest webreq = WebRequest.Create(item.Url.ToString());
						WebResponse webres = webreq.GetResponse();
						using (Stream stream = webres.GetResponseStream())
						{
							bmpWallpaper = (Bitmap)Image.FromStream(stream);
							if (!Directory.Exists(ImageSavePath))
							{
								Directory.CreateDirectory(ImageSavePath);
							}
							//设置文件名为例：bing2017816.jpg
							//图片保存路径为相对路径，保存在程序的目录下 

							bmpWallpaper.Save(fileName, ImageFormat.Jpeg);

						} //保存图片代码到此为止，下面就是
					}//foreach
				}//try
				catch (Exception e)
				{
					e.ToString();
				}
			}//if		    		    
		}
		public static void DownloadWallpaperToDisk()
		{
			//要保存的路径，路径不存在可以使用下面的Directory.CreateDirectory(path)生成文件夹 
			//string  path = @"C:\Users\Long\Pictures\BingWallpaper";
			while (true)
			{
				try
				{
					if (!File.Exists(ImageSavePath + "\\Bing" + DateTime.Today.ToString("yyyyMMdd").ToString() + ".jpg"))
						SaveWallpaperToDisk();
					else
					{

					}//else
					int time = int.Parse(ConfigurationManager.AppSettings["changeTime"].ToString());
					Thread.Sleep(time);//
				}
				catch (Exception e1)
				{
					LogWrite("保存壁纸 ：" + e1.ToString());
				}
			}//while
		}

		public static void SetDeskBackGround()
		{

			//利用系统的用户接口设置壁纸
			while (true)
			{

				string[] URL = GetFilePath();
				if (URL.Length == 0 || URL == null)
					return;
				//Random random = new Random(DateTime.Now.Day);
				int i = new Random().Next(0, URL.Length - 1);
				try
				{
					SystemParametersInfo(20, 1, URL[i], 1);
					int time = int.Parse(ConfigurationManager.AppSettings["changeTime"].ToString());
					Thread.Sleep(time);//6s
				}
				catch (Exception e1)
				{
					LogWrite("设置壁纸 ：" + e1.ToString());
				}
			}
		}

		public static string[] GetFilePath()
		{
			//string ImageSavePath = @"C:\Users\Long\Pictures\BingWallpaper";//保存图片位置
			string[] url;
			if (!Directory.Exists(ImageSavePath))
				return null;
			DirectoryInfo folder = new DirectoryInfo(ImageSavePath);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FileInfo file in folder.GetFiles("*.jpg"))
			{
				stringBuilder.Append(file.FullName + ",");
			}
			url = stringBuilder.ToString().Split(',');
			return url;

		}

		public static Boolean FileExists(string file)
		{
			if (File.Exists(file))
			{
				return true;
			}
			else
				return false;
		}


		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			//窗体关闭原因为单击"关闭"按钮或Alt+F4
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;           //取消关闭操作 表现为不关闭窗体
				this.Hide();               //隐藏窗体
			}
		}
		/***隐藏窗体**/
		private void hide_toolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Hide();                      //隐藏窗体	
		}
		/***显示窗体**/
		private void show_toolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Show();                                //窗体显示
			this.WindowState = FormWindowState.Normal;  //窗体状态默认大小
			this.Activate();                            //激活窗体给予焦点
		}
		/***退出程序**/
		private void exit_toolStripMenuItem_Click(object sender, EventArgs e)
		{
			//点击"是(YES)"退出程序
			if (MessageBox.Show("确定要退出程序?", "安全提示",
						System.Windows.Forms.MessageBoxButtons.YesNo,
						System.Windows.Forms.MessageBoxIcon.Warning)
				== System.Windows.Forms.DialogResult.Yes)
			{
				try
				{
					threadOfDownloadWallpaperToDisk.Abort();
					threadOfSetDeskBackGround.Abort();
				}
				catch (Exception e1)
				{
					e1.ToString();
				}
				Bing_notifyIcon.Visible = false;   //设置图标不可见
				this.Close();                  //关闭窗体
				this.Dispose();                //释放资源
				Application.Exit();            //关闭应用程序窗体
			}
		}
		/***点击图标显示**/
		private void Bing_notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			//点击鼠标"左键"发生
			if (e.Button == MouseButtons.Left)
			{
				this.Visible = true;                        //窗体可见
				this.WindowState = FormWindowState.Normal;  //窗体默认大小
				this.Bing_notifyIcon.Visible = true;            //设置图标可见
			}
		}
		private void button_Start_Click(object sender, EventArgs e)
		{
			Start();
		}
		private void button_Stop_Click(object sender, EventArgs e)
		{
			Stop();
		}

		private void changeTime_toolStripMenuItem_Click(object sender, EventArgs e)
		{
			string str = Interaction.InputBox("输入时间单位分", "设置切换时间", "", -1, -1);
			try
			{

				if (!str.Equals(""))
				{
					str = System.Text.RegularExpressions.Regex.Replace(str, @"[^\d]*", "");
					int time = int.Parse(str.ToString()) * 1000;
					///	ConfigurationManager.AppSettings.Settings["changeTime"]= time.ToString();
					//保存配置
					Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					cfa.AppSettings.Settings["changeTime"].Value = time.ToString();
					cfa.Save(ConfigurationSaveMode.Modified);
					ConfigurationManager.RefreshSection("appSettings");
				}
			}
			catch (Exception e1)
			{
				LogWrite("changeTime设置：" + e1.ToString());
			}
		}

		public static void LogWrite(string str)
		{
			string LogPath = @"c:\log\";
			if (!Directory.Exists(LogPath))
				Directory.CreateDirectory(LogPath);
			using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogPath + @"BingLog.txt", true))
			{
				sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + str + "\n");

			}
		}



	}
}
