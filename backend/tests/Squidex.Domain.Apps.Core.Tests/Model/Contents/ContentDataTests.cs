﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschränkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Domain.Apps.Core.Contents;
using Squidex.Infrastructure.Json.Objects;
using Xunit;

namespace Squidex.Domain.Apps.Core.Model.Contents
{
    public class ContentDataTests
    {
        [Fact]
        public void Should_remove_null_values_from_name_model_when_cleaning()
        {
            var input =
                new ContentData()
                    .AddField("field1", null)
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("en", 2)
                            .AddValue("it", JsonValue.Null));

            var actual = input.ToCleaned();

            var expected =
                new ContentData()
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("en", 2));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_return_same_content_if_merging_same_references()
        {
            var source =
                new ContentData()
                    .AddField("field1",
                        new ContentFieldData()
                            .AddValue("iv", 1))
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("de", 2));

            var actual = source.MergeInto(source);

            Assert.Same(source, actual);
        }

        [Fact]
        public void Should_merge_two_name_models()
        {
            var lhs =
                new ContentData()
                    .AddField("field1",
                        new ContentFieldData()
                            .AddValue("iv", 1))
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("de", 2)
                            .AddValue("it", 2));

            var rhs =
                new ContentData()
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("it", 3)
                            .AddValue("en", 3))
                    .AddField("field3",
                        new ContentFieldData()
                            .AddValue("iv", 4));

            var expected =
                new ContentData()
                    .AddField("field1",
                        new ContentFieldData()
                            .AddValue("iv", 1))
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("it", 2)
                            .AddValue("de", 2)
                            .AddValue("en", 3))
                    .AddField("field3",
                        new ContentFieldData()
                            .AddValue("iv", 4));

            var actual = lhs.MergeInto(rhs);

            Assert.Equal(expected, actual);
            Assert.NotSame(expected, rhs);
            Assert.NotSame(expected, lhs);
        }

        [Fact]
        public void Should_be_equal_when_data_have_same_structure()
        {
            var lhs =
                new ContentData()
                    .AddField("field1",
                        new ContentFieldData()
                            .AddValue("iv", 2))
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("iv", 2));

            var rhs =
                new ContentData()
                    .AddField("field1",
                        new ContentFieldData()
                            .AddValue("iv", 2))
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("iv", 2));

            Assert.True(lhs.Equals(rhs));
            Assert.True(lhs.Equals((object)rhs));
            Assert.Equal(lhs.GetHashCode(), rhs.GetHashCode());
        }

        [Fact]
        public void Should_not_be_equal_when_data_have_not_same_structure()
        {
            var lhs =
                new ContentData()
                    .AddField("field1",
                        new ContentFieldData()
                            .AddValue("iv", 2))
                    .AddField("field2",
                        new ContentFieldData()
                            .AddValue("iv", 2));

            var rhs =
                new ContentData()
                    .AddField("field1",
                        new ContentFieldData()
                            .AddValue("en", 2))
                    .AddField("field3",
                        new ContentFieldData()
                            .AddValue("iv", 2));

            Assert.False(lhs.Equals(rhs));
            Assert.False(lhs.Equals((object)rhs));
            Assert.NotEqual(lhs.GetHashCode(), rhs.GetHashCode());
        }

        [Fact]
        public void Should_be_equal_fields_when_they_have_same_value()
        {
            var lhs =
                new ContentFieldData()
                    .AddValue("iv", 2);

            var rhs =
                new ContentFieldData()
                    .AddValue("iv", 2);

            Assert.True(lhs.Equals(rhs));
            Assert.True(lhs.Equals((object)rhs));
            Assert.Equal(lhs.GetHashCode(), rhs.GetHashCode());
        }

        [Fact]
        public void Should_clone_named_value_and_also_children()
        {
            var source = new ContentData
            {
                ["field1"] = new ContentFieldData(),
                ["field2"] = new ContentFieldData()
            };

            var clone = source.Clone();

            Assert.NotSame(source, clone);

            foreach (var (key, value) in clone)
            {
                Assert.NotSame(value, source[key]);
            }
        }
    }
}
