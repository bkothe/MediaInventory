using FubuMVC.Bender;
using MediaInventory.Infrastructure.Application.Web;
using MediaInventory.Infrastructure.Common.Web.Fubu;

namespace MediaInventory.UI
{
    public class Conventions : Infrastructure.Application.Web.Conventions
    {
        public Conventions()
        {
            Import<FubuBender>(x => x
                .Bindings(y => y.BindRequest())
                .Configure(y => y.Serialization(z => z.WriteMicrosoftJsonDateTime())));


            Policies
                .WrapBehaviorChainsWith<TransactionScopeBehavior>(x => !x.HasAttribute<OverrideTransactionScopeAttribute>() && !x.IsContent() && !x.IsDiagnostics())
                .WrapBehaviorChainsWith<RestExceptionHandlerBehavior>(x => x.IsEndpoint());
            //.WrapBehaviorChainsWith<ExceptionHandlerBehavior>(x => x.IsView() && !Assembly.GetExecutingAssembly().IsInDebugMode())
        }
    }
}