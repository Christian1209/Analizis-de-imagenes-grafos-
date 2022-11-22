using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static UsoDeImagenes.Grafo;
using static UsoDeImagenes.Agent;
using static UsoDeImagenes.depredador;
using static UsoDeImagenes.Grafo.Vertex;

namespace UsoDeImagenes
{
    public partial class Form1 : Form
    {
        bool primeraAnimacion = true;
        bool primerRadar = true;
        int op = 0;
        List<int> visitados = new List<int>();
        List<List<Vertex>> arbolprim = new List<List<Vertex>>();
        Agent agent;
        Bitmap bmpImage;
        Graphics g;
        Bitmap bmpAnimation;
        Grafo graph = new Grafo();
        Grafo grafokruskal = new Grafo();
        Grafo auxiliar = new Grafo();
        Grafo grafoDijkstra = new Grafo();
        List<Grafo> listaDijkstra = new List<Grafo>();
        List<int> agentesDijkstra = new List<int>();
        List<int> agentesDepredadores = new List<int>();
        List<depredador> radares = new List<depredador>();
        public List<depredador> listaDepredadores = new List<depredador>();
        float radioGrl = 0;
        float radioRadar = 0;
        List<Dijkstra> vector = new List<Dijkstra>();
        bool depredador = false;
        float x_p = 0, y_p = 0, x_f = 0, y_f = 0;
        //v_dindex me valida que haya dos clicks,
        int v_dindex = -1;
        Vertex paro;
        Vertex paroprim;
        bool explorar = true;
        List<int> encolados = new List<int>();
        Random rand;
        public Form1()
        {
            InitializeComponent();
            rand = new Random(DateTime.Now.Millisecond);
        }


        private void btnSelection_Click(object sender, EventArgs e)
        {
            listaDepredadores.Clear();
            radares.Clear();
            radioGrl = 0;
            radioRadar = 0;
            agentesDepredadores.Clear();
            paro = null;
            grafoDijkstra.vL.Clear();
            openFileDialog1.ShowDialog();
            bmpImage = new Bitmap(openFileDialog1.FileName);
            bmpAnimation = new Bitmap(openFileDialog1.FileName);
            pbxImage.Image = bmpAnimation;
            pbxImage.BackgroundImage = bmpImage;
            pbxImage.BackgroundImageLayout = ImageLayout.Zoom;
            listBoxCirculos.Items.Clear();
            graph.vL.Clear();
            g = Graphics.FromImage(bmpImage);
        }

        void drawCircle(int x, int y, float d, Bitmap bmpLocal, Color color)
        {
            d = (d * 2) - 5;
            g = Graphics.FromImage(bmpLocal);
            Brush brocha = new SolidBrush(color);
            g.FillEllipse(brocha, x - d / 2, y - d / 2, d, d);
        }

        void drawCircle(int x, int y, float d, Bitmap bmpLocal)
        {
            d = (d * 2) - 5;
            g = Graphics.FromImage(bmpLocal);
            Brush brocha = new SolidBrush(Color.LightYellow);
            g.FillEllipse(brocha, x - d / 2, y - d / 2, d, d);
        }

        void drawCircle(int x, int y, float d, Bitmap bmpLocal, int id)
        {
            d = (d * 2) - 5;
            g = Graphics.FromImage(bmpLocal);
            Brush brocha = new SolidBrush(Color.LightYellow);
            g.FillEllipse(brocha, x - d / 2, y - d / 2, d, d);
        }

        void drawCircleblack(int x, int y, float d, Bitmap bmpLocal)
        {
            d = (d * 2) - 5;
            Graphics g = Graphics.FromImage(bmpLocal);
            Color color = Color.Black;
            Brush brocha = new SolidBrush(color);
            g.FillEllipse(brocha, x - d / 2, y - d / 2, d, d);
        }

        void drawCirclewhite(int x, int y, float d, Bitmap bmpLocal)
        {
            d = (d * 2) + 4;
            g = Graphics.FromImage(bmpLocal);
            Color color = Color.White;
            Brush brocha = new SolidBrush(color);
            g.FillEllipse(brocha, x - d / 2, y - d / 2, d, d);
        }
        void drawCircleclose(int x, int y, int d, Bitmap bmpLocal)
        {
            d = (d * 2);
            g = Graphics.FromImage(bmpLocal);
            Brush brocha = new SolidBrush(Color.Yellow);
            g.FillEllipse(brocha, x - d / 2, y - d / 2, d, d);
        }

        void drawIdentificador(int x, int y, string num, float radio, Bitmap bmpLocal)
        {
            g = Graphics.FromImage(bmpLocal);
            Font fuente = new Font("Times New Roman", (radio));
            g.DrawString(num, fuente, Brushes.Blue, x - radio / 2, y - radio / 2);

        }

        void drawIdentificadorclose(int x, int y, string num, int radio, Bitmap bmpLocal)
        {
            g = Graphics.FromImage(bmpLocal);
            Font fuente = new Font("Times New Roman", (radio / 2));
            g.DrawString(num, fuente, Brushes.Blue, x, y);

        }

        void drawCentroide(int x, int y, int d, Bitmap bmpLocal)
        {
            d = 10;
            g = Graphics.FromImage(bmpLocal);
            Brush brocha = new SolidBrush(Color.Green);
            g.FillEllipse(brocha, x - d / 2, y - d / 2, d, d);
        }

        public void drawGraph()
        {
            g = Graphics.FromImage(bmpImage);
            Pen edgesPen = new Pen(Color.DimGray);
            Brush vertexBrush = new SolidBrush(Color.Firebrick);
            Circle c_o;
            Circle c_d;
            Vertex v_o;
            Vertex v_d;
            int r = 25;
            int R = (bmpImage.Width - 4 * r) / 2;
            for (int i = 0; i < graph.Count; i++)
            {
                v_o = graph.getVertexAt(i);
                c_o = v_o.Circle;
                for (int j = 0; j < v_o.EdgesCount; j++)
                {
                    //AQUI QUEDAMOS.
                    v_d = v_o.getDestinationAt(j);
                    c_d = v_d.Circle;
                    g.DrawLine(edgesPen, c_o.P_c.X, c_o.P_c.Y, c_d.P_c.X, c_d.P_c.Y);
                }
            }
            for (int i = 0; i < graph.Count; i++)
            {
                v_o = graph.getVertexAt(i);
                c_o = v_o.Circle;
                Color color;
                drawCircle(c_o.P_c.X, c_o.P_c.Y, c_o.R, bmpImage, color = Color.MistyRose);
                drawIdentificador(c_o.P_c.X, c_o.P_c.Y, v_o.Id.ToString(), c_o.R, bmpImage);
            }
        }




        //get path
        Point[] getPath(Point p_0, Point p_f)
        {
            float x_0, y_0;
            float x_f, y_f;
            float x_k, y_k;
            float m, b;
            int incremento = 1;

            x_0 = p_0.X;
            y_0 = p_0.Y;

            x_f = p_f.X;
            y_f = p_f.Y;
            int cont = 0;
            //int max;
            Color c_i;
            Point[] path;
            if ((x_f - x_0) == 0)
            {
                path = new Point[Math.Abs((int)y_0 - (int)y_f) + 5];
                if (y_0 > y_f)
                    incremento = -1;
                for (y_k = y_0; y_k != y_f; y_k += incremento)
                {
                    path[cont].X = (int)Math.Round(x_0);
                    path[cont++].Y = (int)Math.Round(y_k);
                }
            }
            else
            {
                m = (y_f - y_0) / (x_f - x_0);
                b = y_0 - m * x_0;

                if (m < 1 && m > -1)
                {
                    path = new Point[Math.Abs((int)x_0 - (int)x_f) + 1];
                    if (x_0 > x_f)
                        incremento = -1;
                    for (x_k = x_0; x_k != x_f; x_k += incremento)
                    {
                        y_k = m * x_k + b;
                        path[cont].X = (int)Math.Round(x_k);
                        path[cont++].Y = (int)Math.Round(y_k);
                    }
                }
                else
                {
                    path = new Point[Math.Abs((int)y_0 - (int)y_f) + 1];
                    if (y_0 > y_f)
                        incremento = -1;
                    for (y_k = y_0; y_k != y_f; y_k += incremento)
                    {
                        x_k = 1 / m * (y_k - b);
                        path[cont].X = (int)Math.Round(x_k);
                        path[cont++].Y = (int)Math.Round(y_k);
                    }
                }
            }
            return path;
        }


        //METODO OBSTACULO
        bool obstaculo(Point p_0, Point p_f)
        {
            float x_0, y_0;
            float x_f, y_f;
            float x_k, y_k;
            float m, b;
            int incremento = 1;

            x_0 = p_0.X;
            y_0 = p_0.Y;

            x_f = p_f.X;
            y_f = p_f.Y;
            int cont = 0;
            //int max;
            Color c_i;
            Point[] path;
            if ((x_f - x_0) == 0)
            {
                path = new Point[Math.Abs((int)y_0 - (int)y_f) + 5];
                if (y_0 > y_f)
                    incremento = -1;
                for (y_k = y_0; y_k != y_f; y_k += incremento)
                {
                    //EVALUO PATH PARA BUSCAR OBSTACULO
                    c_i = bmpImage.GetPixel((int)x_0, (int)y_k);
                    if (!IsWhite(c_i))
                    {
                        return true;
                    }
                    path[cont].X = (int)Math.Round(x_0);
                    path[cont++].Y = (int)Math.Round(y_k);
                }
            }
            else
            {
                m = (y_f - y_0) / (x_f - x_0);
                b = y_0 - m * x_0;

                if (m < 1 && m > -1)
                {
                    path = new Point[Math.Abs((int)x_0 - (int)x_f) + 1];
                    if (x_0 > x_f)
                        incremento = -1;
                    for (x_k = x_0; x_k != x_f; x_k += incremento)
                    {
                        //EVALUO EL PATH PARA BUSCAR OBSTACULO
                        y_k = m * x_k + b;
                        c_i = bmpImage.GetPixel((int)x_k, (int)y_k);
                        if (!IsWhite(c_i))
                        {
                            return true;
                        }
                        path[cont].X = (int)Math.Round(x_k);
                        path[cont++].Y = (int)Math.Round(y_k);
                    }
                }
                else
                {
                    path = new Point[Math.Abs((int)y_0 - (int)y_f) + 1];
                    if (y_0 > y_f)
                        incremento = -1;
                    for (y_k = y_0; y_k != y_f; y_k += incremento)
                    {
                        //EVALUO PATH PARA BUSCAR OBSTACULO
                        x_k = 1 / m * (y_k - b);
                        c_i = bmpImage.GetPixel((int)x_k, (int)y_k);
                        if (!IsWhite(c_i))
                        {
                            return true;
                        }
                        path[cont].X = (int)Math.Round(x_k);
                        path[cont++].Y = (int)Math.Round(y_k);
                    }
                }
            }
            return false;
        }



