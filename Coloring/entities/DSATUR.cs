namespace Coloring.entities
{
    internal class DSATUR
    {
        Boolean isList;
        public List<int> colorList = new List<int>();
        public DSATUR(bool isList)
        {
            this.isList = isList;
        }

        public void run(MatrixGraph m = null, ListGraph l = null) {
            if (this.isList)
            {
                List<WPList> wPList = new List<WPList>();
                foreach (Link link in l.graph)
                {
                    wPList.Add(new WPList(link, l.getNeighbors(l.getVertexIndex(link.label)).Count, 0));
                }

                wPList.Sort((a, b) => b.weight.CompareTo(a.weight));

                do {
                    WPList i = null;

                    foreach (WPList i2 in wPList)
                    {
                        if (i2.color != 0) {
                            continue;
                        }
                        if (i == null) {
                            i = i2;
                            continue;
                        }
                        if (i2.saturation > i.saturation) {
                            i = i2;
                        }
                    }
                    if (i == null) break;
                    
                    List<int> list = this.listColoredNeighbors(i.link, wPList);
                    List<int> available = new List<int>();
                    foreach (String lb in l.getNeighbors(l.getVertexIndex(i.link.label))) {
                        foreach (WPList item in wPList)
                        {
                            if (lb == item.link.label) {
                                item.saturation += 1;
                            }
                        }
                    }
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
                } while (true);
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

                do
                {
                    WPMatrix i = null;

                    foreach (WPMatrix i2 in wPMatrix)
                    {
                        if (i2.color != 0)
                        {
                            continue;
                        }
                        if (i == null)
                        {
                            i = i2;
                            continue;
                        }
                        if (i2.saturation > i.saturation)
                        {
                            i = i2;
                        }
                    }
                    if (i == null) break;
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
                } while (true);
                Console.WriteLine("numero de cores usadas: " + this.colorList.Count);
            }
        }

        public List<int> listColoredNeighbors(Link l, List<WPList> lg)
        {
            List<int> list = new List<int>();
            foreach (ListItem li in l.links)
            {
                foreach (WPList item in lg)
                {
                    if (item.link.label == li.label && item.color != 0)
                    {
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
                        if (item.vertex[0].labelRow == v.labelColumn && item.color != 0)
                        {
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
