//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.Azure.Cosmos.Linq
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Azure.Documents;

    /// <summary> 
    /// This class serve as LINQ query provider implementing IQueryProvider.
    /// </summary> 
    internal sealed class CosmosLinqQueryProvider : IQueryProvider
    {
        private readonly Uri resourceUri;
        private readonly ResourceType resourceType;
        private readonly CosmosQueryClientCore queryClient;
        private readonly CosmosJsonSerializer cosmosJsonSerializer;
        private readonly QueryRequestOptions cosmosQueryRequestOptions;
        private readonly bool allowSynchronousQueryExecution;
        private readonly Action<IQueryable> onExecuteScalarQueryCallback;

        public CosmosLinqQueryProvider(
           Uri resourceUri,
           ResourceType resourceType,
           CosmosJsonSerializer cosmosJsonSerializer,
           CosmosQueryClientCore queryClient,
           QueryRequestOptions cosmosQueryRequestOptions,
           bool allowSynchronousQueryExecution,
           Action<IQueryable> onExecuteScalarQueryCallback = null)
        {
            this.resourceUri = resourceUri;
            this.resourceType = resourceType;
            this.cosmosJsonSerializer = cosmosJsonSerializer;
            this.queryClient = queryClient;
            this.cosmosQueryRequestOptions = cosmosQueryRequestOptions;
            this.allowSynchronousQueryExecution = allowSynchronousQueryExecution;
            this.onExecuteScalarQueryCallback = onExecuteScalarQueryCallback;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new CosmosLinqQuery<TElement>(
                this.resourceUri,
                this.resourceType,
                this.cosmosJsonSerializer,
                this.queryClient,
                this.cosmosQueryRequestOptions,
                expression,
                this.allowSynchronousQueryExecution);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            Type expressionType = TypeSystem.GetElementType(expression.Type);
            Type documentQueryType = typeof(CosmosLinqQuery<bool>).GetGenericTypeDefinition().MakeGenericType(expressionType);
            return (IQueryable)Activator.CreateInstance(
                documentQueryType,
                this.resourceUri,
                this.cosmosJsonSerializer,
                this.queryClient,
                this.cosmosQueryRequestOptions,
                expression,
                this.allowSynchronousQueryExecution);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            Type cosmosQueryType = typeof(CosmosLinqQuery<bool>).GetGenericTypeDefinition().MakeGenericType(typeof(TResult));
            CosmosLinqQuery<TResult> cosmosLINQQuery = (CosmosLinqQuery<TResult>)Activator.CreateInstance(
                cosmosQueryType,
                this.resourceUri,
                this.resourceType,
                this.cosmosJsonSerializer,
                this.queryClient,
                this.cosmosQueryRequestOptions,
                expression,
                this.allowSynchronousQueryExecution);
            this.onExecuteScalarQueryCallback?.Invoke(cosmosLINQQuery);
            return cosmosLINQQuery.ToList().FirstOrDefault();
        }

        //Sync execution of query via direct invoke on IQueryProvider.
        public object Execute(Expression expression)
        {
            Type cosmosQueryType = typeof(CosmosLinqQuery<bool>).GetGenericTypeDefinition().MakeGenericType(typeof(object));
            CosmosLinqQuery<object> cosmosLINQQuery = (CosmosLinqQuery<object>)Activator.CreateInstance(
                cosmosQueryType,
                this.resourceUri,
                this.resourceType,
                this.cosmosJsonSerializer,
                this.queryClient,
                this.cosmosQueryRequestOptions,
                this.allowSynchronousQueryExecution);
            this.onExecuteScalarQueryCallback?.Invoke(cosmosLINQQuery);
            return cosmosLINQQuery.ToList().FirstOrDefault();
        }
    }
}
