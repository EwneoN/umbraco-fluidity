// <copyright file="UmbracoDataTypeHelper.cs" company="Matt Brailsford">
// Copyright (c) 2019 Matt Brailsford and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using Fluidity.Configuration;
using Fluidity.Extensions;
using Fluidity.Models;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace Fluidity.Helpers
{
    internal class UmbracoDataTypeHelper
    {
        private readonly IDataTypeService _dataTypeService;
        private readonly IAppCache _appCache;

        internal UmbracoDataTypeHelper(IDataTypeService dataTypeService, IAppCache appCache)
        {
	        _dataTypeService = dataTypeService;
	        _appCache = appCache;
        }

        internal UmbracoDataTypeHelper()
            : this(Current.Services.DataTypeService, Current.AppCaches.RuntimeCache)
        { }

        internal DataTypeInfo ResolveDataType(FluidityEditorFieldConfig fieldConfig, bool isReadOnly = false)
        {
            var dtdKey = !fieldConfig.DataTypeName.IsNullOrWhiteSpace()
                ? fieldConfig.DataTypeName
                : fieldConfig.GetOrCalculateDefinititionId().ToString();
            dtdKey += $"_{isReadOnly}";

            return _appCache.GetCacheItem($"fluidity_datatypeinfo_{dtdKey}", () =>
            {
                IDataType dataType = null;

                if (!fieldConfig.DataTypeName.IsNullOrWhiteSpace())
                {
                    dataType = _dataTypeService.GetDataType(fieldConfig.DataTypeName);
                }

                if (dataType == null)
                {
                    var dataTypeId = fieldConfig.DataTypeId == 0 && isReadOnly
                        ? -92 // If readonly and no explicit datatype defined, default to label
                        : fieldConfig.GetOrCalculateDefinititionId();
                    dataType = _dataTypeService.GetDataType(dataTypeId);
                }

                var preValues = (ValueListConfiguration)dataType.Configuration;

                return new DataTypeInfo(dataType, dataType.Editor, preValues);
            });
        }
    }
}
