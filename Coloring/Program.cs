
using Coloring.entities;

String filePath = Directory.GetCurrentDirectory().Split("\\bin")[0] + "\\slides.txt";
Boolean end = false;
Boolean reboot = false;

ListGraph lGp = new ListGraph(false, false);
MatrixGraph mGp = new MatrixGraph(false, false);

Console.Clear();
Console.Write("Você deseja criar um grafo de lista(sim/não)? ");
Boolean listGraph = Console.ReadLine().Trim().ToLower() == "sim";

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
                    Console.WriteLine(lGp.isPlane().ToString());
                }
                else {
                    Console.WriteLine(mGp.isPlane().ToString());
                }
                break;
            case "2":
                if (listGraph == true)
                {
                    Console.WriteLine(lGp.Euler().ToString());
                }
                else
                {
                    Console.WriteLine(mGp.Euler().ToString());
                }
                break;
            case "3":
                new WelshPowell(listGraph).run(mGp,lGp);
                break;
            case "4":
                new DSATUR(listGraph).run(mGp, lGp);
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