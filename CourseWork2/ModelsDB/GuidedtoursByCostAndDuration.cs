using System;
using System.Collections.Generic;

namespace CourseWork2.ModelsDB;

public partial class GuidedtoursByCostAndDuration
{
    public int Idэкскурсии { get; set; }

    public int Idсотрудника { get; set; }

    public int КодВыставки { get; set; }

    public int КоличествоЛюдей { get; set; }

    public int Продолжительность { get; set; }

    public decimal Стоимость { get; set; }
}
