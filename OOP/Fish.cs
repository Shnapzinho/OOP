﻿using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public abstract class Fish : Animal
	{
		public Fish(string imagePath) : base(imagePath)
		{
			Type = "Fish";
		}

		public override string Move()
		{
			return "Swimming";
		}
	}
}