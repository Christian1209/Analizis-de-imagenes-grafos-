using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace UsoDeImagenes
{
	public class Agent
	{
		int id;
		int pathIndex;
		Point[] path;
		List<int> visitados = new List<int>();
		int originVertexIndex;
		int destinationVertexIndex;
		int vel = 5;
		int escondete = 0;
		bool soyPresa = false;
		public List<depredador> depredadores = new List<depredador>();
		bool huyendo = false;

		public int PathIndex
		{
			get { return pathIndex; }
			set { pathIndex = value; }
		}

		public int Id
        {
			get { return id; }	
			set { id = value; }	
        }

		public bool Huyendo
        {
			get { return huyendo; }
			set { huyendo = value; }	
        }
		public void addVisitado(int id)
		{
			visitados.Add(id);
		}
		public bool SoyPresa
        {
			get { return soyPresa; }
			set { soyPresa = value; }
        }
		public void addDepredador(depredador d)
        {
			depredadores.Add(d);
        }
		public void vaciaDepredadores()
        {
			for(int i = 0; i < depredadores.Count; i++)
            {
				depredadores[i].Presa = null;
				depredadores[i].Exitado = false;
				depredadores[i].Vel = 2;
            }
        }
		public int Escondete
        {
			get { return escondete; }
			set { escondete = value; }
        }
		public Agent()
        {
			originVertexIndex = 0;
			destinationVertexIndex = 0;
			vel = 5;
			path = null;
        }
		public Point[] Path
		{
			get { return path; }
			set
			{
				path = value;
			}
		}
		public int Vel
        {
			get { return vel; }
			set { vel = value; }
        }
		public int OriginVertexIndex
		{
			get { return originVertexIndex; }
			set { originVertexIndex = value; }
		}
		public int DestinationVertexIndex
		{
			get { return destinationVertexIndex; }
			set { destinationVertexIndex = value; }
		}

		public void newPath(int originVertexIndex, Point[] randomPath, int destinationVertexIndex)
		{
			this.destinationVertexIndex = destinationVertexIndex;
			this.originVertexIndex = originVertexIndex;
			this.path = randomPath;
			pathIndex = 0;
		}
		public Agent(int index)
		{
			pathIndex = 0;
			destinationVertexIndex = index;
			vel = 5;
		}
		public bool canWalk()
        {
            if (huyendo == false)
            {
				if (pathIndex + vel < path.Length - 3)
				{
					return true;
                }
                else
                {
					return false;
                }

            }
            else {
				if (pathIndex - vel > 0)
				{
					return true;
				}
                else
                {
					return false;
				}
			}

		}
		public bool walk()
		{
			if (pathIndex + vel < path.Length - 3)
			{
				pathIndex += vel;
				return true;
			}
			pathIndex = path.Length - 1;
			return false;
		}
		public bool reverseWalk()
		{
			if (pathIndex - vel > 0)
			{
				pathIndex -= vel;
				return true;
			}
			pathIndex = 1;
			return false;
		}
		public Point getActualPosition()
		{
			if(pathIndex>3)
				return path[pathIndex-3];
			return path[0];
		}
		
		public bool visitado(int id)
        {
			for (int i=0; i<visitados.Count; i++)
            {
				if(id == visitados[i])
                {
					return true;
                }
            }
			return false;

        }
	}
}