using System.Collections.Generic;
using Myembro.Infrastructure;

namespace Myembro.ViewModels
{
    public class ConfirmBulkEditViewModel
    {
        public BulkEditOperation Operation { get; private set; }
        public string OperationDescription { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<int> PhotoIds { get; private set; }
        public string Tags { get; private set; }
        public string ReturnUrl { get; private set; }

        public ConfirmBulkEditViewModel(BulkEditOperation operation, string operationDescription, string description, IEnumerable<int> photoIds, string tags, string returnUrl)
        {
            this.Operation = operation;
            this.OperationDescription = operationDescription;
            this.Description = description;
            this.PhotoIds = photoIds;
            this.Tags = tags;
            this.ReturnUrl = returnUrl;
        }
    }
}