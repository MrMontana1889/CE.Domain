// Interfaces.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects;
using CE.Support.Support;

namespace CE.Domain.ModelingObjects.Support
{
    public interface IDomainTableManager<TElementType> : IObjectListManager<TElementType> where TElementType : class, IDomainTable
    {
        void ReQuery();
    }
}
