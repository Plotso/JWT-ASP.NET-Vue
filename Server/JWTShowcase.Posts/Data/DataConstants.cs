namespace JWTShowcase.Posts.Data;

public class DataConstants
{
    public class Posts
    {
        public const int MinTitleLength = 3;

        public const int MaxTitleLength = 100;
        
        public const int MinContentLength = 10;

        public const int MaxContentLength = 30_000;
    }
    
    public class Comments
    {
        public const int MinContentLength = 5;

        public const int MaxContentLength = 5_000;
    }
}