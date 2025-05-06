using System;
using System.Collections.Generic;

namespace CourseWork2.ModelsDB;

public partial class Поставки
{
    public int КодПоставки { get; set; }

    public int ОтветственноеЛицо { get; set; }

    public string СпособПолучения { get; set; } = null!;

    public DateTime ДатаПоставки { get; set; }

    public virtual Сотрудники ОтветственноеЛицоNavigation { get; set; } = null!;

    public virtual ICollection<Экспонат> Экспонатs { get; set; } = new List<Экспонат>();
}
