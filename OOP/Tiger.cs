using OOP;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP
{
	public class Tiger : Mammal
	{
		public Tiger(string imagePath) : base(imagePath)
		{
			Species = "Tiger";
		}

		public override string Sound()
		{
			return "rrrr";
		}
		public override string ToString()
		{
			return $"{base.ToString()} - {Sound()}, {Move()}";
		}
	}
}