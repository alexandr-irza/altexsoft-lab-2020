﻿using System;

namespace RecipeBook2.Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException()
        {

        }

        public BaseException(string message) : base(message)
        {

        }
    }
}
