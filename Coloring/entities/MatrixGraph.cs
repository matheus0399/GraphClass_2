namespace Coloring.entities
{
    internal class MatrixGraph
    {
        public Boolean directed;
        public Boolean pondered;
        public List<List<Vertex>> graph = new List<List<Vertex>>();

        public MatrixGraph(Boolean directed, Boolean pondered)
        {
            this.directed = directed;
            this.pondered = pondered;
            this.graph = new List<List<Vertex>>();
        }

        public Boolean addVertex(String label)
        {
            if (this.hasVertex(label) == false)
            {
                Vertex v = new Vertex(label, label);
                List<Vertex> vList = new List<Vertex>();

                foreach (List<Vertex> vl in this.graph)
                {
                    String lb = vl[0].labelRow;
                    vl.Add(new Vertex(v.labelRow, lb));
                    vList.Add(new Vertex(lb, v.labelRow));
                }
                vList.Add(v);
                this.graph.Add(vList);
                return true;
            }
            else
            {
                Console.WriteLine("Label already exists");
                return false;
            }
        }

        public int getVertexIndex(String label)
        {
            for (int i = 0; i < this.graph.Count(); i++)
            {
                if (this.graph[i][0].labelRow == label)
                    return i;
            }
            return -1;
        }

        public Boolean removeVertex(string label)
        {
            if (this.hasVertex(label) == true)
            {
                var index = -1;
                for (int i = 0; i < this.graph.Count; i++)
                {
                    if (this.graph[i][0].labelRow == label)
                    {
                        index = i;
                        break;
                    }
                }
                this.graph.Remove(this.graph[index]);
                for (int i = 0; i < this.graph.Count; i++)
                {
                    this.graph[i].Remove(this.graph[i][index]);
                }
                return true;
            }
            else
            {
                Console.WriteLine("Vertex does not exist");
                return false;
            }
        }

        public String labelVertex(int index)
        {
            if (this.graph.Count - 1 >= index && index > 0)
            {
                return this.graph[index][0].labelRow;
            }
            return "Not Found";
        }

        public Boolean hasVertex(String label)
        {
            foreach (List<Vertex> l in this.graph)
            {
                if (l[0].labelRow == label)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean addLink(String origin, String destination, double weight = 1)
        {
            if (origin == destination)
            {
                Console.WriteLine("You cannot create a link to the same vertex");
                return false;
            }
            if (this.pondered == false)
            {
                weight = 1;
            }
            else
            {
                if (weight <= 0)
                {
                    Console.WriteLine("For pondered graphs we need the weight of the vertex");
                    return false;
                }
            }
            if (this.hasVertex(origin) == false || this.hasVertex(destination) == false)
            {
                Console.WriteLine("One of the vertices on your input does not exist");
                return false;
            }
            int index0 = -1;
            int index1 = -1;

            for (int i = 0; i < this.graph.Count; i++)
            {
                if (index0 != -1 && index1 != -1) break;
                if (this.graph[i][0].labelRow == origin)
                {
                    index0 = i;
                    continue;
                }
                if (this.graph[i][0].labelRow == destination)
                {
                    index1 = i;
                }
            }

            if (this.linkExists(index0, index1) == false)
            {
                this.addLinkProcess(origin, destination, weight);
                return true;
            }
            else
            {
                Console.WriteLine("Link Already exists");
                return false;
            }
        }

        public Boolean removeLink(int origin, int destination)
        {
            if (origin == destination)
            {
                Console.WriteLine("Vertices do not have links to themselves");
                return false;
            }
            if (this.linkExists(origin, destination) == true)
            {
                this.graph[origin][destination].weight = 0;
                if (this.directed == false)
                {
                    this.graph[destination][origin].weight = 0;
                }
                return true;
            }
            else
            {
                Console.WriteLine("Link does not exist");
                return false;
            }
        }

        public Boolean linkExists(int origin, int destination)
        {
            if (origin == destination) return false;

            if (this.graph[origin][destination].weight > 0)
            {
                return true;
            }

            return false;
        }

        public void addLinkProcess(String origin, String destination, double weight)
        {
            int indexColumn = -1;
            int indexRow = -1;
            for (int i = 0; i < this.graph.Count; i++)
            {
                if (indexColumn != -1 && indexRow != -1)
                {
                    break;
                }
                if (this.graph[i][0].labelRow == origin)
                {
                    indexRow = i;
                }
                if (this.graph[i][0].labelRow == destination)
                {
                    indexColumn = i;
                }
            }
            this.graph[indexRow][indexColumn].weight = weight;
            if (this.directed == false)
            {
                this.graph[indexColumn][indexRow].weight = weight;
            }
        }

        public double linkWeight(int origin, int destination)
        {
            if (origin == destination) return 0;

            if (this.graph.Count - 1 >= origin && this.graph[origin].Count - 1 >= destination)
            {
                if (this.graph[origin][destination].weight > 0)
                {
                    return this.graph[origin][destination].weight;
                }
            }
            return 0;
        }

        public List<String> getNeighbors(int vertex)
        {
            List<String> retorno = new List<String>();
            if (this.graph.Count - 1 >= vertex)
            {
                foreach (Vertex v in this.graph[vertex])
                {
                    if (v.weight > 0)
                    {
                        retorno.Add(v.labelColumn);
                    }
                }
            }
            else
            {
                Console.WriteLine("Vertex does not exist");
            }
            return retorno;
        }

        public void showGraph()
        {
            Console.WriteLine("\nGrafo Array");
            for (int i = 0; i < this.graph.Count; i++)
            {
                Console.WriteLine(i + "= " + this.graph[i][0].labelRow);
            }
            Console.WriteLine("");
            for (int i = 0; i < this.graph.Count; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < this.graph.Count; j++)
                {
                    Console.Write(this.graph[i][j].weight + ", ");
                }
                Console.Write("\n");
            }
        }

        public MatrixGraph readFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            string[] line_one = lines[0].Split(' ');
            Boolean directed = false;
            Boolean pondered = false;
            if (line_one[2] == "1")
            {
                directed = true;
            }
            if (line_one[3] == "1")
            {
                pondered = true;
            }
            int amountOfRows = int.Parse(line_one[0]);

            MatrixGraph ret = new MatrixGraph(directed, pondered);
            for (int i = 0; i < amountOfRows; i++)
            {
                ret.addVertex(i.ToString());
            }
            for (int i = 1; i < lines.Count(); i++)
            {
                string[] info = lines[i].Split(' ');
                
                if (info[2][0] == '.')
                {
                    ret.addLink(info[0], info[1], Double.Parse("0," + info[2].Split('.')[1]));
                }
                else
                {
                    ret.addLink(info[0], info[1], Double.Parse(info[2]));
                }
            }
            return ret;
        }
        public Boolean hasCiclesOfThree()
        {
            foreach (List<Vertex> vertex in this.graph)
            {
                List<List<Vertex>> l1 = new List<List<Vertex>>();
                foreach (String item in this.getNeighbors(this.getVertexIndex(vertex[0].labelRow)))
                {
                    l1.Add(this.graph[this.getVertexIndex(item)]);
                }
                foreach (List<Vertex> neighbor in l1)
                {
                    List<List<Vertex>> l2 = new List<List<Vertex>>();
                    foreach (String item in this.getNeighbors(this.getVertexIndex(neighbor[0].labelRow)))
                    {
                        l2.Add(this.graph[this.getVertexIndex(item)]);
                    }
                    foreach (List<Vertex> neighbor2 in l2)
                    {
                        if (this.getNeighbors(this.getVertexIndex(vertex[0].labelRow)).IndexOf(neighbor2[0].labelRow) != -1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public Boolean isPlane()
        {
            if (this.graph.Count <= 2)
            {
                Console.WriteLine("graph has less then three vertexes");
                return true;
            }

            int c = 2;
            int a = 4;

            if (this.hasCiclesOfThree() == true)
            {
                Console.WriteLine("has cicles of three");
                c = 3;
                a = 6;
            }
            int count = 0;

            foreach (List<Vertex> vertex in this.graph)
            {
                count += this.getNeighbors(this.getVertexIndex(vertex[0].labelRow)).Count;
            }

            count /= 2;

            return count <= ((c * this.graph.Count) - a);
        }

        public Boolean Euler()
        {
            if (this.isPlane() == false)
            {
                Console.WriteLine("cannot do euler on a not plane graph");
                return false;
            }

            if (this.graph.Count <= 2)
            {
                return true;
            }

            int count = 0;

            foreach (List<Vertex> vertex in this.graph)
            {
                count += this.getNeighbors(this.getVertexIndex(vertex[0].labelRow)).Count;
            }

            count /= 2;

            int f = 2 - this.graph.Count + count;

            return f <= (2 * count) / 3;
        }
    }
}