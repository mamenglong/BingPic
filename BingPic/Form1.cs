using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Threading;

namespace BingPic
{
	public partial class Form1 : Form
	{
		string url0 = "http://cn.bing.com/";
		string url = "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=-1&n=30";// ConfigurationSettings.AppSettings.GetValues("url").ToString();
		[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
		public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
		Thread threadOfSetDeskBackGround = new Thread(SetDeskBackGround);

		public Form1()
		{
			InitializeComponent();
			Start();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Start();
		}
		public void Start() {

			
				if (!threadOfSetDeskBackGround.IsAlive)
				{
					threadOfSetDeskBackGround.Start();
					SaveWallpaperToDisk();
				}
			  
				 
		}

		public List<ImageInfo> GetUrls()
		{
			List<ImageInfo> list = new List<ImageInfo> { };
			string postContent = GetPostInfo(url);
			JObject jo = (JObject)JsonConvert.DeserializeObject(postContent);

			//获取第一个json数组
			JArray jArray = JArray.Parse(jo["images"].ToString());
			string[] urls = new string[jArray.Count];
			foreach (var item in jArray)
			{
				ImageInfo imageInfo = new ImageInfo();
				imageInfo.Url= url0 + item["url"].ToString(); 
				imageInfo.Startdate= item["startdate"].ToString();
				imageInfo.Copyright= item["copyright"].ToString();
				list.Add(imageInfo);
				//urls[count++] = url0 + item["url"].ToString();
			}
			return list;
		}
		public string GetPostInfo(string url)
		{
			//访问http方法  
			string strBuff = "";
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
			return strBuff;
		}


		public  void SaveWallpaperToDisk()
		{ //要保存的路径，路径不存在可以使用下面的Directory.CreateDirectory(path)生成文件夹 
			string ImageSavePath = @"C:\Users\Long\Pictures\BingWallpaper"; //保存墙纸路径 
			Bitmap bmpWallpaper;
			string  path = @"C:\Users\Long\Pictures\BingWallpaper";
			if (File.Exists(path + "\\Bing" + DateTime.Today.ToString("yyyyMMdd").ToString() + ".jpg"))
				return;
			foreach (var item in GetUrls())
			{
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
					
					bmpWallpaper.Save(ImageSavePath + "\\Bing" +item.Startdate.ToString()+ ".jpg", ImageFormat.Jpeg);

				} //保存图片代码到此为止，下面就是
			}
			 
			}

		public  static void SetDeskBackGround()
		{
			
			//利用系统的用户接口设置壁纸
			while (true)
			{
				string[] URL = GetFilePath();
				//Random random = new Random(DateTime.Now.Day);
				int i = new Random().Next(0, URL.Length-1);
				SystemParametersInfo(20, 1,URL[i], 1);
				Thread.Sleep(6000);
			}
		}
	
		public static string [] GetFilePath()
		{
			string ImageSavePath = @"C:\Users\Long\Pictures\BingWallpaper";//保存图片位置
			DirectoryInfo folder = new DirectoryInfo(ImageSavePath);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FileInfo file in folder.GetFiles("*.jpg"))
			{
				stringBuilder.Append(file.FullName + ",");
			}
			string[] url = stringBuilder.ToString().Split(',');
			return url;

		}
	}
}
