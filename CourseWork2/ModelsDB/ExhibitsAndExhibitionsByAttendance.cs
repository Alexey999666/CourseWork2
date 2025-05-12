using System;
using System.Collections.Generic;

namespace CourseWork2.ModelsDB;

public partial class ExhibitsAndExhibitionsByAttendance
{
    public string Тематика { get; set; } = null!;

    public string НазваниеЭкспоната { get; set; } = null!;

    public DateTime ДатаНачала { get; set; }

    public DateTime ДатаОкончания { get; set; }
}
