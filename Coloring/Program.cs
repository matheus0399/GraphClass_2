
using Coloring.entities;

String filePath = Directory.GetCurrentDirectory().Split("\\bin")[0] + "\\slides.txt";
Boolean end = false;
Boolean reboot = false;

ListGraph lGp = new ListGraph(false, false);
MatrixGraph mGp = new MatrixGraph(false, false);

Console.Clear();
Console.Write("Você deseja criar um grafo de lista(sim/não)? ");
Boolean listGraph = Console.ReadLine().Trim().ToLower() == "sim";
DateTime time = DateTime.Now;
do
{
    try
    {
        if (reboot == true)
        {
            lGp = new ListGraph(false, false);
            mGp = new MatrixGraph(false, false);
            Console.Write("Você deseja criar um grafo de lista(sim/não)? ");
            listGraph = Console.ReadLine().Trim().ToLower() == "sim";
            reboot = false;
        }
        if (listGraph == true)
        {
            lGp = lGp.readFile(filePath);
            //lGp.showGraph();
        }
        else
        {
            mGp = mGp.readFile(filePath);
            //mGp.showGraph();
        }
        Console.Write(
             "\n 1 - Planaridade"
            + "\n 2 - Euler"
            + "\n 3 - Welsh Powell"
            + "\n 4 - DSATUR"
            + "\n 5 - Reboot"
            + "\nQual o tipo de ação você deseja executar: "
        );
        
        String input0 = Console.ReadLine().Trim().ToLower();

        switch (input0)
        {
            case "1":
                if (listGraph == true){
                    time = DateTime.Now;
                    Console.WriteLine("Planaridade " + lGp.isPlane().ToString());
                    Console.WriteLine("tempo " + (DateTime.Now - time).TotalMilliseconds + " ms.");
                }
                else {
                    time = DateTime.Now;
                    Console.WriteLine("Planaridade " + mGp.isPlane().ToString());
                    Console.WriteLine("tempo " + (DateTime.Now - time).TotalMilliseconds + " ms.");
                }
                break;
            case "2":
                if (listGraph == true)
                {
                    time = DateTime.Now;
                    Console.WriteLine("Planaridade " + lGp.Euler().ToString());
                    Console.WriteLine("tempo " + (DateTime.Now - time).TotalMilliseconds + " ms.");
                }
                else
                {
                    time = DateTime.Now;
                    Console.WriteLine("Planaridade " + mGp.Euler().ToString());
                    Console.WriteLine("tempo " + (DateTime.Now - time).TotalMilliseconds + " ms.");
                }
                break;
            case "3":
                time = DateTime.Now;
                new WelshPowell(listGraph).run(mGp,lGp);
                Console.WriteLine("tempo " + (DateTime.Now - time).TotalMilliseconds + " ms.");
                break;
            case "4":
                time = DateTime.Now;
                new DSATUR(listGraph).run(mGp, lGp);
                Console.WriteLine("tempo " + (DateTime.Now - time).TotalMilliseconds + " ms.");
                break;
            case "5":
                Console.Clear();
                reboot = true;
                break;
        }
        Console.WriteLine("");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine("\n INPUT ERROR \n");
    }
} while (end == false);