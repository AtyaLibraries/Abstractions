// <copyright file="IHasId.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>

namespace Atya.Foundation.Abstractions;

/// <summary>
/// Represents an object that exposes a strongly typed identifier.
/// </summary>
/// <typeparam name="TId">The identifier type.</typeparam>
public interface IHasId<out TId>
{
    /// <summary>
    /// Gets the identifier value.
    /// </summary>
    public TId Id
    {
        get;
    }
}
