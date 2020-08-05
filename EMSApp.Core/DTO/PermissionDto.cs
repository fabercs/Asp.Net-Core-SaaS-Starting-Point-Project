using System.Collections.Generic;

namespace EMSApp.Core.DTO
{
    public class PermissionDto
    {
        public List<PageDto> Pages { get; set; }
    }

    public class PageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string FileUrl { get; set; }
        public string Component { get; set; }
        public string Icon { get; set; }
        public string Type { get; set; }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<ActionDto> Actions { get; set; }

    }

    public class ActionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
