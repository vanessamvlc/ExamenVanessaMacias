using System;
using System.Collections.Generic;

namespace Examen.Models;

public partial class CatModel
{
    public int IdModel { get; set; }

    public int? IdBrand { get; set; }

    public string? Namee { get; set; }

    public decimal? AveragePrice { get; set; }
}
