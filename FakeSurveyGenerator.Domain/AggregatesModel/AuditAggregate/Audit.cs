using System;
using FakeSurveyGenerator.Domain.SeedWork;

namespace FakeSurveyGenerator.Domain.AggregatesModel.AuditAggregate
{
    public class Audit : Entity, IAggregateRoot
    {
        public string WhatHappened { get; set; }
        public DateTime When { get; set; }
    }
}
