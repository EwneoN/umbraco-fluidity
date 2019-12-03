// <copyright file="FluidityEntityPostModel.cs" company="Matt Brailsford">
// Copyright (c) 2019 Matt Brailsford and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Collections.Generic;
using System.Runtime.Serialization;
using Umbraco.Core.Models.Editors;
using Umbraco.Web.Models.ContentEditing;

namespace Fluidity.Web.Models
{
    [DataContract(Name = "entity", Namespace = "")]
    public class FluidityEntityPostModel : FluidityEntityBasicModel, IHaveUploadedFiles
    {
        public FluidityEntityPostModel()
        {
            Properties = new List<ContentPropertyBasic>();
            UploadedFiles = new List<ContentPropertyFile>();
        }

        [DataMember(Name = "section")]
        public string Section { get; set; }

        [DataMember(Name = "properties")]
        public IEnumerable<ContentPropertyBasic> Properties { get; set; }

        [IgnoreDataMember]
        public List<ContentPropertyFile> UploadedFiles { get; private set; }
    }
}