        //metodo a trabajar.
        private void btnDibujaPixel_Click(object sender, EventArgs e)
        {
            grafoDijkstra.vL.Clear();
            listaDijkstra.Clear();
            agentesDijkstra.Clear();
            vector.Clear();
            arbolprim.Clear();
            grafokruskal = new Grafo();
            agentesDepredadores.Clear();
            listaDepredadores.Clear();
            //listagrafo.Clear();

            bool yaera = false;
            int k = 0;
            listBoxCirculos.Items.Clear();
            int id = 0;
            Color c_i;
            for (int y_i = 0; y_i < bmpImage.Height; y_i++)
                for (int x_i = 0; x_i < bmpImage.Width; x_i++)
                {
                    c_i = bmpImage.GetPixel(x_i, y_i);
                    if (IsBlack(c_i))
                    {
                        yaera = false;
                        for (k = 0; k < graph.Count; k++)
                        {
                            bool nuevo = false;
                            nuevo = Nuevocirculo(x_i, y_i, graph.vL[k].Circle.R, graph.vL[k].Circle.P_c.X, graph.vL[k].Circle.P_c.Y, bmpImage);
                            if (nuevo == false)
                            {
                                yaera = true;
                            }

                        }
                        if (yaera == false)
                        {
                            id++;
                            int[] centro;
                            centro = Buscacentro(x_i, y_i, bmpImage);
                            Circle circle = new Circle(new Point((int)(centro[0]), (int)(centro[1])), id, (float)centro[2]);
                            graph.addVertex(circle, id);
                        }

                    }
                }
            Vertex v_o;
            Vertex v_d;
            int idarista = 0;
            for (int i = 0; i < graph.Count; i++)
            {
                v_o = graph.getVertexAt(i);
                for (int j = 0; j < graph.Count; j++)
                {
                    //LOS PINTO DE BLANCO PARA PODER PINTAR LAS ARISTA E IDENTIFICAR LOS OBSTACULOS.
                    drawCirclewhite(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpImage);
                    v_d = graph.getVertexAt(j);
                    drawCirclewhite(v_d.Circle.P_c.X, v_d.Circle.P_c.Y, v_d.Circle.R, bmpImage);
                    //pbxImage.Refresh();
                    //b.Clear(Color.Transparent);
                    Point[] path = getPath(v_o.Circle.P_c, v_d.Circle.P_c);
                    //antes de añadir la arista, voy a recorrerla para ver si hay obstaculos.
                    if (!obstaculo(v_o.Circle.P_c, v_d.Circle.P_c) && v_o.Id != v_d.Id)
                    {
                        ////////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        double distance = GetDistance(v_o, v_d);
                        ///////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        v_o.addEdge(v_o, v_d, j, path, distance);
                        //Edge auxiliar = new Edge(v_o, v_d, idarista, path, distance);
                        //idarista++;
                        //possibles.Add(auxiliar);
                    }
                    drawCircleblack(v_d.Circle.P_c.X, v_d.Circle.P_c.Y, v_d.Circle.R, bmpImage);
                    //pbxImage.Refresh();
                    //b.Clear(Color.Transparent);
                }
                drawCircleblack(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpImage);
                //pbxImage.Refresh();
                //b.Clear(Color.Transparent);

            }
            drawGraph();
            Graphics b = Graphics.FromImage(bmpAnimation);
            b.Clear(Color.Transparent);
            pbxImage.Refresh();
            for (int i = 0; i < graph.Count; i++)
            {
                v_o = graph.getVertexAt(i);
                listBoxCirculos.Items.Add(graph.vL[i]);
                for (int j = 0; j < v_o.EdgesCount; j++)
                {
                    listBoxCirculos.Items.Add(v_o.eL[j]);
                }
            }
            /*
            List<Vertex> verticesprim = new List<Vertex>();
            for (int i = 0; i < graph.Count; i++)
            {
                verticesprim.Add(graph.getVertexAt(i));
            }
            while (verticesprim.Count > 0)
            {
                Vertex aux;
                aux = verticesprim[0];
                listagrafo.Add(Prim(aux, verticesprim));
            }
            grafokruskal = kruskal();
            */
            //Prim(v_o);
        }




        public static double GetDistance(Vertex ori, Vertex dest)
        {
            double x1, x2, y1, y2;
            x1 = ori.Circle.P_c.X;
            x2 = dest.Circle.P_c.X;
            y1 = ori.Circle.P_c.Y;
            y2 = dest.Circle.P_c.Y;
            var distance = Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
            return distance;
        }

        public static bool IsBlack(Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return true;
            else
                return false;
        }

        public static bool IsWhite(Color color)
        {
            if (color.R == 255 && color.G == 255 && color.B == 255)
                return true;
            else
                return false;
        }
        public static int[] Buscacentro(int x, int y, Bitmap bmpx)
        {
            Color color;
            int[] arr = new int[3];
            int ysup, yinf, xder, xizq, ycentro, xcentro, radio;
            ysup = y;
            yinf = y;
            color = bmpx.GetPixel(x, yinf);
            while (IsBlack(color))
            {
                color = bmpx.GetPixel(x, yinf + 1);
                yinf++;
            }
            ycentro = (yinf + ysup) / 2;
            xder = x;
            xizq = x;
            color = bmpx.GetPixel(x, ycentro);
            while (IsBlack(color))
            {
                color = bmpx.GetPixel(xder + 1, ycentro);
                xder++;
            }
            color = bmpx.GetPixel(x, ycentro);
            while (IsBlack(color))
            {
                color = bmpx.GetPixel(xizq - 1, ycentro);
                xder--;
                xizq--;
            }
            while (IsBlack(color))
            {
                color = bmpx.GetPixel(xizq - 1, ycentro);
                xder--;
                xizq--;
            }
            xcentro = (xder + x) / 2;
            xder = xcentro;
            color = bmpx.GetPixel(xder, ycentro);
            while (IsBlack(color))
            {
                color = bmpx.GetPixel(xder + 1, ycentro);
                xder++;
            }
            radio = xder - x;
            arr[0] = xcentro;
            arr[1] = ycentro;
            arr[2] = radio;
            return arr;
        }

        public static bool Nuevocirculo(int xcentro, int ycentro, float radio, int x, int y, Bitmap bmpx)
        {
            int a = 0, b = 0, c = 0;
            Color color;
            color = bmpx.GetPixel(x, y);
            radio = radio + 5;
            while (y != ycentro)
            {
                a++;
                if (y < ycentro)
                {
                    y++;
                }
                else
                {
                    y--;
                }
            }
            while (x != xcentro)
            {
                b++;
                if (x < xcentro)
                {
                    x++;
                }
                else
                {
                    x--;
                }
            }
            c = (a * a) + (b * b);
            c = (int)(c - (radio * radio));
            if (c > 0)
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }


        /*void simulationStart()
        {
            Graphics g = Graphics.FromImage(bmpAnimation);
            Brush agentBrush = new SolidBrush(Color.BlueViolet);
            Random rand = new Random(DateTime.Now.Millisecond);
            Vertex v_o;
            int destinationVertexIndex;
            int originVertexIndex;
            //crear lista de enteros
            do
            {
                List<int> nonvisited = new List<int>();

                while (agent.walk())
                {
                    Point p_k = agent.getActualPosition();
                    g.Clear(Color.Transparent);
                    // Dibuja el nuevo estado del circulo 
                    g.FillEllipse(agentBrush, p_k.X - 20, p_k.Y - 20, 40, 40);
                    pbxImage.Refresh();
                }

                originVertexIndex = agent.DestinationVertexIndex;
                v_o = graph.getVertexAt(originVertexIndex);
                agent.addVisitado(v_o.Id);
                for (int i=0; i < v_o.EdgesCount; i++)
                {
                    if (!agent.visitado(v_o.getDestinationAt(i).Id))
                    {
                       nonvisited.Add(i);
                    }
                }
                if (nonvisited.Count > 0)
                {
                    int randEdgeIndex = rand.Next(0, nonvisited.Count);
                    randEdgeIndex = nonvisited[randEdgeIndex];
                    Point[] randomPath = v_o.getPathAt(randEdgeIndex);
                    agent.Path = randomPath;
                    Vertex v_d = v_o.getDestinationAt(randEdgeIndex);
                    destinationVertexIndex = graph.find(v_d);
                    agent.newPath(originVertexIndex, randomPath, destinationVertexIndex);
                }
            } while (v_o!=paro);
            x_p = 0; y_f = 0; x_f = 0; y_f = 0;
        }
*/

        //clase para hacer BFS
        public class nodo
        {
            int padre;
            int dato;
            public nodo(int padre, int dato)
            {
                this.padre = padre;
                this.dato = dato;
            }
            public int Padre
            {
                get
                {
                    return padre;
                }


                set
                {
                    padre = value;
                }
            }
            public int Dato
            {
                get
                {
                    return dato;
                }

                set
                {
                    dato = value;
                }
            }
        }



        //metodos kruskal

