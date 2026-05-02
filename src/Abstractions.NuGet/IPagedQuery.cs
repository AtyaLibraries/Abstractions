// <copyright file="IPagedQuery.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>

namespace Atya.Foundation.Abstractions;

/// <summary>
/// Represents a minimal paging query contract.
/// </summary>
public interface IPagedQuery
{
    /// <summary>
    /// Gets the 1-based page number.
    /// </summary>
    public int PageNumber
    {
        get;
    }

    /// <summary>
    /// Gets the requested page size.
    /// </summary>
    public int PageSize
    {
        get;
    }
}
