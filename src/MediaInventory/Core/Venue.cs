﻿using System;
using MediaInventory.Infrastructure.Application;

namespace MediaInventory.Core
{
    public class Venue : ITimestampedEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}