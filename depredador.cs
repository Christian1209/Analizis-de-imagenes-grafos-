using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsoDeImagenes
{
    public class depredador
    {
        int pathIndex;
        Point[] path;
        List<int> visitados = new List<int>();
        int originVertexIndex;
        int destinationVertexIndex;
        int vel = 2;
        Circle circle = null;
        bool exitado = false;
        public Agent presa = null;
        float radioRadar = 0;

        public int PathIndex
        {
            get { return pathIndex; }   
            set { pathIndex = value; }
        }

        public Agent Presa
        {
            get { return presa; }
            set { presa = value; }
        }

        public void addVisitado(int id)
        {
            visitados.Add(id);
        }
        public float RadioRadar
        {
            get { return radioRadar; }
            set { radioRadar = value; }
        }
        public depredador()
        {
            originVertexIndex = 0;
            destinationVertexIndex = 0;
            vel = 2;
            path = null;
        }
        public int Vel
        {
            get { return vel; }
            set { vel = value; }
        }
        public Point[] Path
        {
            get { return path; }
            set
            {
                path = value;
            }
        }
        public bool Exitado
        {
            get { return exitado; }
            set { exitado = value; }
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
        public depredador(int index, Circle ce, int radio)
        {
            pathIndex = 0;
            destinationVertexIndex = index;
            vel = 2;
            circle.P_c = ce.P_c;
            circle.R = radio;
        }
        public bool canWalk()
        {
            if (pathIndex + vel < path.Length - 3)
            {
                return true;
            }
            return false;

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
        public Point getActualPosition()
        {
            if (pathIndex == 0)
            {
                return path[0];
            }
            return path[pathIndex - 2];
        }

        public bool comiPresa()
        {
            Point centroPresa = presa.getActualPosition();
            Point centroDepredador = getActualPosition();
            int a = 0;
            int b = 0;
            while (centroPresa.Y != centroDepredador.Y)
            {
                a++;
                if (centroPresa.Y < centroDepredador.Y)
                {
                    centroPresa.Y++;
                }
                else
                {
                    centroPresa.Y--;
                }
            }
            while (centroPresa.X != centroDepredador.X)
            {
                b++;
                if (centroPresa.X < centroDepredador.X)
                {
                    centroPresa.X++;
                }
                else
                {
                    centroPresa.X--;
                }
            }
            int c = a + b;
            if (c > 90)
            {
                presa = null;
                exitado = false;
                vel = 2;
            }
            if (c < 20)
            {
                //MessageBox.Show("me lo comi");
                vel = 2;
                exitado = false;
                return true;
            }
            return false;
        }



        public bool detectePresa(List<Agent> listaAgentes)
        {
            Point centroDepredador = getActualPosition();
            for (int i = 0; i < listaAgentes.Count; i++)
            {
                    presa = esPresa(centroDepredador, listaAgentes[i]);
                if (presa != null)
                {
                    listaAgentes[i].addDepredador(this);
                    presa = listaAgentes[i];
                    exitado = true;
                    vel = (int)(listaAgentes[0].Vel * 1.2);
                    return true;
                }
            }
            return false;
        }

        Agent esPresa(Point centroDepredador, Agent posible)
        {
            Point centroPresa = posible.getActualPosition();
            int a = 0, b = 0, c = 0;
            Color color;
            float radioAux;
            radioAux = (float)(radioRadar);
            while (centroPresa.Y != centroDepredador.Y)
            {
                a++;
                if (centroPresa.Y < centroDepredador.Y)
                {
                    centroPresa.Y++;
                }
                else
                {
                    centroPresa.Y--;
                }
            }
            while (centroPresa.X != centroDepredador.X)
            {
                b++;
                if (centroPresa.X < centroDepredador.X)
                {
                    centroPresa.X++;
                }
                else
                {
                    centroPresa.X--;
                }
            }
            c = (a * a) + (b * b);
            c = (int)(c - (radioAux * radioAux));
            if (c > 0)
            {
                return null;

            }
            else
            {
                posible.SoyPresa = true;
                Presa = posible;
                vel = (int)(presa.Vel + 1.2);
                exitado = true;
                return posible;
            }

        }

        public bool visitado(int id)
        {
            for (int i = 0; i < visitados.Count; i++)
            {
                if (id == visitados[i])
                {
                    return true;
                }
            }
            return false;

        }
    }
}
