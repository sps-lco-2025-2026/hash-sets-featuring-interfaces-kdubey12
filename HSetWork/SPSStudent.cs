using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace HSetWork;

public enum SchoolYear { Year9, Year10, Year11, Year12, Year13 }

public class SPSStudent
{
    private readonly string? _name;
    private readonly SchoolYear _year;
    private readonly string? _tutor;
    public SPSStudent(string name, SchoolYear year, string tutor)
    {
        _name = name;
        _year = year;
        _tutor = tutor;
    }

    public override string ToString() => $"{_name} | Y{_year} | {_tutor}";

    public override int GetHashCode() => ToString().GetHashCode();
    public override bool Equals(object? obj)
    {
        if (obj is not SPSStudent) return false;
        SPSStudent other = (SPSStudent)obj;
        return _name == other._name && _year == other._year && _tutor == other._tutor;
    }
}
