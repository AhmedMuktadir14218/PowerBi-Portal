namespace CRUD_Employee_standAlone.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Link { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedByUsername { get; set; } = string.Empty;
        public int CreatedByUserId { get; set; }
    }

    public class CreateCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Link { get; set; }
    }

    public class UpdateCategoryRequest
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? Link { get; set; }
    }

    public class GrantPermissionRequest
    {
        public int UserId { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }

    public class UserPermissionResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<CategoryPermissionInfo> Permissions { get; set; } = new List<CategoryPermissionInfo>();
    }

    public class CategoryPermissionInfo
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime GrantedAt { get; set; }
        public string GrantedByUsername { get; set; } = string.Empty;
    }
}