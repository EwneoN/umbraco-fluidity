// <copyright file="DataTypeInfo.cs" company="Matt Brailsford">
// Copyright (c) 2019 Matt Brailsford and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;

namespace Fluidity.Models
{
    internal class DataTypeInfo
    {
        public IDataType DataType { get; }

        public IDataEditor PropertyEditor { get; }

        public ValueListConfiguration PreValues { get; }

        public DataTypeInfo(IDataType dataType, IDataEditor propertyEditor, ValueListConfiguration preValues)
        {
            DataType = dataType;
            PropertyEditor = propertyEditor;
            PreValues = preValues;
        }
    }
}