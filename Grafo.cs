using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UsoDeImagenes
{
    public class Grafo
    {
        public List<Vertex> vL;

        public Grafo()
        {
            vL = new List<Vertex>();
        }
        public int Count
        {
            get { return vL.Count; }
        }
        public void addVertex(Circle c, int id)
        {
            Vertex v_new = new Vertex(c, id);
            vL.Add(v_new);
        }
        public void addVertex(Vertex c)
        {
            vL.Add(c);
        }
        public Vertex getVertexAt(int pos)
        {
            return vL[pos];
        }
        public int getCount()
        {
            return vL.Count;
        }

        public int find(Vertex v_d)
        {
            return vL.FindIndex(x => x.Id == v_d.Id);
        }

        public class Vertex
        {
            Circle circle;
            int id;
            bool seleccionado;
            bool visitado;
            public List<Edge> eL;
            public Vertex(Circle c, int id)
            {
                this.id = id;
                this.circle = c;
                eL = new List<Edge>();
                seleccionado = false;
                visitado = false;
            }
            public void addEdge(Vertex v_o, Vertex v_d, int weight, Point[] path, double distance)
            {
                Edge e_new = new Edge(v_o, v_d, weight, path, distance);
                eL.Add(e_new);
            }
            public int Id
            {
                get { return id; }
            }
            public Circle Circle
            {
                get { return circle; }
            }
            public bool Seleccionado
            {
                get { return seleccionado; }
                set { seleccionado = value; }
            }
            public bool Vistado
            {
                get { return visitado; }
                set { visitado = value; }
            }
            public int EdgesCount
            {
                get { return eL.Count; }
            }
            public class Edge
            {
                public Vertex destination;
                public int id;
                Point[] path;
                public double distance;
                public Vertex origin;
                public Edge(Vertex v_o, Vertex v_d, int id, Point[] path, double distance)
                {
                    origin = v_o;
                    destination = v_d;
                    this.id = id;
                    this.path = path;
                    this.distance = distance;
                }
                public Edge()
                {
                    origin = null;
                    destination = null;
                    id = -1;
                    path = null;
                    distance = 0;
                }
                public Point[] Path
                {
                    get { return path; }
                }
                public override string ToString()
                {
                    string s = "Edge: " + id + ") Vertice destino: " + destination + ")Distancia" + distance;
                    return s;
                }
            }
            public Vertex getDestinationAt(int pos)
            {
                return eL[pos].destination;
            }
            public Point[] getPathAt(int pos)
            {
                if (pos >= eL.Count)
                    return null;
                return eL[pos].Path;
            }

            public override string ToString()
            {
                string s = "Vertice:" + id + ") \r\n X = \r\n " + circle.P_c.X + ") Y = " + circle.P_c.Y;
                return s;
            }
        }
    }
}
