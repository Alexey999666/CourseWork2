using System;
using System.Collections.Generic;

namespace CourseWork2.ModelsDB;

public partial class Экспонат
{
    public int ИнвентарныйНомер { get; set; }

    public string НазваниеЭкспоната { get; set; } = null!;

    public int КодВыставки { get; set; }

    public int КодПоставки { get; set; }

    public decimal Стоимость { get; set; }

    public string Подлиность { get; set; } = null!;

    public virtual Выставка КодВыставкиNavigation { get; set; } = null!;

    public virtual Поставки КодПоставкиNavigation { get; set; } = null!;
}
