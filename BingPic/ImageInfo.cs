namespace BingPic
{
	public class ImageInfo
	{
		private string url;
		private string startdate;
		private string copyright;

		public string Copyright { get => copyright; set => copyright = value; }
		public string Startdate { get => startdate; set => startdate = value; }
		public string Url { get => url; set => url = value; }
	}
}
