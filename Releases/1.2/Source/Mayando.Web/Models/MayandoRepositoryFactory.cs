using System.Transactions;

namespace Mayando.Web.Models
{
    /// <summary>
    /// Creates instances of the <see cref="MayandoRepository"/>.
    /// </summary>
    public static class MayandoRepositoryFactory
    {
        #region CreateRepository

        /// <summary>
        /// Creates a new instance of the <see cref="MayandoRepository"/> class.
        /// </summary>
        /// <returns>A repository which does not support a transaction around it.</returns>
        public static MayandoRepository CreateRepository()
        {
            return CreateRepository(false, false);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MayandoRepository"/> class.
        /// </summary>
        /// <param name="writable">Determines if a transaction should be used around the entire lifetime of the <see cref="MayandoRepository"/>.</param>
        public static MayandoRepository CreateRepository(bool writable)
        {
            return CreateRepository(writable, false);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MayandoRepository"/> class.
        /// </summary>
        /// <param name="writable">Determines if a transaction should be used around the entire lifetime of the <see cref="MayandoRepository"/>.</param>
        /// <param name="isolated">If a transaction is used, determines if it is isolated from any other transaction.</param>
        public static MayandoRepository CreateRepository(bool writable, bool isolated)
        {
            if (!writable)
            {
                return new MayandoRepository();
            }
            else
            {
                return new MayandoRepository(isolated ? TransactionScopeOption.RequiresNew : TransactionScopeOption.Required);
            }
        }

        #endregion
    }
}