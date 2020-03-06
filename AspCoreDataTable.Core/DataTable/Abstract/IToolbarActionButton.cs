﻿using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface IToolbarActionButton<T>
        where T : IActionButton<T>
    {
        T FormSide(EnumFormSide formSide);
    }
}
