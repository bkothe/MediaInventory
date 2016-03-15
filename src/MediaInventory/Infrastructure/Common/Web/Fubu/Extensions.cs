using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Bottles;
using FubuMVC.Core;
using FubuMVC.Core.Assets.Http;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.DSL;
using FubuMVC.Core.Registration.Nodes;

namespace MediaInventory.Infrastructure.Common.Web.Fubu
{
    public static class Extensions
    {
        public static void RemoveActivator<T>(this IPackageFacility facility) where T : IActivator
        {
            var action = (IList<Action<PackagingRuntimeGraph>>)facility.GetType().GetField("_configurableActions",
                BindingFlags.NonPublic | BindingFlags.Instance).GetValue(facility);
            action.Add(graph =>
            {
                var activators = (IList<IActivator>)graph.GetType().GetField("_activators",
                    BindingFlags.NonPublic | BindingFlags.Instance).GetValue(graph);
                var activator = activators.OfType<T>().FirstOrDefault();
                if (activator != null) activators.Remove(activator);
            });
        }

        public static PoliciesExpression WrapBehaviorChainsBefore<T, TAfter>(
            this PoliciesExpression policies,
            Expression<Func<ActionCall, bool>> expression = null,
            string configurationType = null)
            where T : IActionBehavior
            where TAfter : BehaviorNode
        {
            policies.WrapBehaviorChainsWith<T>(expression, configurationType);
            policies.Reorder(x =>
            {
                x.WhatMustBeBefore = node => node.BehaviorType == typeof(T);
                x.WhatMustBeAfter = node => node is TAfter;
            });
            return policies;
        }

        public static PoliciesExpression WrapBehaviorChainsWith<T>(
            this PoliciesExpression policies,
            Expression<Func<ActionCall, bool>> expression = null,
           string configurationType = ConfigurationType.Attachment) where T : IActionBehavior
        {
            policies.Add(policy => {
                if (expression != null) policy.Where.LastActionMatches(expression);
                policy.Wrap.WithBehavior<T>();
            }, configurationType);
            return policies;
        }

        public static PoliciesExpression AddPolicy<T>(this PoliciesExpression policies) where T : IConfigurationAction, new()
        {
            policies.Add<T>();
            return policies;
        }

        public static bool IsContent(this ActionCall action)
        {
            return action.HandlerType == typeof(AssetWriter);
        }

        public static bool IsDiagnostics(this ActionCall action)
        {
            var name = action.HandlerType.Assembly.GetName().Name;
            return name == "FubuMVC.Diagnostics" || name == "FubuMVC.SlickGrid";
        }
    }
}