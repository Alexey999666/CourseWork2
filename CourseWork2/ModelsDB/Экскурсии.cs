using System;
using System.Collections.Generic;

namespace CourseWork2.ModelsDB;

public partial class Экскурсии
{
    public int Idэкскурсии { get; set; }

    public int Idсотрудника { get; set; }

    public int КодВыставки { get; set; }

    public int КоличествоЛюдей { get; set; }

    public TimeOnly Продолжительность { get; set; }

    public decimal Стоимость { get; set; }

    public virtual Сотрудники IdсотрудникаNavigation { get; set; } = null!;

    public virtual Выставка КодВыставкиNavigation { get; set; } = null!;
}
