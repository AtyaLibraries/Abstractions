// <copyright file="IAuditable.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>

namespace Atya.Foundation.Abstractions;

/// <summary>
/// Represents neutral audit information for create and update operations.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Gets or sets the UTC timestamp when the object was created.
    /// </summary>
    public DateTimeOffset CreatedAtUtc
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the actor who created the object.
    /// </summary>
    public string? CreatedBy
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the UTC timestamp of the last modification.
    /// </summary>
    public DateTimeOffset? LastModifiedAtUtc
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the actor who last modified the object.
    /// </summary>
    public string? LastModifiedBy
    {
        get; set;
    }
}
