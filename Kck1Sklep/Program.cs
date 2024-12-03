using System;
using System.Collections.Generic;
using System.Linq;
using Kck1Sklep.Models;
using Kck1Sklep.Views;
using Kck1Sklep.Controllers;

class Program
{
    static void Main()
    {
        View view = new View();
        view.ShowEpilepsyWarning();
        Controller controller = new Controller();
        controller.Run();
    }
}