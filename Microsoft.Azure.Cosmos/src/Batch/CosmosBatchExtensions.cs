//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.Azure.Cosmos.Friends
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions to <see cref="CosmosBatch"/> for internal use.
    /// </summary>
    public static class CosmosBatchExtensions
    {
        /// <summary>
        /// Adds an operation to patch an item into the batch.
        /// </summary>
        /// <param name="batch">Cosmos Batch</param>
        /// <param name="id">The cosmos item id.</param>
        /// <param name="patchStream">A <see cref="Stream"/> containing the patch specification.</param>
        /// <param name="itemRequestOptions">(Optional) The options for the item request. <see cref="ItemRequestOptions"/></param>
        /// <returns>The <see cref="CosmosBatch"/> instance with the operation added.</returns>
        public static CosmosBatch PatchItemStream(this CosmosBatch batch, string id, Stream patchStream, ItemRequestOptions itemRequestOptions = null)
        {
            return batch.PatchItemStream(id, patchStream, itemRequestOptions);
        }

        /// <summary>
        /// Executes the batch at the Azure Cosmos service as an asynchronous operation.
        /// </summary>
        /// <param name="batch">Cosmos Batch</param>
        /// <param name="requestOptions">Options that apply to the batch.</param>
        /// <param name="cancellationToken">(Optional) <see cref="CancellationToken"/> representing request cancellation.</param>
        /// <returns>An awaitable <see cref="CosmosBatchResponse"/> which contains the completion status and results of each operation.</returns>
        public static Task<CosmosBatchResponse> ExecuteAsync(this CosmosBatch batch, RequestOptions requestOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            return batch.ExecuteAsync(requestOptions, cancellationToken);
        }
    }
}
