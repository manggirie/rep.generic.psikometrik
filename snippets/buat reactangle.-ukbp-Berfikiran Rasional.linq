<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Drawing.dll</Reference>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
</Query>

void Main()
{
	
	
	var width = 30;
	var height = 20;
	
	for (int i = 1; i <= 100; i++)
	{
		var shades = new Shades[] {Shades.VeryDark, Shades.Dark, Shades.Light, Shades.VeryLight};
		foreach (var sh in shades)
		{
			var td = new Td(i, sh);
			var bmp = new Bitmap(width, height);
			var g = Graphics.FromImage(bmp);

			g.FillRectangle(td.Background, 0, 0, width, height);

			var font = new Font("Arial", 8);
			g.DrawString(i.ToString(), font, td.Pen, 8, 2);
			bmp.Save($@"C:\project\rep.generic.psikometrik\web\images\ukbp\{sh}-{i}.png", ImageFormat.Png);

			g.Dispose();
			bmp.Dispose();

		}
	}
}

// Define other methods and classes here

public enum Shades
{
	VeryDark,
	Dark,
	Light,
	VeryLight
}


public class Td
{
	public int Value { get;  }
	public Shades Shade { get; }
	public Brush Background { get; }
	public Brush Pen { get; }

	public Td(int value, Shades shade)
	{
		this.Value = value;
		this.Shade = shade;
		switch (shade)
		{
			case Shades.VeryDark:
				Background = Brushes.DarkBlue;
				Pen = Brushes.White;
				break;
			case Shades.Dark:
				Background = Brushes.Blue;
				Pen = Brushes.White;
				break;
			case Shades.Light:
				Background = Brushes.LightBlue;
				Pen = Brushes.Black;
				break;
			case Shades.VeryLight:
				Background = Brushes.White;
				Pen = Brushes.Black;
				break;
		}
	}

}
