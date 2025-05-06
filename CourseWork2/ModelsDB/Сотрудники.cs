using System;
using System.Collections.Generic;

namespace CourseWork2.ModelsDB;

public partial class Сотрудники
{
    public int Id { get; set; }

    public string Фамилия { get; set; } = null!;

    public string Имя { get; set; } = null!;

    public string? Отчество { get; set; }

    public string Должность { get; set; } = null!;

    public int СтажРаботы { get; set; }

    public virtual ICollection<Поставки> Поставкиs { get; set; } = new List<Поставки>();

    public virtual ICollection<Экскурсии> Экскурсииs { get; set; } = new List<Экскурсии>();
}
