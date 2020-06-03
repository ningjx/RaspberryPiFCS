using RaspberryPiFCS.Equipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Main
{
    public static class Connection
    {
        static Connection()
        {
            EquipmentBus.E34_2G4D20D = new E34_2G4D20D("");
            EquipmentBus.E34_2G4D20D.Lunch();
        }
    }
}
