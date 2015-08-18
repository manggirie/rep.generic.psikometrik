<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Drawing.dll</Reference>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
</Query>

var petaks = new Dictionary<int, Tuple<Brush,Brush>>();

petaks.Add(1, new Tuple<Brush,Brush>(Brushes.White, Brushes.Black));
petaks.Add(2, new Tuple<Brush,Brush>(Brushes.White, Brushes.Black));
petaks.Add(3, new Tuple<Brush,Brush>(Brushes.White, Brushes.Black));
petaks.Add(4, new Tuple<Brush,Brush>(Brushes.White, Brushes.Black));
petaks.Add(5, new Tuple<Brush,Brush>(Brushes.White, Brushes.Black));
petaks.Add(6, new Tuple<Brush,Brush>(Brushes.White, Brushes.Black));
petaks.Add(7, new Tuple<Brush,Brush>(Brushes.White, Brushes.Black));
petaks.Add(8, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(9, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(10, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(11, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(12, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(13, new Tuple<Brush,Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(14, new Tuple<Brush, Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(15, new Tuple<Brush, Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(16, new Tuple<Brush, Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(17, new Tuple<Brush, Brush>(Brushes.Green, Brushes.White));
petaks.Add(18, new Tuple<Brush, Brush>(Brushes.Green, Brushes.White));
petaks.Add(19, new Tuple<Brush, Brush>(Brushes.Green, Brushes.White));
petaks.Add(20, new Tuple<Brush, Brush>(Brushes.Green, Brushes.White));

var width = 30;
var height = 20;


foreach (var k in petaks.Keys)
{
	var bmp = new Bitmap(width, height);
	var g = Graphics.FromImage(bmp);

	g.FillRectangle(petaks[k].Item1, 0, 0, width, height);

	var font = new Font("Arial", 5);
	g.DrawString(k.ToString(), font, petaks[k].Item2, 8, 2);
	bmp.Save($@"C:\project\rep.generic.psikometrik\web\images\ukbp\nas-{k}.png", ImageFormat.Png);
	
	g.Dispose();
	bmp.Dispose();
}