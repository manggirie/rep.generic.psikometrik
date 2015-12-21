void Main()
{

	var width = 30;
	var height = 20;

	for (int i = 0; i <= 100; i++)
	{
		var shades = new Shades[] { Shades.VeryDark, Shades.Dark, Shades.Light, Shades.VeryLight };
		foreach (var sh in shades)
		{
			var td = new Td(i, sh);
			var bmp = new Bitmap(width, height);
			var g = Graphics.FromImage(bmp);

			g.FillRectangle(td.Background, 0, 0, width, height);

			var borderPen = new Pen(Color.Red, 3);

			g.DrawRectangle(borderPen, 0, 0, width - 1, height - 2);

			var font = new Font("Arial", 8);
			g.DrawString(i.ToString(), font, td.Pen, 8, 2);
			bmp.Save($@"C:\project\rep.generic.psikometrik\web\images\hlp\{sh}-{i}-on.png", ImageFormat.Png);

			g.Dispose();
			bmp.Dispose();

			// without red border
			var bmp2 = new Bitmap(width, height);
			var g2 = Graphics.FromImage(bmp2);
			var borderPen2 = new Pen(Color.Black, 1);

			g2.FillRectangle(td.Background, 0, 0, width, height);
			g2.DrawRectangle(borderPen2, 0, 0, width-1, height-2);
			var text = i == 0? "" : i.ToString();
			g2.DrawString(text, font, td.Pen, 8, 2);
			bmp2.Save($@"C:\project\rep.generic.psikometrik\web\images\hlp\{sh}-{i}.png", ImageFormat.Png);

			g2.Dispose();
			bmp2.Dispose();

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
	public int Value { get; }
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
				Background = Brushes.DarkGreen;
				Pen = Brushes.White;
				break;
			case Shades.Dark:
				Background = Brushes.Green;
				Pen = Brushes.White;
				break;
			case Shades.Light:
				Background = Brushes.YellowGreen;
				Pen = Brushes.Black;
				break;
			case Shades.VeryLight:
				Background = Brushes.White;
				Pen = Brushes.Black;
				break;
		}
	}

}
