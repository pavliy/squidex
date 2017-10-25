﻿// ==========================================================================
//  BooleanField.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

namespace Squidex.Domain.Apps.Core.Schemas
{
    public sealed class BooleanField : Field<BooleanFieldProperties>
    {
        public BooleanField(long id, string name, Partitioning partitioning)
            : base(id, name, partitioning, new BooleanFieldProperties())
        {
        }

        public BooleanField(long id, string name, Partitioning partitioning, BooleanFieldProperties properties)
            : base(id, name, partitioning, properties)
        {
        }

        public override T Accept<T>(IFieldVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