        Grafo kruskal()
        {
            int aux = graph.Count;
            int[,] componentes = inicializaCC(aux);
            Vertex v_o = graph.getVertexAt(0);
            List<Edge> possibles = new List<Edge>();
            possibles = getPossibles();
            List<Edge> solucion = new List<Edge>();
            List<Vertex> arbol = new List<Vertex>();
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph.getVertexAt(i).EdgesCount == 0)
                {
                    arbol.Add(graph.getVertexAt(i));
                    drawCircle(graph.getVertexAt(i).Circle.P_c.X, graph.getVertexAt(i).Circle.P_c.Y, graph.getVertexAt(i).Circle.R / 2, bmpImage, Color.PaleVioletRed);
                    pbxImage.Refresh();
                }
            }
            while (solucion.Count != aux - 1 && possibles.Count > 0)
            {
                Edge auxiliar = select(possibles);
                int ind1 = FindCC(auxiliar.origin, componentes);
                int ind2 = FindCC(auxiliar.destination, componentes);
                if (ind1 != ind2)
                {
                    solucion.Add(auxiliar);
                    combina(componentes, ind1, ind2, aux);
                    Vertex drawor = auxiliar.origin;
                    Vertex drawdest = auxiliar.destination;
                    g = Graphics.FromImage(bmpImage);
                    Pen edgesPen = new Pen(Color.Red, 7);
                    g.DrawLine(edgesPen, drawor.Circle.P_c.X, drawor.Circle.P_c.Y, drawdest.Circle.P_c.X, drawdest.Circle.P_c.Y);
                    drawCircle(drawor.Circle.P_c.X, drawor.Circle.P_c.Y, drawor.Circle.R / 2, bmpImage, Color.PaleVioletRed);
                    drawCircle(drawdest.Circle.P_c.X, drawdest.Circle.P_c.Y, drawdest.Circle.R / 2, bmpImage, Color.PaleVioletRed);
                    drawIdentificador(drawor.Circle.P_c.X, drawor.Circle.P_c.Y, drawor.Id.ToString(), drawor.Circle.R, bmpImage);
                    drawIdentificador(drawdest.Circle.P_c.X, drawdest.Circle.P_c.Y, drawdest.Id.ToString(), drawdest.Circle.R, bmpImage);
                    pbxImage.Refresh();
                    Vertex hoja = new Vertex(auxiliar.origin.Circle, auxiliar.origin.Id);
                    for (int i = 0; i < arbol.Count; i++)
                    {
                        if (hoja.Id == arbol[i].Id)
                        {
                            hoja = arbol[i];
                            arbol.RemoveAt(i);
                        }
                    }
                    Vertex hojadestin = new Vertex(auxiliar.destination.Circle, auxiliar.destination.Id);
                    //hoja.addEdge(hoja, hojadestin, auxiliar.id, auxiliar.Path, auxiliar.distance);
                    //arbol.Add(hoja);
                    Vertex destinoauxiliar = auxiliar.destination;
                    for (int i = 0; i < arbol.Count; i++)
                    {
                        if (hojadestin.Id == arbol[i].Id)
                        {
                            hojadestin = arbol[i];
                            arbol.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < destinoauxiliar.EdgesCount; i++)
                    {
                        if (destinoauxiliar.eL[i].destination.Id == auxiliar.origin.Id)
                        {
                            hojadestin.addEdge(hojadestin, hoja, destinoauxiliar.eL[i].id, destinoauxiliar.eL[i].Path, destinoauxiliar.eL[i].distance);
                        }
                    }
                    hoja.addEdge(hoja, hojadestin, auxiliar.id, auxiliar.Path, auxiliar.distance);
                    arbol.Add(hoja);
                    arbol.Add(hojadestin);
                    //////////////////////////////////
                }
                // MessageBox.Show(prueba);
            }
            Grafo grafo = new Grafo();
            for (int i = 0; i < arbol.Count; i++)
            {
                ////////////!!!!!!!!!!!!!! ID DE VERTICES EDITAR SI NO FUNCIONA
                grafo.addVertex(arbol[i]);
            }
            return grafo;

        }

        void combina(int[,] CC, int ind1, int ind2, int aux)
        {
            int auxiliar = 0;
            if (ind2 == 13)
            {
                auxiliar = 0;
            }
            if (ind2 < ind1)
            {
                auxiliar = ind1;
                ind1 = ind2;
                ind2 = auxiliar;
            }
            for (int i = 0; i < aux; i++)
            {
                if (CC[ind2, i] != -1)
                {
                    string prueba = CC[ind2, i].ToString();
                    CC[ind1, i] = CC[ind2, i];
                    CC[ind2, i] = -1;
                }
            }

        }

        public int[,] inicializaCC(int aux)
        {
            int[,] CC = new int[aux, aux];
            for (int i = 0; i < aux; i++)
            {

                for (int j = 0; j < aux; j++)
                {
                    CC[i, j] = -1;
                }
            }
            for (int i = 0; i < graph.Count; i++)
            {
                Vertex auxiliar = graph.getVertexAt(i);
                CC[i, i] = auxiliar.Id;
            }
            return CC;

        }

        public Edge select(List<Edge> possibles)
        {
            int indmenor = 0;
            for (int i = 0; i < possibles.Count; i++)
            {
                if (possibles[i].distance < possibles[indmenor].distance)
                {
                    indmenor = i;
                }
            }
            Edge aux = possibles[indmenor];
            possibles.RemoveAt(indmenor);
            return aux;
        }

