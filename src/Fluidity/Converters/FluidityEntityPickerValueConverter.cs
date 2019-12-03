// <copyright file="FluidityEntityPickerValueConverter.cs" company="Matt Brailsford">
// Copyright (c) 2019 Matt Brailsford and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.Linq;
using Fluidity.Configuration;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Fluidity.Converters
{
    public class FluidityEntityPickerValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(IPublishedPropertyType propertyType)
        {
            return propertyType.EditorAlias.InvariantEquals("Fluidity.EntityPicker");
        }
		
        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType,
                                                           PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
		{
			try
			{
				if (owner == null || owner.ToString().IsNullOrWhiteSpace())
					return null;

				var ids = owner.ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
				if (ids.Length == 0)
					return null;

				var preValues = (ValueListConfiguration)propertyType.DataType.Configuration;

				ValueListConfiguration.ValueListItem item = preValues?.Items?.First(x => x.Value == "collection");

				if (preValues == null || item == null)
					throw new ApplicationException($"Fluidity DataType {propertyType.DataType.Id} has no 'collection' pre value.");

				var collectionParts = item.Value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
				if (collectionParts.Length < 2)
					throw new ApplicationException($"Fluidity DataType {propertyType.DataType.Id} has an invalid 'collection' pre value.");

				var section = FluidityContext.Current.Config.Sections[collectionParts[0]];
				if (section == null)
					throw new ApplicationException($"Fluidity DataType {propertyType.DataType.Id} has an invalid 'collection' pre value. No section found with the alias {collectionParts[0]}");

				var collection = section.Tree.FlattenedTreeItems[collectionParts[1]] as FluidityCollectionConfig;
				if (collection == null)
					throw new ApplicationException($"Fluidity DataType {propertyType.DataType.Id} has an invalid 'collection' pre value. No collection found with the alias {collectionParts[1]}");

				return FluidityContext.Current.Services.EntityService.GetEntitiesByIds(section, collection, ids);

			}
			catch (Exception e)
			{
				Current.Logger.Error<FluidityEntityPickerValueConverter>("Error converting value", e);
			}

			return null;
		}
	}
}
