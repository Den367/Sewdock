using System;
using System.Configuration;
using System.Data.Objects;
using System.Transactions;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// A repository for managing changes to an <see cref="ObjectContext"/>.
    /// </summary>
    /// <typeparam name="TObjectContext">The type of the object context.</typeparam>
    public class Repository<TObjectContext> : IDisposable where TObjectContext : ObjectContext
    {
        #region Constants

        /// <summary>
        /// Determines if transactions are disabled.
        /// </summary>
        private readonly static bool DisableTransactions;

        /// <summary>
        /// Initializes the <see cref="Repository&lt;TObjectContext&gt;"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Boolean.TryParse(System.String,System.Boolean@)")]
        static Repository()
        {
            bool.TryParse(ConfigurationManager.AppSettings["DisableTransactions"], out DisableTransactions);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the underlying <see cref="ObjectContext"/>.
        /// </summary>
        protected TObjectContext Context { get; private set; }

        /// <summary>
        /// Gets or sets the transaction.
        /// </summary>
        private TransactionScope Transaction { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TObjectContext&gt;"/> class.
        /// </summary>
        /// <param name="context">The object context.</param>
        public Repository(TObjectContext context)
            : this(context, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TObjectContext&gt;"/> class.
        /// </summary>
        /// <param name="context">The object context.</param>
        /// <param name="scopeOption">If a transaction is used, defines the transaction scope option.</param>
        public Repository(TObjectContext context, TransactionScopeOption? scopeOption)
            : this(context, scopeOption, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TObjectContext&gt;"/> class.
        /// </summary>
        /// <param name="context">The object context.</param>
        /// <param name="scopeOption">If a transaction is used, defines the transaction scope option.</param>
        /// <param name="isolationLevel">If a transaction is used, defines the transaction isolation level.</param>
        /// <param name="transactionTimeout">If a transaction is used, defines the transaction timeout.</param>
        private Repository(TObjectContext context, TransactionScopeOption? scopeOption, IsolationLevel? isolationLevel, TimeSpan? transactionTimeout)
        {
            this.Context = context;
            if (!DisableTransactions && scopeOption.HasValue)
            {
                var options = new TransactionOptions();
                if (isolationLevel.HasValue)
                {
                    options.IsolationLevel = isolationLevel.Value;
                }
                if (transactionTimeout.HasValue)
                {
                    options.Timeout = transactionTimeout.Value;
                }
                this.Transaction = new TransactionScope(scopeOption.Value, options);
            }
        }

        #endregion

        #region CommitChanges

        /// <summary>
        /// Saves changes and commits the transaction.
        /// </summary>
        public void CommitChanges()
        {
            this.Context.SaveChanges();
            if (this.Transaction != null)
            {
                this.Transaction.Complete();
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><see paramref="langword" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Transaction != null)
                {
                    this.Transaction.Dispose();
                    this.Transaction = null;
                }
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }

        #endregion
    }
}