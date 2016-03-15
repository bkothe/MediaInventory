using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Assets;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.RegexUrlPolicy;

namespace MediaInventory.Infrastructure.Application.Web
{
    public abstract class Conventions : FubuRegistry
    {
        public const string ActionPrefix = "Execute";
        public const string HandlerSuffix = "Handler";
        public const string PublicHandlerSuffix = "Public";

        public const string GetHandlerSuffix = "Get" + HandlerSuffix;
        public const string PostHandlerSuffix = "Post" + HandlerSuffix;
        public const string PutHandlerSuffix = "Put" + HandlerSuffix;
        public const string DeleteHandlerSuffix = "Delete" + HandlerSuffix;
        public const string UpdateHandlerSuffix = "Update" + HandlerSuffix;

        public static List<string> PublicHandlerSuffixes =
            new[] { GetHandlerSuffix, PostHandlerSuffix, PutHandlerSuffix, DeleteHandlerSuffix, UpdateHandlerSuffix }
                .Select(x => PublicHandlerSuffix + x).ToList();

        protected Conventions()
        {
            Actions
                .FindBy(x => x
                    .IncludeTypeNamesSuffixed(HandlerSuffix)
                    .IncludeMethodsPrefixed(ActionPrefix)
                    .Applies.ToAssemblyContainingType(GetType()));

            Routes
                .IgnoreFolders()
                .UrlPolicy(RegexUrlPolicy.Create(
                    x => x.IgnoreAssemblyNamespace(GetType())
                        .IgnoreClassName()
                        .IgnoreMethodNames(ActionPrefix)
                        .ConstrainClassToGetEndingWith(GetHandlerSuffix)
                        .ConstrainClassToPostEndingWith(PostHandlerSuffix)
                        .ConstrainClassToPutEndingWith(PutHandlerSuffix)
                        .ConstrainClassToDeleteEndingWith(DeleteHandlerSuffix)
                        .ConstrainClassToUpdateEndingWith(UpdateHandlerSuffix)));

            this.Assets().YSOD_on_missing_assets(false);

            Policies
                .Add(x =>
                {
                    x.Conneg.ApplyConneg();
                    x.Where.ChainMatches(y => !y.HasOutput());
                });
        }
    }

    public static class ConventionExtensions
    {
        public static bool IsEndpoint(this ActionCall action)
        {
            return (!action.IsView() && !action.IsContinuation());
        }

        public static bool IsView(this ActionCall action)
        {
            return action.ParentChain().HasOutput() &&
                action.ParentChain().Output.Writers.Any(x => x is FubuMVC.Core.View.ViewNode) &&
                !action.IsContinuation();
        }

        public static bool IsSecure(this ActionCall action)
        {
            return !Conventions.PublicHandlerSuffixes
                .Any(suffix => action.HandlerType.Name.EndsWith(suffix));
        }
    }
}
