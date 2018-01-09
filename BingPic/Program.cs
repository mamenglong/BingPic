using System;
using System.Windows.Forms;

namespace BingPic
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Form1 form1 = new Form1();
				Application.Run();
			}
			catch (Exception e) {
				Form1.LogWrite("ProgramMain:" + e.ToString());
			}
		}
	}
}
