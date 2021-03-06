// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Framework.Caching.Memory
{
    public class EntryLink : IEntryLink
    {
        private bool _disposed;

        public EntryLink()
            : this(parent: null)
        {
        }

        public EntryLink(EntryLink parent)
        {
            Parent = parent;
        }

        public EntryLink Parent { get; }

        private readonly List<IExpirationTrigger> _triggers = new List<IExpirationTrigger>();

        public DateTimeOffset? AbsoluteExpiration { get; private set; }

        public IEnumerable<IExpirationTrigger> Triggers { get { return _triggers; } }

        public void AddExpirationTriggers(IList<IExpirationTrigger> triggers)
        {
            _triggers.AddRange(triggers);
        }

        public void SetAbsoluteExpiration(DateTimeOffset absoluteExpiration)
        {
            if (!AbsoluteExpiration.HasValue)
            {
                AbsoluteExpiration = absoluteExpiration;
            }
            else if (absoluteExpiration < AbsoluteExpiration.Value)
            {
                AbsoluteExpiration = absoluteExpiration;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                EntryLinkHelpers.DisposeLinkingScope();
            }
        }
    }
}