namespace Submission.API.Endpoints
{
    public static class EndpointRegistration
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            // POST api/articles
            CreateArticleEndpoint.Map(app);
            
            // POST api/articles/{articleId:int}/authors/{authorId:int}
            AssignAuthorEndpoint.Map(app);
            
            // POST api/articles/{articleId:int}/authors
            CreateAndAssignAuthorEndpoint.Map(app);
            
            // POST api/articles/{articleId:int}/assets/manuscript:upload
            UploadManuscriptFileEndpoint.Map(app);
        
            return app;
        }
    }
}