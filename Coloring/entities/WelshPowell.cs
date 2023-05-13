using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coloring.entities
{
    internal class WPList
    {
        public Link link;
        public int weight;
        public int color;
        public int saturation;
        public WPList(Link link, int weight, int color, int saturation = 0) { 
            this.link = link;
            this.weight = weight;
            this.color = color;
            this.saturation = saturation;
        }
    }
    internal class WPMatrix
    {
        public List<Vertex> vertex;
        public int weight;
        public int color;
        public int saturation;
        public WPMatrix(List<Vertex> vertex, int weight, int color, int saturation = 0)
        {
            this.color = color;
            this.vertex = vertex;
            this.weight = weight;
            this.saturation = saturation;
        }
    }
    internal class WelshPowell
    {
        public Boolean isList;
        public List<int> colorList = new List<int>();
        public WelshPowell(bool isList)
        {
            this.isList = isList;
        }
        public void run(MatrixGraph m = null, ListGraph l = null)
        {
            if (this.isList)
            {
                List<WPList> wPList = new List<WPList>();
                foreach(Link link in l.graph) {
                    wPList.Add(new WPList(link, l.getNeighbors(l.getVertexIndex(link.label)).Count, 0));
                }

                wPList.Sort((a, b) => b.weight.CompareTo(a.weight));

                foreach (WPList i in wPList) {
                    List<int> list = this.listColoredNeighbors(i.link, wPList);
                    List<int> available = new List<int>();
                    foreach (int item in this.colorList) { 
                        available.Add(item);
                    }
                    foreach (int color in list) { 
                        if (available.IndexOf(color) != -1) { 
                            available.RemoveAt(available.IndexOf(color));
                        }
                    }
                    int cl = 0;
                    if (available.Count == 0)
                    {
                        cl = this.colorList.Count + 1;
                        this.colorList.Add(cl);
                    }
                    else {
                        cl = available[0];
                    }
                    i.color = cl;
                }
                Console.WriteLine("numero de cores usadas: " + this.colorList.Count);
            }
            else
            {
                List<WPMatrix> wPMatrix = new List<WPMatrix>();
                foreach (List<Vertex> vertex in m.graph)
                {
                    wPMatrix.Add(new WPMatrix(vertex, l.getNeighbors(l.getVertexIndex(vertex[0].labelRow)).Count, 0));
                }

                wPMatrix.Sort((a, b) => b.weight.CompareTo(a.weight));

                foreach (WPMatrix i in wPMatrix)
                {
                    List<int> list = this.matrixColoredNeighbors(i.vertex, wPMatrix);
                    List<int> available = new List<int>();
                    foreach (int item in this.colorList)
                    {
                        available.Add(item);
                    }
                    foreach (int color in list)
                    {
                        if (available.IndexOf(color) != -1)
                        {
                            available.RemoveAt(available.IndexOf(color));
                        }
                    }
                    int cl = 0;
                    if (available.Count == 0)
                    {
                        cl = this.colorList.Count + 1;
                        this.colorList.Add(cl);
                    }
                    else
                    {
                        cl = available[0];
                    }
                    i.color = cl;
                }
                Console.WriteLine("numero de cores usadas: " + this.colorList.Count);
            }
        }

        public List<int> listColoredNeighbors(Link l, List<WPList> lg) {
            List<int> list = new List<int>();
            foreach (ListItem li in l.links) {
                foreach (WPList item in lg) {
                    if (item.link.label == li.label && item.color != 0) {
                        list.Add(item.color);
                        break;
                    }
                }
            }
            return list;
        }

        public List<int> matrixColoredNeighbors(List<Vertex> l, List<WPMatrix> mg)
        {
            List<int> list = new List<int>();
            foreach (Vertex v in l)
            {
                if (v.labelColumn != v.labelRow && v.weight != 0)
                {
                    foreach (WPMatrix item in mg)
                    {
                        if (item.vertex[0].labelRow == v.labelColumn && item.color != 0) {
                            list.Add(item.color);
                            break;
                        }
                    }
                }
            }
            return list;
        }
    }
}
