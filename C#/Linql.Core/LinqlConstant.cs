﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linql.Core
{
    public class LinqlConstant : LinqlExpression
    {
        public LinqlType ConstantType { get; set; }

        public object Value { get; set; }

        public LinqlConstant() { }

        public LinqlConstant(LinqlType ConstantType, object Value) 
        {
            this.ConstantType = ConstantType;
            this.Value = Value;
        }

        public override string ToString()
        {
            return $"LinqlConstant {this.ConstantType.ToString()}";
        }
    }
}
