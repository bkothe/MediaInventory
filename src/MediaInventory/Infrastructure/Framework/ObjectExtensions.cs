﻿using System;

namespace MediaInventory.Infrastructure.Framework
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object value)
        {
            return value == null || value == DBNull.Value;
        }

        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }
    }
}