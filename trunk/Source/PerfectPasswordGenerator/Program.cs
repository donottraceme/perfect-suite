using System;
using System.Windows.Forms;
using Nabu.Forms.Html;

namespace PerfectPasswordGenerator
{
	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			using (HtmlGuiThread htmlGuiThread = new HtmlGuiThread())
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
		}
	}
}
