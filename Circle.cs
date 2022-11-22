using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsoDeImagenes
{

	public class Circle
	{
		Point p_c;
		int id;
		float r;

		public Point P_c
		{
			get
			{
				return p_c;
			}

			set
			{
				p_c = value;
			}
		}

		public int Id
		{
			get
			{
				return id;
			}
		}

		public float R
		{
			get
			{
				return r;
			}

			set
			{
				r = value;
			}
		}

		public Circle(Point p_c, int id, float r)
		{
			this.p_c = p_c;
			this.id = id;
			this.r = r;
		}


	}
}