        int FindCC(Vertex v_d, int[,] CC)
        {
            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph.Count; j++)
                {
                    if (CC[i, j] == v_d.Id)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public List<Edge> getPossibles()
        {
            List<Edge> possibles = new List<Edge>();
            for (int i = 0; i < graph.Count; i++)
            {
                Vertex aux = graph.getVertexAt(i);
                for (int j = 0; j < aux.EdgesCount; j++)
                {
                    Edge auxiliar = aux.eL[j];
                    possibles.Add(auxiliar);
                }
            }
            return possibles;
        }
        //metodos prim


        Grafo Prim(Vertex v_i, List<Vertex> verticesprim)
        {
            int aux = graph.Count;
            int[] prometedor = inicializaprom(graph.Count);
            List<Edge> candidatos = new List<Edge>();
            List<Edge> solucion = new List<Edge>();
            List<Vertex> arbol = new List<Vertex>();
            prometedor[v_i.Id - 1] = v_i.Id;
            if (v_i.EdgesCount == 0)
            {
                arbol.Add(v_i);
                drawCircle(v_i.Circle.P_c.X, v_i.Circle.P_c.Y, v_i.Circle.R, bmpImage, Color.LightSkyBlue);
                verticesprim.RemoveAt(0);
                pbxImage.Refresh();
            }
            for (int i = 0; i < v_i.EdgesCount; i++)
            {
                candidatos.Add(v_i.eL[i]);
            }
            while (solucion.Count != aux - 1 && candidatos.Count > 0)
            {
                Edge auxiliar = select(candidatos);
                bool band = pertenece(auxiliar, prometedor, aux);
                if (band == false)
                {
                    Vertex drawor = auxiliar.origin;
                    Vertex drawdest = auxiliar.destination;
                    g = Graphics.FromImage(bmpImage);
                    Pen edgesPen = new Pen(Color.Blue, 15);
                    g.DrawLine(edgesPen, drawor.Circle.P_c.X, drawor.Circle.P_c.Y, drawdest.Circle.P_c.X, drawdest.Circle.P_c.Y);
                    drawCircle(drawor.Circle.P_c.X, drawor.Circle.P_c.Y, drawor.Circle.R, bmpImage, Color.LightSkyBlue);
                    drawCircle(drawdest.Circle.P_c.X, drawdest.Circle.P_c.Y, drawdest.Circle.R, bmpImage, Color.LightSkyBlue);
                    drawIdentificador(drawor.Circle.P_c.X, drawor.Circle.P_c.Y, drawor.Id.ToString(), drawor.Circle.R, bmpImage);
                    drawIdentificador(drawdest.Circle.P_c.X, drawdest.Circle.P_c.Y, drawdest.Id.ToString(), drawdest.Circle.R, bmpImage);
                    pbxImage.Refresh();
                    solucion.Add(auxiliar);
                    Vertex hoja = new Vertex(auxiliar.origin.Circle, auxiliar.origin.Id);
                    for (int i = 0; i < arbol.Count; i++)
                    {
                        if (hoja.Id == arbol[i].Id)
                        {
                            hoja = arbol[i];
                            arbol.RemoveAt(i);
                        }
                    }
                    Vertex hojadestin = new Vertex(auxiliar.destination.Circle, auxiliar.destination.Id);
                    hoja.addEdge(hoja, hojadestin, auxiliar.id, auxiliar.Path, auxiliar.distance);
                    arbol.Add(hoja);
                    Vertex destinoauxiliar = auxiliar.destination;
                    for (int i = 0; i < destinoauxiliar.EdgesCount; i++)
                    {
                        if (destinoauxiliar.eL[i].destination.Id == auxiliar.origin.Id)
                        {
                            hojadestin.addEdge(hojadestin, hoja, destinoauxiliar.eL[i].id, destinoauxiliar.eL[i].Path, destinoauxiliar.eL[i].distance);
                        }
                    }
                    arbol.Add(hojadestin);
                    //////////////////////////////////
                    Vertex nuevo = auxiliar.destination;
                    for (int i = 0; i < nuevo.EdgesCount; i++)
                    {
                        candidatos.Add(nuevo.eL[i]);
                    }
                    prometedor[auxiliar.destination.Id - 1] = auxiliar.destination.Id;
                    for (int i = 0; i < verticesprim.Count; i++)
                    {
                        if (verticesprim[i].Id == v_i.Id)
                        {
                            verticesprim.RemoveAt(i);
                        }
                        if (verticesprim[i].Id == auxiliar.destination.Id)
                        {
                            verticesprim.RemoveAt(i);
                        }
                    }
                }

            }
            Grafo grafo = new Grafo();
            for (int i = 0; i < arbol.Count; i++)
            {
                ////////////!!!!!!!!!!!!!! ID DE VERTICES EDITAR SI NO FUNCIONA
                grafo.addVertex(arbol[i]);
            }
            return grafo;


        }

        bool pertenece(Edge auxiliar, int[] prometedor, int tamgrafo)
        {
            bool pertenece = false;
            int i;
            for (i = 0; i < tamgrafo; i++)
            {
                if (prometedor[i] == auxiliar.destination.Id)
                {
                    pertenece = true;
                }
            }
            int wait = 0;
            return pertenece;
        }

        int[] inicializaprom(int aux)
        {
            int[] prometedor = new int[aux];
            for (int i = 0; i < aux; i++)
            {
                prometedor[i] = -1;
            }
            return prometedor;
        }

        Vertex DFS(Vertex v_o, List<nodo> Arbol, Vertex v_obj, Vertex v_padre)
        {
            g = Graphics.FromImage(bmpAnimation);
            Brush agentBrush = new SolidBrush(Color.BlueViolet);
            int destinationVertexIndex;
            int originVertexIndex;
            //crear lista de enteros
            visitados.Add(v_o.Id);
            if (v_o.Id == v_obj.Id)
            {
                explorar = false;
                return v_o;
            }
            //Este for es para generar el arbol de BFS.
            for (int m = 0; m < v_o.EdgesCount; m++)
            {
                Vertex destinos = v_o.getDestinationAt(m);
                bool yaeracola = false;
                encolados.Add(v_o.Id);
                for (int c = 0; c < encolados.Count; c++)
                {
                    if (destinos.Id == encolados[c])
                    {
                        yaeracola = true;
                    }
                }
                if (yaeracola == false)
                {
                    nodo auxiliar = new nodo(v_o.Id, destinos.Id);
                    Arbol.Add(auxiliar);
                    encolados.Add(destinos.Id);
                }
            }
            List<Vertex> destinosnovisitados = new List<Vertex>();
            bool existe;
            for (int m = 0; m < v_o.EdgesCount; m++)
            {
                existe = false;
                for (int c = 0; c < visitados.Count; c++)
                {
                    if (visitados[c] == v_o.getDestinationAt(m).Id)
                    {
                        existe = true;
                        break;
                    }

                }
                if (existe == false)
                {
                    destinosnovisitados.Add(v_o.getDestinationAt(m));
                }
            }
            while (destinosnovisitados.Count > 0)
            {
                destinosnovisitados.Clear();
                for (int m = 0; m < v_o.EdgesCount; m++)
                {
                    existe = false;
                    for (int c = 0; c < visitados.Count; c++)
                    {
                        if (visitados[c] == v_o.getDestinationAt(m).Id)
                        {
                            existe = true;
                            break;
                        }

                    }
                    if (existe == false)
                    {
                        destinosnovisitados.Add(v_o.getDestinationAt(m));
                    }
                }
                int i = rand.Next(0, destinosnovisitados.Count);
                if (destinosnovisitados.Count > 0)
                {
                    Vertex v_d = destinosnovisitados[i];
                    int k;
                    for (k = 0; k < v_o.EdgesCount; k++)
                    {
                        Vertex aux = v_o.getDestinationAt(k);
                        if (v_d.Id == aux.Id)
                        {
                            break;
                        }
                    }
                    Point[] randomPath = v_o.getPathAt(k);
                    agent.Path = randomPath;
                    originVertexIndex = graph.find(v_o);
                    destinationVertexIndex = v_d.Id;
                    agent.newPath(originVertexIndex, randomPath, destinationVertexIndex);

                    while (agent.walk())
                    {
                        Point p_k = agent.getActualPosition();
                        g.Clear(Color.Transparent);
                        /** Dibuja el nuevo estado del circulo **/
                        g.FillEllipse(agentBrush, p_k.X - 20, p_k.Y - 20, 40, 40);
                        pbxImage.Refresh();
                    }
                    Vertex auxiliar = DFS(v_d, Arbol, v_obj, v_o);
                    destinosnovisitados.RemoveAt(i);
                    if (auxiliar != null)
                    {
                        return auxiliar;
                    }
                }
                //}
            }
            bool regreso = false;
            int h;
            for (h = 0; h < v_o.EdgesCount; h++)
            {
                Vertex aux2 = v_o.getDestinationAt(h);
                if (v_padre.Id == aux2.Id)
                {
                    regreso = true;
                    break;
                }
                else
                {
                    regreso = false;
                }

            }
            if (regreso == true && explorar == true)
            {
                Point[] pathregreso = v_o.getPathAt(h);
                agent.Path = pathregreso;
                originVertexIndex = graph.find(v_o);
                destinationVertexIndex = v_padre.Id;
                agent.newPath(originVertexIndex, pathregreso, destinationVertexIndex);
                //si no es visitado lo añado a la pila, y camino hacia el v_d
                while (agent.walk())
                {
                    Point p_k = agent.getActualPosition();
                    g.Clear(Color.Transparent);
                    /** Dibuja el nuevo estado del circulo **/
                    g.FillEllipse(agentBrush, p_k.X - 20, p_k.Y - 20, 40, 40);
                    pbxImage.Refresh();
                }
            }
            return null;
        }

        void BFS(List<nodo> Arbol, Vertex raiz, Vertex v_obj)
        {
            Vertex v_d = raiz;
            Graphics g = Graphics.FromImage(bmpAnimation);
            for (int i = 0; i < Arbol.Count; i++)
            {
                int h;
                bool explorar = false;
                if (v_obj.Id == Arbol[i].Dato)
                {
                    for (h = 0; h < v_obj.EdgesCount; h++)
                    {
                        v_d = v_obj.getDestinationAt(h);
                        if (Arbol[i].Padre == v_d.Id)
                        {
                            explorar = true;
                            break;
                        }
                        else
                        {
                            explorar = false;
                        }

                    }
                    if (explorar)
                    {
                        Point[] pathregreso = v_obj.getPathAt(h);
                        agent.Path = pathregreso;
                        int originVertexIndex = graph.find(v_obj);
                        int destinationVertexIndex = Arbol[i].Padre;
                        agent.newPath(originVertexIndex, pathregreso, destinationVertexIndex);
                        Pen edgesPen = new Pen(Color.Red);
                        g.DrawLine(edgesPen, v_obj.Circle.P_c.X, v_obj.Circle.P_c.Y, v_d.Circle.P_c.X, v_d.Circle.P_c.Y);
                        pbxImage.Refresh();
                        if (v_obj.Id == raiz.Id)
                        {
                            return;
                        }
                        v_obj = graph.getVertexAt(Arbol[i].Padre - 1);

                        BFS(Arbol, raiz, v_obj);
                    }
                }

            }
        }

        private void listBoxCirculos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //recorrido prim
        private void btnPRIM_Click(object sender, EventArgs e)
        {
            x_p = 0;
            y_p = 0;


        }

        Vertex getPrim(Vertex v_o)
        {
            Vertex aux = v_o;
            bool origenprim = false;
            while (origenprim == false)
            {
                //esa es la lista de lista de vertices
                for (int i = 0; i < arbolprim.Count; i++)
                {
                    //esta es la lista de vertices contenida en esa iteracion
                    for (int j = 0; j < arbolprim[i].Count; j++)
                    {
                        //buscamos el vertce equivalente
                        if (arbolprim[i][j].Id == v_o.Id)
                        {
                            return arbolprim[i][j];
                        }

                    }
                }

            }
            return aux;
        }

        void viajeprim(Vertex v_o, List<int> visitados, Vertex v_obj, Vertex v_padre, Grafo grafo)
        {
            Graphics g = Graphics.FromImage(bmpAnimation);
            Brush agentBrush = new SolidBrush(Color.BlueViolet);
            int destinationVertexIndex;
            int originVertexIndex;
            //crear lista de enteros
            visitados.Add(v_o.Id);
            if (v_o.Id == v_obj.Id)
            {
                explorar = false;
                return;
            }
            //seleccion de arista
            for (int i = 0; i < v_o.EdgesCount; i++)
            {

                if (explorar == false)
                {
                    return;
                }
                Vertex v_d = getMenor(v_o, v_obj, visitados);
                bool yavisitado = false;
                for (int l = 0; l < visitados.Count; l++)
                {
                    if (v_d.Id == visitados[l])
                    {
                        yavisitado = true;
                    }
                }
                int k;
                if (!yavisitado)
                {
                    for (k = 0; k < v_o.EdgesCount; k++)
                    {
                        Vertex aux = v_o.getDestinationAt(k);
                        if (v_d.Id == aux.Id)
                        {
                            break;
                        }
                    }
                    Point[] randomPath = v_o.getPathAt(k);
                    agent.Path = randomPath;
                    originVertexIndex = grafo.find(v_o);
                    destinationVertexIndex = v_d.Id;
                    agent.newPath(originVertexIndex, randomPath, destinationVertexIndex);
                    //si no es visitado lo añado a la pila, y camino hacia el v_d
                    while (agent.walk())
                    {
                        Point p_k = agent.getActualPosition();
                        g.Clear(Color.Transparent);
                        /** Dibuja el nuevo estado del circulo **/
                        g.FillEllipse(agentBrush, p_k.X - 20, p_k.Y - 20, 40, 40);
                        dibujaSensor(p_k.X, p_k.Y, paro.Circle.P_c.X, paro.Circle.P_c.Y, g);
                        pbxImage.Refresh();
                    }
                    viajeprim(v_d, visitados, paro, v_o, grafo);
                }
            }
            bool regreso = false;
            int h;
            for (h = 0; h < v_o.EdgesCount; h++)
            {
                Vertex aux2 = v_o.getDestinationAt(h);
                if (v_padre.Id == aux2.Id)
                {
                    regreso = true;
                    break;
                }
                else
                {
                    regreso = false;
                }

            }
            if (regreso == true && explorar == true)
            {
                Point[] pathregreso = v_o.getPathAt(h);
                agent.Path = pathregreso;
                originVertexIndex = grafo.find(v_o);
                destinationVertexIndex = v_padre.Id;
                agent.newPath(originVertexIndex, pathregreso, destinationVertexIndex);
                //si no es visitado lo añado a la pila, y camino hacia el v_d
                while (agent.walk())
                {
                    Point p_k = agent.getActualPosition();
                    g.Clear(Color.Transparent);
                    /** Dibuja el nuevo estado del circulo **/
                    dibujaSensor(p_k.X, p_k.Y, paro.Circle.P_c.X, paro.Circle.P_c.Y, g);
                    g.FillEllipse(agentBrush, p_k.X - 20, p_k.Y - 20, 40, 40);
                    pbxImage.Refresh();
                }
            }

        }

        void viajekruskal(Vertex v_o, List<int> visitados, Vertex v_obj, Vertex v_padre, Grafo grafo)
        {
            Graphics g = Graphics.FromImage(bmpAnimation);
            Brush agentBrush = new SolidBrush(Color.BlueViolet);
            int destinationVertexIndex;
            int originVertexIndex;
            //crear lista de enteros
            visitados.Add(v_o.Id);
            if (v_o.Id == v_obj.Id)
            {
                explorar = false;
                return;
            }
            //seleccion de arista
            for (int i = 0; i < v_o.EdgesCount; i++)
            {

                if (explorar == false)
                {
                    return;
                }
                Vertex v_d = getMenor(v_o, v_obj, visitados);
                bool yavisitado = false;
                for (int l = 0; l < visitados.Count; l++)
                {
                    if (v_d.Id == visitados[l])
                    {
                        yavisitado = true;
                    }
                }
                int k;
                if (!yavisitado)
                {
                    for (k = 0; k < v_o.EdgesCount; k++)
                    {
                        Vertex aux = v_o.getDestinationAt(k);
                        if (v_d.Id == aux.Id)
                        {
                            break;
                        }
                    }
                    Point[] randomPath = v_o.getPathAt(k);
                    agent.Path = randomPath;
                    originVertexIndex = grafo.find(v_o);
                    destinationVertexIndex = v_d.Id;
                    agent.newPath(originVertexIndex, randomPath, destinationVertexIndex);
                    //si no es visitado lo añado a la pila, y camino hacia el v_d
                    while (agent.walk())
                    {
                        Point p_k = agent.getActualPosition();
                        g.Clear(Color.Transparent);
                        /** Dibuja el nuevo estado del circulo **/
                        g.FillEllipse(agentBrush, p_k.X - 20, p_k.Y - 20, 40, 40);
                        dibujaSensor(p_k.X, p_k.Y, paro.Circle.P_c.X, paro.Circle.P_c.Y, g);
                        pbxImage.Refresh();
                    }
                    viajekruskal(v_d, visitados, paro, v_o, grafo);
                }
            }
            bool regreso = false;
            int h;
            for (h = 0; h < v_o.EdgesCount; h++)
            {
                Vertex aux2 = v_o.getDestinationAt(h);
                if (v_padre.Id == aux2.Id)
                {
                    regreso = true;
                    break;
                }
                else
                {
                    regreso = false;
                }

            }
            if (regreso == true && explorar == true)
            {
                Point[] pathregreso = v_o.getPathAt(h);
                agent.Path = pathregreso;
                originVertexIndex = grafo.find(v_o);
                destinationVertexIndex = v_padre.Id;
                agent.newPath(originVertexIndex, pathregreso, destinationVertexIndex);
                //si no es visitado lo añado a la pila, y camino hacia el v_d
                while (agent.walk())
                {
                    Point p_k = agent.getActualPosition();
                    g.Clear(Color.Transparent);
                    /** Dibuja el nuevo estado del circulo **/
                    dibujaSensor(p_k.X, p_k.Y, paro.Circle.P_c.X, paro.Circle.P_c.Y, g);
                    g.FillEllipse(agentBrush, p_k.X - 20, p_k.Y - 20, 40, 40);
                    pbxImage.Refresh();
                }
            }

        }

        void dibujaSensor(int x_p, int y_p, int x_f, int y_f, Graphics g)
        {
            float x_0, y_0;
            float x_k, y_k;
            float theta;
            float d_x, d_y;
            float d_x2, d_y2;
            float h = 50;
            d_y = y_f - y_p;
            d_x = x_f - x_p;
            theta = (float)Math.Atan2(d_y, d_x); // Tal vez esto se puede sacar mejor con SOR CAR TOA
            /*double angle = (float)Math.Atan2(d_y, d_x) - (float)Math.Atan2(y_p, x_p);
			if (angle < 0) { 
				angle += 2 * Math.PI;
			}*/
            double angle = theta * (180 / Math.PI);
            x_k = x_p + (float)Math.Cos(theta) * h;
            y_k = y_p + (float)Math.Sin(theta) * h;

            //g.Clear(Color.Transparent);
            g.DrawLine(new Pen(Color.Purple, 5), x_p, y_p, x_k, y_k);
            x_p = 0;
            y_p = 0;
            x_f = 0;
            y_f = 0;
        }

        Vertex getMenor(Vertex v_origen, Vertex v_obj, List<int> visitados)
        {
            double objetivo = theta(v_origen, v_obj);
            double aux = 0;
            double menor = 0;
            double distance = 3600;
            bool primera = true;
            Vertex optimo = v_origen;
            for (int i = 0; i < v_origen.EdgesCount; i++)
            {
                aux = theta(v_origen, v_origen.eL[i].destination);
                double auxdistance = distancia(aux, objetivo);
                if (auxdistance < distance)
                {
                    bool valida = true;
                    for (int j = 0; j < visitados.Count; j++)
                    {
                        if (v_origen.eL[i].destination.Id == visitados[j])
                        {
                            valida = false;
                        }
                    }
                    if (valida == true)
                    {
                        distance = auxdistance;
                        optimo = v_origen.eL[i].destination;
                    }
                }

            }
            return optimo;

        }

        double theta(Vertex v_origen, Vertex v_obj)
        {
            float x_0, y_0;
            float x_k, y_k;
            float theta;
            float d_x, d_y;
            float d_x2, d_y2;
            float h = 30;
            float x_p = v_origen.Circle.P_c.X;
            float y_p = v_origen.Circle.P_c.Y;

            float x_f = v_obj.Circle.P_c.X;
            float y_f = v_obj.Circle.P_c.Y;
            d_y = y_f - y_p;
            d_x = x_f - x_p;
            theta = (float)Math.Atan2(d_y, d_x); // Tal vez esto se puede sacar mejor con SOR CAR TOA
            double angle = theta * (180 / Math.PI);
            //labelTetha.Text = "Theta = " + angle;
            x_k = x_p + (float)Math.Cos(theta) * h;
            y_k = y_p + (float)Math.Sin(theta) * h;

            x_p = 0;
            y_p = 0;
            x_f = 0;
            return angle;
        }

        double distancia(double origen, double destino)
        {
            //redondear theta
            double origenr = 0;
            double destinor = 0;
            origenr = (double)decimal.Round((decimal)origen, 1);
            destinor = (double)decimal.Round((decimal)destino, 1);
            int iteradorizq = 0;
            int iteradorder = 0;
            double aux = origenr;
            bool vuelta = false;
            while (aux != destinor)
            {
                iteradorizq = iteradorizq + 1;
                if (aux <= 180)
                {
                    aux = aux + 0.1;
                    aux = (double)decimal.Round((decimal)aux, 1);
                }
                else
                {
                    aux = -180;
                    //aux = aux + 0.1;
                    aux = (double)decimal.Round((decimal)aux, 1);

                }
            }
            /// iteracion en sentido contrario
            aux = origenr;
            while (aux != destinor)
            {
                iteradorder = iteradorder + 1;
                if (aux >= -180)
                {
                    aux = aux - 0.1;
                    aux = (double)decimal.Round((decimal)aux, 1);
                }
                else
                {
                    aux = 180;
                    //aux = aux - 0.1;
                    aux = (double)decimal.Round((decimal)aux, 1);

                }

            }
            if (iteradorder > iteradorizq)
            {
                return iteradorizq;
            }
            return iteradorder;

        }

        private void btnKruskal_Click(object sender, EventArgs e)
        {
            if (paro == null)
            {
                MessageBox.Show("No hay objetivo, agrega un objetivo");
                return;
            }
            if (agentesDijkstra.Count == 0)
            {
                MessageBox.Show("No hay agentes, agrega uno primero");
                return;
            }
            if (primeraAnimacion == true)
            {
                inicializaDepredadores(listaDepredadores);
                inicializaRutasDepredadores(listaDepredadores, agentesDepredadores);
                primeraAnimacion = false;
            }
            borraDepredadores(agentesDepredadores);
            reiniciaPresas(listaDepredadores);
            animaDijkstra(paro);

        }


        void reiniciaPresas(List<depredador> listaDepredadores)
        {
            for (int i = 0; i < listaDepredadores.Count; i++)
            {
                listaDepredadores[i].Presa = null;
                listaDepredadores[i].Exitado = false;
                listaDepredadores[i].Vel = 2;
            }
        }


        void ocultarmeDeDepredadores(Agent agente)
        {
            for (int i = 0; i < agente.depredadores.Count; i++)
            {
                if (agente.depredadores[i].presa != null)
                {
                    if (agente.depredadores[i].Presa.Id == agente.Id)
                    {
                        agente.depredadores[i].Presa = null;
                        agente.depredadores[i].Exitado = false;
                        agente.depredadores[i].Vel = 2;
                    }
                }
            }
        }

        public void dibujaAgentes()
        {
            Vertex v_o;
            for (int i = 0; i < agentesDijkstra.Count; i++)
            {
                v_o = graph.getVertexAt(agentesDijkstra[i] - 1);
                v_o.Seleccionado = true;
                g = Graphics.FromImage(bmpImage);
                Brush agentBrush = new SolidBrush(Color.BlueViolet);
                float ra = (v_o.Circle.R * 2) - 5;
                g.FillEllipse(agentBrush, v_o.Circle.P_c.X - ra / 2, v_o.Circle.P_c.Y - ra / 2, ra, ra);
            }
        }


        public void dibujaDepredadores()
        {
            Vertex v_o;
            for (int i = 0; i < agentesDepredadores.Count; i++)
            {
                v_o = graph.getVertexAt(agentesDepredadores[i] - 1);
                v_o.Seleccionado = true;
                g = Graphics.FromImage(bmpAnimation);
                Brush agentBrush = new SolidBrush(Color.Red);
                float ra = (v_o.Circle.R * 2) - 5;
                g.FillEllipse(agentBrush, v_o.Circle.P_c.X - ra / 2, v_o.Circle.P_c.Y - ra / 2, ra, ra);
            }

        }

        public void dibujaRadares()
        {
            Vertex v_o;
            for (int i = 0; i < agentesDepredadores.Count; i++)
            {

                v_o = graph.getVertexAt(agentesDepredadores[i] - 1);
                v_o.Seleccionado = true;
                g = Graphics.FromImage(bmpAnimation);
                Color prueba = Color.FromArgb(50, 153, 255, 153);

                Brush agentBrush = new SolidBrush(prueba);
                float ra = (radioRadar * 2) - 5;
                g.FillEllipse(agentBrush, v_o.Circle.P_c.X - radioRadar / 2, v_o.Circle.P_c.Y - radioRadar / 2, radioRadar, radioRadar);
            }

        }


        void dibujaPosiciones(List<depredador> listaDepredadores)
        {
            Color prueba = Color.FromArgb(50, 153, 255, 153);
            Graphics g = Graphics.FromImage(bmpAnimation);
            Brush radarBrush = new SolidBrush(prueba);
            Brush predatorBrush = new SolidBrush(Color.Red);
            for (int i = 0; i < listaDepredadores.Count; i++)
            {
                Point p_k = listaDepredadores[i].getActualPosition();
                /** Dibuja el nuevo estado del circulo **/
                g.FillEllipse(radarBrush, p_k.X - radioRadar / 2, p_k.Y - radioRadar / 2, radioRadar, radioRadar);
                g.FillEllipse(predatorBrush, p_k.X - radioGrl / 2, p_k.Y - radioGrl / 2, radioGrl, radioGrl);
                if (listaDepredadores[i].canWalk() == false)
                {
                    renuevaRutaDepredador(listaDepredadores[i]);
                }
            }
        }

        public int belongsToAVertex(float x_b, float y_b, Grafo graph)
        {
            Vertex v_i;
            Circle c;
            float x_c, y_c, s, r;

            for (int i = 0; i < graph.Count; i++)
            {
                v_i = graph.getVertexAt(i);
                c = v_i.Circle;
                x_c = c.P_c.X;
                y_c = c.P_c.Y;
                r = c.R;
                s = (x_b - x_c) * (x_b - x_c) + (y_b - y_c) * (y_b - y_c) - (r * r);
                if (s <= 0)
                    return i;
            }
            return -1;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //METODOS DKJISTRA.
        public class Dijkstra
        {
            Vertex proveniente;
            double peso_acumulado;
            bool definitivo;
            public Dijkstra(Vertex prov, int peso, bool def)
            {
                proveniente = prov;
                peso_acumulado = peso;
                definitivo = def;

            }
            public Vertex Proveniente
            {
                get { return proveniente; }
                set { proveniente = value; }
            }
            public double PesoAcumulado
            {
                get { return peso_acumulado; }
                set { peso_acumulado = value; }
            }
            public bool Definitivo
            {
                get { return definitivo; }
                set { definitivo = value; }
            }
        }


        public void inicializaVectorDijkstra(Grafo G, List<Dijkstra> vector, Vertex v_i)
        {
            for (int i = 0; i < G.Count; i++)
            {
                Dijkstra elemento_dijkstra = new Dijkstra(null, 100000, false);
                if (v_i.Id - 1 == i)
                {
                    vector.Add(elemento_dijkstra);
                    vector[v_i.Id - 1].PesoAcumulado = 0;
                    vector[v_i.Id - 1].Proveniente = v_i;

                }
                else
                {
                    vector.Add(elemento_dijkstra);
                }

            }

        }

        public int seleccionaDefinitivo(List<Dijkstra> vector)
        {
            int indxmenor = menorNoDefinitivo(vector);
            if (indxmenor != -1)
            {
                vector[indxmenor].Definitivo = true;
            }
            return indxmenor;
        }

        public int menorNoDefinitivo(List<Dijkstra> vector)
        {
            Dijkstra auxiliar = new Dijkstra(null, 100000, false);
            int indiceMenor = -1;
            for (int i = 0; i < vector.Count; i++)
            {
                if (vector[i].Definitivo == false)
                {
                    if (vector[i].PesoAcumulado < auxiliar.PesoAcumulado)
                    {
                        auxiliar = vector[i];
                        indiceMenor = i;

                    }
                }
            }
            return indiceMenor;
        }

        public bool solucion(List<Dijkstra> vector)
        {
            bool esSolucion = true;
            for (int i = 0; i < vector.Count; i++)
            {
                if (vector[i].Definitivo == false)
                {
                    esSolucion = false;
                    break;
                }
            }
            return esSolucion;
        }



        void actualizaVector(List<Dijkstra> vector, int indxmenor)
        {
            double peso_actual = vector[indxmenor].PesoAcumulado;
            Vertex v = graph.getVertexAt(indxmenor);
            for (int i = 0; i < v.EdgesCount; i++)
            {
                double peso_i = peso_actual + v.eL[i].distance;
                Vertex v_d = v.eL[i].destination;
                if (vector[v_d.Id - 1].PesoAcumulado > peso_i)
                {
                    vector[v_d.Id - 1].PesoAcumulado = peso_i;
                    vector[v_d.Id - 1].Proveniente = v;
                }

            }
        }


        List<Dijkstra> doDijkstra(Vertex v_i)
        {
            List<Dijkstra> vector = new List<Dijkstra>();
            inicializaVectorDijkstra(graph, vector, v_i);
            int index = 0;
            while (!solucion(vector) && index != -1)
            {
                index = seleccionaDefinitivo(vector);
                if (index != -1)
                {
                    actualizaVector(vector, index);
                }

            }
            return vector;

        }


        void inicializaDepredadores(List<depredador> listaDepredadores)
        {
            listaDepredadores.Clear();
            for (int i = 0; i < agentesDepredadores.Count; i++)
            {
                depredador depre = new depredador();
                depre.RadioRadar = radioRadar;
                listaDepredadores.Add(depre);
            }
        }

        void inicializaAgentes(List<Agent> listaAgentes)
        {
            for (int i = 0; i < agentesDijkstra.Count; i++)
            {
                Agent agente = new Agent();
                agente.Id = i;
                listaAgentes.Add(agente);
                
            }
        }

        private void pbxImage_MouseMove(object sender, MouseEventArgs e)
        {
            float w_b, h_b;
            float w_p, h_p;
            float x_b, y_b, x_v, y_v;
            float r_x, r_y, r;
            float d_x, d_y;

            if (paro != null)
            {
                Vertex v_o = graph.getVertexAt(0);
                int x_f = e.X;
                int y_f = e.Y;
                w_b = bmpImage.Width;
                h_b = bmpImage.Height;
                w_p = pbxImage.Width;
                h_p = pbxImage.Height;

                r_x = w_p / w_b;
                r_y = h_p / h_b;

                r = r_y;
                if (r_x < r_y)
                    r = r_x;

                d_x = (w_p - r * w_b) / 2;
                d_y = (h_p - r * h_b) / 2;
                int v_index = 0;
                x_v = (x_f - d_x) / r;
                y_v = (y_f - d_y) / r;
                v_index = belongsToAVertex(x_v, y_v, graph);
                if (v_index != -1 && paro != null)
                {
                    Graphics g = Graphics.FromImage(bmpAnimation);
                    g.Clear(Color.Transparent);
                    v_o = graph.getVertexAt(v_index);
                    Grafo aux = new Grafo();
                    dibujaAgentes();
                    if (listaDepredadores.Count > 0)
                    {
                        dibujaPosiciones(listaDepredadores);
                    }
                    else
                    {
                        dibujaRadares();
                        dibujaDepredadores();
                    }
                    dibujaDijkstra(v_o, vector, paro, aux);
                }


            }

        }

        public bool vaHaciaDepredador(Agent agente, List<depredador> predator)
        {
            for(int i = 0; i<agente.depredadores.Count; i++)
            {
                if ((agente.OriginVertexIndex == predator[i].DestinationVertexIndex) && (agente.DestinationVertexIndex == predator[i].OriginVertexIndex + 1))
                {
                    agente.Huyendo = true;
                    return true;
                }
            }
            for (int i = 0; i < agente.depredadores.Count; i++)
            {
                if ((agente.OriginVertexIndex == predator[i].OriginVertexIndex + 1) && (agente.DestinationVertexIndex == predator[i].DestinationVertexIndex))
                {
                    if (agente.PathIndex < predator[i].PathIndex)
                    {
                        agente.Huyendo = true;
                        return true;
                    }
                }
            }
            return false; 
        }

        void animaDijkstra(Vertex Objetivo)
        {
            Graphics g = Graphics.FromImage(bmpAnimation);
            Color prueba = Color.FromArgb(50, 153, 255, 153);
            Brush agentBrush = new SolidBrush(Color.BlueViolet);
            Brush predatorBrush = new SolidBrush(Color.Red);
            Brush radarBrush = new SolidBrush(prueba);
            Vertex v_o;
            List<Agent> listaAgentes = new List<Agent>();
            inicializaAgentes(listaAgentes);
            int originVertexIndex;
            int destinationVertexIndex = 0;
            List<int> noConexos = new List<int>();
            bool ObjetivoConsumido = false;
            int k;
            inicializaRutas(listaAgentes, agentesDijkstra, grafoDijkstra, vector, noConexos);
            if (puedenCaminar(listaAgentes))
            {
                while (puedenCaminar(listaAgentes) || seEsconden(listaAgentes))
                {
                    for (int i = 0; i < listaAgentes.Count; i++)
                    {
                        if (listaAgentes[i].canWalk() == true)
                        {
                            if (listaAgentes[i].SoyPresa)
                            {
                                if (vaHaciaDepredador(listaAgentes[i], listaAgentes[i].depredadores) == true)
                                {
                                    listaAgentes[i].reverseWalk();
                                }
                                else
                                {
                                    listaAgentes[i].Huyendo = false;
                                    listaAgentes[i].walk();
                                }
                            }
                            else
                            {
                                listaAgentes[i].walk();
                            }
                            Point p_k = listaAgentes[i].getActualPosition();
                            /** Dibuja el nuevo estado del circulo **/
                            g.FillEllipse(agentBrush, p_k.X - radioGrl / 2, p_k.Y - radioGrl / 2, radioGrl, radioGrl);
                        }
                        else
                        {
                            Point p_k = listaAgentes[i].getActualPosition();
                            g.FillEllipse(agentBrush, p_k.X - radioGrl / 2, p_k.Y - radioGrl / 2, radioGrl, radioGrl);
                        }
                        if (listaAgentes[i].canWalk() == false)
                        {
                            ocultarmeDeDepredadores(listaAgentes[i]);
                            if (listaAgentes[i].SoyPresa == true && listaAgentes[i].Escondete < 50)
                            {
                                Point p_k = listaAgentes[i].getActualPosition();
                                /** Dibuja el nuevo estado del circulo **/
                                g.FillEllipse(agentBrush, p_k.X - radioGrl / 2, p_k.Y - radioGrl / 2, radioGrl, radioGrl);
                                listaAgentes[i].Escondete = listaAgentes[i].Escondete + 1;
                                if (listaAgentes[i].Escondete == 50)
                                    listaAgentes[i].SoyPresa = false;
                            }
                            if (listaAgentes[i].SoyPresa == false || listaAgentes[i].Escondete>49)
                            {
                                listaAgentes[i].Escondete = 0;
                                if (listaAgentes[i].Huyendo == true)
                                {
                                    listaAgentes[i].Huyendo = false;
                                    ocultarmeDeDepredadores(listaAgentes[i]);
                                    listaAgentes[i].Escondete = 0;
                                }
                                else
                                {
                                    int sigue = -1;
                                    if (ObjetivoConsumido == false)
                                    {
                                        sigue = renuevaRuta(listaAgentes[i]);
                                    }
                                    if (sigue == 0)
                                    {
                                        listaAgentes[i].walk();
                                        Point p_k = listaAgentes[i].getActualPosition();
                                        /** Dibuja el nuevo estado del circulo **/
                                        g.FillEllipse(agentBrush, p_k.X - radioGrl / 2, p_k.Y - radioGrl / 2, radioGrl, radioGrl);

                                    }
                                    else
                                    {
                                        ObjetivoConsumido = true;
                                        //MessageBox.Show("para!!");
                                    }


                                }

                            }

                        }
                    }
                    for (int i = 0; i < listaDepredadores.Count; i++)
                    {
                        Point p_k;
                        listaDepredadores[i].walk();
                        if(listaDepredadores[i].Presa == null)
                        {
                            listaDepredadores[i].Exitado = false;
                        }
                        if (listaDepredadores[i].Exitado == true)
                        {
                            if (listaDepredadores[i].comiPresa() == true)
                            {
                                eliminaAgente(listaAgentes, listaDepredadores[i]);
                                listaDepredadores[i].Presa = null;
                            }
                            if (listaDepredadores[i].canWalk() == false)
                            {
                                Vertex obj;
                                if (listaDepredadores[i].Presa == null)
                                {
                                    listaDepredadores[i].Exitado = false;
                                }
                                else
                                {
                                    obj = getMenorPresa(listaDepredadores[i], listaDepredadores[i].Presa);
                                    renuevaRutaExitado(listaDepredadores[i], obj);
                                    listaDepredadores[i].walk();
                                }
                            }
                            p_k = listaDepredadores[i].getActualPosition();
                            g.FillEllipse(radarBrush, p_k.X - radioRadar / 2, p_k.Y - radioRadar / 2, radioRadar, radioRadar);
                            g.FillEllipse(predatorBrush, p_k.X - radioGrl / 2, p_k.Y - radioGrl / 2, radioGrl, radioGrl);
                        }
                        else
                        {
                            listaDepredadores[i].detectePresa(listaAgentes);
                            //listaDepredadores[i].walk();
                            p_k = listaDepredadores[i].getActualPosition();
                            /** Dibuja el nuevo estado del circulo **/
                            g.FillEllipse(radarBrush, p_k.X - radioRadar / 2, p_k.Y - radioRadar / 2, radioRadar, radioRadar);
                            g.FillEllipse(predatorBrush, p_k.X - radioGrl / 2, p_k.Y - radioGrl / 2, radioGrl, radioGrl);
                            if (listaDepredadores[i].walk() == false)
                            {
                                renuevaRutaDepredador(listaDepredadores[i]);
                            }
                        }
                    }
                    pbxImage.Refresh();
                    g.Clear(Color.Transparent);

                }
                for (int i = 0; i < agentesDijkstra.Count; i++)
                {
                    Vertex aux = graph.getVertexAt(agentesDijkstra[i] - 1);
                    aux.Seleccionado = false;
                }
                agentesDijkstra.Clear();
                for (int i = 0; i < listaAgentes.Count; i++)
                {
                    bool nuevo = true;
                    for (int h = 0; h < agentesDijkstra.Count; h++)
                    {
                        if (agentesDijkstra[h] == listaAgentes[i].DestinationVertexIndex)
                        {
                            nuevo = false;
                        }
                    }
                    if (nuevo)
                        agentesDijkstra.Add(listaAgentes[i].DestinationVertexIndex);
                }
                for (int i = 0; i < noConexos.Count; i++)
                {
                    agentesDijkstra.Add(noConexos[i]);
                }
                listaAgentes.Clear();
            }
            dibujaAgentes();
            dibujaPosiciones(listaDepredadores);
            pbxImage.Refresh();

        }

        public void eliminaAgente(List<Agent> listaAgentes, depredador depredador)
        {
            for (int i = 0; i < listaAgentes.Count; i++)
            {
                if (depredador.Presa.Id == listaAgentes[i].Id)
                {
                    listaAgentes.RemoveAt(i);
                    depredador.Presa = null;
                    depredador.Vel = 2;
                    depredador.Exitado = false;
                    return;
                }
            }
        }

        Vertex getMenorPresa(depredador depredador, Agent presa)
        {

            double aux = 0;
            double menor = 0;
            double distance = 3600;
            bool primera = true;
            Vertex optimo = graph.getVertexAt(depredador.DestinationVertexIndex);
            double objetivo = thetaPresa(optimo.Circle.P_c, presa.getActualPosition());
            Vertex destino = optimo;
            for (int i = 0; i < optimo.EdgesCount; i++)
            {
                aux = theta(optimo, optimo.eL[i].destination);
                double auxdistance = distancia(aux, objetivo);
                if (auxdistance < distance)
                {
                    distance = auxdistance;
                    destino = optimo.eL[i].destination;
                }

            }
            return destino;
        }

        double thetaPresa(Point v_origen, Point v_obj)
        {
            float x_0, y_0;
            float x_k, y_k;
            float theta;
            float d_x, d_y;
            float d_x2, d_y2;
            float h = 30;
            float x_p = v_origen.X;
            float y_p = v_origen.Y;

            float x_f = v_obj.X;
            float y_f = v_obj.Y;
            d_y = y_f - y_p;
            d_x = x_f - x_p;
            theta = (float)Math.Atan2(d_y, d_x); // Tal vez esto se puede sacar mejor con SOR CAR TOA
            double angle = theta * (180 / Math.PI);
            //labelTetha.Text = "Theta = " + angle;
            x_k = x_p + (float)Math.Cos(theta) * h;
            y_k = y_p + (float)Math.Sin(theta) * h;

            x_p = 0;
            y_p = 0;
            x_f = 0;
            return angle;
        }

        public void inicializaRutas(List<Agent> listaDeAgentes, List<int> vertices, Grafo grafoDijsktra, List<Dijkstra> vector, List<int> listaNoConexos)
        {
            int aux = listaDeAgentes.Count;
            int auxremove = 0;
            for (int i = 0; i < aux; i++)
            {
                Vertex v_o;
                Color color2;
                v_o = graph.getVertexAt(vertices[i] - 1);
                drawCircle(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpAnimation, color2 = Color.Transparent);
                drawCircle(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpImage, color2 = Color.MistyRose);
                drawIdentificador(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Id.ToString(), v_o.Circle.R, bmpImage);
                Vertex v_d = vector[v_o.Id - 1].Proveniente;
                if (v_d == null)
                {
                    listaNoConexos.Add(v_o.Id);
                    listaDeAgentes.RemoveAt(auxremove);
                }
                else
                {
                    int k;
                    for (k = 0; k < v_o.EdgesCount; k++)
                    {
                        if (v_o.eL[k].destination.Id == v_d.Id)
                        {
                            break;
                        }
                    }
                    Point[] randomPath = v_o.getPathAt(k);
                    listaDeAgentes[auxremove].Path = randomPath;
                    int originVertexIndex = graph.find(v_o);
                    int destinationVertexIndex = v_d.Id;
                    listaDeAgentes[auxremove].newPath(originVertexIndex, randomPath, destinationVertexIndex);
                    auxremove = auxremove + 1;

                }
            }
            return;
        }

        void inicializaRutasDepredadores(List<depredador> listaDeDepredadores, List<int> vertices)
        {
            int aux = listaDeDepredadores.Count;
            for (int i = 0; i < aux; i++)
            {
                Vertex v_o;
                Color color2;

                v_o = graph.getVertexAt(vertices[i] - 1);
                int randEdgeIndex = rand.Next(0, v_o.eL.Count);
                int auxi = randEdgeIndex;
                //randEdgeIndex = v_o.eL[randEdgeIndex].id;
                Point[] randomPath = v_o.getPathAt(randEdgeIndex);
                listaDeDepredadores[i].Path = randomPath;
                Vertex v_d = v_o.getDestinationAt(auxi);
                int destinationVertexIndex = graph.find(v_d);
                int originVertexIndex = graph.find(v_o);
                listaDeDepredadores[i].newPath(originVertexIndex, randomPath, destinationVertexIndex);
                drawCircle(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpAnimation, color2 = Color.Transparent);
            }
            return;

        }

        void borraDepredadores(List<int> vertices)
        {
            int aux = listaDepredadores.Count();
            for (int i = 0; i < aux; i++)
            {
                Color color2;
                Vertex v_o;
                v_o = graph.getVertexAt(vertices[i] - 1);
                drawCircle(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpImage, color2 = Color.MistyRose);
                drawIdentificador(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Id.ToString(), v_o.Circle.R, bmpImage);
                drawCircle(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpAnimation, color2 = Color.Transparent);

            }
        }



        void inicializaRutaDepredador(List<int> vertices )
        {
            depredador auxiliarDepredador = new depredador();
            int aux = vertices.Count;
            auxiliarDepredador.RadioRadar = radioRadar;
            for (int i = 0; i < aux; i++)
            {
                Vertex v_o;
                Color color2;

                v_o = graph.getVertexAt(vertices[aux-1]-1);
                int randEdgeIndex = rand.Next(0, v_o.eL.Count);
                int auxi = randEdgeIndex;
                //randEdgeIndex = v_o.eL[randEdgeIndex].id;
                Point[] randomPath = v_o.getPathAt(randEdgeIndex);
                auxiliarDepredador.Path = randomPath;
                Vertex v_d = v_o.getDestinationAt(auxi);
                int destinationVertexIndex = graph.find(v_d);
                int originVertexIndex = graph.find(v_o);
                auxiliarDepredador.newPath(originVertexIndex, randomPath, destinationVertexIndex);
                drawCircle(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpImage, color2 = Color.MistyRose);
                drawIdentificador(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Id.ToString(), v_o.Circle.R, bmpImage);
                drawCircle(v_o.Circle.P_c.X, v_o.Circle.P_c.Y, v_o.Circle.R, bmpAnimation, color2 = Color.Transparent);
            }
            listaDepredadores.Add(auxiliarDepredador);
            return;

        }

        public bool puedenCaminar(List<Agent> listaDeAgentes)
        {
            bool sipueden = false;
            for (int i = 0; i < listaDeAgentes.Count; i++)
            {
                if (listaDeAgentes[i].Path == null)
                {
                    MessageBox.Show("acutaliza Objetivo!");
                    return false;
                }
                if (listaDeAgentes[i].canWalk())
                {
                    sipueden = true;
                }
            }
            return sipueden;
        }

        public bool seEsconden(List<Agent> listaDeAgentes)
        {
            bool sipueden = false;
            for (int i = 0; i < listaDeAgentes.Count; i++)
            {
                if (listaDeAgentes[i].SoyPresa == true)
                    return true;
            }
            return sipueden;
        }

        private void btnDeprededar_Click(object sender, EventArgs e)
        {
            depredador = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            depredador = false;
        }

        private void btnReducir_Click(object sender, EventArgs e)
        {

            radioRadar = radioRadar - (float)(radioGrl * 0.25);
            g = Graphics.FromImage(bmpAnimation);
            g.Clear(Color.Transparent);
            if (primeraAnimacion)
            {
                dibujaRadares();
                dibujaDepredadores();
                pbxImage.Refresh();
            }

            dibujaPosiciones(listaDepredadores);
            pbxImage.Refresh();
        }

        private void btnAumentar_Click(object sender, EventArgs e)
        {
            if (primerRadar)
            {
                radioRadar = (float)(radioGrl * 1.25);
                primerRadar = false;
            }
            else
            {
                radioRadar = radioRadar + (float)(radioGrl * 0.25);
            }
            g = Graphics.FromImage(bmpAnimation);
            g.Clear(Color.Transparent);
            if (primeraAnimacion)
            {
                dibujaRadares();
                dibujaDepredadores();
                pbxImage.Refresh();
            }
            dibujaPosiciones(listaDepredadores);
            pbxImage.Refresh();
        }

        public int renuevaRuta(Agent agente)
        {
            Vertex v_o;
            v_o = graph.getVertexAt(agente.DestinationVertexIndex - 1);
            Vertex v_d = vector[v_o.Id - 1].Proveniente;
            int k;
            for (k = 0; k < v_o.EdgesCount; k++)
            {
                if (v_o.eL[k].destination.Id == v_d.Id)
                {
                    break;
                }
            }
            Point[] randomPath = v_o.getPathAt(k);
            if (randomPath == null)
            {
                return -1;
            }
            agente.Path = randomPath;
            int originVertexIndex = graph.find(v_o);
            int destinationVertexIndex = v_d.Id;
            agente.newPath(originVertexIndex, randomPath, destinationVertexIndex);
            return 0;
        }

        void renuevaRutaExitado(depredador predator, Vertex obj)
        {
            Vertex v_o;
            Color color2;

            v_o = graph.getVertexAt(predator.DestinationVertexIndex);
            int i = 0;
            for (i=0; i < v_o.eL.Count; i++)
            {
                if(v_o.eL[i].destination.Id == obj.Id)
                {
                    break;
                }
            }
            //randEdgeIndex = v_o.eL[randEdgeIndex].id;
            Point[] randomPath = v_o.getPathAt(i);
            predator.Path = randomPath;
            Vertex v_d = v_o.getDestinationAt(i);
            int destinationVertexIndex = graph.find(v_d);
            int originVertexIndex = graph.find(v_o);
            predator.newPath(originVertexIndex, randomPath, destinationVertexIndex); ;
        }


        void renuevaRutaDepredador(depredador predator)
        {
            Vertex v_o;
            Color color2;

            v_o = graph.getVertexAt(predator.DestinationVertexIndex);
            int randEdgeIndex = rand.Next(0, v_o.eL.Count);
            int auxi = randEdgeIndex;
            //randEdgeIndex = v_o.eL[randEdgeIndex].id;
            Point[] randomPath = v_o.getPathAt(randEdgeIndex);
            predator.Path = randomPath;
            Vertex v_d = v_o.getDestinationAt(auxi);
            int destinationVertexIndex = graph.find(v_d);
            int originVertexIndex = graph.find(v_o);
            predator.newPath(originVertexIndex, randomPath, destinationVertexIndex);

        }

        void dibujaDijkstra(Vertex v_i, List<Dijkstra> vector, Vertex v_objetivo, Grafo grafo)
        {
            if (v_i.Id == v_objetivo.Id)
            {
                return;
            }
            Vertex destino = vector[v_i.Id - 1].Proveniente;
            if (destino == null)
            {
                return;
            }
            //valido que mi origen no exista ya en el grafo.
            bool yaexistia = false;
            Vertex auxorigen = new Vertex(v_i.Circle, v_i.Id);
            for (int i = 0; i < grafo.Count; i++)
            {
                if (auxorigen.Id == grafo.getVertexAt(i).Id)
                {
                    auxorigen = grafo.getVertexAt(i);
                    yaexistia = true;
                }
            }
            if (!yaexistia)
            {
                grafo.addVertex(auxorigen);
            }
            //fin de validacion de vertice origen
            Vertex auxDestino = new Vertex(destino.Circle, destino.Id);
            //GENERO ARISTAS NUEVAS DE MI NUEVO GRAFO, POR PARAMETRO PARA QUE NO SE REFERENCIEN AL VIEJO GRAFO.
            for (int i = 0; i < v_i.EdgesCount; i++)
            {
                if (v_i.eL[i].destination.Id == destino.Id)
                {
                    auxorigen.addEdge(auxorigen, auxDestino, v_i.eL[i].id, v_i.eL[i].Path, v_i.eL[i].distance);
                    break;
                }
            }
            for (int i = 0; i < destino.EdgesCount; i++)
            {
                if (destino.eL[i].destination.Id == v_i.Id)
                {
                    auxDestino.addEdge(auxDestino, auxorigen, destino.eL[i].id, destino.eL[i].Path, destino.eL[i].distance);
                    break;
                }
            }
            //fIN DE GENERAR ARISTAS, GENERAMOS ARISTAS DE IDA Y VUELTA.
            grafo.addVertex(auxDestino);
            g = Graphics.FromImage(bmpAnimation);
            Pen edgesPen = new Pen(Color.Red);
            g.DrawLine(edgesPen, v_i.Circle.P_c.X, v_i.Circle.P_c.Y, destino.Circle.P_c.X, destino.Circle.P_c.Y);
            v_i = vector[v_i.Id - 1].Proveniente;
            pbxImage.Refresh();
            dibujaDijkstra(v_i, vector, v_objetivo, grafo);
        }

        private void pbxImage_MouseClick(object sender, MouseEventArgs e)
        {
            bool clickderecho = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                clickderecho = true;

            }
            //ajusta tamaños de la picturebox y del bitmap
            float w_b, h_b;
            float w_p, h_p;
            float x_b, y_b, x_v, y_v;
            float r_x, r_y, r;
            float d_x, d_y;
            Vertex v_o = graph.getVertexAt(0);
            if (clickderecho)
            {
                x_p = e.X;
                y_p = e.Y;
                v_dindex = -1;

            }
            else
            {
                x_f = e.X;
                y_f = e.Y;
            }

            w_b = bmpImage.Width;
            h_b = bmpImage.Height;
            w_p = pbxImage.Width;
            h_p = pbxImage.Height;

            r_x = w_p / w_b;
            r_y = h_p / h_b;

            r = r_y;
            if (r_x < r_y)
                r = r_x;

            d_x = (w_p - r * w_b) / 2;
            d_y = (h_p - r * h_b) / 2;

            x_b = (x_p - d_x) / r;
            y_b = (y_p - d_y) / r;
            int v_index = 0;
            if (depredador)
            {
                x_v = (x_f - d_x) / r;
                y_v = (y_f - d_y) / r;
                v_dindex = belongsToAVertex(x_v, y_v, graph);
                if (v_dindex == -1)
                {
                    MessageBox.Show("No seleccionaste un Vertice, intenta de nuevo!");
                    x_f = 0;
                    y_f = 0;
                    x_v = 0;
                    y_v = 0;
                }
                else
                {
                    if (agentesDepredadores.Count == graph.vL.Count - 2 || (agentesDijkstra.Count+agentesDepredadores.Count) == graph.vL.Count-1)
                    {
                        MessageBox.Show("Solo se admiten n-2 agentes Depredadores, este vertice solo puede ser seleccionado como objetivo");
                    }
                    else
                    {
                        v_o = graph.getVertexAt(v_dindex);
                        if (v_o.Seleccionado == true)
                        {
                            MessageBox.Show("Vertice previamente seleccionado, escoge otro!");
                            v_index = -1;
                        }
                        else
                        {
                            g = Graphics.FromImage(bmpImage);
                            Brush agentBrush = new SolidBrush(Color.Red);
                            float ra = (v_o.Circle.R * 2) - 5;
                            g.FillEllipse(agentBrush, v_o.Circle.P_c.X-ra/2, v_o.Circle.P_c.Y-ra/2, ra, ra);
                            radioGrl = ra;
                            pbxImage.Refresh();
                            agentesDepredadores.Add(v_o.Id);
                            if (primeraAnimacion == false)
                            {
                                inicializaRutaDepredador(agentesDepredadores);
                            }
                            //v_o.Seleccionado = true;
                        }
                    }
                }
                return;
            }
            //valido segundo click
            if (clickderecho)
            {
                v_index = belongsToAVertex(x_b, y_b, graph);
                if (v_index == -1)
                {
                    MessageBox.Show("No seleccionaste un Vertice, intenta de nuevo");
                    x_p = 0;
                    y_p = 0;
                    x_b = 0;
                    y_b = 0;
                    clickderecho = false;
                }
                else
                {
                    if (paro != null)
                    {
                        Color color2;
                        drawCircle(paro.Circle.P_c.X, paro.Circle.P_c.Y, paro.Circle.R, bmpImage, color2 = Color.MistyRose);
                        drawIdentificador(paro.Circle.P_c.X, paro.Circle.P_c.Y, paro.Id.ToString(), paro.Circle.R, bmpImage);
                        dibujaAgentes();
                        paro.Seleccionado = false;
                    }
                    paro = graph.getVertexAt(v_index);
                    paro.Seleccionado = true;
                    Color color;
                    drawCircle(paro.Circle.P_c.X, paro.Circle.P_c.Y, paro.Circle.R, bmpImage, color = Color.Yellow);
                    drawIdentificador(paro.Circle.P_c.X, paro.Circle.P_c.Y, paro.Id.ToString(), paro.Circle.R, bmpImage);
                    pbxImage.Refresh();
                }
            }
            else
            {
                x_v = (x_f - d_x) / r;
                y_v = (y_f - d_y) / r;
                v_dindex = belongsToAVertex(x_v, y_v, graph);
                if (v_dindex == -1)
                {
                    MessageBox.Show("No seleccionaste un Vertice, intenta de nuevo!");
                    x_f = 0;
                    y_f = 0;
                    x_v = 0;
                    y_v = 0;
                }
                else
                {
                    if (agentesDijkstra.Count == graph.vL.Count - 1 || (agentesDijkstra.Count + agentesDepredadores.Count) == graph.vL.Count - 1)
                    {
                        MessageBox.Show("Solo se admiten n-1 agentes, este vertice solo puede ser seleccionado como objetivo");
                    }
                    else
                    {
                        v_o = graph.getVertexAt(v_dindex);
                        if (v_o.Seleccionado == true)
                        {
                            MessageBox.Show("Vertice previamente seleccionado, escoge otro!");
                            v_index = -1;
                        }
                        else
                        {
                            g = Graphics.FromImage(bmpImage);
                            Brush agentBrush = new SolidBrush(Color.BlueViolet);
                            float ra = (v_o.Circle.R * 2) - 5;
                            g.FillEllipse(agentBrush, v_o.Circle.P_c.X - ra / 2, v_o.Circle.P_c.Y - ra / 2, ra, ra);
                            radioGrl = ra;
                            pbxImage.Refresh();
                            agentesDijkstra.Add(v_o.Id);
                            v_o.Seleccionado = true;
                        }
                    }
                }
            }
            if (clickderecho)
            {
                if (v_o.EdgesCount != 0)
                {
                    grafoDijkstra.vL.Clear();
                    vector = doDijkstra(paro);
                    //List<int> visitados = new List<int>();
                }
                else
                {
                    MessageBox.Show("Vertice sin aristas! No se puede realizar Dijsktra");
                }
                x_f = 0;
                y_f = 0;
                //limpio encolados y explorar para una segunda vuelta.
                /*
                simulationStart();
                */
                //BFS();
            }
        }






    }
}
