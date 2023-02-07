﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linql.Core
{
    public class LinqlProperty : LinqlExpression
    {
        public string PropertyName { get; set; }


        public LinqlProperty() { }

        public LinqlProperty(string PropertyName) 
        {
            this.PropertyName = PropertyName;
        }

        public override string ToString()
        {
            return this.PropertyName;
        }
    }
}
