using System;
using System.Collections.Generic;
using league_of_legends_app.CORE.Interfaces;

namespace league_of_legends_app.Utils;

public class WindowAdapter : IWindowAdapter
{
    public Dictionary<string, Action> Commands { get; set; }
    
    
}