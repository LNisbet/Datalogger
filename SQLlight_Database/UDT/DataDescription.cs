﻿namespace SQLight_Database
{
    internal class DataDescription(List<string> columnNames, List<string?> values)
    {
        internal List<string> ColumnNames { get; set; } = columnNames;
        internal List<string?> Values { get; set; } = values;
    }
}
