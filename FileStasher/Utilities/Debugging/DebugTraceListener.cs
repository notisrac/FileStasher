﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// source: https://spin.atomicobject.com/2013/12/11/wpf-data-binding-debug/

namespace FileStasher.Utilities.Debugging
{
    public class DebugTraceListener : TraceListener
    {
        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {
            //Debugger.Break();
        }
    }
}
