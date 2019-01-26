
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckYourselfException : 
    Exception
{

    public FuckYourselfException()
    {
    }

    public FuckYourselfException(string message)
        : base(message)
    {
    }

    public FuckYourselfException(string message, Exception inner)
        : base(message, inner)
    {
    }

}
