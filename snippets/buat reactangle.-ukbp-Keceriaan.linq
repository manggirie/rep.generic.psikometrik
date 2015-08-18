<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Drawing.dll</Reference>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
</Query>

var petaks = new Dictionary<int, Tuple<Brush,Brush>>();

petaks.Add(5, new Tuple<Brush,Brush>(Brushes.Green, Brushes.White));
petaks.Add(10, new Tuple<Brush,Brush>(Brushes.Green, Brushes.White));
petaks.Add(15, new Tuple<Brush,Brush>(Brushes.Green, Brushes.White));
petaks.Add(20, new Tuple<Brush,Brush>(Brushes.Green, Brushes.White));
petaks.Add(25, new Tuple<Brush,Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(30, new Tuple<Brush,Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(35, new Tuple<Brush,Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(40, new Tuple<Brush,Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(45, new Tuple<Brush,Brush>(Brushes.LightGreen, Brushes.Black));
petaks.Add(50, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(55, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(60, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(65, new Tuple<Brush,Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(70, new Tuple<Brush, Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(75, new Tuple<Brush, Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(80, new Tuple<Brush, Brush>(Brushes.YellowGreen, Brushes.Black));
petaks.Add(85, new Tuple<Brush, Brush>(Brushes.White, Brushes.Black));
petaks.Add(90, new Tuple<Brush, Brush>(Brushes.White, Brushes.Black));
petaks.Add(95, new Tuple<Brush, Brush>(Brushes.White, Brushes.Black));
petaks.Add(100, new Tuple<Brush, Brush>(Brushes.White, Brushes.Black));

var width = 30;
var height = 20;


foreach (var k in petaks.Keys)
{
	var bmp = new Bitmap(width, height);
	var g = Graphics.FromImage(bmp);

	g.FillRectangle(petaks[k].Item1, 0, 0, width, height);

	var font = new Font("Arial", 5);
	g.DrawString(k.ToString(), font, petaks[k].Item2, 8, 2);
	bmp.Save($@"C:\project\rep.generic.psikometrik\web\images\ukbp\Keceriaan-{k}.png", ImageFormat.Png);
	
	g.Dispose();
	bmp.Dispose();
}