using System;
using System.Collections.Generic;

namespace CourseWork2.ModelsDB;

public partial class SuppliesPurchasesWithHighWorkExperience
{
    public int КодПоставки { get; set; }

    public int ОтветственноеЛицо { get; set; }

    public string СпособПолучения { get; set; } = null!;

    public DateTime ДатаПоставки { get; set; }
}
