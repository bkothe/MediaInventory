using System;
using FubuMVC.Core.Behaviors;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Infrastructure.Common.Web.Fubu
{
    public class OverrideTransactionScopeAttribute : Attribute { }

    public class TransactionScopeBehavior : IActionBehavior
    {
        private readonly IActionBehavior _innerBehavior;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionScopeBehavior(IActionBehavior innerBehavior, IUnitOfWork unitOfWork)
        {
            _innerBehavior = innerBehavior;
            _unitOfWork = unitOfWork;
        }

        public void Invoke()
        {
            _unitOfWork.Begin();
            try
            {
                _innerBehavior.Invoke();
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public void InvokePartial()
        {
            _innerBehavior.InvokePartial();
        }
    }
}