// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

namespace Microsoft.Azure.Cosmos.Friends
{
    /// <summary>
    /// Remove this with the next release of the SDK.
    /// </summary>
    internal static class BatchConstants
    {
        // This should match (maxResourceSize - resourceSizePadding) from Server settings.xml
        public const int MaxResourceSizeInBytes = (2 * 1024 * 1024) - 1;

        // This should match maxBatchRequestBodySize from Server settings.xml
        public const int MaxDirectModeBatchRequestBodySizeInBytes = 2202010;

        // This should match maxBatchOperationsPerRequest from Server settings.xml
        public const int MaxOperationsInDirectModeBatchRequest = 100;

        public const int MaxGatewayModeBatchRequestBodySizeInBytes = 16 * 1024 * 1024;

        public const int MaxOperationsInGatewayModeBatchRequest = 1000;
    }
}