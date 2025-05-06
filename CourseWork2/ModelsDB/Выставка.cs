using System;
using System.Collections.Generic;

namespace CourseWork2.ModelsDB;

public partial class Выставка
{
    public int КодВыставки { get; set; }

    public string Тематика { get; set; } = null!;

    public DateTime ДатаНачала { get; set; }

    public DateTime ДатаОкончания { get; set; }

    public int КоличествоЭкспонатов { get; set; }

    public string Посещаемость { get; set; } = null!;

    public virtual ICollection<Экскурсии> Экскурсииs { get; set; } = new List<Экскурсии>();

    public virtual ICollection<Экспонат> Экспонатs { get; set; } = new List<Экспонат>();
}
