// <copyright file="UmbracoPublishedPropertyTypeHelper.cs" company="Matt Brailsford">
// Copyright (c) 2019 Matt Brailsford and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Fluidity.Helpers
{
    internal static class UmbracoPublishedPropertyTypeHelper
    {
        public static ValueListConfiguration GetPreValues(this PublishedPropertyType propType)
        {
            return Current
                   .AppCaches
                   .RequestCache
                   .GetCacheItem($"UmbracoPublishedPropertyTypeHelper.GetPreValues_{propType.DataType.Id}", 
                                 () =>
				(ValueListConfiguration)Current.Services.DataTypeService.GetDataType(propType.DataType.Id).Configuration);
        } 
    }
}
